using System.Diagnostics;

// nuget�Œǉ� //
using System.Device.Gpio;
using nanoFramework.Hardware.Esp32;

namespace GPIO_ValueChangedEvent
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

            // GPIO19�Ƀs����ԕω����̃C�x���g��ǉ�
            GPIO19.ValueChanged += GPIO19_ValueChanged;

            while (true)
            {
                // �������Ȃ�
            }
        }

        /// <summary>
        /// GPIO19�ł�ValueChanged�C�x���g����
        /// GPIO19�̒l���ω����邽�тɂ��̃��\�b�h�����s�����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void GPIO19_ValueChanged(object sender, PinValueChangedEventArgs e)
        {
            // �C�x���g����GPIO19�s���̏�Ԃ��m�F
            Debug.WriteLine("Button (event) : " + e.ChangeType.ToString());

            // GPIO19�s���̏�Ԃ𒼐ړǂݎ��
            Debug.WriteLine("Button (direct) : " + GPIO19.Read());

            // GPIO19��High����Low�ɂȂ�Ƃ�
            if (e.ChangeType == PinEventTypes.Falling)
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
