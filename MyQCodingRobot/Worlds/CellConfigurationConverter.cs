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
        private Dictionary<string, CellConfiguration> CellConfigurations;

        public CellConfigurationConverter()
        {
            CellConfigurations = typeof(CellConfiguration)
               .GetFields(BindingFlags.Public | BindingFlags.Static)
               .Where(f => f.FieldType == typeof(CellConfiguration))
               .ToDictionary(f => ((CellConfiguration)f.GetValue(null)!).CellCode ?? String.Empty,
                             f => (CellConfiguration)f.GetValue(null)!);
        }

        public CellConfiguration GetByCode(string? code)
        {
            if (CellConfigurations.TryGetValue(code ?? String.Empty, out CellConfiguration? cellConfiguration))
            {
                return cellConfiguration;
            }

            throw new Exception();
        }
    }
}
