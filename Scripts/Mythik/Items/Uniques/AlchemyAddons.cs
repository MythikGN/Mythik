using Server;
using Server.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scripts.Mythik.Items.Uniques
{
    public class AlchemySandals : Sandals, IUniqueItem
    {
        public int UniqueLevel
        {
            get
            {
                return 1;
            }
        }
        [Constructable]
        public AlchemySandals()
        {
            Hue = 0x79c;
            Name = "sandals of better alchemy";
            SkillBonuses.SetValues(0, Server.SkillName.Alchemy, 2.0);
        }
        public AlchemySandals(Serial serial) : base(serial)
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
    public class AlchemyBandena : Cap, IUniqueItem
    {
        public int UniqueLevel
        {
            get
            {
                return 2;
            }
        }
        [Constructable]
        public AlchemyBandena()
        {
            Hue = 0x79c;
            Name = "bandana of better alchemy";
            SkillBonuses.SetValues(0, Server.SkillName.Alchemy, 4.0);
        }
        public AlchemyBandena(Serial serial) : base(serial)
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
    public class AlchemyApron : FullApron, IUniqueItem
    {
        public int UniqueLevel
        {
            get
            {
                return 3;
            }
        }
        [Constructable]
        public AlchemyApron()
        {
            Hue = 0x79c;
            Name = "alchemist's apron";
            SkillBonuses.SetValues(0, Server.SkillName.Alchemy, 6.0);
        }
        public AlchemyApron(Serial serial) : base(serial)
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
    public class AlchemyGloves : LeatherGloves, IUniqueItem
    {
        public int UniqueLevel
        {
            get
            {
                return 4;
            }
        }
        [Constructable]
        public AlchemyGloves()
        {
            Hue = 0x79c;
            Name = "alchemist's rubber gloves";
            SkillBonuses.SetValues(0, Server.SkillName.Alchemy, 8.0);
        }
        public AlchemyGloves(Serial serial) : base(serial)
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

    public class AlchemyRobe : Robe, IUniqueItem
    {
        public int UniqueLevel
        {
            get
            {
                return 5;
            }
        }
        [Constructable]
        public AlchemyRobe()
        {
            Hue = 0x79c;
            Name = "alchemist's lab coat";
            SkillBonuses.SetValues(0, Server.SkillName.Alchemy, 10.0);
        }
        public AlchemyRobe(Serial serial) : base(serial)
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
