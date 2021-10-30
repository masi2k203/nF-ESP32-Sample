using System;
using System.Diagnostics;

// nugetで追加 //
// System.Mathを追加してください。
using System.Net;
using System.Net.Sockets;

namespace Net_NTPClient_Sample
{
    public static class NTPClient
    {
        /// <summary>
        /// Base Year
        /// </summary>
        private const int BASE_YEAR = 1900;

        /// <summary>
        /// Base Month
        /// </summary>
        private const int BASE_MONTH = 1;

        /// <summary>
        /// Base Day
        /// </summary>
        private const int BASE_DAY = 1;

        /// <summary>
        /// 指定したNTPサーバからUTCで時刻を取得する
        /// </summary>
        /// <param name="socket"></param>
        /// <returns>UTC時刻</returns>
        public static DateTime GetNowUTC(string ntpSever)
        {
            using (Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp))
            {
                // 返却用DateTime
                DateTime now;

                try
                {
                    // NTP接続用に3000msとする
                    socket.ReceiveTimeout = 3000;

                    // ソケットに接続する
                    socket.Connect(GetIPEndPoint(ntpSever));

                    // 送信データを作成
                    byte[] buffer = new byte[48];

                    // 先頭を0xBに設定
                    buffer[0] = 0xB;

                    // データを送信
                    socket.Send(buffer);

                    // 返信を受信
                    int bytes = socket.Receive(buffer);

                    if (bytes > 0)
                    {
                        var r = buffer[40] * Math.Pow((double)2, (8 * 3)) +
                                buffer[41] * Math.Pow((double)2, (8 * 2)) +
                                buffer[42] * Math.Pow((double)2, (8 * 1)) +
                                buffer[43];

                        var elapsedTimeTicks = (long)(r * 10000000);

                        var baseDate = new DateTime(BASE_YEAR, BASE_MONTH, BASE_DAY);
                        var baseTicks = baseDate.Ticks;
                        var ticks = baseTicks + elapsedTimeTicks;

                        now = new DateTime(ticks);

                    }
                    else
                    {
                        now = new DateTime(BASE_YEAR, BASE_MONTH, BASE_DAY);
                    }
                }
                catch (SocketException ex)
                {
                    Debug.WriteLine($"** Socket exception occurred: {ex.Message} error code {ex.ErrorCode}!**");
                    now = new DateTime(BASE_YEAR, BASE_MONTH, BASE_DAY);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"** Exception occurred: {ex.Message}!**");
                    now = new DateTime(BASE_YEAR, BASE_MONTH, BASE_DAY);
                }
                finally
                {
                    socket.Close();
                }

                return now;

            }

        }

        /// <summary>
        /// NTPサーバ名からIPEndPointを取得する
        /// </summary>
        /// <returns>IPEndPoint</returns>
        private static IPEndPoint GetIPEndPoint(string ntpSever)
        {
            // ホスト名からIPアドレスを取得
            IPHostEntry hostEntry = Dns.GetHostEntry(ntpSever);

            // IPアドレス、ポートからなるIPEndPointを返却
            return new IPEndPoint(hostEntry.AddressList[0], 123);
        }
    }
}
