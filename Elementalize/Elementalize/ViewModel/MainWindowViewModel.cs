using System;
using System.Collections.Generic;
using System.Linq;
using Elementalize.Model;
using MVVMBase.ViewModel;

namespace Elementalize.ViewModel
{
	public class MainWindowViewModel : ViewModelBase
	{
		private string _title;
		private string _input;
		private IEnumerable<WordViewModel> _words;

		public MainWindowViewModel()
			: base()
		{
			_title = "Elementalize";
			ElementData.LoadElements(@"Content\elements.csv");
		}

		#region Properties

		public string Title
		{
			get { return _title; }
			set
			{
				_title = value;
				OnPropertyChanged("Title");
			}
		}

		public string Input
		{
			get { return _input; }
			set
			{
				_input = value;
				OnPropertyChanged("Input");

				BuildWords(_input);
			}
		}

		public IEnumerable<WordViewModel> Words
		{
			get { return _words; }
			set
			{
				_words = value;
				OnPropertyChanged("Words");
			}
		}

		#endregion // Properties

		#region Private

		private void BuildWords(string line)
		{
			if (String.IsNullOrEmpty(line))
			{
				Words = null;
				return;
			}

			Words = line
				.Split(' ')
				.Where(term => !String.IsNullOrEmpty(term))
				.Select(term => new WordViewModel(term))
				.Where(word => word.HasMatch())
				.ToList()
				.AsReadOnly();
		}

		#endregion // Private
	}
}
