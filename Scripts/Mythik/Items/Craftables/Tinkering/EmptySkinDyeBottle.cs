using Scripts.Mythik.Items.Rares;
using Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scripts.Mythik.Items.Craftables.Tinkering
{
    class EmptySkinDyeBottle : Item
    {
        [Constructable]
        public EmptySkinDyeBottle() : base(0x1847)
        {
            Name = "empty skin dye bottle";
        }

        public override void OnDoubleClick(Mobile from)
        {
            if(from.Hue != 0)
            {
                var newItem = new SkinDye(from.Hue);
                from.AddToBackpack(newItem);
                this.Consume();
                from.Hue = Utility.RandomSkinHue();
            }
        }

        [Constructable]
        public EmptySkinDyeBottle(Serial serial) : base(serial)
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
