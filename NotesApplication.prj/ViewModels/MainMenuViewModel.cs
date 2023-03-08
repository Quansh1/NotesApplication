using Microsoft.Win32;
using NotesApplication.Commands;
using NotesApplication.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;

namespace NotesApplication.ViewModels
{
	public class MainMenuViewModel : ViewModelBase
	{
		private static readonly string _pathToDefaultDirectory = Environment.CurrentDirectory + "\\NotesApplication";


		private string newNoteName;
		public string NewNoteName
		{
			get
			{
				return newNoteName;
			}
			set
			{
				newNoteName = value;
				OnPropertyChanged(nameof(NewNoteName));
			}
		}


		private string searchNoteByName;
		public string SearchNoteByName
		{
			get
			{
				return searchNoteByName;
			}
			set
			{
				searchNoteByName = value;
				OnPropertyChanged(nameof(SearchNoteByName));
			}
		}


		private string selectedNote;
		public string SelectedNote
		{
			get
			{ 
				return selectedNote;
			}
			set
			{
				selectedNote = value;
				OnPropertyChanged(nameof(SelectedNote));
			}
		}

		private string selectedObject;
		public string SelectedObject
		{
			get
			{
				return selectedObject;
			}
			set
			{
				selectedObject = value;
				OnPropertyChanged(nameof(SelectedObject));
			}
		}

		private ObservableCollection<string> notesNames;
		public ObservableCollection<string> NotesNames
		{
			get
			{
				return notesNames;
			}
			set
			{
				notesNames = value;
				OnPropertyChanged(nameof(NotesNames));
			}
		}

		public static List<FontFamily> FontNames
		{
			get { return EditingNoteServices.FontNames; }
		}

		public static List<double> FontSize
		{
			get { return EditingNoteServices.FontSize; }
		}

		public ICommand AddNewNoteCommand { get; }
		public ICommand DeleteNoteCommand { get; }
		public ICommand BoldTextCommand { get; }
		public ICommand ItalicTextCommand { get; }
		public ICommand UnderlinedTextCommand { get; }
		public ICommand ItemChangedCommand { get; }
		public ICommand RefreshNotesList { get; }
		

		public MainMenuViewModel()
        {
			new FilesServices().CreateDefaultDirectoryForNotes(_pathToDefaultDirectory);
		
			newNoteName = "Имя новой заметки";
			searchNoteByName = "Имя заметки";
			
			NotesNames = new ObservableCollection<string>();
			new FilesServices().GetActualNotesList(NotesNames);

			AddNewNoteCommand = new AddNewNoteCommand(this);
			DeleteNoteCommand = new DeleteNoteCommand(this);
			RefreshNotesList = new RefreshNotesList(this);

			ItemChangedCommand = new ItemChangedCommand(this);
		}

		
		
	}
}
