using System;
using System.Diagnostics;

// nuget�Œǉ� //
using nanoFramework.Networking;
using System.Threading;
using Windows.Devices.WiFi;

namespace Net_NTPClient_Sample
{
    public class Program
    {
        private static string WIFISSID = "�C�ӂ̃l�b�g���[�NSSID�����";
        private static string WIFIPASS = "�C�ӂ̃l�b�g���[�N�̃p�X���[�h�����";

        public static void Main()
        {
            CancellationTokenSource cs = new(60000);

            // NTP�̐ڑ��e�X�g�������̂�setDateTime��false
            bool success = NetworkHelper.ConnectWifiDhcp(WIFISSID, WIFIPASS, setDateTime: false, token: cs.Token);

            // Wifi�ɐڑ��ł��Ȃ������ꍇ
            if (!success)
            {
                if (NetworkHelper.ConnectionError.Exception != null)
                {
                    Debug.WriteLine($"Exception: {NetworkHelper.ConnectionError.Exception}");
                }
                return;
            }

            // �K��WiFi�ɐڑ��ł��Ă���s��
            DateTime now = NTPClient.GetNowUTC("ntp.jst.mfeed.ad.jp");

            // ���ʂ�\������
            Debug.WriteLine("now >> " + now.ToString());

            Thread.Sleep(Timeout.Infinite);
        }
    }
}
