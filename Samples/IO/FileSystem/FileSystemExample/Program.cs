using System;
using System.Diagnostics;
using System.Threading;
using System.IO;
using System.Text;

namespace FileSystemExample
{
    public class Program
    {

        /// <summary>
        /// ファイル名(パス付きで指定)
        /// ESP32 内部EEPROMのドライブレター => I:\\...
        /// </summary>
        private static string _filename = "I:\\sample1.txt";

        public static void Main()
        {
            Debug.WriteLine("===== System.IO.FileSystem Example =====\n");

            CreateFileExample();

            WriteFileExample();

            ModifyFileExample();

            ReadFileExample();

            DeleteFileExample();

            Debug.WriteLine("===== System.IO.FileSystem Example End =====");
        }

        /// <summary>
        /// ファイルを作成するサンプル
        /// </summary>
        public static void CreateFileExample()
        {
            Debug.WriteLine("===== File create sample. =====");

            Debug.WriteLine("Create >> " + _filename);

            /// File.Exists(string path)
            ///     - ファイルの存在を確認する
            ///     - ファイルが存在すればtrue
            ///     - ファイルが存在しなければfalse
            bool exists = File.Exists(_filename);
            Debug.WriteLine("exists >> " + exists);

            /// File.Create(string path)
            ///     - ファイルを作成する
            File.Create(_filename);

            exists = File.Exists(_filename);
            Debug.WriteLine("exists >> " + exists);

            Debug.WriteLine("===== End of file create sample. =====\n\n");
        }

        /// <summary>
        /// ファイルに書き込むサンプル
        /// </summary>
        public static void WriteFileExample()
        {
            Debug.WriteLine("===== Write sample. =====");

            // ファイルが存在すれば
            if (File.Exists(_filename))
            {
                // 書き込む文字列(byte配列)を用意する
                byte[] write = Encoding.UTF8.GetBytes("FileSystem Example.");

                try
                {
                    /// FileStream(string path, FileMode mode, FileAccess)
                    ///     - ファイル読み書きストリームを作成する
                    ///     - 引数1 : ファイルパス
                    ///     - 引数2 : ファイルのモード (Opem = 開く など)
                    ///     - 引数3 : ファイルアクセス (ReadWrite = 読み書き など)
                    ///     - 使い終わったら閉じるべき (Dispose)
                    using (FileStream stream = new FileStream(_filename, FileMode.Open, FileAccess.ReadWrite))
                    {
                        /// Write(byte[] buffer, int offset, int count)
                        ///     - bufferの内容をoffsetだけオフセットした状態で、countだけ書き込み
                        stream.Write(write, 0, write.Length);
                    }

                    Debug.WriteLine("===== Complete. =====");
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"===== Faild. =====");
                    Debug.WriteLine(ex.Message);
                }
            }

            Debug.WriteLine("===== End of write sample. =====\n\n");
        }

        /// <summary>
        /// ファイルに追加で書き込むサンプル
        /// </summary>
        public static void ModifyFileExample()
        {
            Debug.WriteLine("===== Modify sample. =====");

            // ファイルが存在すれば
            if (File.Exists(_filename))
            {
                // 追加で書き込む文字列(byte配列)を用意する
                byte[] write1 = Encoding.UTF8.GetBytes("start");
                byte[] write2 = Encoding.UTF8.GetBytes("end");

                try
                {
                    using (FileStream stream = new FileStream(_filename, FileMode.Open, FileAccess.ReadWrite))
                    {
                        // ストリームがシーク可能な場合
                        if (stream.CanSeek)
                        {
                            // ファイルの先頭にカーソルをセット
                            stream.Seek(0, SeekOrigin.Begin);
                            stream.Write(write1, 0, write1.Length);

                            // ファイルの末尾にカーソルをセット
                            stream.Seek(0, SeekOrigin.End);
                            stream.Write(write2, 0, write2.Length);
                        }
                    }

                    Debug.WriteLine("===== Complete. =====");
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"===== Faild. =====");
                    Debug.WriteLine(ex.Message);
                }
            }

            Debug.WriteLine("===== End of modify sample. =====\n\n");
        }

        /// <summary>
        /// ファイルの中身を読み取るサンプル
        /// </summary>
        public static void ReadFileExample()
        {
            Debug.WriteLine("===== Read sample. =====");

            // ファイルが存在すれば
            if (File.Exists(_filename))
            {
                try
                {
                    using (FileStream stream = new FileStream(_filename, FileMode.Open, FileAccess.Read))
                    {
                        // ファイル長分のbyte配列を作成
                        byte[] read = new byte[stream.Length];
                        // ファイルを読み取る
                        stream.Read(read, 0, read.Length);

                        // 出力してみる
                        foreach (var item in read)
                        {
                            Console.Write("" + (char)item);
                        }
                        Debug.WriteLine("\n<end>\n");
                    }

                    Debug.WriteLine("===== Complete. =====");
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"===== Faild. =====");
                    Debug.WriteLine(ex.Message);
                }
            }

            Debug.WriteLine("===== End of read sample. =====\n\n");
        }

        /// <summary>
        /// ファイルを削除するサンプル
        /// </summary>
        public static void DeleteFileExample()
        {
            Debug.WriteLine("===== File delete sample. =====");

            Debug.WriteLine("Delete >> " + _filename);


            bool exists = File.Exists(_filename);
            Debug.WriteLine("exists >> " + exists);

            /// File.Delete(string path)
            ///     - ファイルを削除する
            File.Delete(_filename);

            exists = File.Exists(_filename);
            Debug.WriteLine("exists >> " + exists);

            Debug.WriteLine("===== End of file delete sample. =====\n\n");
        }
    }
}
