#pragma once

extern "C"
{
    __declspec(dllexport) void Negative(int* input, int* output, int length);
    __declspec(dllexport) void GrayScale(int* input, int* output, int width, int height, int byteDepth);
    __declspec(dllexport) void Brightness(int* input, int* output, int length, int value);
    __declspec(dllexport) void Contrast(int* input, int* output, int length, double factor);
    __declspec(dllexport) void GrayLevelSlice(int* input, int* output, int width, int height, int byteDepth, int low, int high, int highlight, int keepBackground);
    __declspec(dllexport) void BitPlaneSlice(int* input, int* output, int width, int height, int byteDepth, int bitPlane);
    __declspec(dllexport) void HistogramStretch(int* input, int* output, int width, int height, int byteDepth);
    __declspec(dllexport) void HistogramEqualization(int* input, int* output, int width, int height, int byteDepth);
    __declspec(dllexport) void SpatialFilter(int* input, int* output, int width, int height, int byteDepth, int filterType, int kernelSize);
    __declspec(dllexport) void OtsuThreshold(int* input, int* output, int width, int height, int byteDepth);
    __declspec(dllexport) void LineDetection(int* input, int* output, int width, int height, int byteDepth, int lineType);
    __declspec(dllexport) void CannyEdgeDetection(int* input, int* output, int width, int height, int byteDepth);
}
