using System;
using System.Diagnostics;

// nugetで追加 //
using nanoFramework.Networking;
using System.Threading;
using Windows.Devices.WiFi;

namespace Net_NTPClient_Sample
{
    public class Program
    {
        private static string WIFISSID = "任意のネットワークSSIDを入力";
        private static string WIFIPASS = "任意のネットワークのパスワードを入力";

        public static void Main()
        {
            CancellationTokenSource cs = new(60000);

            // NTPの接続テストしたいのでsetDateTimeはfalse
            bool success = NetworkHelper.ConnectWifiDhcp(WIFISSID, WIFIPASS, setDateTime: false, token: cs.Token);

            // Wifiに接続できなかった場合
            if (!success)
            {
                if (NetworkHelper.ConnectionError.Exception != null)
                {
                    Debug.WriteLine($"Exception: {NetworkHelper.ConnectionError.Exception}");
                }
                return;
            }

            // 必ずWiFiに接続できてから行う
            DateTime now = NTPClient.GetNowUTC("ntp.jst.mfeed.ad.jp");

            // 結果を表示する
            Debug.WriteLine("now >> " + now.ToString());

            Thread.Sleep(Timeout.Infinite);
        }
    }
}
