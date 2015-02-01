using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityFramework.DAC.Entities
{
    public class ForeignKeyInfo
    {
        private string _referenceName;

        public string Name { get; set; }

        public string ReferencedTable { get; set; }

        public string ReferencedTableSchema { get; set; }

        public string ReferenceName
        {
            get
            {
                if (string.IsNullOrEmpty(_referenceName))
                {
                    _referenceName = EntitiesHelper.GetForeignKeyNavigationName(Name);
                }
                return _referenceName;
            }
        }

    }
}
