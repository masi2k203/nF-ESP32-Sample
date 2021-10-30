using System;
using System.Threading;
using System.Device.I2c;

namespace Mas2k_NF_Sample
{
    class AQM1602x
    {

        #region �t�B�[���h

        /// <summary>
        /// I2C�f�o�C�X
        /// </summary>
        private I2cDevice _i2cDevice;

        private bool _is5V;

        #endregion

        /// <summary>
        /// �R���X�g���N�^
        /// </summary>
        /// <param name="device">I2cDevice</param>
        /// <param name="is5V">5V�d�����g�p����̂ł����true</param>
        public AQM1602x(I2cDevice device, bool is5V)
        {
            _i2cDevice = device;
            _is5V = is5V;

            Initialize();
        }

        #region ���\�b�h

        /// <summary>
        /// LCD������������֐�
        /// </summary>
        private void Initialize()
        {
            // �������p�R�}���h
            byte[] initializeCommand1 = new byte[]
            {
                0x38, 0x39, 0x14, 0x73, 0x5E, 0x6C
            };
            byte[] initializeCommand2 = new byte[]
            {
                0x38, 0x01, 0x0C
            };

            // �d���d���ɂ���ăR�}���h��ύX
            if (_is5V)
            {
                initializeCommand1[4] = 0x56;
            }

            // �R�}���h���M
            foreach (var item in initializeCommand1)
            {
                WriteCommand(item);
            }

            // �ҋ@
            Thread.Sleep(200);

            // �R�}���h���M
            foreach (var item in initializeCommand2)
            {
                WriteCommand(item);
            }
        }

        /// <summary>
        /// LCD�ɃR�}���h�𑗐M���郁�\�b�h
        /// </summary>
        /// <param name="command">�R�}���h</param>
        /// <returns>���M����</returns>
        private I2cTransferResult WriteCommand(byte command)
        {
            // ���MI2C�f�[�^�z��쐬
            SpanByte i2cData = new byte[2];
            i2cData[0] = 0x00;
            i2cData[1] = command;

            // ���M���đ��M���ʂ��擾
            var result = _i2cDevice.Write(i2cData);

            // �ҋ@
            Thread.Sleep(1);

            return result;
        }

        /// <summary>
        /// LCD��1�o�C�g�����𑗐M���郁�\�b�h
        /// </summary>
        /// <param name="data">���M���镶��</param>
        /// <returns></returns>
        private I2cTransferResult WriteByteData(char data)
        {
            // ���MI2C�f�[�^�z��쐬
            SpanByte i2cData = new byte[2];
            i2cData[0] = 0x40;
            i2cData[1] = (byte)data;

            // ���M���đ��M���ʂ��擾
            var result = _i2cDevice.Write(i2cData);

            // �ҋ@
            Thread.Sleep(1);

            return result;
        }

        /// <summary>
        /// LCD�ɕ������\�����郁�\�b�h
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

                // 1�s��
                for (int i = 0; i < outLine1.Length; i++)
                {
                    WriteByteData(outLine1[i]);
                }
                // ���s
                WriteCommand(0x40 + 0x80);
                // 2�s��
                for (int i = 0; i < outLine2.Length; i++)
                {
                    // 2�s�ڂ�16�����ڂ܂ŏ�������B���̌�͎̂Ă�
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

            // �S�ĕ\�����I������z�[���ɖ߂�
            WriteCommand(0x02);
        }

        /// <summary>
        /// LCD�̕\�����������郁�\�b�h
        /// </summary>
        public void Clear()
        {
            WriteCommand(0x01);
        }

        #endregion
    }
}