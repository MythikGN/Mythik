using Server;
using Server.Mobiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scripts.Mythik.Mobiles.Monsters.Reptile
{
    [CorpseName("a shadow wyrm corpse")]
    public class ShadowWyrm : BaseCreature
    {
        [Constructable]
        public ShadowWyrm() : base(AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            Name = "a shadow wyrm";
            Body = Utility.RandomList(12, 59);
            BaseSoundID = 362;

            SetStr(1001, 1410);
            SetDex(171, 270);
            SetInt(301, 325);

            SetHits(801, 1100);
            SetMana(60);

            SetDamage(20, 30);

            SetDamageType(ResistanceType.Physical, 100);

            SetResistance(ResistanceType.Physical, 55, 65);
            SetResistance(ResistanceType.Fire, 50, 60);
            SetResistance(ResistanceType.Cold, 30, 40);
            SetResistance(ResistanceType.Poison, 25, 35);
            SetResistance(ResistanceType.Energy, 45, 55);

            SetSkill(SkillName.Anatomy, 75.1, 80.0);
            SetSkill(SkillName.MagicResist, 85.1, 100.0);
            SetSkill(SkillName.Tactics, 100.1, 110.0);
            SetSkill(SkillName.Wrestling, 100.1, 120.0);

            Fame = 17000;
            Karma = -17000;

            VirtualArmor = 60;

            Tamable = true;
            ControlSlots = 4;
            MinTameSkill = 169.9;
        }

    

        public override bool ReacquireOnMovement { get { return !Controlled; } }
        public override bool HasBreath { get { return true; } } // fire breath enabled
        public override bool AutoDispel { get { return !Controlled; } }
        public override int TreasureMapLevel { get { return 5; } }
        public override int Meat { get { return 19; } }
        public override int Hides { get { return 20; } }
        public override HideType HideType { get { return HideType.Barbed; } }
        public override int Scales { get { return 7; } }
        public override ScaleType ScaleType { get { return (Body == 12 ? ScaleType.Yellow : ScaleType.Red); } }
        public override FoodType FavoriteFood { get { return FoodType.Meat; } }
        public override bool CanAngerOnTame { get { return true; } }
        public override bool CanFly { get { return true; } }

        public ShadowWyrm(Serial serial) : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write((int)0);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();
        }
    }
}
