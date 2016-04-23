using Scripts.Mythik.Gumps;
using Server;
using Server.Items;
using Server.Mobiles;
using Server.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scripts.Mythik.Items.Stones
{

    public class TravelStone : Item
    {
        private static int[] m_BasePrices = new int[] { 100, 100, 0 };

        private int m_Markup = 0;

        public int GuardedPrice { get { return m_BasePrices[0] + m_Markup; } }
        public int UnguardedPrice { get { return m_BasePrices[1] + m_Markup; } }
        public int NeutralPrice { get { return m_BasePrices[2] + m_Markup; } }

        [CommandProperty(AccessLevel.GameMaster)]
        public int Markup
        {
            get { return m_Markup; }
            set { m_Markup = value; }
        }

        [Constructable]
        public TravelStone() : base(0xED4)
        {
            Movable = false;
            Hue = 0x12D;
            Name = "a Travel Stone";
        }

        public TravelStone(Serial serial) : base(serial)
        {
        }

        public override void OnDoubleClick(Mobile from)
        {
            PlayerMobile pm = (PlayerMobile)from;
            if (pm.InRange(GetWorldLocation(), 2))
            {
                if (pm.CanBeginAction(this))
                {
                    pm.Frozen = true;
                    pm.SendGump(new TravelStoneGump(pm, this));
                }
                else
                {
                    pm.SendMessage("You are already being teleported elsewhere!");
                }
            }
            else
            {
                pm.SendLocalizedMessage(500446); // That is too far away.
            }
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0); // version 

            writer.Write((int)m_Markup);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();

            switch (version)
            {
                case 0:
                    {
                        m_Markup = reader.ReadInt();
                        break;
                    }
            }
        }
    }

    public class TravelTimer : Timer
    {
        private PlayerMobile m_Mobile;
        private Point3D m_Location;
        private int m_Price;

        private int m_Count = 5;

        public TravelTimer(PlayerMobile m, Point3D location, int price) : base(TimeSpan.Zero, TimeSpan.FromSeconds(1.0), 6)
        {
            m_Mobile = m;
            m_Location = location;
            m_Price = price;
        }

        protected override void OnTick()
        {
            if (m_Mobile.AccessLevel > AccessLevel.Player)
            {
                m_Count = 0;
                m_Price = 0;
            }

            if (m_Count == 0)
            {
                if (m_Price == 0 || m_Mobile.Backpack.ConsumeTotal(typeof(Gold), m_Price, true) || Banker.Withdraw(m_Mobile, m_Price))
                {
                    BaseCreature.TeleportPets(m_Mobile, m_Location, Map.Felucca, false);

                    m_Mobile.PlaySound(0x1FC);
                    m_Mobile.MoveToWorld(m_Location, Map.Felucca);
                    m_Mobile.PlaySound(0x1FC);
                }
                else
                {
                    m_Mobile.SendMessage(String.Format("You do not have {0} gold to pay for this service!", m_Price));
                }

                Stop();
                //m_Mobile.TravelTimer = null;
                m_Mobile.EndAction(this);
            }
            else
            {
                m_Mobile.PublicOverheadMessage(MessageType.Label, 0x481, true, m_Count.ToString(), true);
                m_Count--;
            }
        }
    }
}
