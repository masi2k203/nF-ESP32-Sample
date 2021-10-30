using System;
using System.Device.I2c;

namespace Mas2k_NF_Sample
{
    class ADXL345
    {
        #region �t�B�[���h

        /// <summary>
        /// I2C�f�o�C�X
        /// </summary>
        private I2cDevice _i2cDevice;

        #endregion

        #region �v���p�e�B

        private double _x;
        public double X
        {
            get
            {
                return this._x;
            }
        }

        private double _y;
        public double Y
        {
            get
            {
                return this._y;
            }
        }

        private double _z;
        public double Z
        {
            get
            {
                return this._z;
            }
        }

        #endregion

        /// <summary>
        /// �R���X�g���N�^
        /// </summary>
        public ADXL345(I2cDevice device)
        {
            _i2cDevice = device;

            Initialize();
            UpdateValue();
        }

        #region ���\�b�h

        /// <summary>
        /// ������
        /// </summary>
        private void Initialize()
        {
            // �ő啪��\���[�h
            SpanByte init1 = new byte[2] { 0x31, 0x0B };
            _i2cDevice.Write(init1);

            // �ߓd����
            SpanByte init2 = new byte[2] { 0x2D, 0x08 };
            _i2cDevice.Write(init2);
        }

        /// <summary>
        /// �l���X�V����
        /// </summary>
        public void UpdateValue()
        {
            // �擪�A�h���X�ֈړ�
            _i2cDevice.WriteByte(0x32);

            // �����x�擾
            SpanByte readData = new byte[6];
            _i2cDevice.Read(readData);

            // �f�[�^�ϊ�
            short cal_x = (short)(((short)readData[1] << 8) | readData[0]);
            short cal_y = (short)(((short)readData[3] << 8) | readData[2]);
            short cal_z = (short)(((short)readData[5] << 8) | readData[4]);

            // �f�[�^�ۑ�
            this._x = cal_x * 0.0392266;
            this._y = cal_y * 0.0392266;
            this._z = cal_z * 0.0392266;
        }

        #endregion
    }
}