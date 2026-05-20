#include "ImageProc.h"

void Negative(unsigned char* img,
    int width,
    int height)
{
    int size = width * height * 3;

    for (int i = 0; i < size; i++)
    {
        img[i] = 255 - img[i];
    }
}