using Server;
using Server.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scripts.Mythik.Items.Uniques
{
    class PuritansSet
    {

        public class PuritanShirt : FancyShirt, IUniqueItem
        {
            public RareLevel UniqueLevel
            {
                get
                {
                    return RareLevel.Rare;
                }
            }
            [Constructable]
            public PuritanShirt()
            {

                Hue = 0x07AA;
                Name = "Puritan's fancy shirt";
                SkillBonuses.SetValues(0, Server.SkillName.Tinkering, 8.0);
            }
            public PuritanShirt(Serial serial) : base(serial)
        {

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
}
