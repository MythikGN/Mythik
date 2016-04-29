using Server;
using Server.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scripts.Mythik.Items.Craftables.Tinkering.GemArmor
{
   
    public class SapphirePlateChest : PlateChest, IItemSet
    {
        public BaseGearSet GetItemSet
        {
            get
            {
                return ItemSets.SapphireSet; ;
            }
        }

        [Constructable]
        public SapphirePlateChest() : base()
        {
            Name = "sapphire platemail chest";
            Hue = GetItemSet.Hue;
            SkillBonuses.SetValues(0, GetItemSet.ItemBonus.Skill, GetItemSet.ItemBonus.Value);
        }
        public SapphirePlateChest(Serial serial) : base(serial)
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

    public class SapphirePlateLegs : PlateLegs, IItemSet
    {
        public BaseGearSet GetItemSet
        {
            get
            {
                return ItemSets.SapphireSet; ;
            }
        }

        [Constructable]
        public SapphirePlateLegs() : base()
        {
            Name = "sapphire platemail legs";
            Hue = GetItemSet.Hue;
            SkillBonuses.SetValues(0, GetItemSet.ItemBonus.Skill, GetItemSet.ItemBonus.Value);
        }
        public SapphirePlateLegs(Serial serial) : base(serial)
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

    public class SapphirePlateHelm : PlateHelm, IItemSet
    {
        public BaseGearSet GetItemSet
        {
            get
            {
                return ItemSets.SapphireSet; ;
            }
        }

        [Constructable]
        public SapphirePlateHelm() : base()
        {
            Name = "sapphire platemail helm";
            Hue = GetItemSet.Hue;
            SkillBonuses.SetValues(0, GetItemSet.ItemBonus.Skill, GetItemSet.ItemBonus.Value);
        }
        public SapphirePlateHelm(Serial serial) : base(serial)
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

    public class SapphirePlateGorgot : PlateGorget, IItemSet
    {
        public BaseGearSet GetItemSet
        {
            get
            {
                return ItemSets.SapphireSet; ;
            }
        }

        [Constructable]
        public SapphirePlateGorgot() : base()
        {
            Name = "sapphire platemail gorgot";
            Hue = GetItemSet.Hue;
            SkillBonuses.SetValues(0, GetItemSet.ItemBonus.Skill, GetItemSet.ItemBonus.Value);
        }
        public SapphirePlateGorgot(Serial serial) : base(serial)
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
    public class SapphirePlateArms : PlateArms, IItemSet
    {
        public BaseGearSet GetItemSet
        {
            get
            {
                return ItemSets.SapphireSet; ;
            }
        }

        [Constructable]
        public SapphirePlateArms() : base()
        {
            Name = "sapphire platemail arms";
            Hue = GetItemSet.Hue;
            SkillBonuses.SetValues(0, GetItemSet.ItemBonus.Skill, GetItemSet.ItemBonus.Value);
        }
        public SapphirePlateArms(Serial serial) : base(serial)
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
    public class SapphirePlateGloves : PlateGloves, IItemSet
    {
        public BaseGearSet GetItemSet
        {
            get
            {
                return ItemSets.SapphireSet; ;
            }
        }

        [Constructable]
        public SapphirePlateGloves() : base()
        {
            Name = "sapphire platemail gloves";
            Hue = GetItemSet.Hue;
            SkillBonuses.SetValues(0, GetItemSet.ItemBonus.Skill, GetItemSet.ItemBonus.Value);
        }
        public SapphirePlateGloves(Serial serial) : base(serial)
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

    public class SapphireHeaterShield : HeaterShield, IItemSet
    {
        public BaseGearSet GetItemSet
        {
            get
            {
                return ItemSets.SapphireSet; ;
            }
        }

        [Constructable]
        public SapphireHeaterShield() : base()
        {
            Name = "sapphire heater shield";
            Hue = GetItemSet.Hue;
            SkillBonuses.SetValues(0, GetItemSet.ItemBonus.Skill, GetItemSet.ItemBonus.Value);
        }
        public SapphireHeaterShield(Serial serial) : base(serial)
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
