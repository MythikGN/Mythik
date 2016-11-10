using Server;
using Server.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scripts.Mythik.Items.Uniques
{
    public class TinkeringSkirt : Skirt, IUniqueItem
    {
        public RareLevel UniqueLevel
        {
            get
            {
                return RareLevel.UniqueLvl1;
            }
        }
        [Constructable]
        public TinkeringSkirt()
        {
            Hue = 0x07AA;
            Name = "Skirt of Greater Tinkering";
            SkillBonuses.SetValues(0, Server.SkillName.Tinkering, 2.0);
        }
        public TinkeringSkirt(Serial serial) : base(serial)
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
    public class TinkeringShirt : Shirt, IUniqueItem
    {
        public RareLevel UniqueLevel
        {
            get
            {
                return RareLevel.UniqueLvl2;
            }
        }
        [Constructable]
        public TinkeringShirt()
        {
            Hue = 0x07AA;
            Name = "Shirt of Better Tinkering";
            SkillBonuses.SetValues(0, Server.SkillName.Tinkering, 4.0);
        }
        public TinkeringShirt(Serial serial) : base(serial)
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
    public class TinkeringBoots : Boots, IUniqueItem
    {
        public RareLevel UniqueLevel
        {
            get
            {
                return RareLevel.UniqueLvl3;
            }
        }
        [Constructable]
        public TinkeringBoots()
        {
            Hue = 0x07AA;
            Name = "Boots of Tinkering";
            SkillBonuses.SetValues(0, Server.SkillName.Tinkering, 6.0);
        }
        public TinkeringBoots(Serial serial) : base(serial)
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
    public class TinkeringCloak : LeatherCloak, IUniqueItem
    {
        public RareLevel UniqueLevel
        {
            get
            {
                return RareLevel.UniqueLvl4;
            }
        }
        [Constructable]
        public TinkeringCloak()
        {
            Hue = 0x07AA;
            Name = "Tinker's Safety Cloak";
            SkillBonuses.SetValues(0, Server.SkillName.Tinkering, 8.0);
        }
        public TinkeringCloak(Serial serial) : base(serial)
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
        public RareLevel UniqueLevel
        {
            get
            {
                return RareLevel.UniqueLvl5;
            }
        }
        [Constructable]
        public TinkeringApron()
        {
            Hue = 0x07AA;
            Name = "Tinker's Full Apron";
            SkillBonuses.SetValues(0, Server.SkillName.Tinkering, 10.0);
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
}
