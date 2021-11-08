using System.Reflection;

namespace MyQCodingRobot.Worlds
{
	public static class CellConfigurationConverter
	{
		private const string EmptyCellName = "";
		private static readonly Dictionary<string, CellConfiguration> CellConfigurations = typeof(CellConfiguration)
				.GetFields(BindingFlags.Public | BindingFlags.Static)
				.Where(f => f.FieldType == typeof(CellConfiguration))
				.ToDictionary(f => ((CellConfiguration)f.GetValue(null)!).CellCode ?? EmptyCellName,
							 f => (CellConfiguration)f.GetValue(null)!);

		public static CellConfiguration GetByCode(string? code)
		{
			if (CellConfigurations.TryGetValue(code ?? EmptyCellName, out CellConfiguration? cellConfiguration))
			{
				return cellConfiguration;
			}

			throw new Exception($"Cell code {code} not found");
		}
	}
}
