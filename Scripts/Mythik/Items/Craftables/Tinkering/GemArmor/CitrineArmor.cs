using Server;
using Server.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scripts.Mythik.Items.Craftables.Tinkering.GemArmor
{
   
    public class CitrinePlateChest : PlateChest, IItemSet
    {
        public BaseGearSet GetItemSet
        {
            get
            {
                return ItemSets.CitrineSet; ;
            }
        }

        [Constructable]
        public CitrinePlateChest() : base()
        {
            Name = "citrine platemail chest";
            Hue = GetItemSet.Hue;
            SkillBonuses.SetValues(0, GetItemSet.ItemBonus.Skill, GetItemSet.ItemBonus.Value);
        }
        public CitrinePlateChest(Serial serial) : base(serial)
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

    public class CitrinePlateLegs : PlateLegs, IItemSet
    {
        public BaseGearSet GetItemSet
        {
            get
            {
                return ItemSets.CitrineSet; ;
            }
        }

        [Constructable]
        public CitrinePlateLegs() : base()
        {
            Name = "citrine platemail legs";
            Hue = GetItemSet.Hue;
            SkillBonuses.SetValues(0, GetItemSet.ItemBonus.Skill, GetItemSet.ItemBonus.Value);
        }
        public CitrinePlateLegs(Serial serial) : base(serial)
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

    public class CitrinePlateHelm : PlateHelm, IItemSet
    {
        public BaseGearSet GetItemSet
        {
            get
            {
                return ItemSets.CitrineSet; ;
            }
        }

        [Constructable]
        public CitrinePlateHelm() : base()
        {
            Name = "citrine platemail helm";
            Hue = GetItemSet.Hue;
            SkillBonuses.SetValues(0, GetItemSet.ItemBonus.Skill, GetItemSet.ItemBonus.Value);
        }
        public CitrinePlateHelm(Serial serial) : base(serial)
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

    public class CitrinePlateGorgot : PlateGorget, IItemSet
    {
        public BaseGearSet GetItemSet
        {
            get
            {
                return ItemSets.CitrineSet; ;
            }
        }

        [Constructable]
        public CitrinePlateGorgot() : base()
        {
            Name = "citrine platemail gorgot";
            Hue = GetItemSet.Hue;
            SkillBonuses.SetValues(0, GetItemSet.ItemBonus.Skill, GetItemSet.ItemBonus.Value);
        }
        public CitrinePlateGorgot(Serial serial) : base(serial)
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
    public class CitrinePlateArms : PlateArms, IItemSet
    {
        public BaseGearSet GetItemSet
        {
            get
            {
                return ItemSets.CitrineSet; ;
            }
        }

        [Constructable]
        public CitrinePlateArms() : base()
        {
            Name = "citrine platemail arms";
            Hue = GetItemSet.Hue;
            SkillBonuses.SetValues(0, GetItemSet.ItemBonus.Skill, GetItemSet.ItemBonus.Value);
        }
        public CitrinePlateArms(Serial serial) : base(serial)
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
    public class CitrinePlateGloves : PlateGloves, IItemSet
    {
        public BaseGearSet GetItemSet
        {
            get
            {
                return ItemSets.CitrineSet; ;
            }
        }

        [Constructable]
        public CitrinePlateGloves() : base()
        {
            Name = "citrine platemail gloves";
            Hue = GetItemSet.Hue;
            SkillBonuses.SetValues(0, GetItemSet.ItemBonus.Skill, GetItemSet.ItemBonus.Value);
        }
        public CitrinePlateGloves(Serial serial) : base(serial)
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

    public class CitrineHeaterShield : HeaterShield, IItemSet
    {
        public BaseGearSet GetItemSet
        {
            get
            {
                return ItemSets.CitrineSet; ;
            }
        }

        [Constructable]
        public CitrineHeaterShield() : base()
        {
            Name = "citrine heater shield";
            Hue = GetItemSet.Hue;
            SkillBonuses.SetValues(0, GetItemSet.ItemBonus.Skill, GetItemSet.ItemBonus.Value);
        }
        public CitrineHeaterShield(Serial serial) : base(serial)
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
