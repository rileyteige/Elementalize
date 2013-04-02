using Elementalize.Model;

namespace Elementalize.ViewModel
{
	public class ElementViewModel : MVVMBase.ViewModel.ViewModelBase
	{
		public ElementViewModel(Element element)
			: base()
		{
			_atomicNumber = element.AtomicNumber;
			_name = element.Name;
			_symbol = element.Symbol;
		}

		public string AtomicNumber
		{
			get { return _atomicNumber; }
		}

		public string Name
		{
			get { return _name; }
		}

		public string Symbol
		{
			get { return _symbol; }
		}

		private string _atomicNumber;
		private string _name;
		private string _symbol;
	}
}
