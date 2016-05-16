using Server;
using Server.Items;
using Server.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scripts.Mythik.Items.Rares.Equipment
{
    public class HolyWarFork : WarFork , IUniqueItem
    {
        public RareLevel UniqueLevel
        {
            get
            {
                return RareLevel.UniqueLvl3;
            }
        }

        [Constructable]
        public HolyWarFork() : base()
        {
            Name = "Holy Warfork";
            Hue = 0x481;
        }

        public override void OnHit(Mobile attacker, Mobile defender, double damageBonus)
        {
            if (defender.Kills > 3 && Utility.Random(5) == 0)
            { //1 in 5 chance
                attacker.LocalOverheadMessage(MessageType.Emote, 0x481, true, "The holy power strikes!");
                defender.Hits -= Utility.Random(10, 15);
            }
            base.OnHit(attacker, defender, damageBonus);
        }

        public override bool OnEquip(Mobile from)
        {
            if (from.Karma < 0 || from.Kills > 3)
            {
                from.SendAsciiMessage("This sword is far too pure for your soul.");
                return false;
            }
            return base.OnEquip(from);
        }
        public HolyWarFork(Serial serial) : base(serial)
        {

        }
        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
        }
        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
        }
    }
    public class UnHolyWarFork : WarFork, IUniqueItem
    {
        public RareLevel UniqueLevel
        {
            get
            {
                return RareLevel.UniqueLvl3;
            }
        }

        [Constructable]
        public UnHolyWarFork() : base()
        {
            Name = "Unholy Warfork";
            Hue = 0x481;
        }

        public override void OnHit(Mobile attacker, Mobile defender, double damageBonus)
        {
            if (defender.Kills < 3 && Utility.Random(5) == 0)
            { //1 in 5 chance
                attacker.LocalOverheadMessage(MessageType.Emote, 0x481, true, "The evil power strikes!");
                defender.Hits -= Utility.Random(10, 15);
            }
            base.OnHit(attacker, defender, damageBonus);
        }

        public override bool OnEquip(Mobile from)
        {
            if (from.Kills <= 3)
            {
                from.SendAsciiMessage("This sword is far too evil for your soul.");
                return false;
            }
            return base.OnEquip(from);
        }
        public UnHolyWarFork(Serial serial) : base(serial)
        {

        }
        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
        }
        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
        }
    }
}
