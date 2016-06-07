using Server;
using Server.Gumps;
using Server.Items;
using Server.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scripts.Mythik.Items.Rares.Equipment
{
    public class SkillRing : GoldRing, IUniqueItem
    {
        public RareLevel UniqueLevel
        {
            get
            {
                return RareLevel.Rare;
            }
        }

		[Constructable]
		public SkillRing()
        {
            var mod = GenerateBonus();
            Name = GenerateName(mod,"ring");
            SkillBonuses.SetValues(0, mod.Skill, mod.Value);
        }
		public SkillRing(Serial serial) : base(serial)
        {

        }
        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
        }
        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
        }

        public static string GenerateName(SkillMod mod, string item)
        {
            var name = "";
            if (mod.Value <= 1.0)
                name = "Lighter ";
            else if (mod.Value <= 2.0)
                name = "Light ";
            else if (mod.Value <= 3.0)
                name = "";
            else if (mod.Value <= 4.0)
                name = "Great ";
            else if (mod.Value <= 5.0)
                name = "Greater ";
            name += item + " of " + CliLoc.LocToString(1042347 +  (int)mod.Skill);
            return name;

        }

        public static SkillMod GenerateBonus()
        {
            SkillName skill = (SkillName)Utility.Random(0, 48);
            var bonus = Utility.Random(0, 500) / 100.0;

            return new DefaultSkillMod(skill, true, bonus);
        }
    }

    public class SkillBracelet : GoldBracelet, IUniqueItem
    {
        public RareLevel UniqueLevel
        {
            get
            {
                return RareLevel.Rare;
            }
        }

        public SkillBracelet()
        {
            var mod = SkillRing.GenerateBonus();
            Name = SkillRing.GenerateName(mod, "bracelet");
            SkillBonuses.SetValues(0, mod.Skill, mod.Value);
        }
        public SkillBracelet(Serial serial) : base(serial)
        {

        }
        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
        }
        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
        }
    }
    public class SkillNecklace : GoldNecklace, IUniqueItem
    {
        public RareLevel UniqueLevel
        {
            get
            {
                return RareLevel.Rare;
            }
        }

        public SkillNecklace()
        {
            var mod = SkillRing.GenerateBonus();
            Name = SkillRing.GenerateName(mod, "necklace");
            SkillBonuses.SetValues(0, mod.Skill, mod.Value);
        }
        public SkillNecklace(Serial serial) : base(serial)
        {

        }
        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
        }
        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
        }
    }

    public class SkillEarrings : GoldEarrings, IUniqueItem
    {
        public RareLevel UniqueLevel
        {
            get
            {
                return RareLevel.Rare;
            }
        }

        public SkillEarrings()
        {
            var mod = SkillRing.GenerateBonus();
            Name = SkillRing.GenerateName(mod, "earrings");
            SkillBonuses.SetValues(0, mod.Skill, mod.Value);
        }
        public SkillEarrings(Serial serial) : base(serial)
        {

        }
        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
        }
        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
        }
    }
}