using System;
using System.Diagnostics;
using System.Threading;
using System.IO;
using System.Text;

namespace FileSystemExample
{
    public class Program
    {

        /// <summary>
        /// �t�@�C����(�p�X�t���Ŏw��)
        /// ESP32 ����EEPROM�̃h���C�u���^�[ => I:\\...
        /// </summary>
        private static string _filename = "I:\\sample1.txt";

        public static void Main()
        {
            Debug.WriteLine("===== System.IO.FileSystem Example =====\n");

            CreateFileExample();

            WriteFileExample();

            ModifyFileExample();

            ReadFileExample();

            DeleteFileExample();

            Debug.WriteLine("===== System.IO.FileSystem Example End =====");
        }

        /// <summary>
        /// �t�@�C�����쐬����T���v��
        /// </summary>
        public static void CreateFileExample()
        {
            Debug.WriteLine("===== File create sample. =====");

            Debug.WriteLine("Create >> " + _filename);

            /// File.Exists(string path)
            ///     - �t�@�C���̑��݂��m�F����
            ///     - �t�@�C�������݂����true
            ///     - �t�@�C�������݂��Ȃ����false
            bool exists = File.Exists(_filename);
            Debug.WriteLine("exists >> " + exists);

            /// File.Create(string path)
            ///     - �t�@�C�����쐬����
            File.Create(_filename);

            exists = File.Exists(_filename);
            Debug.WriteLine("exists >> " + exists);

            Debug.WriteLine("===== End of file create sample. =====\n\n");
        }

        /// <summary>
        /// �t�@�C���ɏ������ރT���v��
        /// </summary>
        public static void WriteFileExample()
        {
            Debug.WriteLine("===== Write sample. =====");

            // �t�@�C�������݂����
            if (File.Exists(_filename))
            {
                // �������ޕ�����(byte�z��)��p�ӂ���
                byte[] write = Encoding.UTF8.GetBytes("FileSystem Example.");

                try
                {
                    /// FileStream(string path, FileMode mode, FileAccess)
                    ///     - �t�@�C���ǂݏ����X�g���[�����쐬����
                    ///     - ����1 : �t�@�C���p�X
                    ///     - ����2 : �t�@�C���̃��[�h (Opem = �J�� �Ȃ�)
                    ///     - ����3 : �t�@�C���A�N�Z�X (ReadWrite = �ǂݏ��� �Ȃ�)
                    ///     - �g���I����������ׂ� (Dispose)
                    using (FileStream stream = new FileStream(_filename, FileMode.Open, FileAccess.ReadWrite))
                    {
                        /// Write(byte[] buffer, int offset, int count)
                        ///     - buffer�̓��e��offset�����I�t�Z�b�g������ԂŁAcount������������
                        stream.Write(write, 0, write.Length);
                    }

                    Debug.WriteLine("===== Complete. =====");
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"===== Faild. =====");
                    Debug.WriteLine(ex.Message);
                }
            }

            Debug.WriteLine("===== End of write sample. =====\n\n");
        }

        /// <summary>
        /// �t�@�C���ɒǉ��ŏ������ރT���v��
        /// </summary>
        public static void ModifyFileExample()
        {
            Debug.WriteLine("===== Modify sample. =====");

            // �t�@�C�������݂����
            if (File.Exists(_filename))
            {
                // �ǉ��ŏ������ޕ�����(byte�z��)��p�ӂ���
                byte[] write1 = Encoding.UTF8.GetBytes("start");
                byte[] write2 = Encoding.UTF8.GetBytes("end");

                try
                {
                    using (FileStream stream = new FileStream(_filename, FileMode.Open, FileAccess.ReadWrite))
                    {
                        // �X�g���[�����V�[�N�\�ȏꍇ
                        if (stream.CanSeek)
                        {
                            // �t�@�C���̐擪�ɃJ�[�\�����Z�b�g
                            stream.Seek(0, SeekOrigin.Begin);
                            stream.Write(write1, 0, write1.Length);

                            // �t�@�C���̖����ɃJ�[�\�����Z�b�g
                            stream.Seek(0, SeekOrigin.End);
                            stream.Write(write2, 0, write2.Length);
                        }
                    }

                    Debug.WriteLine("===== Complete. =====");
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"===== Faild. =====");
                    Debug.WriteLine(ex.Message);
                }
            }

            Debug.WriteLine("===== End of modify sample. =====\n\n");
        }

        /// <summary>
        /// �t�@�C���̒��g��ǂݎ��T���v��
        /// </summary>
        public static void ReadFileExample()
        {
            Debug.WriteLine("===== Read sample. =====");

            // �t�@�C�������݂����
            if (File.Exists(_filename))
            {
                try
                {
                    using (FileStream stream = new FileStream(_filename, FileMode.Open, FileAccess.Read))
                    {
                        // �t�@�C��������byte�z����쐬
                        byte[] read = new byte[stream.Length];
                        // �t�@�C����ǂݎ��
                        stream.Read(read, 0, read.Length);

                        // �o�͂��Ă݂�
                        foreach (var item in read)
                        {
                            Console.Write("" + (char)item);
                        }
                        Debug.WriteLine("\n<end>\n");
                    }

                    Debug.WriteLine("===== Complete. =====");
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"===== Faild. =====");
                    Debug.WriteLine(ex.Message);
                }
            }

            Debug.WriteLine("===== End of read sample. =====\n\n");
        }

        /// <summary>
        /// �t�@�C�����폜����T���v��
        /// </summary>
        public static void DeleteFileExample()
        {
            Debug.WriteLine("===== File delete sample. =====");

            Debug.WriteLine("Delete >> " + _filename);


            bool exists = File.Exists(_filename);
            Debug.WriteLine("exists >> " + exists);

            /// File.Delete(string path)
            ///     - �t�@�C�����폜����
            File.Delete(_filename);

            exists = File.Exists(_filename);
            Debug.WriteLine("exists >> " + exists);

            Debug.WriteLine("===== End of file delete sample. =====\n\n");
        }
    }
}
