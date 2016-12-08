using Server;
using Server.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scripts.Mythik.Items.Uniques
{
    public class LumberjackBoots : ThighBoots, IUniqueItem
    {
        public RareLevel UniqueLevel
        {
            get
            {
                return RareLevel.UniqueLvl1;
            }
        }
        [Constructable]
        public LumberjackBoots()
        {
            Hue = 0x7FF;
            Name = "Tough Boots of Forestry";
            SkillBonuses.SetValues(0, Server.SkillName.Lumberjacking, 1.0);
        }
        public LumberjackBoots(Serial serial) : base(serial)
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
    public class LumberjackSkirt : Skirt, IUniqueItem
    {
        public RareLevel UniqueLevel
        {
            get
            {
                return RareLevel.UniqueLvl2;
            }
        }
        [Constructable]
        public LumberjackSkirt()
        {
            Hue = 0x7FF;
            Name = "Skirt of Forestry";
            SkillBonuses.SetValues(0, Server.SkillName.Lumberjacking, 2.0);
        }
        public LumberjackSkirt(Serial serial) : base(serial)
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

    public class LumberjackShirt : FancyShirt, IUniqueItem
    {
        public RareLevel UniqueLevel
        {
            get
            {
                return RareLevel.UniqueLvl3;
            }
        }
        [Constructable]
        public LumberjackShirt()
        {
            Hue = 0x7FF;
            Name = "Shirt of Forestry";
            SkillBonuses.SetValues(0, Server.SkillName.Lumberjacking, 4.0);
        }
        public LumberjackShirt(Serial serial) : base(serial)
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

    public class LumberjackCloak : Cloak, IUniqueItem
    {
        public RareLevel UniqueLevel
        {
            get
            {
                return RareLevel.UniqueLvl4;
            }
        }
        [Constructable]
        public LumberjackCloak()
        {
            Hue = 0x7FF;
            Name = "Cloak of Forestry";
            SkillBonuses.SetValues(0, Server.SkillName.Lumberjacking, 5.0);
        }
        public LumberjackCloak(Serial serial) : base(serial)
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

    public class LumberjackApron : FullApron, IUniqueItem
    {
        public RareLevel UniqueLevel
        {
            get
            {
                return RareLevel.UniqueLvl5;
            }
        }
        [Constructable]
        public LumberjackApron()
        {
            Hue = 0x7FF;
            Name = "Apron of Forestry";
            SkillBonuses.SetValues(0, Server.SkillName.Lumberjacking, 8.0);
        }
        public LumberjackApron(Serial serial) : base(serial)
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
