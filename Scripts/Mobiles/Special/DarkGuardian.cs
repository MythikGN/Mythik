using System;
using Server;
using Server.Items;

namespace Server.Mobiles
{
    [CorpseName("a dark guardian corpse")]
    public class DarkGuardian : BaseCreature
    {
        [Constructable]
        public DarkGuardian() : base(AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            Name = "a dark guardian";
            Body = 79;
            BaseSoundID = 412;

            Hue = 0x455;

            SetStr(126, 150);
            SetDex(101, 120);
            SetInt(201, 235);

            SetHits(153, 179);

            SetDamage(24, 26);

            SetDamageType(ResistanceType.Physical, 10);
            SetDamageType(ResistanceType.Cold, 40);
            SetDamageType(ResistanceType.Energy, 50);

            SetResistance(ResistanceType.Physical, 40, 50);
            SetResistance(ResistanceType.Fire, 20, 45);
            SetResistance(ResistanceType.Cold, 50, 60);
            SetResistance(ResistanceType.Poison, 20, 45);
            SetResistance(ResistanceType.Energy, 30, 40);

            SetSkill(SkillName.Meditation, 85.1, 95.0);
            SetSkill(SkillName.EvalInt, 40.1, 50.0);
            SetSkill(SkillName.Magery, 50.1, 60.0);
            SetSkill(SkillName.MagicResist, 50.1, 70.0);
            SetSkill(SkillName.Tactics, 50.1, 70.0);

            Fame = 8000;
            Karma = -8000;

            VirtualArmor = 50;
            PackItem(new GnarledStaff());
            PackItem(new DaemonBone(30));
            PackNecroReg(15, 25);
        }

        public override OppositionGroup OppositionGroup
        {
            get { return OppositionGroup.FeyAndUndead; }
        }

        public override void GenerateLoot()
        {
            AddLoot(LootPack.Rich);
            AddLoot(LootPack.Average);
            AddLoot(LootPack.MedScrolls, 2);
        }

        public override void OnDeath(Container c)
        {
            base.OnDeath(c);

            switch (Utility.Random(6))
            {
                case 0: c.DropItem(new TreasureMap(Utility.RandomMinMax(1, 3), Map.Felucca)); break;
                case 1: c.DropItem(new TreasureMap(Utility.RandomMinMax(1, 3), Map.Trammel)); break;
                case 2: c.DropItem(new TreasureMap(Utility.RandomMinMax(1, 3), Map.Ilshenar)); break;
                case 3: c.DropItem(new TreasureMap(Utility.RandomMinMax(1, 3), Map.Malas)); break;
                case 4: c.DropItem(new TreasureMap(Utility.RandomMinMax(1, 3), Map.Tokuno)); break;
                case 5: c.DropItem(new TreasureMap(Utility.RandomMinMax(1, 3), Map.TerMur)); break;

            }
        }

        public override bool CanRummageCorpses { get { return true; } }
        public override bool BleedImmune { get { return true; } }
        public override Poison PoisonImmune { get { return Poison.Lethal; } }

        public override int GetIdleSound()
        {
            return 413;
        }

        public override int GetAngerSound()
        {
            return 412;
        }

        public override int GetDeathSound()
        {
            return 416;
        }

        public override int GetAttackSound()
        {
            return 414;
        }

        public override int GetHurtSound()
        {
            return 415;
        }

        public DarkGuardian(Serial serial) : base(serial)
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

