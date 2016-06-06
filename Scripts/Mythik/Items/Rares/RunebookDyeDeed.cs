using Server;
using Server.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scripts.Mythik.Items.Rares
{
  
    public class RunebookDyeDeed : Item , IUniqueItem
    {
        public RareLevel UniqueLevel
        {
            get
            {
                return RareLevel.Rare;
            }
        }
        [Constructable]
        public RunebookDyeDeed() : base(0xFF4)
        {
            Name = "Runebook Dye Deed";
            this.Hue = MythikStaticValues.RareClothHues[Utility.Random(0,MythikStaticValues.RareClothHues.Length-1)];
        }

        [Constructable]
        public RunebookDyeDeed(int hue) : base(0xFF4)
        {
            Name = "Runebook Dye Deed";
            this.Hue = hue;
        }
        [Constructable]
        public RunebookDyeDeed(Serial serial) : base(serial)
        {

        }
        public override void OnDoubleClick(Mobile from)
        {
            from.SendAsciiMessage("Select a Runebook to dye.");
            from.BeginTarget(1, false, Server.Targeting.TargetFlags.None, OnTarget);
        }

        private void OnTarget(Mobile from, object targeted)
        {
            if (targeted == null)
                return;
            var rb = targeted as Runebook;
            if (rb == null)
            {
                from.SendAsciiMessage("That is not a Runebook.");
                return;
            }

            rb.Hue = this.Hue;
            from.SendAsciiMessage("You dye the Runebook.");
            this.Consume();
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
