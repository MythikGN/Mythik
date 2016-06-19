using Server;
using Server.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scripts.Mythik.Items.Misc
{
    public class RareArmorDyes : Item
    {
        public int UsesRemaining { get; set; }

        [Constructable]
        public RareArmorDyes() :base(0xEFF)
        {
            this.Hue = Utility.RandomList(MythikStaticValues.RareClothHues);
            UsesRemaining = 10;
        }
        public RareArmorDyes(Serial serial) : base(serial)
        {

        }
        public override void OnDoubleClick(Mobile from)
        {
            from.BeginTarget(1, false, Server.Targeting.TargetFlags.None, OnItemTargeted);
        }

        private void OnItemTargeted(Mobile from, object targeted)
        {
            var item = targeted as BaseArmor;
            if (item == null)
                from.SendLocalizedMessage(1042417); // You cannot dye that.
            else if (!from.InRange(item.GetWorldLocation(), 3) || !IsAccessibleTo(from))
                from.SendLocalizedMessage(502436); // That is not accessible.
            else if (CraftResources.GetType(item.Resource) == CraftResourceType.Metal)
            {
                item.Hue = Hue;

                if (--UsesRemaining <= 0)
                    Delete();

                from.PlaySound(0x23E);
            }
            else
            {
                from.SendLocalizedMessage(1042417); // You cannot dye that.
            }
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write((int)1);//ver
            writer.Write(UsesRemaining);
        }
        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            reader.ReadInt();
            UsesRemaining = reader.ReadInt();
        }
    }
}
