using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elementalize.Model
{
	public class Element
	{
		public Element(string symbol, string name, string atomicNumber)
		{
			AtomicNumber = atomicNumber;
			Symbol = symbol;
			Name = name;
		}

		public string AtomicNumber { get; set; }
		public string Symbol { get; set; }
		public string Name { get; set; }
	}
}
