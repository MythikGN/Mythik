﻿using Server;
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
            Hue = GenerateHue(mod);
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
        public static int GenerateHue(SkillMod mod)
        {
            var name = 0;
            if (mod.Value <= 1.0)
                name = 0x1b5;
            else if (mod.Value <= 2.0)
                name = 0x2a1;
            else if (mod.Value <= 3.0)
                name = 0x539;
            else if (mod.Value <= 4.0)
                name = 0x814;
            else if (mod.Value <= 5.0)
                name = 0x819;
            return name;
        }

        public static SkillMod GenerateBonus()
        {
            SkillName skill = (SkillName)Utility.Random(0, 48);
            var bonus = Utility.Random(0, 300) / 100.0;

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
        [Constructable]
        public SkillBracelet()
        {
            var mod = SkillRing.GenerateBonus();
            Name = SkillRing.GenerateName(mod, "bracelet");
            Hue = SkillRing.GenerateHue(mod);
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
  /*  public class SkillNecklace : GoldNecklace, IUniqueItem
    {
        public RareLevel UniqueLevel
        {
            get
            {
                return RareLevel.Rare;
            }
        }
        [Constructable]
        public SkillNecklace()
        {
            var mod = SkillRing.GenerateBonus();
            Name = SkillRing.GenerateName(mod, "necklace");
            Hue = SkillRing.GenerateHue(mod);
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
    }*/

    public class SkillEarrings : GoldEarrings, IUniqueItem
    {
        public RareLevel UniqueLevel
        {
            get
            {
                return RareLevel.Rare;
            }
        }
        [Constructable]
        public SkillEarrings()
        {
            var mod = SkillRing.GenerateBonus();
            Name = SkillRing.GenerateName(mod, "earrings");
            Hue = SkillRing.GenerateHue(mod);
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