#include "ImageProc.h"
#include "pch.h"
extern "C" __declspec(dllexport) void ProcessInvert(int* input, int* output, int length) {

    for (int i = 0; i < length; i++) {

        output[i] = 255 - input[i];
    }
}