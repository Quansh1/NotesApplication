using Microsoft.Win32;
using NotesApplication.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.IO.Packaging;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Automation.Peers;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Interop;

namespace NotesApplication.Services
{
	public class FilesServices
	{
		private readonly string _pathToDefaultDirectory = Environment.CurrentDirectory + "\\NotesApplication"; //путь до папки с заметками

		/// <summary>
		/// Получение актуального списка заметок
		/// </summary>
		/// <param name="NotesNames">имя заметок</param>
		public void GetActualNotesList(ObservableCollection<string> NotesNames)
		{
			NotesNames.Clear();
			DirectoryInfo defaultDirectory = new DirectoryInfo(_pathToDefaultDirectory);
			FileInfo[] notes = defaultDirectory.GetFiles("*.rtf");
			foreach(FileInfo note in notes)
			{
				if(!NotesNames.Contains(note.Name.Remove(note.Name.Length - 4)))
				{
					NotesNames.Add(note.Name.Remove(note.Name.Length - 4));
				}
			}
		}

		/// <summary>
		/// Добавление изображения в заметку
		/// </summary>
		/// <param name="richTextBoxName">Имя richTextBox для добавления картинки в заметку</param>
		public void AddImageOpenFileDialog(RichTextBox richTextBoxName)
		{
			OpenFileDialog addImageFileDialog = new OpenFileDialog();
			addImageFileDialog.Filter = "Images |*.bmp;*.jpg;*.png";
			addImageFileDialog.Multiselect = false;
			addImageFileDialog.FileName = "";

			if(addImageFileDialog.ShowDialog() == true)
			{
				new EditingNoteServices().AddImageToNote(addImageFileDialog.FileName, richTextBoxName);
			}
		}

		/// <summary>
		/// Сохранение заметки
		/// </summary>
		/// <param name="richTextBoxName">Имя richTextBox для сохранения содержимого заметки</param>
		public void SaveNote(RichTextBox richTextBoxName, string pathToSaveNote, string nameNote)
		{
			if(nameNote == "")
			{
				MessageBox.Show("Задайте имя заметке!");
			}
			else
			{
				TextRange doc = new TextRange(richTextBoxName.Document.ContentStart, richTextBoxName.Document.ContentEnd);
				using(FileStream fcreate = File.Open(pathToSaveNote + nameNote + ".rtf", FileMode.Create))
				{
					doc.Save(fcreate, DataFormats.Rtf);

					MessageBox.Show("Заметка успешно сохранена, она появится в списке после его обновления!");
				}
			}
		}

		/// <summary>
		/// Открытие заметки
		/// </summary>
		/// <param name="richTextBoxName">Имя richTextBox для вывода содержимого заметки</param>
		public void OpenNote(RichTextBox richTextBoxName, string pathToNote)
		{
			try
			{
				TextRange doc = new TextRange(richTextBoxName.Document.ContentStart, richTextBoxName.Document.ContentEnd);
				using(FileStream fs = new FileStream(_pathToDefaultDirectory + "\\" + pathToNote + ".rtf", FileMode.Open))
				{
					doc.Load(fs, DataFormats.Rtf);
				}
			}
			catch(Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		/// <summary>
		/// Создание директории для сохранения/хранения заметок
		/// </summary>
		public void CreateDefaultDirectoryForNotes(string path)
		{
			if(!Directory.Exists(path))
				Directory.CreateDirectory(path);
			CreateFirstNote(path);
		}

		/// <summary>
		/// Текст для новой заметки
		/// </summary>
		/// <param name="path">путь до новой заметки</param>
		private void CreateFirstNote(string path)
		{
			if(new iniFileForUserSettings().ReadIni("UserSettings", "FirstLaunchСompleted") != "True")
			{
				FlowDocument workDoc = new FlowDocument(new Paragraph(new Run("Welcome to NotesApplication!")));
				TextRange selection = new TextRange(workDoc.ContentStart, workDoc.ContentEnd);
				using(FileStream fs = File.Create(path + "\\Ваша первая заметка(Можно нажать).rtf"))
				{
					selection.Save(fs, DataFormats.Rtf);
				}
				new iniFileForUserSettings().WriteIni("UserSettings", "FirstLaunchСompleted", "True");

			}
		}
	}
	
}
