using System;
using System.Threading;
using System.Device.I2c;

namespace Mas2k_NF_Sample
{
    class AQM1602x
    {

        #region フィールド

        /// <summary>
        /// I2Cデバイス
        /// </summary>
        private I2cDevice _i2cDevice;

        private bool _is5V;

        #endregion

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="device">I2cDevice</param>
        /// <param name="is5V">5V電源を使用するのであればtrue</param>
        public AQM1602x(I2cDevice device, bool is5V)
        {
            _i2cDevice = device;
            _is5V = is5V;

            Initialize();
        }

        #region メソッド

        /// <summary>
        /// LCDを初期化する関数
        /// </summary>
        private void Initialize()
        {
            // 初期化用コマンド
            byte[] initializeCommand1 = new byte[]
            {
                0x38, 0x39, 0x14, 0x73, 0x5E, 0x6C
            };
            byte[] initializeCommand2 = new byte[]
            {
                0x38, 0x01, 0x0C
            };

            // 電源電圧によってコマンドを変更
            if (_is5V)
            {
                initializeCommand1[4] = 0x56;
            }

            // コマンド送信
            foreach (var item in initializeCommand1)
            {
                WriteCommand(item);
            }

            // 待機
            Thread.Sleep(200);

            // コマンド送信
            foreach (var item in initializeCommand2)
            {
                WriteCommand(item);
            }
        }

        /// <summary>
        /// LCDにコマンドを送信するメソッド
        /// </summary>
        /// <param name="command">コマンド</param>
        /// <returns>送信結果</returns>
        private I2cTransferResult WriteCommand(byte command)
        {
            // 送信I2Cデータ配列作成
            SpanByte i2cData = new byte[2];
            i2cData[0] = 0x00;
            i2cData[1] = command;

            // 送信して送信結果を取得
            var result = _i2cDevice.Write(i2cData);

            // 待機
            Thread.Sleep(1);

            return result;
        }

        /// <summary>
        /// LCDに1バイト文字を送信するメソッド
        /// </summary>
        /// <param name="data">送信する文字</param>
        /// <returns></returns>
        private I2cTransferResult WriteByteData(char data)
        {
            // 送信I2Cデータ配列作成
            SpanByte i2cData = new byte[2];
            i2cData[0] = 0x40;
            i2cData[1] = (byte)data;

            // 送信して送信結果を取得
            var result = _i2cDevice.Write(i2cData);

            // 待機
            Thread.Sleep(1);

            return result;
        }

        /// <summary>
        /// LCDに文字列を表示するメソッド
        /// </summary>
        /// <param name="text"></param>
        public void Write(string text)
        {
            char[] outLine1 = null;
            char[] outLine2 = null;

            if (text.Length > 16)
            {
                outLine1 = text.ToCharArray(0, 16);
                outLine2 = text.ToCharArray(16, text.Length - 16);

                // 1行目
                for (int i = 0; i < outLine1.Length; i++)
                {
                    WriteByteData(outLine1[i]);
                }
                // 改行
                WriteCommand(0x40 + 0x80);
                // 2行目
                for (int i = 0; i < outLine2.Length; i++)
                {
                    // 2行目は16文字目まで処理する。その後は捨てる
                    if (i < 16)
                    {
                        WriteByteData(outLine2[i]);
                    }
                }
            }
            else
            {
                outLine1 = text.ToCharArray();
                for (int i = 0; i < outLine1.Length; i++)
                {
                    WriteByteData(outLine1[i]);
                }
            }

            // 全て表示し終えたらホームに戻る
            WriteCommand(0x02);
        }

        /// <summary>
        /// LCDの表示を消去するメソッド
        /// </summary>
        public void Clear()
        {
            WriteCommand(0x01);
        }

        #endregion
    }
}