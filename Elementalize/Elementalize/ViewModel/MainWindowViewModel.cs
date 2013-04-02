using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using MVVMBase.Command;
using MVVMBase.ViewModel;
using PeriodicName.Model;

namespace PeriodicName.ViewModel
{
	public class MainWindowViewModel : ViewModelBase
	{
		private string _title;
		private string _input;
		private string _output;
		private static List<Element> _possibleElements;
		private ReadOnlyCollection<ElementViewModel> _elements;

		public MainWindowViewModel()
			: base()
		{
			_title = "Elementalize";
			_input = String.Empty;
			_possibleElements = LoadElements();
			_possibleElements.Reverse();
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
				FindElements();
			}
		}

		public string Output
		{
			get { return _output; }
			set
			{
				_output = value;
				OnPropertyChanged("Output");
			}
		}

		public ReadOnlyCollection<ElementViewModel> Elements
		{
			get { return _elements; }
			set
			{
				_elements = value;
				OnPropertyChanged("Elements");
			}
		}

		#endregion // Properties

		#region Commands

		public ICommand Process
		{
			get
			{
				return new AppCommand(param => FindElements());
			}
		}

		#endregion // Commands

		#region Private

		private void FindElements()
		{
			var matchedElements = Match(Input.Replace(" ", String.Empty));

			if (matchedElements == null)
			{
				Elements = null;
				return;
			}

			List<ElementViewModel> elements = new List<ElementViewModel>();

			foreach (var element in matchedElements)
				elements.Add(new ElementViewModel(element));

			Elements = elements.AsReadOnly();
		}

		private ReadOnlyCollection<Element> Match(string word)
		{
			List<Element> elements = new List<Element>();

			string workingWord = word;

			foreach (Element elem in _possibleElements)
			{
				if (IsPossible(workingWord, elem))
				{
					elements.Add(elem);
					if (elem.Symbol.Length == word.Length)
						return elements.AsReadOnly();

					var remainingElements = Match(workingWord.Substring(elem.Symbol.Length));
					if (remainingElements == null)
						elements.Remove(elem);
					else
					{
						elements.AddRange(remainingElements);
						return elements.AsReadOnly();
					}
				}
			}

			return null;
		}

		private bool IsPossible(string word, Element elem)
		{
			return word.Length >= elem.Symbol.Length &&
				word.Substring(0, elem.Symbol.Length).ToUpper().Contains(elem.Symbol.ToUpper());
		}

		private List<Element> LoadElements()
		{
			return File.ReadAllLines(@"Content\elements.csv")
						.Select(line =>
						{
							string[] data = line.Split(',');
							return new Element
							(
								symbol: data[1],
								name: data[2],
								atomicNumber: data[0]
							);
						})
						.ToList();
		}

		#endregion // Private
	}
}
