using Server;
using Server.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scripts.Mythik.Items.Craftables.Tinkering.GemArmor
{
   
    public class TourmalinePlateChest : PlateChest, IItemSet
    {
        public BaseGearSet GetItemSet
        {
            get
            {
                return ItemSets.TourmalineSet; ;
            }
        }

        [Constructable]
        public TourmalinePlateChest() : base()
        {
            Name = "tourmaline platemail chest";
            Hue = GetItemSet.Hue;
            int i = 0; GetItemSet.ItemBonus.ForEach( (e) => { SkillBonuses.SetValues(i++, e.Skill, e.Value); });
        }
        public TourmalinePlateChest(Serial serial) : base(serial)
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

    public class TourmalinePlateLegs : PlateLegs, IItemSet
    {
        public BaseGearSet GetItemSet
        {
            get
            {
                return ItemSets.TourmalineSet; ;
            }
        }

        [Constructable]
        public TourmalinePlateLegs() : base()
        {
            Name = "tourmaline platemail legs";
            Hue = GetItemSet.Hue;
            int i = 0; GetItemSet.ItemBonus.ForEach( (e) => { SkillBonuses.SetValues(i++, e.Skill, e.Value); });
        }
        public TourmalinePlateLegs(Serial serial) : base(serial)
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

    public class TourmalinePlateHelm : PlateHelm, IItemSet
    {
        public BaseGearSet GetItemSet
        {
            get
            {
                return ItemSets.TourmalineSet; ;
            }
        }

        [Constructable]
        public TourmalinePlateHelm() : base()
        {
            Name = "tourmaline platemail helm";
            Hue = GetItemSet.Hue;
            int i = 0; GetItemSet.ItemBonus.ForEach( (e) => { SkillBonuses.SetValues(i++, e.Skill, e.Value); });
        }
        public TourmalinePlateHelm(Serial serial) : base(serial)
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

    public class TourmalinePlateGorgot : PlateGorget, IItemSet
    {
        public BaseGearSet GetItemSet
        {
            get
            {
                return ItemSets.TourmalineSet; ;
            }
        }

        [Constructable]
        public TourmalinePlateGorgot() : base()
        {
            Name = "tourmaline platemail gorgot";
            Hue = GetItemSet.Hue;
            int i = 0; GetItemSet.ItemBonus.ForEach( (e) => { SkillBonuses.SetValues(i++, e.Skill, e.Value); });
        }
        public TourmalinePlateGorgot(Serial serial) : base(serial)
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
    public class TourmalinePlateArms : PlateArms, IItemSet
    {
        public BaseGearSet GetItemSet
        {
            get
            {
                return ItemSets.TourmalineSet; ;
            }
        }

        [Constructable]
        public TourmalinePlateArms() : base()
        {
            Name = "tourmaline platemail arms";
            Hue = GetItemSet.Hue;
            int i = 0; GetItemSet.ItemBonus.ForEach( (e) => { SkillBonuses.SetValues(i++, e.Skill, e.Value); });
        }
        public TourmalinePlateArms(Serial serial) : base(serial)
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
    public class TourmalinePlateGloves : PlateGloves, IItemSet
    {
        public BaseGearSet GetItemSet
        {
            get
            {
                return ItemSets.TourmalineSet; ;
            }
        }

        [Constructable]
        public TourmalinePlateGloves() : base()
        {
            Name = "tourmaline platemail gloves";
            Hue = GetItemSet.Hue;
            int i = 0; GetItemSet.ItemBonus.ForEach( (e) => { SkillBonuses.SetValues(i++, e.Skill, e.Value); });
        }
        public TourmalinePlateGloves(Serial serial) : base(serial)
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

    public class TourmalineHeaterShield : HeaterShield, IItemSet
    {
        public BaseGearSet GetItemSet
        {
            get
            {
                return ItemSets.TourmalineSet; ;
            }
        }

        [Constructable]
        public TourmalineHeaterShield() : base()
        {
            Name = "tourmaline heater shield";
            Hue = GetItemSet.Hue;
            int i = 0; GetItemSet.ItemBonus.ForEach( (e) => { SkillBonuses.SetValues(i++, e.Skill, e.Value); });
        }
        public TourmalineHeaterShield(Serial serial) : base(serial)
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
