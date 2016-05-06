using Server;
using Server.Mobiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scripts.Mythik.Mobiles.Monsters.Reptile
{
    [CorpseName("a drake corpse")]
    public class CrystalDrake : BaseCreature
    {
              [Constructable]
        public CrystalDrake() : base(AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            Body = Utility.RandomList(60, 61);
            Hue = 0x7FFF;
            Name = "a crystal drake";
            BaseSoundID = 362;

            SetStr(601, 630);
            SetDex(100, 125);
            SetInt(101, 140);

            SetHits(441, 458);

            SetDamage(16, 22);

            SetDamageType(ResistanceType.Physical, 80);
            SetDamageType(ResistanceType.Fire, 20);

            SetResistance(ResistanceType.Physical, 45, 50);
            SetResistance(ResistanceType.Fire, 50, 60);
            SetResistance(ResistanceType.Cold, 40, 50);
            SetResistance(ResistanceType.Poison, 20, 30);
            SetResistance(ResistanceType.Energy, 30, 40);

            SetSkill(SkillName.MagicResist, 75.1, 90.0);
            SetSkill(SkillName.Tactics, 75.1, 90.0);
            SetSkill(SkillName.Wrestling, 75.1, 90.0);

            Fame = 9500;
            Karma = -9500;

            VirtualArmor = 46;

            Tamable = true;
            ControlSlots = 2;
            MinTameSkill = 114.9;

            PackReg(3);
        }

        public override void GenerateLoot()
        {
            AddLoot(LootPack.Rich);
            AddLoot(LootPack.FilthyRich);
            AddLoot(LootPack.MedScrolls, 2);
        }

        public override bool ReacquireOnMovement { get { return true; } }
        public override bool HasBreath { get { return true; } } // fire breath enabled
        public override int TreasureMapLevel { get { return 4; } }
        public override int Meat { get { return 10; } }
        public override int Hides { get { return 20; } }
        public override HideType HideType { get { return HideType.Horned; } }
        public override int Scales { get { return 2; } }
        public override ScaleType ScaleType { get { return (Body == 60 ? ScaleType.Yellow : ScaleType.Red); } }
        public override FoodType FavoriteFood { get { return FoodType.Meat | FoodType.Fish; } }
        public override bool CanFly { get { return true; } }

        public CrystalDrake(Serial serial) : base(serial)
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
