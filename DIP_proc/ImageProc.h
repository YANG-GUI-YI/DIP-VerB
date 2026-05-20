#pragma once

extern "C"
{
    __declspec(dllexport)
        void Negative(
            unsigned char* img,
            int width,
            int height);

    __declspec(dllexport)
        void GrayScale(
            unsigned char* img,
            int width,
            int height);

    __declspec(dllexport)
        void Brightness(
            unsigned char* img,
            int width,
            int height,
            int value);
}