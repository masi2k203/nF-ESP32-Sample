using System.Threading;

// nuget�Œǉ� //
using System.Device.Gpio;
using nanoFramework.Hardware.Esp32;

namespace GPIO_Basic
{
    public class Program
    {
        /// <summary>
        /// GPIO18�s���̃C���X�^���X
        /// </summary>
        private static GpioPin GPIO18;
        /// <summary>
        /// GPIO19�s���̃C���X�^���X
        /// </summary>
        private static GpioPin GPIO19;


        public static void Main()
        {
            // GpioController�̍쐬 (GPIO�̐ݒ�⑀����s��)
            GpioController gpioController = new GpioController();

            // GPIO18���J��
            GPIO18 = gpioController.OpenPin(Gpio.IO18);
            // GPIO18���o�͂ɐݒ�
            GPIO18.SetPinMode(PinMode.Output);

            // GPIO19���J��
            GPIO19 = gpioController.OpenPin(Gpio.IO19);
            // GPIO19�����(�v���A�b�v)�ɐݒ�
            GPIO19.SetPinMode(PinMode.InputPullUp);

            // GPIO18��HIGH�ɂ���
            GPIO18.Write(PinValue.High);

            // 1000ms�҂�
            Thread.Sleep(1000);

            // GPIO18��LOW�ɂ���
            GPIO18.Write(PinValue.Low);

            while (true)
            {
                // GPIO19��Low�̎�(�{�^���������ꂽ��)�ɏ���������
                if (GPIO19.Read() == PinValue.Low)
                {
                    GPIO18.Write(PinValue.High);
                }
                else
                {
                    GPIO18.Write(PinValue.Low);
                }
            }
        }
    }
}