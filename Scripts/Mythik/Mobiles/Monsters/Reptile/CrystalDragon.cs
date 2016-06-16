using Server;
using Server.Mobiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scripts.Mythik.Mobiles.Monsters.Reptile
{
    [CorpseName("a dragon corpse")]
    public class CrystalDragon : BaseCreature
    {
        [Constructable]
        public CrystalDragon() : base(AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            Name = "a crystal dragon";
            Body = Utility.RandomList(12, 59);
            BaseSoundID = 362;

            SetStr(896, 925);
            SetDex(86, 105);
            SetInt(436, 475);

            SetHits(744,855);

            SetDamage(18, 30);

            SetDamageType(ResistanceType.Physical, 100);

            SetResistance(ResistanceType.Physical, 55, 65);
            SetResistance(ResistanceType.Fire, 60, 70);
            SetResistance(ResistanceType.Cold, 30, 40);
            SetResistance(ResistanceType.Poison, 25, 35);
            SetResistance(ResistanceType.Energy, 35, 45);

            SetSkill(SkillName.EvalInt, 40.1, 60.0);
            SetSkill(SkillName.Magery, 40.1, 60.0);
            SetSkill(SkillName.MagicResist, 99.1, 100.0);
            SetSkill(SkillName.Tactics, 97.6, 100.0);
            SetSkill(SkillName.Wrestling, 95.1, 99.5);

            Fame = 17000;
            Karma = -17000;

            VirtualArmor = 60;

            Tamable = true;
            ControlSlots = 4;
            MinTameSkill = 149.9;
        }

        public override void GenerateLoot()
        {
            AddLoot(LootPack.FilthyRich, 4);
            AddLoot(LootPack.Gems, 8);
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

        public CrystalDragon(Serial serial) : base(serial)
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
