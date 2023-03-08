using NotesApplication.Services;
using NotesApplication.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interop;

namespace NotesApplication.Commands
{
	public class ItemChangedCommand : CommandBase
	{
		private readonly MainMenuViewModel _mainMenuViewModel;
		private readonly string pathToSaveNewNote = Environment.CurrentDirectory + "\\NotesApplication\\";



		public ItemChangedCommand(MainMenuViewModel mainMenuViewModel)
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
			_mainMenuViewModel.SelectedNote = _mainMenuViewModel.SelectedObject;
		}


	}
}
