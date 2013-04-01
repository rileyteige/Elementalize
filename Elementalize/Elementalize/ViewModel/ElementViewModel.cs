﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PeriodicName.Model;

namespace PeriodicName.ViewModel
{
	public class ElementViewModel : MVVMBase.ViewModel.ViewModelBase
	{
		public ElementViewModel(Element element)
			: base()
		{
			_name = element.Name;
			_symbol = element.Symbol;
		}

		public string Name
		{
			get { return _name; }
		}

		public string Symbol
		{
			get { return _symbol; }
		}

		private string _name;
		private string _symbol;
	}
}