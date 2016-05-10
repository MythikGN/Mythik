﻿using Server;
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
        public RareLevel UniqueLevel
        {
            get
            {
                return RareLevel.UniqueLvl1;
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
    public class TinkeringPants : LongPants, IUniqueItem
    {
        public RareLevel UniqueLevel
        {
            get
            {
                return RareLevel.UniqueLvl3;
            }
        }
        [Constructable]
        public TinkeringPants()
        {
            Hue = 0x07AA;
            Name = "Long Pants of Tinkering";
            SkillBonuses.SetValues(0, Server.SkillName.Tinkering, 6.0);
        }
        public TinkeringPants(Serial serial) : base(serial)
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
        public RareLevel UniqueLevel
        {
            get
            {
                return RareLevel.UniqueLvl4;
            }
        }
        [Constructable]
        public TinkeringGloves()
        {
            Hue = 0x07AA;
            Name = "Tinker's Safety Gloves";
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