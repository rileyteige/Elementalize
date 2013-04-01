using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeriodicName.Model
{
	public class Element
	{
		public Element(string symbol, string name)
		{
			Symbol = symbol;
			Name = name;
		}

		public string Symbol { get; set; }
		public string Name { get; set; }
	}
}
