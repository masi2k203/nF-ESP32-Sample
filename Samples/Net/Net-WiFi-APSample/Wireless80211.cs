using nanoFramework.Networking;
using System;
using System.Diagnostics;
using System.Net.NetworkInformation;
using System.Threading;


namespace Net_WiFi_APSample
{
    class Wireless80211
    {
        public static bool IsEnabled()
        {
            Wireless80211Configuration wconf = GetConfiguration();
            return !string.IsNullOrEmpty(wconf.Ssid);
        }

        /// <summary>
        /// ワイヤレスステーションインターフェースを無効化する
        /// </summary>
        public static void Disable()
        {
            Wireless80211Configuration wconf = GetConfiguration();
            wconf.Options = Wireless80211Configuration.ConfigurationOptions.None;
            wconf.SaveConfiguration();
        }

        /// <summary>
        /// ワイヤレスステーションインターフェースを設定し有効化する
        /// </summary>
        /// <param name="ssid"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public static bool Configure(string ssid, string password)
        {
            var success = NetworkHelper.ConnectWifiDhcp(ssid, password, token: new CancellationTokenSource(10000).Token);
            Debug.WriteLine($"Connection is {success}");
            Wireless80211Configuration wconf = GetConfiguration();
            wconf.Options = Wireless80211Configuration.ConfigurationOptions.AutoConnect | Wireless80211Configuration.ConfigurationOptions.Enable;
            wconf.SaveConfiguration();
            return true;
        }

        /// <summary>
        /// ワイヤレスステーション構成を取得する
        /// </summary>
        /// <returns></returns>
        public static Wireless80211Configuration GetConfiguration()
        {
            NetworkInterface ni = GetInterface();
            return Wireless80211Configuration.GetAllWireless80211Configurations()[ni.SpecificConfigId];
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
                if (ni.NetworkInterfaceType == NetworkInterfaceType.Wireless80211)
                {
                    return ni;
                }
            }
            return null;
        }

        public static string WaitIP()
        {
            while (true)
            {
                NetworkInterface ni = GetInterface();
                if (ni.IPv4Address != null && ni.IPv4Address.Length > 0)
                {
                    if (ni.IPv4Address[0] != '0')
                    {
                        return ni.IPv4Address;
                    }
                }
                Thread.Sleep(500);
            }
        }
    }
}
