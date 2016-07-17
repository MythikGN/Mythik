using Scripts.Mythik.Systems;
using Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scripts.Mythik.Items.Craftables
{
    public class CreatureGuideBook : Item
    {
        [Constructable]
        public CreatureGuideBook() :base(0x2254)
        {
            Weight = 0.5;
            Hue = 0x788;
            Name = "Creature Handbook";
            
        }
        public override bool CheckNewbied()
        {
            return true;
        }
        public override void OnDoubleClick(Mobile from)
        {
            base.OnDoubleClick(from);
            from.SendGump(new CreatureListGump(CreatureListGump.Buttons.None));
        }

        public CreatureGuideBook(Serial serial): base ( serial )
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
