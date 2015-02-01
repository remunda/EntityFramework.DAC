using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityFramework.DAC.Entities
{
    public class EntityInfo
    {
        public string FQN { get; set; }

        public string Name { get; set; }

        public string SchemaName { get; set; }

        public List<EntityPropertyInfo> Properties { get; set; }


        public EntityInfo()
        {
            Properties = new List<EntityPropertyInfo>(); 
        }

        public string GetPluralName()
        {
            return Name.GetPlural();
        }

        public string GetSingularName()
        {
            return Name.GetSingular();
        }
    }
}
