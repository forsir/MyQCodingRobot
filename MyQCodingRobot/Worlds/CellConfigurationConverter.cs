using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MyQCodingRobot.Worlds
{
	public class CellConfigurationConverter
	{
		private readonly Dictionary<string, CellConfiguration> _cellConfigurations;
		private const string EmptyCellName = "null";

		public CellConfigurationConverter()
		{
			_cellConfigurations = typeof(CellConfiguration)
				.GetFields(BindingFlags.Public | BindingFlags.Static)
				.Where(f => f.FieldType == typeof(CellConfiguration))
				.ToDictionary(f => ((CellConfiguration)f.GetValue(null)!).CellCode ?? EmptyCellName,
							 f => (CellConfiguration)f.GetValue(null)!);
		}

		public CellConfiguration GetByCode(string? code)
		{
			if (_cellConfigurations.TryGetValue(code ?? EmptyCellName, out CellConfiguration? cellConfiguration))
			{
				return cellConfiguration;
			}

			throw new Exception();
		}
	}
}
