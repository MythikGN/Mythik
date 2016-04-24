using Scripts.Mythik.Gumps;
using Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scripts.Mythik.Items.Stones
{
    public class ItemStone : Item
    {
        private int m_Markup = 0;
        private double m_Tax = 0.0;

        [CommandProperty(AccessLevel.GameMaster)]
        public int Markup
        {
            get { return m_Markup; }
            set { m_Markup = value; }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public double Tax
        {
            get { return m_Tax; }
            set { m_Tax = value; }
        }

        [Constructable]
        public ItemStone() : this(0, 0.0)
        {
        }

        [Constructable]
        public ItemStone(int markup, double tax) : base(0xED4)
        {
            Movable = false;
            Hue = 0x033;
            Name = "a Item Stone";

            m_Markup = markup;
            m_Tax = tax;
        }

        public ItemStone(Serial serial) : base(serial)
        {
        }

        public override void OnDoubleClick(Mobile from)
        {
            if (from.InRange(GetWorldLocation(), 2))
            {
                from.Frozen = true;
                from.SendGump(new ItemStoneGump(from, this));
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

            writer.Write((int)m_Markup);
            writer.Write((double)m_Tax);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();

            m_Markup = reader.ReadInt();
            m_Tax = reader.ReadDouble();
        }
    }
}
