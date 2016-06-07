using Server;
using Server.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scripts.Mythik.Items.Craftables.Tinkering.GemArmor
{

    public class TurquoisePlateChest : LeatherChest, IItemSet
    {
        public BaseGearSet GetItemSet
        {
            get
            {
                return ItemSets.TurquoiseSet; ;
            }
        }

        [Constructable]
        public TurquoisePlateChest() : base()
        {
            Name = "turquoise leather chest";
            Hue = GetItemSet.Hue;
            int i = 0; GetItemSet.ItemBonus.ForEach((e) => { SkillBonuses.SetValues(i++, e.Skill, e.Value); });
        }
        public TurquoisePlateChest(Serial serial) : base(serial)
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

    public class TurquoisePlateLegs : LeatherLegs, IItemSet
    {
        public BaseGearSet GetItemSet
        {
            get
            {
                return ItemSets.TurquoiseSet; ;
            }
        }

        [Constructable]
        public TurquoisePlateLegs() : base()
        {
            Name = "turquoise leather legs";
            Hue = GetItemSet.Hue;
            int i = 0; GetItemSet.ItemBonus.ForEach((e) => { SkillBonuses.SetValues(i++, e.Skill, e.Value); });
        }
        public TurquoisePlateLegs(Serial serial) : base(serial)
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

    public class TurquoisePlateHelm : LeatherCap, IItemSet
    {
        public BaseGearSet GetItemSet
        {
            get
            {
                return ItemSets.TurquoiseSet; ;
            }
        }

        [Constructable]
        public TurquoisePlateHelm() : base()
        {
            Name = "turquoise leather helm";
            Hue = GetItemSet.Hue;
            int i = 0; GetItemSet.ItemBonus.ForEach((e) => { SkillBonuses.SetValues(i++, e.Skill, e.Value); });
        }
        public TurquoisePlateHelm(Serial serial) : base(serial)
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

    public class TurquoisePlateGorgot : LeatherGorget, IItemSet
    {
        public BaseGearSet GetItemSet
        {
            get
            {
                return ItemSets.TurquoiseSet; ;
            }
        }

        [Constructable]
        public TurquoisePlateGorgot() : base()
        {
            Name = "turquoise leather gorgot";
            Hue = GetItemSet.Hue;
            int i = 0; GetItemSet.ItemBonus.ForEach((e) => { SkillBonuses.SetValues(i++, e.Skill, e.Value); });
        }
        public TurquoisePlateGorgot(Serial serial) : base(serial)
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
    public class TurquoisePlateArms : LeatherArms, IItemSet
    {
        public BaseGearSet GetItemSet
        {
            get
            {
                return ItemSets.TurquoiseSet; ;
            }
        }

        [Constructable]
        public TurquoisePlateArms() : base()
        {
            Name = "turquoise leather arms";
            Hue = GetItemSet.Hue;
            int i = 0; GetItemSet.ItemBonus.ForEach((e) => { SkillBonuses.SetValues(i++, e.Skill, e.Value); });
        }
        public TurquoisePlateArms(Serial serial) : base(serial)
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
    public class TurquoisePlateGloves : LeatherGloves, IItemSet
    {
        public BaseGearSet GetItemSet
        {
            get
            {
                return ItemSets.TurquoiseSet; ;
            }
        }

        [Constructable]
        public TurquoisePlateGloves() : base()
        {
            Name = "turquoise leather gloves";
            Hue = GetItemSet.Hue;
            int i = 0; GetItemSet.ItemBonus.ForEach((e) => { SkillBonuses.SetValues(i++, e.Skill, e.Value); });
        }
        public TurquoisePlateGloves(Serial serial) : base(serial)
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

    public class TurquoiseHeaterShield : HeaterShield, IItemSet
    {
        public BaseGearSet GetItemSet
        {
            get
            {
                return ItemSets.TurquoiseSet; ;
            }
        }

        [Constructable]
        public TurquoiseHeaterShield() : base()
        {
            Name = "turquoise heater shield";
            Hue = GetItemSet.Hue;
            int i = 0; GetItemSet.ItemBonus.ForEach((e) => { SkillBonuses.SetValues(i++, e.Skill, e.Value); });
        }
        public TurquoiseHeaterShield(Serial serial) : base(serial)
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
