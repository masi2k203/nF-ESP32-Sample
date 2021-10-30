using System;
using System.Device.I2c;

namespace Mas2k_NF_Sample
{
    class ADXL345
    {
        #region フィールド

        /// <summary>
        /// I2Cデバイス
        /// </summary>
        private I2cDevice _i2cDevice;

        #endregion

        #region プロパティ

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
        /// コンストラクタ
        /// </summary>
        public ADXL345(I2cDevice device)
        {
            _i2cDevice = device;

            Initialize();
            UpdateValue();
        }

        #region メソッド

        /// <summary>
        /// 初期化
        /// </summary>
        private void Initialize()
        {
            // 最大分解能モード
            SpanByte init1 = new byte[2] { 0x31, 0x0B };
            _i2cDevice.Write(init1);

            // 節電制御
            SpanByte init2 = new byte[2] { 0x2D, 0x08 };
            _i2cDevice.Write(init2);
        }

        /// <summary>
        /// 値を更新する
        /// </summary>
        public void UpdateValue()
        {
            // 先頭アドレスへ移動
            _i2cDevice.WriteByte(0x32);

            // 加速度取得
            SpanByte readData = new byte[6];
            _i2cDevice.Read(readData);

            // データ変換
            short cal_x = (short)(((short)readData[1] << 8) | readData[0]);
            short cal_y = (short)(((short)readData[3] << 8) | readData[2]);
            short cal_z = (short)(((short)readData[5] << 8) | readData[4]);

            // データ保存
            this._x = cal_x * 0.0392266;
            this._y = cal_y * 0.0392266;
            this._z = cal_z * 0.0392266;
        }

        #endregion
    }
}