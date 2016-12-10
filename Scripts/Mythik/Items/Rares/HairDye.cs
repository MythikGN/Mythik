using Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scripts.Mythik.Items.Rares
{
    class RareHairDye : Item
    {
        [Constructable]
        public RareHairDye(int hue) : base(0xEFF)
        {
            if (MythikStaticValues.RareClothHues.Contains(hue))
                Name = "Rare Hair Dye";
            else
                Name = "Hair Dye";
            Hue = hue;
        }
        [Constructable]
        public RareHairDye() : base(0xEFF)
        {
            Name = "Rare Hair Dye";
            Hue = MythikStaticValues.GetRandomRareSkinDyeHue();
        }

        public override void OnDoubleClick(Mobile from)
        {
            from.HairHue = this.Hue;
            from.FacialHairHue = this.Hue;
            this.Consume();
        }
        [Constructable]
        public RareHairDye(Serial serial) : base(serial)
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
