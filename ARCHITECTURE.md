# DIP-MDI 專案架構

## 專案概覽

`DIP-MDI` 是一個以 Windows Forms MDI 介面呈現的數位影像處理程式。主程式負責開圖、顯示、選單與互動視窗；影像處理核心主要放在 C++ DLL `dip_proc.dll` 內，並由 C# 透過 P/Invoke 呼叫。

## 方案結構

```text
DIP-MDI/
├── DIP VerB.sln              # Visual Studio solution
├── DIP/                      # C# WinForms 主程式
│   ├── DIP.csproj
│   ├── Program.cs
│   ├── DIPSample.cs          # 主 MDI Form、選單事件、P/Invoke、互動視窗
│   ├── DIPSample.Designer.cs # 主表單 UI/選單定義
│   ├── MSForm.cs             # 單張影像子視窗
│   ├── MSForm.Designer.cs
│   └── DIP_proc.dll          # C# 專案執行時載入的 DLL 複本
├── dip_proc/                 # C++ 影像處理 DLL
│   ├── dip_proc.vcxproj
│   ├── ImageProc.h           # DLL 匯出函式宣告
│   ├── ImageProc.cpp         # 影像處理演算法實作
│   ├── dllmain.cpp
│   ├── pch.h
│   └── pch.cpp
├── x64/Debug/dip_proc.dll    # x64 Debug 建置輸出
├── Debug/                    # Win32 Debug 建置輸出
└── Standard Images/          # 測試影像資料
```

## 執行架構

```text
使用者
  ↓
DIPSample 主視窗選單
  ↓
讀取目前作用中的 MSForm.pBitmap
  ↓
Bitmap 轉 int[] 影像陣列
  ↓
P/Invoke 呼叫 dip_proc.dll
  ↓
C++ 演算法處理
  ↓
int[] 轉回 Bitmap
  ↓
建立新的 MSForm 顯示結果影像
```

幾何處理如放大縮小與旋轉會改變影像尺寸，目前在 C# 端用 `System.Drawing` 產生新 Bitmap，不透過固定寬高的 DLL 陣列介面。

## C# WinForms 主程式

### 主要檔案

| 檔案 | 責任 |
| --- | --- |
| `DIP/DIPSample.cs` | 主邏輯、選單事件、DLL P/Invoke、Bitmap/陣列轉換、直方圖視窗、滑桿預覽視窗、文字輸入視窗 |
| `DIP/DIPSample.Designer.cs` | 主表單、選單、StatusStrip |
| `DIP/MSForm.cs` | 顯示單張圖片，滑鼠移動時顯示座標與 RGB 值 |
| `DIP/Program.cs` | WinForms 進入點 |

### 選單分組

| 選單 | 功能 |
| --- | --- |
| `File` | 開啟 BMP 檔 |
| `基本處理` | 灰階、負片、位元切片、Otsu 分割 |
| `調整與直方圖` | 亮度調整、對比、直方圖顯示、直方圖等化 |
| `幾何` | 放大縮小、圖片旋轉 |
| `濾波與偵測` | 平均濾波、中值濾波、銳化、邊緣偵測、拉普拉斯濾波、Canny Edge Detection |

### 影像資料轉換

`DIPSample.cs` 內有兩個核心轉換流程：

| 方法 | 功能 |
| --- | --- |
| `dyn_bmp2array(...)` | 將 `Bitmap` 透過 `LockBits` 轉為 `int[]` |
| `dyn_array2bmp(...)` | 將 DLL 處理後的 `int[]` 轉回 `Bitmap` |

陣列排列方式為：

```text
index = (x + width * y) * byteDepth + channel
```

其中 `byteDepth` 會依 PixelFormat 取得，常見值為：

| byteDepth | 影像格式 |
| --- | --- |
| 1 | 8-bit indexed / gray-like |
| 3 | 24-bit BGR |
| 4 | 32-bit BGRA |

## C++ DLL：dip_proc

### 主要檔案

| 檔案 | 責任 |
| --- | --- |
| `dip_proc/ImageProc.h` | 匯出函式宣告 |
| `dip_proc/ImageProc.cpp` | 影像處理演算法實作 |
| `dip_proc/dllmain.cpp` | DLL 進入點 |

### 匯出函式

| 函式 | 功能 |
| --- | --- |
| `Negative` | 負片 |
| `GrayScale` | 灰階轉換 |
| `Brightness` | 亮度調整 |
| `Contrast` | 對比調整 |
| `GrayLevelSlice` | 灰階範圍切片，保留供相容 |
| `BitPlaneSlice` | 位元切片 |
| `HistogramStretch` | 直方圖伸展，保留供擴充 |
| `HistogramEqualization` | 直方圖等化 |
| `SpatialFilter` | 平均、中值、銳化、邊緣、拉普拉斯等空間濾波 |
| `OtsuThreshold` | Otsu 自動閾值分割 |
| `LineDetection` | 舊版方向線偵測，保留供相容 |
| `CannyEdgeDetection` | Canny Edge Detection |

### SpatialFilter 類型

| filterType | 功能 |
| --- | --- |
| `0` | 平均濾波 |
| `1` | 中值濾波 |
| `2` | 銳化 |
| `3` | 邊緣偵測 |
| `4` | 拉普拉斯濾波 |

## 互動視窗

| 視窗類別 | 用途 |
| --- | --- |
| `TrackPreviewForm` | 滑桿即時預覽，例如位元切片、亮度、對比 |
| `ValueInputForm` | TextBox + 確認/取消，例如放大縮小比例、旋轉角度 |
| `HistogramForm` | 顯示直方圖 |

## DLL 載入與同步

C# 端使用：

```csharp
[DllImport("dip_proc.dll", CallingConvention = CallingConvention.Cdecl)]
```

因此執行時需要在 EXE 同層目錄找到 `dip_proc.dll`。目前常用同步位置包含：

```text
DIP/DIP_proc.dll
DIP/bin/x64/Debug/DIP_proc.dll
DIP/bin/Debug/DIP_proc.dll
dip_proc.dll
```

建置 C++ DLL 後，若 C# 執行目錄仍載入舊 DLL，需要手動複製新版 DLL 到上述執行目錄。

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

## 擴充新功能的建議流程

1. 若功能不改變影像尺寸，優先在 `ImageProc.h/.cpp` 新增 DLL 匯出函式。
2. 在 `DIPSample.cs` 加上 `DllImport` 宣告。
3. 新增或修改選單事件，呼叫 `ApplyImageOperation(...)`。
4. 若功能需要互動參數：
   - 滑桿即時預覽用 `TrackPreviewForm`
   - 單次輸入值用 `ValueInputForm`
5. 重新建置 solution。
6. 將新版 `dip_proc.dll` 同步到 C# 執行目錄。

## 注意事項

- C# 與 C++ 的函式簽章必須完全一致，尤其是參數順序與 `CallingConvention.Cdecl`。
- 目前 DLL 多數函式假設輸入與輸出影像尺寸相同。
- 會改變尺寸的功能，例如縮放與旋轉，目前在 C# 端處理。
- 影像資料主要以 BGR/BGRA 的 byte channel 順序處理。
- 若支援更多格式，應先檢查 `PixelFormat` 與 `byteDepth` 的對應。
