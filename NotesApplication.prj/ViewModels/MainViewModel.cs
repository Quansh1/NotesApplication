using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotesApplication.ViewModels
{
	public class MainViewModel : ViewModelBase
	{
		public ViewModelBase CurrentViewModel { get; }

		public MainViewModel()
		{
			CurrentViewModel = new MainMenuViewModel();
		}
	}
}
