using Server.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Server;

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
        [Constructable]
        public TamingShirt()
        {
            Hue = 0xaa;
            Name = "little bo-beeps shirt";
            SkillBonuses.SetValues(0, Server.SkillName.AnimalTaming, 4.0);
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write((int)1);
        }
        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            var version = reader.ReadInt();
        }


    }
}
