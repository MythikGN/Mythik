using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Server;
using Server.Mobiles;

namespace Scripts.Mythik.Items.Craftables.Tinkering.GemArmor
{


    public abstract class RubySet : BaseGemArmor
    {
        public override int BasePhysicalResistance { get { return 5; } }
        public override int BaseFireResistance { get { return 3; } }
        public override int BaseColdResistance { get { return 2; } }
        public override int BasePoisonResistance { get { return 3; } }
        public override int BaseEnergyResistance { get { return 2; } }

        public override int InitMinHits { get { return 50; } }
        public override int InitMaxHits { get { return 65; } }

        public override int AosStrReq { get { return 95; } }
        public override int OldStrReq { get { return 60; } }

        public override int OldDexBonus { get { return -8; } }

        public override int ArmorBase { get { return 40; } }

        //  Applied when wearing the set
        static SkillMod m_SkillMod = new DefaultSkillMod(SkillName.AnimalTaming,true, 10.0);
        public RubySet(int itemID): base(itemID)
        {
            this.Hue = 0x804;

            SkillBonuses.SetValues(0, Server.SkillName.AnimalTaming, 2.0);
            SkillBonuses.SetValues(1, Server.SkillName.Veterinary, 2.0);
        }
        public override void CheckPartAdded(Mobile parent)
        {
            var cnt = base.GetSetCount(parent, typeof(RubySet));
            if(cnt >= 2)
            {
                parent.AddSkillMod(m_SkillMod);
            }
        }
        public override void CheckPartRemoved(Mobile parent)
        {
            var cnt = base.GetSetCount(parent, typeof(RubySet));
            if (cnt < 2)
            {
                (parent as PlayerMobile).RemoveSkillMod(m_SkillMod);
            }
        }
    }
    public class RubyPlateChest : RubySet
    {
       
        [Constructable]
        public RubyPlateChest() : base(0x1415)
        {
            Weight = 10;
            Name = "ruby platemail chest";
        }
        public RubyPlateChest(Serial serial) : base(serial)
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
    public class RubyPlateLegs : RubySet
    {

        [Constructable]
        public RubyPlateLegs() : base(0x1411)
        {
            Weight = 8;
            Name = "ruby platemail legs";
        }
        public RubyPlateLegs(Serial serial) : base(serial)
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
    public class RubyPlateGloves : RubySet
    {

        [Constructable]
        public RubyPlateGloves() : base(0x1414)
        {
            Weight = 6;
            Name = "ruby platemail gloves";
        }
        public RubyPlateGloves(Serial serial) : base(serial)
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
    public class RubyPlateArms : RubySet
    {

        [Constructable]
        public RubyPlateArms() : base(0x1410)
        {
            Weight = 5;
            Name = "ruby platemail arms";
        }
        public RubyPlateArms(Serial serial) : base(serial)
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
    public class RubyPlateGorgot : RubySet
    {

        [Constructable]
        public RubyPlateGorgot() : base(0x1413)
        {
            Weight = 2;
            Name = "ruby platemail gorgot";
        }
        public RubyPlateGorgot(Serial serial) : base(serial)
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
    public class RubyPlateHelm : RubySet
    {

        [Constructable]
        public RubyPlateHelm() : base(0x1412)
        {
            Weight = 5;
            Name = "ruby platemail helm";
        }
        public RubyPlateHelm(Serial serial) : base(serial)
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
    public class RubyPlateHeater : RubySet
    {
        //todo this should inherit from BaseShield not BaseArmor
        [Constructable]
        public RubyPlateHeater() : base(0x1B76)
        {
            Weight = 8;
            Name = "ruby heater shield";
        }
        public RubyPlateHeater(Serial serial) : base(serial)
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
