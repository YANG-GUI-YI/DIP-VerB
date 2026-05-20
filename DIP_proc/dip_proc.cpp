// dip_proc.cpp : 定義 DLL 應用程式的匯出函式。
//
#include "pch.h"
#include "image_lib.h"


extern "C" {
	//===========================================================================
	//
	//===========================================================================
	 __declspec(dllexport) void encode(int *f,int w,int h,int *g)
	{
		int i0,j0;
		int *b,wb,hb;

		wb=w/4;
		hb=h/4;
		b=new int[wb*hb];

		i0=w/4;
		j0=h/4;

		block_get(f,w,h,b,wb,hb,i0,j0);
		contrast(b,wb,hb,1.5);
		copy(f,w,h,g);
		block_put(b,wb,hb,g,w,h,i0,j0);
		//===========================================================================
	}

	 __declspec(dllexport) void encode_gray(int* f, int w, int h, int* g, int d)
	 {
		 for (int j = 0; j < h; j++)
		 {
			 for (int i = 0; i < w; i++)
			 {
				 if (d == 1)
				 {
					 g[(j * w + i)] = f[(j * w + i)];
				 }
				 else
				 {
					 int avg = (double)f[(j * w + i) * 3] * 0.144 + (double)f[(j * w + i) * 3 + 1] * 0.587 + (double)f[(j * w + i) * 3 + 2] * 0.299;
					 for (int k = 0; k < 3; k++)
					 {
						 g[(j * w + i) * 3 + k] = avg;
					 }
				 }
			 }
		 }

		 //===========================================================================
	 }


	 


}