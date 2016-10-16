using Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scripts.Mythik.Items.Rares
{
    class SkinDye : Item
    {
        [Constructable]
        public SkinDye(int hue) : base(0x1847)
        {
            if (MythikStaticValues.RareClothHues.Contains(hue))
                Name = "Rare Skin Dye";
            else
                Name = "Common Skin Dye";
            Hue = hue;
        }
        [Constructable]
        public SkinDye() : base(0x1847)
        {
            Name = "Rare Skin Dye";
            Hue = MythikStaticValues.GetSkinDyeHue();
        }

        public override void OnDoubleClick(Mobile from)
        {
            from.Hue = this.Hue;
            this.Consume();
        }
        [Constructable]
        public SkinDye(Serial serial) : base(serial)
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
