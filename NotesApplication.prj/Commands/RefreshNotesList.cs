using NotesApplication.Services;
using NotesApplication.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotesApplication.Commands
{
	public class RefreshNotesList : CommandBase
	{
		private readonly MainMenuViewModel _mainMenuViewModel;

		public RefreshNotesList(MainMenuViewModel mainMenuViewModel)
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
			new FilesServices().GetActualNotesList(_mainMenuViewModel.NotesNames);
		}
	}
}
