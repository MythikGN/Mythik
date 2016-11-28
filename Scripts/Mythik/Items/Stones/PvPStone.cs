using Scripts.Mythik.Gumps;
using Scripts.Mythik.Items.Craftables.Alch;
using Scripts.Mythik.Localizations;
using Server;
using Server.Items;
using Server.Mobiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scripts.Mythik.Items.Stones
{
    public class PvpStone : Item
    {
        
        [Constructable]
        public PvpStone() : base(0x1183)
        {
            Movable = false;
            Hue = 0x033;
            Name = "a PvP Stone";
        }

        public PvpStone(Serial serial) : base(serial)
        {
        }

        public override void OnDoubleClick(Mobile from)
        {
            PlayerMobile pm = (PlayerMobile)from;
            var reags = new BagOfReagents(50);
            reags.AddItem(new TotalManaPotion() { Amount = 25 });
            reags.AddItem(new GreaterHealPotion() { Amount = 25 });
            reags.AddItem(new InvisibilityPotion() { Amount = 25 });
            reags.AddItem(new GreaterCurePotion() { Amount = 10 });
            reags.AddItem(new GreaterRestorePotion() { Amount = 25 });
            pm.AddToBackpack(reags);
            pm.SendMessage("A bag of PvP supplies has been added to your backpack.");
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
