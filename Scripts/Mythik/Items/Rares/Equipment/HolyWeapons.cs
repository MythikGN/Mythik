using Server;
using Server.Gumps;
using Server.Items;
using Server.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scripts.Mythik.Items.Rares.Equipment
{
    public class EquipInfoGump : Gump
    {

        public EquipInfoGump(Item item) : base(605,10)
        {
            this.Closable = true;
            this.Disposable = true;
            this.Dragable = true;
            this.Resizable = false;
            this.AddPage(0);
            this.AddBackground(82, 78, 136, 246, 9200);
            this.AddBackground(89, 87, 122, 229, 9200);
            this.AddAlphaRegion(80, 77, 141, 250);
            var text = @"<CENTER>";
            int y = 80;
            if (string.IsNullOrWhiteSpace(item.Name))
                AddHtmlLocalized(98, y += 17, 100, 15, item.LabelNumber, false, false);
            else
                AddHtml(98, y += 17, 100, 15, item.Name, false, false);
            foreach (var prop in item.PropertyList.Props)
            {
                if (prop.Item2 == null)
                    AddHtmlLocalized(98, y += 17, 100, 15, prop.Item1, false, false);
                else
                    AddHtmlLocalized(98, y+=17, 100, 15, prop.Item1,prop.Item2,0,false,false);

            }
            //this.AddHtml(90, 87, 119, 231, text, (bool)false, (bool)false);

        }
    }

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
        public override void OnSingleClick(Mobile from)
        {
            base.OnSingleClick(from);
            if(from.NetState.Version.Major <=3)
            {
                from.SendGump(new EquipInfoGump(this));
            }
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
            Hue = 0x79c;
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
