using System;
using System.Net.NetworkInformation;

namespace Net_WiFi_APSample
{
    class WirelessAP
    {
        /// <summary>
        /// 次の再起動のためにソフトAPを無効にする
        /// </summary>
        public static void Disable()
        {
            WirelessAPConfiguration wapconf = GetConfiguration();
            wapconf.Options = WirelessAPConfiguration.ConfigurationOptions.None;
            wapconf.SaveConfiguration();
        }

        /// <summary>
        /// ワイヤレスAPを設定し、有効化して保存する
        /// </summary>
        /// <returns></returns>
        public static bool Setup()
        {
            string SoftApIP = "192.168.4.1";

            NetworkInterface ni = GetInterface();
            WirelessAPConfiguration wapconf = GetConfiguration();

            // 既に有効化されているか確認する
            if (wapconf.Options == (WirelessAPConfiguration.ConfigurationOptions.Enable |
                                    WirelessAPConfiguration.ConfigurationOptions.AutoStart) &&
                ni.IPv4Address == SoftApIP)
            {
                return true;
            }

            // IPアドレス、サブネットマスクを設定する
            ni.EnableStaticIPv4(SoftApIP, "255.255.255.0", SoftApIP);

            // ネットワークインターフェースオプション設定
            wapconf.Options = WirelessAPConfiguration.ConfigurationOptions.AutoStart |
                            WirelessAPConfiguration.ConfigurationOptions.Enable;

            // SSID名を設定する (設定しなければデフォルトのSSID名が使われる)
            // wapconf.Ssid = "ESP32_AP";

            // 同時接続できる最大数を設定する
            wapconf.MaxConnections = 1;

            // ↓認証無しで設定するなら↓
            wapconf.Authentication = AuthenticationType.Open;
            wapconf.Password = "";

            // ↓認証ありで設定するなら↓
            wapconf.Authentication = AuthenticationType.WPA2;
            wapconf.Password = "xxxxxxxx";

            // 再起動時にアクセスポイントが実行されるように、設定を保存する
            wapconf.SaveConfiguration();

            return false;
        }

        /// <summary>
        /// ネットワークインターフェースを取得する
        /// </summary>
        /// <returns></returns>
        private static NetworkInterface GetInterface()
        {
            NetworkInterface[] Interfaces = NetworkInterface.GetAllNetworkInterfaces();

            // Find WirelessAP interface
            foreach (NetworkInterface ni in Interfaces)
            {
                if (ni.NetworkInterfaceType == NetworkInterfaceType.WirelessAP)
                {
                    return ni;
                }
            }
            return null;
        }

        /// <summary>
        /// ワイヤレスAP構成を取得する
        /// </summary>
        /// <returns></returns>
        private static WirelessAPConfiguration GetConfiguration()
        {
            NetworkInterface ni = GetInterface();
            return WirelessAPConfiguration.GetAllWirelessAPConfigurations()[ni.SpecificConfigId];
        }

        /// <summary>
        /// IPアドレス(IPv4)を取得する
        /// </summary>
        /// <returns></returns>
        public static string GetIP()
        {
            NetworkInterface ni = GetInterface();
            return ni.IPv4Address;
        }
    }
}
