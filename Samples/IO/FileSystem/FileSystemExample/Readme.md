# System.IO.FileSystem �T���v��

## ����
- ESP32�̓���EEPROM�Ńt�@�C��������s���T���v���ł��B
- ESP32-WROOM-32�œ���m�F�ς݂ł��B
- �����T���v����[������](https://github.com/nanoframework/Samples/tree/main/samples/System.IO.FileSystem)

## ���e
1. �t�@�C���̍쐬
2. �t�@�C���̓ǂݏ���
3. �t�@�C���ւ̒ǉ���������
4. �t�@�C���̑��ݔ���
5. �t�@�C���̍폜

## ����
- ESP32�̓���EEPROM�́AnanoFramework�ł̓h���C�u���^�[:I�ƂȂ�܂��B

## �T���v���̎��s���� (Program.cs)
```powershell
===== System.IO.FileSystem Example =====

===== File create sample. =====
Create >> I:\sample1.txt
exists >> True
exists >> True
===== End of file create sample. =====


===== Write sample. =====
===== Complete. =====
===== End of write sample. =====


===== Modify sample. =====
===== Complete. =====
===== End of modify sample. =====


===== Read sample. =====
FileSystem Example.startend
<end>

===== Complete. =====
===== End of read sample. =====


===== File delete sample. =====
Delete >> I:\sample1.txt
exists >> True
exists >> False
===== End of file delete sample. =====


===== System.IO.FileSystem Example End =====
```