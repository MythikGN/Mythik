using Server;
using Server.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scripts.Mythik.Items.Craftables.Tinkering.GemArmor
{
   
    public class PearlPlateChest : PlateChest, IItemSet
    {
        public BaseGearSet GetItemSet
        {
            get
            {
                return ItemSets.PearlSet; ;
            }
        }

        [Constructable]
        public PearlPlateChest() : base()
        {
            Name = "pearl platemail chest";
            Hue = GetItemSet.Hue;
            int i = 0; GetItemSet.ItemBonus.ForEach( (e) => { SkillBonuses.SetValues(i++, e.Skill, e.Value); });
        }
        public PearlPlateChest(Serial serial) : base(serial)
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

    public class PearlPlateLegs : PlateLegs, IItemSet
    {
        public BaseGearSet GetItemSet
        {
            get
            {
                return ItemSets.PearlSet; ;
            }
        }

        [Constructable]
        public PearlPlateLegs() : base()
        {
            Name = "pearl platemail legs";
            Hue = GetItemSet.Hue;
            int i = 0; GetItemSet.ItemBonus.ForEach( (e) => { SkillBonuses.SetValues(i++, e.Skill, e.Value); });
        }
        public PearlPlateLegs(Serial serial) : base(serial)
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

    public class PearlPlateHelm : PlateHelm, IItemSet
    {
        public BaseGearSet GetItemSet
        {
            get
            {
                return ItemSets.PearlSet; ;
            }
        }

        [Constructable]
        public PearlPlateHelm() : base()
        {
            Name = "pearl platemail helm";
            Hue = GetItemSet.Hue;
            int i = 0; GetItemSet.ItemBonus.ForEach( (e) => { SkillBonuses.SetValues(i++, e.Skill, e.Value); });
        }
        public PearlPlateHelm(Serial serial) : base(serial)
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

    public class PearlPlateGorgot : PlateGorget, IItemSet
    {
        public BaseGearSet GetItemSet
        {
            get
            {
                return ItemSets.PearlSet; ;
            }
        }

        [Constructable]
        public PearlPlateGorgot() : base()
        {
            Name = "pearl platemail gorgot";
            Hue = GetItemSet.Hue;
            int i = 0; GetItemSet.ItemBonus.ForEach( (e) => { SkillBonuses.SetValues(i++, e.Skill, e.Value); });
        }
        public PearlPlateGorgot(Serial serial) : base(serial)
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
    public class PearlPlateArms : PlateArms, IItemSet
    {
        public BaseGearSet GetItemSet
        {
            get
            {
                return ItemSets.PearlSet; ;
            }
        }

        [Constructable]
        public PearlPlateArms() : base()
        {
            Name = "pearl platemail arms";
            Hue = GetItemSet.Hue;
            int i = 0; GetItemSet.ItemBonus.ForEach( (e) => { SkillBonuses.SetValues(i++, e.Skill, e.Value); });
        }
        public PearlPlateArms(Serial serial) : base(serial)
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
    public class PearlPlateGloves : PlateGloves, IItemSet
    {
        public BaseGearSet GetItemSet
        {
            get
            {
                return ItemSets.PearlSet; ;
            }
        }

        [Constructable]
        public PearlPlateGloves() : base()
        {
            Name = "pearl platemail gloves";
            Hue = GetItemSet.Hue;
            int i = 0; GetItemSet.ItemBonus.ForEach( (e) => { SkillBonuses.SetValues(i++, e.Skill, e.Value); });
        }
        public PearlPlateGloves(Serial serial) : base(serial)
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

    public class PearlHeaterShield : HeaterShield, IItemSet
    {
        public BaseGearSet GetItemSet
        {
            get
            {
                return ItemSets.PearlSet; ;
            }
        }

        [Constructable]
        public PearlHeaterShield() : base()
        {
            Name = "pearl heater shield";
            Hue = GetItemSet.Hue;
            int i = 0; GetItemSet.ItemBonus.ForEach( (e) => { SkillBonuses.SetValues(i++, e.Skill, e.Value); });
        }
        public PearlHeaterShield(Serial serial) : base(serial)
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
