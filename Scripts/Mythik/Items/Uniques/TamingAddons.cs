using Server.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Server;

namespace Scripts.Mythik.Items.Uniques
{
    public class TamingSkirt : Skirt, IUniqueItem
    {
        public int UniqueLevel
        {
            get
            {
                return 1;
            }
        }
        [Constructable]
        public TamingSkirt()
        {
            Hue = 0x7a1;
            Name = "little bo-beeps skirt";
            SkillBonuses.SetValues(0, Server.SkillName.AnimalTaming, 2.0);
        }
        public TamingSkirt(Serial serial) : base(serial)
        {

        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write((int)1);
        }
        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            var version = reader.ReadInt();
        }


    }
    public class TamingShirt : FancyShirt, IUniqueItem
    {
        public int UniqueLevel
        {
            get
            {
                return 2;
            }
        }
        [Constructable]
        public TamingShirt()
        {
            Hue = 0x7a1;
            Name = "little bo-beeps shirt";
            SkillBonuses.SetValues(0, Server.SkillName.AnimalTaming, 4.0);
        }
        public TamingShirt(Serial serial) : base(serial)
        {

        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write((int)1);
        }
        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            var version = reader.ReadInt();
        }


    }
    public class TamingCloak : Cloak, IUniqueItem
    {
        public int UniqueLevel
        {
            get
            {
                return 2;
            }
        }
        [Constructable]
        public TamingCloak()
        {
            Hue = 0x7a1;
            Name = "little bo-beeps cloak";
            SkillBonuses.SetValues(0, Server.SkillName.AnimalTaming, 4.0);
        }
        public TamingCloak(Serial serial) : base(serial)
        {

        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write((int)1);
        }
        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            var version = reader.ReadInt();
        }


    }
    public class TamingBoots : Boots, IUniqueItem
    {
        public int UniqueLevel
        {
            get
            {
                return 3;
            }
        }
        [Constructable]
        public TamingBoots()
        {
            Hue = 0x7a1;
            Name = "little bo-beeps boots";
            SkillBonuses.SetValues(0, Server.SkillName.AnimalTaming, 6.0);
        }
        public TamingBoots(Serial serial) : base(serial)
        {

        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write((int)1);
        }
        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            var version = reader.ReadInt();
        }


    }
    public class TamingRobe : Robe, IUniqueItem
    {
        public int UniqueLevel
        {
            get
            {
                return 4;
            }
        }
        [Constructable]
        public TamingRobe()
        {
            Hue = 0x7a1;
            Name = "little bo-beeps robe";
            SkillBonuses.SetValues(0, Server.SkillName.AnimalTaming, 8.0);
        }
        public TamingRobe(Serial serial) : base(serial)
        {

        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write((int)1);
        }
        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            var version = reader.ReadInt();
        }


    }
    public class TamingDoublet : Robe, IUniqueItem
    {
        public int UniqueLevel
        {
            get
            {
                return 4;
            }
        }
        [Constructable]
        public TamingDoublet()
        {
            Hue = 0x7a1;
            Name = "little bo-beeps doublet";
            SkillBonuses.SetValues(0, Server.SkillName.AnimalTaming, 8.0);
        }
        public TamingDoublet(Serial serial) : base(serial)
        {

        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write((int)1);
        }
        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            var version = reader.ReadInt();
        }


    }

    public class TamingCrook : ShepherdsCrook, IUniqueItem
    {
        public int UniqueLevel
        {
            get
            {
                return 4;
            }
        }
        [Constructable]
        public TamingCrook()
        {
            Hue = 0x7a1;
            Name = "little bo-beeps crook";
            SkillBonuses.SetValues(0, Server.SkillName.AnimalTaming, 10.0);
        }
        public TamingCrook(Serial serial) : base(serial)
        {

        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write((int)1);
        }
        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            var version = reader.ReadInt();
        }


    }
}
