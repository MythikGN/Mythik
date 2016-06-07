using Server;
using Server.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scripts.Mythik.Items.Craftables.Tinkering.GemArmor
{
   
    public class PearlPlateChest : LeatherChest, IItemSet
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
            Name = "pearl leather chest";
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

    public class PearlPlateLegs : LeatherLegs, IItemSet
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
            Name = "pearl leather legs";
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

    public class PearlPlateHelm : LeatherCap, IItemSet
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
            Name = "pearl leather helm";
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

    public class PearlPlateGorgot : LeatherGorget, IItemSet
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
            Name = "pearl leather gorgot";
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
    public class PearlPlateArms : LeatherArms, IItemSet
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
            Name = "pearl leather arms";
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
    public class PearlPlateGloves : LeatherGloves, IItemSet
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
            Name = "pearl leather gloves";
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
