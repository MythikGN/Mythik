using Server;
using Server.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scripts.Mythik.Items.Uniques
{
    public class BowcraftShoes : Shoes, IUniqueItem
    {
        public RareLevel UniqueLevel
        {
            get
            {
                return RareLevel.UniqueLvl1;
            }
        }
        [Constructable]
        public BowcraftShoes()
        {
            Hue = 0x783;
            Name = "Shoes of Better Bowcraft";
            SkillBonuses.SetValues(0, Server.SkillName.Fletching, 2.0);
        }
        public BowcraftShoes(Serial serial) : base(serial)
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
    public class BowcraftSkirt : Skirt, IUniqueItem
    {
        public RareLevel UniqueLevel
        {
            get
            {
                return RareLevel.UniqueLvl2;
            }
        }
        [Constructable]
        public BowcraftSkirt()
        {
            Hue = 0x783;
            Name = "Bowcrafting Skirt";
            SkillBonuses.SetValues(0, Server.SkillName.Fletching, 4.0);
        }
        public BowcraftSkirt(Serial serial) : base(serial)
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
    public class BowcraftShirt : FancyShirt, IUniqueItem
    {
        public RareLevel UniqueLevel
        {
            get
            {
                return RareLevel.UniqueLvl3;
            }
        }
        [Constructable]
        public BowcraftShirt()
        {
            Hue = 0x783;
            Name = "Bowcrafter's Shirt";
            SkillBonuses.SetValues(0, Server.SkillName.Fletching, 6.0);
        }
        public BowcraftShirt(Serial serial) : base(serial)
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
    public class BowcraftDoublet : Doublet, IUniqueItem
    {
        public RareLevel UniqueLevel
        {
            get
            {
                return RareLevel.UniqueLvl4;
            }
        }
        [Constructable]
        public BowcraftDoublet()
        {
            Hue = 0x783;
            Name = "Bowcrafter's Doublet";
            SkillBonuses.SetValues(0, Server.SkillName.Fletching, 8.0);
        }
        public BowcraftDoublet(Serial serial) : base(serial)
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

    public class BowcraftDagger : Dagger, IUniqueItem
    {
        public RareLevel UniqueLevel
        {
            get
            {
                return RareLevel.UniqueLvl5;
            }
        }
        [Constructable]
        public BowcraftDagger()
        {
            Hue = 0x783;
            Name = "Master Bowcrafter's Dagger";
            SkillBonuses.SetValues(0, Server.SkillName.Fletching, 10.0);
        }
        public BowcraftDagger(Serial serial) : base(serial)
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
