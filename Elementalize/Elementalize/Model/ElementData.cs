using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elementalize.Model
{
	public static class ElementData
	{
		private static IEnumerable<Element> _elements;

		public static IEnumerable<Element> Elements
		{
			get { return _elements; }
		}

		public static void LoadElements(string filepath)
		{
			_elements = File.ReadAllLines(filepath)
						.Select(line =>
						{
							string[] data = line.Split(',');
							return new Element
							(
								atomicNumber: data[0],
								symbol: data[1],
								name: data[2]
							);
						})
						.Reverse()
						.ToList()
						.AsReadOnly();
		}
	}
}
