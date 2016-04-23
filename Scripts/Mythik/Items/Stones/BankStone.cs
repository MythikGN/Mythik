using Scripts.Mythik.Gumps;
using Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scripts.Mythik.Items.Stones
{
    public class BankStone : Item
    {
        [Constructable]
        public BankStone() : base(0xED4)
        {
            Movable = false;
            Hue = 0x029;
            Name = "a Bank Stone";
        }

        public BankStone(Serial serial) : base(serial)
        {
        }

        public override void OnDoubleClick(Mobile from)
        {
            if (from.InRange(GetWorldLocation(), 2))
            {
                from.Frozen = true;
                from.SendGump(new BankStoneGump(from));
            }
            else
            {
                from.SendLocalizedMessage(500446); // That is too far away.
            }
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
