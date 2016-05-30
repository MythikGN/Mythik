using Server;
using Server.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scripts.Mythik.Items.Craftables.Tinkering.GemArmor
{
   
    public class StarSapphirePlateChest : PlateChest, IItemSet
    {
        public BaseGearSet GetItemSet
        {
            get
            {
                return ItemSets.StarSapphireSet; ;
            }
        }

        [Constructable]
        public StarSapphirePlateChest() : base()
        {
            Name = "star sapphire platemail chest";
            Hue = GetItemSet.Hue;
            int i = 0; GetItemSet.ItemBonus.ForEach( (e) => { SkillBonuses.SetValues(i++, e.Skill, e.Value); });
        }
        public StarSapphirePlateChest(Serial serial) : base(serial)
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

    public class StarSapphirePlateLegs : PlateLegs, IItemSet
    {
        public BaseGearSet GetItemSet
        {
            get
            {
                return ItemSets.StarSapphireSet; ;
            }
        }

        [Constructable]
        public StarSapphirePlateLegs() : base()
        {
            Name = "star sapphire platemail legs";
            Hue = GetItemSet.Hue;
            int i = 0; GetItemSet.ItemBonus.ForEach( (e) => { SkillBonuses.SetValues(i++, e.Skill, e.Value); });
        }
        public StarSapphirePlateLegs(Serial serial) : base(serial)
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

    public class StarSapphirePlateHelm : PlateHelm, IItemSet
    {
        public BaseGearSet GetItemSet
        {
            get
            {
                return ItemSets.StarSapphireSet; ;
            }
        }

        [Constructable]
        public StarSapphirePlateHelm() : base()
        {
            Name = "star sapphire platemail helm";
            Hue = GetItemSet.Hue;
            int i = 0; GetItemSet.ItemBonus.ForEach( (e) => { SkillBonuses.SetValues(i++, e.Skill, e.Value); });
        }
        public StarSapphirePlateHelm(Serial serial) : base(serial)
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

    public class StarSapphirePlateGorgot : PlateGorget, IItemSet
    {
        public BaseGearSet GetItemSet
        {
            get
            {
                return ItemSets.StarSapphireSet; ;
            }
        }

        [Constructable]
        public StarSapphirePlateGorgot() : base()
        {
            Name = "star sapphire platemail gorgot";
            Hue = GetItemSet.Hue;
            int i = 0; GetItemSet.ItemBonus.ForEach( (e) => { SkillBonuses.SetValues(i++, e.Skill, e.Value); });
        }
        public StarSapphirePlateGorgot(Serial serial) : base(serial)
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
    public class StarSapphirePlateArms : PlateArms, IItemSet
    {
        public BaseGearSet GetItemSet
        {
            get
            {
                return ItemSets.StarSapphireSet; ;
            }
        }

        [Constructable]
        public StarSapphirePlateArms() : base()
        {
            Name = "star sapphire platemail arms";
            Hue = GetItemSet.Hue;
            int i = 0; GetItemSet.ItemBonus.ForEach( (e) => { SkillBonuses.SetValues(i++, e.Skill, e.Value); });
        }
        public StarSapphirePlateArms(Serial serial) : base(serial)
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
    public class StarSapphirePlateGloves : PlateGloves, IItemSet
    {
        public BaseGearSet GetItemSet
        {
            get
            {
                return ItemSets.StarSapphireSet; ;
            }
        }

        [Constructable]
        public StarSapphirePlateGloves() : base()
        {
            Name = "star sapphire platemail gloves";
            Hue = GetItemSet.Hue;
            int i = 0; GetItemSet.ItemBonus.ForEach( (e) => { SkillBonuses.SetValues(i++, e.Skill, e.Value); });
        }
        public StarSapphirePlateGloves(Serial serial) : base(serial)
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

    public class StarSapphireHeaterShield : HeaterShield, IItemSet
    {
        public BaseGearSet GetItemSet
        {
            get
            {
                return ItemSets.StarSapphireSet; ;
            }
        }

        [Constructable]
        public StarSapphireHeaterShield() : base()
        {
            Name = "star sapphire heater shield";
            Hue = GetItemSet.Hue;
            int i = 0; GetItemSet.ItemBonus.ForEach( (e) => { SkillBonuses.SetValues(i++, e.Skill, e.Value); });
        }
        public StarSapphireHeaterShield(Serial serial) : base(serial)
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
