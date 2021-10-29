using System;
using System.Diagnostics;
using System.Threading;

// nugetで追加 //
using Windows.Devices.WiFi;

namespace Net_WiFi_Basic
{
    public class Program
    {
        // SSIDとパスワードを用意する
        const string WIFISSID = "任意のネットワークSSIDを入力";
        const string WIFIPASS = "任意のネットワークのパスワードを入力";

        public static void Main()
        {
            try
            {
                // 最初のWiFiアダプタを取得する
                WiFiAdapter wifi = WiFiAdapter.FindAllAdapters()[0];

                // AvailableNetworksChangedイベントを購読する
                wifi.AvailableNetworksChanged += Wifi_AvailableNetworksChanged;

                // 30秒ごとにWiFiのスキャンを繰り返す
                while (true)
                {
                    Debug.WriteLine("starting wifi scan.");
                    wifi.ScanAsync();

                    Thread.Sleep(30000);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("message : " + ex.Message);
                Debug.WriteLine("stack : " + ex.StackTrace);
            }

            Thread.Sleep(Timeout.Infinite);
        }

        /// <summary>
        /// イベント処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void Wifi_AvailableNetworksChanged(WiFiAdapter sender, object e)
        {
            Debug.WriteLine("Wifi_availableNetworkChanged - get report");

            // 全てのスキャンしたWiFiネットワークからレポートを取得する
            WiFiNetworkReport report = sender.NetworkReport;

            // ネットワークを列挙する
            foreach (var net in report.AvailableNetworks)
            {
                Debug.WriteLine($"Net SSID : {net.Ssid}, BSSID : {net.Bsid}, rssi : {net.NetworkRssiInDecibelMilliwatts.ToString()}, signal : {net.SignalBars.ToString()}");

                // 探しているネットワークの場合は接続する
                if (net.Ssid == WIFISSID)
                {
                    // ネットワークから切断する
                    sender.Disconnect();

                    // ネットワークに接続する
                    WiFiConnectionResult result = sender.Connect(net, WiFiReconnectionKind.Automatic, WIFIPASS);

                    // 状態を表示する
                    if (result.ConnectionStatus == WiFiConnectionStatus.Success)
                    {
                        Debug.WriteLine("Connected to WiFi network");
                    }
                    else
                    {
                        Debug.WriteLine("Error " + result.ConnectionStatus.ToString() + " connecting a WiFi network");
                    }


                }
            }
        }
    }
}
