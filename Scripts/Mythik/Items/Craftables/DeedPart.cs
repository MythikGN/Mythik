using Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scripts.Mythik.Items.Craftables
{
    public class DeedPart  : Item
    {
        [Constructable]
        public DeedPart() :base(0xFF4)
        {
            Weight = 0.5;
            Hue = 0x788;
            Name = "deed part";
        }

        public DeedPart(Serial serial): base ( serial )
		{
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0); // version 
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }
}
