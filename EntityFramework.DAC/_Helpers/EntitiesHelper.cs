using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityFramework.DAC
{
    public static class EntitiesHelper
    {
        private static readonly string[] ForeignSuffixes = new string[] { "Uid", "Id", "Key", "Code" };
        
        public static string GetForeignKeyNavigationName(string foreignKeyColumnName)
        {
            foreach (string suffix in ForeignSuffixes)
            {
                if (foreignKeyColumnName.ToUpper().EndsWith(suffix.ToUpper()))
                {
                    return foreignKeyColumnName.Remove(foreignKeyColumnName.Length - suffix.Length);
                }
            }
            return foreignKeyColumnName;
        }
    }
}
