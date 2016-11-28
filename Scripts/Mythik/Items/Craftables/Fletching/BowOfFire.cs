using Server;
using Server.Engines.Craft;
using Server.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scripts.Mythik.Items.Craftables.Fletching
{
    class BowOfFire : Bow, ICraftable
    {
        [Constructable]
        public BowOfFire() :base()
        {
            Hue = 0x898;
            Name = "Bow of Fire";
            Attributes.WeaponDamage = 40;
            WeaponAttributes.HitFireball = 20;

        }
        public new int OnCraft(int quality, bool makersMark, Mobile from, CraftSystem craftSystem, Type typeRes, BaseTool tool, CraftItem craftItem, int resHue)
        {
            Quality = (WeaponQuality)quality;

            if (makersMark)
                Crafter = from;

            PlayerConstructed = true;
            if (Quality == WeaponQuality.Exceptional)
            {
                Attributes.WeaponDamage = 50;
                WeaponAttributes.HitFireball = 30;
                from.CheckSkill(SkillName.ArmsLore, 0, 100);
            }
            return quality;
        }

        public override void GetDamageTypes(Mobile wielder, out int phys, out int fire, out int cold, out int pois, out int nrgy, out int chaos, out int direct)
        {
            fire = 100;
            cold = phys = pois = nrgy = chaos = direct = 0;
        }

        public BowOfFire(Serial serial): base ( serial )
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
