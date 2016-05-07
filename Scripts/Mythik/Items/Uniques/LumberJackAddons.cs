using Server;
using Server.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scripts.Mythik.Items.Uniques
{
    public class LumberjackCap : Cap, IUniqueItem
    {
        public int UniqueLevel
        {
            get
            {
                return 1;
            }
        }
        [Constructable]
        public LumberjackCap()
        {
            Hue = 0x4D;
            Name = "Cap of Forestry";
            SkillBonuses.SetValues(0, Server.SkillName.Lumberjacking, 2.0);
        }
        public LumberjackCap(Serial serial) : base(serial)
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
    public class LumberjackGloves : LeatherGloves, IUniqueItem
    {
        public int UniqueLevel
        {
            get
            {
                return 2;
            }
        }
        [Constructable]
        public LumberjackGloves()
        {
            Hue = 0x4D;
            Name = "Tough Gloves of Forestry";
            SkillBonuses.SetValues(0, Server.SkillName.Lumberjacking, 4.0);
        }
        public LumberjackGloves(Serial serial) : base(serial)
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

    public class LumberjackShirt : LeatherGloves, IUniqueItem
    {
        public int UniqueLevel
        {
            get
            {
                return 3;
            }
        }
        [Constructable]
        public LumberjackShirt()
        {
            Hue = 0x4D;
            Name = "Shirt of Forestry";
            SkillBonuses.SetValues(0, Server.SkillName.Lumberjacking, 6.0);
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

    public class LumberjackPants : LongPants, IUniqueItem
    {
        public int UniqueLevel
        {
            get
            {
                return 4;
            }
        }
        [Constructable]
        public LumberjackPants()
        {
            Hue = 0x4D;
            Name = "Long Pants of Forestry";
            SkillBonuses.SetValues(0, Server.SkillName.Lumberjacking, 8.0);
        }
        public LumberjackPants(Serial serial) : base(serial)
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
        public int UniqueLevel
        {
            get
            {
                return 5;
            }
        }
        [Constructable]
        public LumberjackApron()
        {
            Hue = 0x4D;
            Name = "Apron of Forestry";
            SkillBonuses.SetValues(0, Server.SkillName.Lumberjacking, 10.0);
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
