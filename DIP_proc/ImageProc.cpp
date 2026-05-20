#include "pch.h"
#include "ImageProc.h"

#include <algorithm>
#include <vector>

namespace
{
    int Clamp(int value)
    {
        if (value < 0)
        {
            return 0;
        }
        if (value > 255)
        {
            return 255;
        }
        return value;
    }

    int MinInt(int left, int right)
    {
        return left < right ? left : right;
    }

    int MaxInt(int left, int right)
    {
        return left > right ? left : right;
    }

    int BoundCoord(int value, int upper)
    {
        return MinInt(MaxInt(value, 0), upper - 1);
    }

    int ChannelCount(int byteDepth)
    {
        return byteDepth >= 3 ? 3 : 1;
    }

    int PixelIndex(int x, int y, int width, int byteDepth, int channel)
    {
        return (y * width + x) * byteDepth + channel;
    }

    int GrayAt(int* input, int x, int y, int width, int byteDepth)
    {
        int index = PixelIndex(x, y, width, byteDepth, 0);
        if (byteDepth >= 3)
        {
            int b = input[index];
            int g = input[index + 1];
            int r = input[index + 2];
            return Clamp(static_cast<int>(0.114 * b + 0.587 * g + 0.299 * r + 0.5));
        }
        return Clamp(input[index]);
    }

    void SetPixelChannels(int* output, int x, int y, int width, int byteDepth, int value)
    {
        int index = PixelIndex(x, y, width, byteDepth, 0);
        int channels = ChannelCount(byteDepth);
        for (int c = 0; c < channels; ++c)
        {
            output[index + c] = value;
        }
        if (byteDepth == 4)
        {
            output[index + 3] = 255;
        }
    }

    void CopyAlpha(int* input, int* output, int length, int byteDepth)
    {
        if (byteDepth != 4)
        {
            return;
        }

        for (int i = 3; i < length; i += 4)
        {
            output[i] = input[i];
        }
    }

    void BuildGrayMap(int* input, int width, int height, int byteDepth, int* histogram, int* grayValues)
    {
        std::fill(histogram, histogram + 256, 0);
        int total = width * height;
        for (int i = 0; i < total; ++i)
        {
            int x = i % width;
            int y = i / width;
            int gray = GrayAt(input, x, y, width, byteDepth);
            grayValues[i] = gray;
            histogram[gray]++;
        }
    }
}

extern "C"
{
    __declspec(dllexport) void Negative(int* input, int* output, int length)
    {
        for (int i = 0; i < length; ++i)
        {
            output[i] = 255 - Clamp(input[i]);
        }
    }

    __declspec(dllexport) void GrayScale(int* input, int* output, int width, int height, int byteDepth)
    {
        int length = width * height * byteDepth;
        std::copy(input, input + length, output);

        for (int y = 0; y < height; ++y)
        {
            for (int x = 0; x < width; ++x)
            {
                SetPixelChannels(output, x, y, width, byteDepth, GrayAt(input, x, y, width, byteDepth));
            }
        }
        CopyAlpha(input, output, length, byteDepth);
    }

    __declspec(dllexport) void Brightness(int* input, int* output, int length, int value)
    {
        for (int i = 0; i < length; ++i)
        {
            output[i] = Clamp(input[i] + value);
        }
    }

    __declspec(dllexport) void Contrast(int* input, int* output, int length, double factor)
    {
        for (int i = 0; i < length; ++i)
        {
            output[i] = Clamp(static_cast<int>((input[i] - 128) * factor + 128 + 0.5));
        }
    }

    __declspec(dllexport) void GrayLevelSlice(int* input, int* output, int width, int height, int byteDepth, int low, int high, int highlight, int keepBackground)
    {
        int length = width * height * byteDepth;
        std::copy(input, input + length, output);

        low = Clamp(low);
        high = Clamp(high);
        if (low > high)
        {
            std::swap(low, high);
        }

        for (int y = 0; y < height; ++y)
        {
            for (int x = 0; x < width; ++x)
            {
                int gray = GrayAt(input, x, y, width, byteDepth);
                int value = (gray >= low && gray <= high) ? Clamp(highlight) : (keepBackground ? gray : 0);
                SetPixelChannels(output, x, y, width, byteDepth, value);
            }
        }
        CopyAlpha(input, output, length, byteDepth);
    }

    __declspec(dllexport) void BitPlaneSlice(int* input, int* output, int width, int height, int byteDepth, int bitPlane)
    {
        int length = width * height * byteDepth;
        std::copy(input, input + length, output);

        if (bitPlane < 1)
        {
            bitPlane = 1;
        }
        if (bitPlane > 8)
        {
            bitPlane = 8;
        }

        int mask = 1 << (bitPlane - 1);
        for (int y = 0; y < height; ++y)
        {
            for (int x = 0; x < width; ++x)
            {
                int gray = GrayAt(input, x, y, width, byteDepth);
                int value = (gray & mask) != 0 ? 255 : 0;
                SetPixelChannels(output, x, y, width, byteDepth, value);
            }
        }
        CopyAlpha(input, output, length, byteDepth);
    }

    __declspec(dllexport) void HistogramStretch(int* input, int* output, int width, int height, int byteDepth)
    {
        int length = width * height * byteDepth;
        int channels = ChannelCount(byteDepth);
        std::copy(input, input + length, output);

        for (int c = 0; c < channels; ++c)
        {
            int minValue = 255;
            int maxValue = 0;
            for (int y = 0; y < height; ++y)
            {
                for (int x = 0; x < width; ++x)
                {
                    int value = Clamp(input[PixelIndex(x, y, width, byteDepth, c)]);
                    minValue = MinInt(minValue, value);
                    maxValue = MaxInt(maxValue, value);
                }
            }

            if (minValue == maxValue)
            {
                continue;
            }

            for (int y = 0; y < height; ++y)
            {
                for (int x = 0; x < width; ++x)
                {
                    int index = PixelIndex(x, y, width, byteDepth, c);
                    output[index] = Clamp((input[index] - minValue) * 255 / (maxValue - minValue));
                }
            }
        }
        CopyAlpha(input, output, length, byteDepth);
    }

    __declspec(dllexport) void HistogramEqualization(int* input, int* output, int width, int height, int byteDepth)
    {
        int length = width * height * byteDepth;
        int total = width * height;
        std::vector<int> grayValues(total);
        int histogram[256];
        int transform[256];

        BuildGrayMap(input, width, height, byteDepth, histogram, grayValues.data());

        int cdf = 0;
        int cdfMin = 0;
        for (int i = 0; i < 256; ++i)
        {
            cdf += histogram[i];
            if (cdfMin == 0 && cdf > 0)
            {
                cdfMin = cdf;
            }
            transform[i] = cdf;
        }

        int denominator = total - cdfMin;
        for (int i = 0; i < 256; ++i)
        {
            transform[i] = denominator > 0 ? Clamp((transform[i] - cdfMin) * 255 / denominator) : i;
        }

        std::copy(input, input + length, output);
        for (int y = 0; y < height; ++y)
        {
            for (int x = 0; x < width; ++x)
            {
                int gray = grayValues[y * width + x];
                SetPixelChannels(output, x, y, width, byteDepth, transform[gray]);
            }
        }
        CopyAlpha(input, output, length, byteDepth);
    }

    __declspec(dllexport) void SpatialFilter(int* input, int* output, int width, int height, int byteDepth, int filterType, int kernelSize)
    {
        int length = width * height * byteDepth;
        int channels = ChannelCount(byteDepth);
        std::copy(input, input + length, output);

        if (kernelSize < 3)
        {
            kernelSize = 3;
        }
        if (kernelSize % 2 == 0)
        {
            kernelSize++;
        }

        int radius = kernelSize / 2;
        std::vector<int> window;
        window.reserve(kernelSize * kernelSize);

        for (int y = 0; y < height; ++y)
        {
            for (int x = 0; x < width; ++x)
            {
                for (int c = 0; c < channels; ++c)
                {
                    int value = 0;
                    if (filterType == 1)
                    {
                        window.clear();
                        for (int ky = -radius; ky <= radius; ++ky)
                        {
                            int sy = BoundCoord(y + ky, height);
                            for (int kx = -radius; kx <= radius; ++kx)
                            {
                                int sx = BoundCoord(x + kx, width);
                                window.push_back(input[PixelIndex(sx, sy, width, byteDepth, c)]);
                            }
                        }
                        std::nth_element(window.begin(), window.begin() + window.size() / 2, window.end());
                        value = window[window.size() / 2];
                    }
                    else if (filterType == 2)
                    {
                        static const int kernel[3][3] = {
                            {0, -1, 0},
                            {-1, 5, -1},
                            {0, -1, 0}
                        };
                        for (int ky = -1; ky <= 1; ++ky)
                        {
                            int sy = BoundCoord(y + ky, height);
                            for (int kx = -1; kx <= 1; ++kx)
                            {
                                int sx = BoundCoord(x + kx, width);
                                value += input[PixelIndex(sx, sy, width, byteDepth, c)] * kernel[ky + 1][kx + 1];
                            }
                        }
                    }
                    else if (filterType == 3)
                    {
                        static const int kernel[3][3] = {
                            {-1, -1, -1},
                            {-1, 8, -1},
                            {-1, -1, -1}
                        };
                        for (int ky = -1; ky <= 1; ++ky)
                        {
                            int sy = BoundCoord(y + ky, height);
                            for (int kx = -1; kx <= 1; ++kx)
                            {
                                int sx = BoundCoord(x + kx, width);
                                value += input[PixelIndex(sx, sy, width, byteDepth, c)] * kernel[ky + 1][kx + 1];
                            }
                        }
                        value = std::abs(value);
                    }
                    else
                    {
                        int sum = 0;
                        for (int ky = -radius; ky <= radius; ++ky)
                        {
                            int sy = BoundCoord(y + ky, height);
                            for (int kx = -radius; kx <= radius; ++kx)
                            {
                                int sx = BoundCoord(x + kx, width);
                                sum += input[PixelIndex(sx, sy, width, byteDepth, c)];
                            }
                        }
                        value = sum / (kernelSize * kernelSize);
                    }

                    output[PixelIndex(x, y, width, byteDepth, c)] = Clamp(value);
                }
            }
        }
        CopyAlpha(input, output, length, byteDepth);
    }
}
