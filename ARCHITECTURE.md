# DIP-MDI 專案架構

## 專案概述

`DIP-MDI` 是一個 Windows Forms MDI 影像處理程式。使用者透過 C# WinForms 介面開啟圖片、選擇功能，影像資料會轉成陣列後交給 C++ DLL `dip_proc.dll` 處理，再將結果轉回 `Bitmap` 顯示在新的子視窗中。

整體分工如下：

- `DIP/`：C# WinForms 前端、選單、視窗、Bitmap 與陣列轉換、P/Invoke 呼叫。
- `dip_proc/`：C++ 影像處理 DLL，負責像素運算、濾波、分割與偵測。
- `x64/Debug/dip_proc.dll`：C++ DLL 編譯輸出。

## 目錄結構

```text
DIP-MDI/
├── DIP VerB.sln
├── ARCHITECTURE.md
├── dip_proc.dll
├── DIP/
│   ├── DIP.csproj
│   ├── Program.cs
│   ├── DIPSample.cs
│   ├── DIPSample.Designer.cs
│   ├── MSForm.cs
│   ├── MSForm.Designer.cs
│   ├── DIP_proc.dll
│   └── bin/
├── dip_proc/
│   ├── dip_proc.vcxproj
│   ├── ImageProc.h
│   ├── ImageProc.cpp
│   ├── dllmain.cpp
│   ├── pch.h
│   └── pch.cpp
├── x64/
│   └── Debug/
│       └── dip_proc.dll
└── Standard Images/
```

## 執行流程

```text
使用者點擊選單
  ↓
DIPSample.cs 取得目前作用中的 MSForm 圖片
  ↓
Bitmap 轉成 int[] 像素陣列
  ↓
透過 P/Invoke 呼叫 dip_proc.dll
  ↓
C++ DLL 執行影像處理
  ↓
int[] 結果轉回 Bitmap
  ↓
以新的 MSForm 顯示處理後圖片
```

## C# 前端

| 檔案 | 說明 |
| --- | --- |
| `DIP/DIPSample.cs` | 主 MDI Form、選單事件、P/Invoke 宣告、影像處理流程、預覽與輸入視窗。 |
| `DIP/DIPSample.Designer.cs` | 主視窗 UI 與 MenuItem 設定。 |
| `DIP/MSForm.cs` | 顯示單張圖片的 MDI 子視窗。 |
| `DIP/Program.cs` | WinForms 進入點。 |

### 目前選單

| 主選單 | 功能 |
| --- | --- |
| `File` | 開啟 BMP 圖片。 |
| `基本處理` | 灰階、負片、位元切片、Otsu 分割。 |
| `調整與直方圖` | 亮度調整、對比、直方圖顯示、直方圖等化。 |
| `幾何` | 放大縮小、圖片旋轉。 |
| `濾波與偵測` | 平均濾波、中值濾波、高斯濾波、銳化、邊緣偵測、拉普拉斯銳化、Prewitt、Sobel、自定義 Kernel、Canny Edge Detection、Hough Line Detection、Hough Circle Detection。 |

## C++ DLL

| 檔案 | 說明 |
| --- | --- |
| `dip_proc/ImageProc.h` | DLL 匯出函式宣告。 |
| `dip_proc/ImageProc.cpp` | 影像處理演算法實作。 |
| `dip_proc/dllmain.cpp` | DLL 進入點。 |

### 匯出函式

| 函式 | 功能 |
| --- | --- |
| `Negative` | 負片轉換。 |
| `GrayScale` | 灰階轉換。 |
| `Brightness` | 亮度調整。 |
| `Contrast` | 對比調整。 |
| `GrayLevelSlice` | 灰階切片。 |
| `BitPlaneSlice` | 位元切片。 |
| `HistogramStretch` | 直方圖轉換。 |
| `HistogramEqualization` | 直方圖等化。 |
| `SpatialFilter` | 平均、中值、高斯、銳化、邊緣、Prewitt、Sobel 等濾波。 |
| `CustomKernelFilter` | 使用自定義 3x3 kernel、divisor、offset 做濾波。 |
| `OtsuThreshold` | Otsu 自動閥值分割。 |
| `LineDetection` | 舊版線偵測函式。 |
| `CannyEdgeDetection` | Canny 邊緣偵測。 |
| `HoughTransformLineDetection` | Hough Transform 直線偵測，結果以紅線標示。 |
| `HoughTransformCircleDetection` | Hough Transform 圓偵測，結果以紅圈標示。 |

### SpatialFilter 類型

| `filterType` | 功能 |
| --- | --- |
| `0` | 平均濾波。 |
| `1` | 中值濾波。 |
| `2` | 銳化。 |
| `3` | 拉普拉斯邊緣偵測。 |
| `4` | 拉普拉斯銳化。 |
| `5` | Prewitt 濾波。 |
| `6` | Sobel 濾波。 |
| `7` | 高斯濾波，使用 3x3 kernel `[1 2 1; 2 4 2; 1 2 1] / 16`。 |

## 輔助視窗

| 類別 | 說明 |
| --- | --- |
| `TrackPreviewForm` | 使用 TrackBar 即時預覽，例如位元切片、亮度與對比。 |
| `ValueInputForm` | TextBox 加確認按鈕，用於放大縮小與旋轉。 |
| `HeaderImageForm` | 在圖片上方顯示資訊，例如 Otsu 閥值。 |
| `HistogramForm` | 顯示圖片直方圖，等化後可顯示等化圖片與新直方圖。 |
| `CustomKernelForm` | 輸入自定義 3x3 kernel、divisor、offset。 |

## 像素資料格式

C# 端會將 `Bitmap` 轉為 `int[]`，C++ DLL 直接處理此陣列。索引計算方式：

```text
index = (x + width * y) * byteDepth + channel
```

| `byteDepth` | 格式 |
| --- | --- |
| `1` | 8-bit indexed / gray-like |
| `3` | 24-bit BGR |
| `4` | 32-bit BGRA |

## DLL 放置位置

C# 使用下列 P/Invoke 設定載入 DLL：

```csharp
[DllImport("dip_proc.dll", CallingConvention = CallingConvention.Cdecl)]
```

為了確保設計階段、執行階段與 Debug 輸出都能找到 DLL，目前會同步到：

```text
dip_proc.dll
DIP/DIP_proc.dll
DIP/bin/x64/Debug/DIP_proc.dll
DIP/bin/Debug/DIP_proc.dll
```

## 建置方式

建議使用 x64 Debug：

```powershell
& 'C:\Program Files\Microsoft Visual Studio\2022\Community\MSBuild\Current\Bin\amd64\MSBuild.exe' '.\DIP VerB.sln' /p:Configuration=Debug /p:Platform=x64 /m
```

主要輸出：

```text
x64/Debug/dip_proc.dll
DIP/bin/x64/Debug/DIP.exe
```

## 新增功能流程

1. 若需要 DLL 運算，在 `ImageProc.h` 宣告函式，並在 `ImageProc.cpp` 實作。
2. 在 `DIPSample.cs` 新增 `DllImport` 宣告。
3. 在 `DIPSample.cs` 新增選單事件，通常透過 `ApplyImageOperation(...)` 呼叫處理流程。
4. 在 `DIPSample.Designer.cs` 新增或調整 MenuItem。
5. 重新建置 solution。
6. 將新版 `dip_proc.dll` 同步到 C# 執行會載入的位置。

## 注意事項

- C# 與 C++ 的呼叫慣例需保持 `CallingConvention.Cdecl`。
- C# 端與 DLL 端的函式參數順序、型別必須完全一致。
- 彩色圖片目前以 BGR/BGRA channel 順序處理。
- 若執行中的 `DIP.exe` 鎖住輸出檔，需先關閉程式再重新建置。
