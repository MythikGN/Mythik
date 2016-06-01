using Server;
using Server.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scripts.Mythik.Items.Uniques
{
    public class TailorSandals : Sandals, IUniqueItem
    {
        public RareLevel UniqueLevel
        {
            get
            {
                return RareLevel.UniqueLvl1;
            }
        }
        [Constructable]
        public TailorSandals()
        {
            Hue = 0x98;
            Name = "Sandals of Tailoring";
            SkillBonuses.SetValues(0, Server.SkillName.Tailoring, 2.0);
        }
        public TailorSandals(Serial serial) : base(serial)
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
    public class TailorGloves : LeatherGloves, IUniqueItem
    {
        public RareLevel UniqueLevel
        {
            get
            {
                return RareLevel.UniqueLvl2;
            }
        }
        [Constructable]
        public TailorGloves()
        {
            Hue = 0x98;
            Name = "Greater Gloves of Tailoring";
            SkillBonuses.SetValues(0, Server.SkillName.Tailoring, 4.0);
        }
        public TailorGloves(Serial serial) : base(serial)
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

    public class TailorHat : FloppyHat, IUniqueItem
    {
        public RareLevel UniqueLevel
        {
            get
            {
                return RareLevel.UniqueLvl3;
            }
        }
        [Constructable]
        public TailorHat()
        {
            Hue = 0x98;
            Name = "Floppy Hat of Tailoring";
            SkillBonuses.SetValues(0, Server.SkillName.Mining, 6.0);
        }
        public TailorHat(Serial serial) : base(serial)
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

    public class TailoringDoublet : Doublet, IUniqueItem
    {
        public RareLevel UniqueLevel
        {
            get
            {
                return RareLevel.UniqueLvl4;
            }
        }
        [Constructable]
        public TailoringDoublet()
        {
            Hue = 0x98;
            Name = "Doublet of Tailoring";
            SkillBonuses.SetValues(0, Server.SkillName.Tailoring, 8.0);
        }
        public TailoringDoublet(Serial serial) : base(serial)
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

    public class TailorSkirt : Skirt, IUniqueItem
    {
        public RareLevel UniqueLevel
        {
            get
            {
                return RareLevel.UniqueLvl5;
            }
        }
        [Constructable]
        public TailorSkirt()
        {
            Hue = 0x98;
            Name = "Skirt of Tailoring";
            SkillBonuses.SetValues(0, Server.SkillName.Tailoring, 10.0);
        }
        public TailorSkirt(Serial serial) : base(serial)
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
