using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityFramework.DAC.Entities
{
    public class EntityPropertyInfo
    {
        public EntityPropertyInfo()
        {
            ForeignKeys = new List<ForeignKeyInfo>();
        }


        public string Name { get; set; }

        public string ClrType { get; set; }

        public bool IsNullable { get; set; }

        public bool IsKey { get; set; }

        public bool IsForeignKey { get; set; }

        public List<ForeignKeyInfo> ForeignKeys { get; set; }

        public bool IsIdentity { get; set; }

        public bool IsComputed { get; set; }

        public string DbType { get; set; }
    }
}
