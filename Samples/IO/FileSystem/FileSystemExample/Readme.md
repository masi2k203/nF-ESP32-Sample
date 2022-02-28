# System.IO.FileSystem サンプル

## 説明
- ESP32の内部EEPROMでファイル操作を行うサンプルです。
- ESP32-WROOM-32で動作確認済みです。
- 公式サンプルは[こちら](https://github.com/nanoframework/Samples/tree/main/samples/System.IO.FileSystem)

## 内容
1. ファイルの作成
2. ファイルの読み書き
3. ファイルへの追加書き込み
4. ファイルの存在判定
5. ファイルの削除

## 注意
- ESP32の内部EEPROMは、nanoFrameworkではドライブレター:Iとなります。

## サンプルの実行結果 (Program.cs)
```powershell
===== System.IO.FileSystem Example =====

===== File create sample. =====
Create >> I:\sample1.txt
exists >> True
exists >> True
===== End of file create sample. =====


===== Write sample. =====
===== Complete. =====
===== End of write sample. =====


===== Modify sample. =====
===== Complete. =====
===== End of modify sample. =====


===== Read sample. =====
FileSystem Example.startend
<end>

===== Complete. =====
===== End of read sample. =====


===== File delete sample. =====
Delete >> I:\sample1.txt
exists >> True
exists >> False
===== End of file delete sample. =====


===== System.IO.FileSystem Example End =====
```