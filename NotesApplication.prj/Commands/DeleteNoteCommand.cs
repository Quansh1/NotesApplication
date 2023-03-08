using Microsoft.Win32;
using NotesApplication.Services;
using NotesApplication.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace NotesApplication.Commands
{
	public class DeleteNoteCommand : CommandBase
	{
		private readonly MainMenuViewModel _mainMenuViewModel;

		public DeleteNoteCommand(MainMenuViewModel mainMenuViewModel)
		{
			_mainMenuViewModel = mainMenuViewModel;

			_mainMenuViewModel.PropertyChanged += OnViewModelPropertyChanged;
		}

		private void OnViewModelPropertyChanged(object? sender, PropertyChangedEventArgs e)
		{
			OnCanExecuteChanged();
		}

		public override void Execute(object? parameter)
		{
			DirectoryInfo defaultDirectory = new DirectoryInfo(Environment.CurrentDirectory+ "\\NotesApplication\\");
			FileInfo[] notes = defaultDirectory.GetFiles("*.rtf");
			foreach(FileInfo note in notes)
			{
				if(note.Name.Remove(note.Name.Length - 4) == _mainMenuViewModel.SelectedNote)
				{
					note.Delete();
					new FilesServices().GetActualNotesList(_mainMenuViewModel.NotesNames);
				}
			}
		}
	}
}
