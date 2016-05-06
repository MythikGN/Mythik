using Server;
using Server.Mobiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scripts.Mythik.Mobiles.Mounts
{
    [CorpseName("a mustang corpse")]
    public class Mustang : BaseMount
    {
        private static int[] m_Hues = new int[]
            {
                0x455, 0x1B6,
                0x31C, 0x158,
                0x033, 0x263,
                0x279, 0x1BB,
                0x3E7
            };

        [Constructable]
        public Mustang() : this("a mustang")
        {
        }

        [Constructable]
        public Mustang(string name) : base(name, 0xE2, 0x3EA0, AIType.AI_Animal, FightMode.Aggressor, 10, 1, 0.2, 0.4)
        {
            int random = Utility.Random(9);

            Hue = m_Hues[random];

            BaseSoundID = 0xA8;


            SetStr(40, 102);
            SetDex(62, 81);
            SetInt(8, 13);

            SetHits(50, 90);
            SetMana(0);

            SetDamage(4, 5);

            SetDamageType(ResistanceType.Physical, 100);

            SetSkill(SkillName.MagicResist, 30.1, 40.0);
            SetSkill(SkillName.Tactics, 35.7, 49.0);
            SetSkill(SkillName.Wrestling, 35.5, 46.2);

            Fame = 400;
            Karma = 400;

            Tamable = true;
            ControlSlots = 1;
            MinTameSkill = 90.1;
        }

        public override int Meat { get { return 4; } }
        public override int Hides { get { return 15; } }
        public override FoodType FavoriteFood { get { return FoodType.FruitsAndVegies | FoodType.GrainsAndHay; } }

        public Mustang(Serial serial) : base(serial)
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
