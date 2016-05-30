using Server;
using Server.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scripts.Mythik.Items.Craftables.Tinkering.GemArmor
{
   
    public class EmeraldPlateChest : PlateChest, IItemSet
    {
        public BaseGearSet GetItemSet
        {
            get
            {
                return ItemSets.EmeraldSet; ;
            }
        }

        [Constructable]
        public EmeraldPlateChest() : base()
        {
            Name = "emerald platemail chest";
            Hue = GetItemSet.Hue;
            int i = 0; GetItemSet.ItemBonus.ForEach( (e) => { SkillBonuses.SetValues(i++, e.Skill, e.Value); });
        }
        public EmeraldPlateChest(Serial serial) : base(serial)
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

    public class EmeraldPlateLegs : PlateLegs, IItemSet
    {
        public BaseGearSet GetItemSet
        {
            get
            {
                return ItemSets.EmeraldSet; ;
            }
        }

        [Constructable]
        public EmeraldPlateLegs() : base()
        {
            Name = "emerald platemail legs";
            Hue = GetItemSet.Hue;
            int i = 0; GetItemSet.ItemBonus.ForEach( (e) => { SkillBonuses.SetValues(i++, e.Skill, e.Value); });
        }
        public EmeraldPlateLegs(Serial serial) : base(serial)
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

    public class EmeraldPlateHelm : PlateHelm, IItemSet
    {
        public BaseGearSet GetItemSet
        {
            get
            {
                return ItemSets.EmeraldSet; ;
            }
        }

        [Constructable]
        public EmeraldPlateHelm() : base()
        {
            Name = "emerald platemail helm";
            Hue = GetItemSet.Hue;
            int i = 0; GetItemSet.ItemBonus.ForEach( (e) => { SkillBonuses.SetValues(i++, e.Skill, e.Value); });
        }
        public EmeraldPlateHelm(Serial serial) : base(serial)
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

    public class EmeraldPlateGorgot : PlateGorget, IItemSet
    {
        public BaseGearSet GetItemSet
        {
            get
            {
                return ItemSets.EmeraldSet; ;
            }
        }

        [Constructable]
        public EmeraldPlateGorgot() : base()
        {
            Name = "emerald platemail gorgot";
            Hue = GetItemSet.Hue;
            int i = 0; GetItemSet.ItemBonus.ForEach( (e) => { SkillBonuses.SetValues(i++, e.Skill, e.Value); });
        }
        public EmeraldPlateGorgot(Serial serial) : base(serial)
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
    public class EmeraldPlateArms : PlateArms, IItemSet
    {
        public BaseGearSet GetItemSet
        {
            get
            {
                return ItemSets.EmeraldSet; ;
            }
        }

        [Constructable]
        public EmeraldPlateArms() : base()
        {
            Name = "emerald platemail arms";
            Hue = GetItemSet.Hue;
            int i = 0; GetItemSet.ItemBonus.ForEach( (e) => { SkillBonuses.SetValues(i++, e.Skill, e.Value); });
        }
        public EmeraldPlateArms(Serial serial) : base(serial)
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
    public class EmeraldPlateGloves : PlateGloves, IItemSet
    {
        public BaseGearSet GetItemSet
        {
            get
            {
                return ItemSets.EmeraldSet; ;
            }
        }

        [Constructable]
        public EmeraldPlateGloves() : base()
        {
            Name = "emerald platemail gloves";
            Hue = GetItemSet.Hue;
            int i = 0; GetItemSet.ItemBonus.ForEach( (e) => { SkillBonuses.SetValues(i++, e.Skill, e.Value); });
        }
        public EmeraldPlateGloves(Serial serial) : base(serial)
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

    public class EmeraldHeaterShield : HeaterShield, IItemSet
    {
        public BaseGearSet GetItemSet
        {
            get
            {
                return ItemSets.EmeraldSet; ;
            }
        }

        [Constructable]
        public EmeraldHeaterShield() : base()
        {
            Name = "emerald heater shield";
            Hue = GetItemSet.Hue;
            int i = 0; GetItemSet.ItemBonus.ForEach( (e) => { SkillBonuses.SetValues(i++, e.Skill, e.Value); });
        }
        public EmeraldHeaterShield(Serial serial) : base(serial)
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
