using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace NotesApplication.Services
{
	public class iniFileForUserSettings
	{
		private readonly string _pathToDefaultDirectory = Environment.CurrentDirectory + "\\NotesApplication" + "\\settings.ini"; //Имя файла.


		[DllImport("kernel32")] // Подключаем kernel32.dll и описываем его функцию WritePrivateProfilesString
		static extern long WritePrivateProfileString(string Section, string Key, string Value, string FilePath);

		[DllImport("kernel32")] // Еще раз подключаем kernel32.dll, а теперь описываем функцию GetPrivateProfileString
		static extern int GetPrivateProfileString(string Section, string Key, string Default, StringBuilder RetVal, int Size, string FilePath);



		//Записываем в ini-файл. Запись происходит в выбранную секцию в выбранный ключ.
		public void WriteIni(string Section, string Key, string Value)
		{
			WritePrivateProfileString(Section, Key, Value, _pathToDefaultDirectory);
		}

		//Читаем ini-файл и возвращаем значение указного ключа из заданной секции.
		public string ReadIni(string Section, string Key)
		{
			var RetVal = new StringBuilder(255);
			GetPrivateProfileString(Section, Key, "", RetVal, 255, _pathToDefaultDirectory);
			return RetVal.ToString();

		}
	}
}
