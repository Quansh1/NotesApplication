using NotesApplication.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;

namespace NotesApplication.Commands
{
	public class AddNewNoteCommand : CommandBase
	{
		private readonly MainMenuViewModel _mainMenuViewModel;
		private readonly string pathToSaveNewNote = Environment.CurrentDirectory + "\\NotesApplication\\";



		public AddNewNoteCommand(MainMenuViewModel mainMenuViewModel)
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
			string nameForNewNote = pathToSaveNewNote + _mainMenuViewModel.NewNoteName + ".rtf";
			if(File.Exists(nameForNewNote))
			{
				MessageBox.Show("Заметка с таким именем существует!");
			}
			else
			{
				_mainMenuViewModel.NotesNames.Add(_mainMenuViewModel.NewNoteName);

				FlowDocument workDoc = new FlowDocument();
				TextRange selection = new TextRange(workDoc.ContentStart, workDoc.ContentEnd);
				using(FileStream fs = File.Create(nameForNewNote))
				{
					selection.Save(fs, DataFormats.Rtf);
				}

			}
		}
	}
}
