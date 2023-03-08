using Microsoft.Win32;
using NotesApplication.Services;
using NotesApplication.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Runtime.Intrinsics.Arm;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Media.Imaging;


namespace NotesApplication.Views
{
	/// <summary>
	/// Interaction logic for MainMenuView.xaml
	/// </summary>
	public partial class MainMenuView : UserControl
	{
		public MainMenuView()
		{
			InitializeComponent();
		}

		
		// Добавить изображение в редактируемую заметку
		private void AddImageToNoteBtn_Click(object sender, RoutedEventArgs e)
		{
			new FilesServices().AddImageOpenFileDialog(RichTextBoxNoteEditor);
		}
		
		//Сохранить редактируемую заметку
		private void SaveNoteBtn_Click(object sender, RoutedEventArgs e)
		{
			new FilesServices().SaveNote(RichTextBoxNoteEditor, Environment.CurrentDirectory + "\\NotesApplication\\", SelectedNoteName.Text);
		}

		//Выбрать и открыть(предоставить возможность редактирования) заметку из списка
		private void NotesList_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			if(NotesList.SelectedItem != null)
				new FilesServices().OpenNote(RichTextBoxNoteEditor, NotesList.SelectedItem.ToString());
		}

		//Сделать выделенный текст полужирным
		private void selectionTextBoldBtn_Click(object sender, RoutedEventArgs e)
		{
			new EditingNoteServices().makeSelectTextBold(RichTextBoxNoteEditor);
		}
		
		//Применить курсив к выделенному тексту
		private void selectionTextItalicBtn_Click(object sender, RoutedEventArgs e)
		{
			new EditingNoteServices().makeSelectTextItalic(RichTextBoxNoteEditor);
		}

		//Сделать выделенный текст подчеркнутым
		private void selectionTextUnderLineBtn_Click(object sender, RoutedEventArgs e)
		{
			new EditingNoteServices().makeSelectTextUnderlined(RichTextBoxNoteEditor);
		}

		//Выбор нового шрифта для выделенного текста
		private void ListOfFontFamily_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			new EditingNoteServices().changeFontFamily(RichTextBoxNoteEditor, ListOfFontFamily);
		}

		//Выбор нового размера шрифта для выделенного текста
		private void ListOfFontSize_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			new EditingNoteServices().changeFontSize(RichTextBoxNoteEditor, ListOfFontSize);
		}
	}
}
