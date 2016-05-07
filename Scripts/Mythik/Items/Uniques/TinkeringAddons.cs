using Server;
using Server.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scripts.Mythik.Items.Uniques
{
    public class TinkeringCap : SkullCap, IUniqueItem
    {
        public int UniqueLevel
        {
            get
            {
                return 1;
            }
        }
        [Constructable]
        public TinkeringCap()
        {
            Hue = 0x07AA;
            Name = "Cap of Greater Tinkering";
            SkillBonuses.SetValues(0, Server.SkillName.Tinkering, 2.0);
        }
        public TinkeringCap(Serial serial) : base(serial)
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
    public class TinkeringBandena : Cap, IUniqueItem
    {
        public int UniqueLevel
        {
            get
            {
                return 2;
            }
        }
        [Constructable]
        public TinkeringBandena()
        {
            Hue = 0x07AA;
            Name = "bandana of better Tinkering";
            SkillBonuses.SetValues(0, Server.SkillName.Tinkering, 4.0);
        }
        public TinkeringBandena(Serial serial) : base(serial)
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
    public class TinkeringApron : FullApron, IUniqueItem
    {
        public int UniqueLevel
        {
            get
            {
                return 3;
            }
        }
        [Constructable]
        public TinkeringApron()
        {
            Hue = 0x07AA;
            Name = "alchemist's apron";
            SkillBonuses.SetValues(0, Server.SkillName.Tinkering, 6.0);
        }
        public TinkeringApron(Serial serial) : base(serial)
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
    public class TinkeringGloves : LeatherGloves, IUniqueItem
    {
        public int UniqueLevel
        {
            get
            {
                return 4;
            }
        }
        [Constructable]
        public TinkeringGloves()
        {
            Hue = 0x07AA;
            Name = "alchemist's rubber gloves";
            SkillBonuses.SetValues(0, Server.SkillName.Tinkering, 8.0);
        }
        public TinkeringGloves(Serial serial) : base(serial)
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

    public class TinkeringRobe : Robe, IUniqueItem
    {
        public int UniqueLevel
        {
            get
            {
                return 5;
            }
        }
        [Constructable]
        public TinkeringRobe()
        {
            Hue = 0x07AA;
            Name = "alchemist's lab coat";
            SkillBonuses.SetValues(0, Server.SkillName.Tinkering, 10.0);
        }
        public TinkeringRobe(Serial serial) : base(serial)
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
