using Scripts.Mythik.Gumps;
using Scripts.Mythik.Localizations;
using Server;
using Server.Mobiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scripts.Mythik.Items.Stones
{
    public class ResurrectionStone : Item
    {
        
        [Constructable]
        public ResurrectionStone() : base(0x1183)
        {
            Movable = false;
            Hue = 0x042;
            Name = "a Resurrection Stone";
        }

        public ResurrectionStone(Serial serial) : base(serial)
        {
        }

        public override void OnDoubleClick(Mobile from)
        {
            PlayerMobile pm = (PlayerMobile)from;
            //AgatePlayerMobile apm = (AgatePlayerMobile)pm;

            if (!pm.Alive)
            {
                if (pm.InRange(GetWorldLocation(), 2))
                {
                    if(pm.CanBeginAction(this))
                    {
                        pm.Frozen = true;
                        pm.SendGump(new ResurrectionStoneGump(from));
                    }
                    else
                    {
                        var timer = pm.GetAction<DeathTimer>();
                        pm.SendMessage(Locale.GetLocale(pm).RESSURRECTION_STONE_WAIT, (timer.Next - DateTime.Now).Seconds, (timer.Next - DateTime.Now).Seconds == 1 ? null : "s");
                    }
                }
                else
                {
                    pm.SendLocalizedMessage(500446); // That is too far away.
                }
            }
            else
            {
                pm.SendMessage(Locale.GetLocale(pm).RESSURRECTION_STONE_NOT_DEAD);
            }
        }

        public override void OnDoubleClickDead(Mobile from)
        {
            OnDoubleClick(from);
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
    public class DeathTimer : Timer
    {
        private PlayerMobile m_Owner;
        private static TimeSpan RessurrectionTimer = TimeSpan.FromSeconds(20);
        public DeathTimer(PlayerMobile owner) : base(RessurrectionTimer)
        {
            m_Owner = owner;
        }
        protected override void OnTick()
        {
            m_Owner.EndAction(this);
            base.OnTick();
        }
    }
}
