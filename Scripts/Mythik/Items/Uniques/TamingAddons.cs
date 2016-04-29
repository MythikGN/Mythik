using Server.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scripts.Mythik.Items.Uniques
{
    public class TamingShirt : Shirt, IUniqueItem
    {
        public int UniqueLevel
        {
            get
            {
                return 2;
            }
        }
        public TamingShirt()
        {
            Hue = 0xaa;
            Name = "little bo-beeps shirt";
            SkillBonuses.SetValues(0, Server.SkillName.AnimalTaming, 4.0);
        }

       
    }
}
