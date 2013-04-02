using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Elementalize.Model;

namespace Elementalize.ViewModel
{
	public class WordViewModel : MVVMBase.ViewModel.ViewModelBase
	{
		private IEnumerable<ElementViewModel> _elements;

		public WordViewModel(string word)
			: base()
		{
			_elements = FindElements(word);
		}

		#region Properties

		public IEnumerable<ElementViewModel> Elements
		{
			get { return _elements; }
			set
			{
				_elements = value;
				OnPropertyChanged("Elements");
			}
		}

		#endregion // Properties

		#region Public

		public bool HasMatch()
		{
			return _elements != null && _elements.Count() > 0;
		}

		#endregion // Public

		#region Private

		private IEnumerable<ElementViewModel> FindElements(string word)
		{
			var matchedElements = Match(word);

			if (matchedElements == null)
				return null;

			List<ElementViewModel> elements = new List<ElementViewModel>();

			foreach (var element in matchedElements)
				elements.Add(new ElementViewModel(element));

			return elements.AsReadOnly();
		}

		private ReadOnlyCollection<Element> Match(string word)
		{
			List<Element> elements = new List<Element>();

			string workingWord = word;

			foreach (Element elem in ElementData.Elements)
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

		#endregion // Private
	}
}
