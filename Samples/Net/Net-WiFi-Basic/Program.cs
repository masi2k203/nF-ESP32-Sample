using System;
using System.Diagnostics;
using System.Threading;

// nuget�Œǉ� //
using Windows.Devices.WiFi;

namespace Net_WiFi_Basic
{
    public class Program
    {
        // SSID�ƃp�X���[�h��p�ӂ���
        const string WIFISSID = "�C�ӂ̃l�b�g���[�NSSID�����";
        const string WIFIPASS = "�C�ӂ̃l�b�g���[�N�̃p�X���[�h�����";

        public static void Main()
        {
            try
            {
                // �ŏ���WiFi�A�_�v�^���擾����
                WiFiAdapter wifi = WiFiAdapter.FindAllAdapters()[0];

                // AvailableNetworksChanged�C�x���g���w�ǂ���
                wifi.AvailableNetworksChanged += Wifi_AvailableNetworksChanged;

                // 30�b���Ƃ�WiFi�̃X�L�������J��Ԃ�
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
        /// �C�x���g����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void Wifi_AvailableNetworksChanged(WiFiAdapter sender, object e)
        {
            Debug.WriteLine("Wifi_availableNetworkChanged - get report");

            // �S�ẴX�L��������WiFi�l�b�g���[�N���烌�|�[�g���擾����
            WiFiNetworkReport report = sender.NetworkReport;

            // �l�b�g���[�N��񋓂���
            foreach (var net in report.AvailableNetworks)
            {
                Debug.WriteLine($"Net SSID : {net.Ssid}, BSSID : {net.Bsid}, rssi : {net.NetworkRssiInDecibelMilliwatts.ToString()}, signal : {net.SignalBars.ToString()}");

                // �T���Ă���l�b�g���[�N�̏ꍇ�͐ڑ�����
                if (net.Ssid == WIFISSID)
                {
                    // �l�b�g���[�N����ؒf����
                    sender.Disconnect();

                    // �l�b�g���[�N�ɐڑ�����
                    WiFiConnectionResult result = sender.Connect(net, WiFiReconnectionKind.Automatic, WIFIPASS);

                    // ��Ԃ�\������
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
