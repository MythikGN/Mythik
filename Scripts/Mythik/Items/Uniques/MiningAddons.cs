using Server;
using Server.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scripts.Mythik.Items.Uniques
{
    public class MiningBoots : ThighBoots, IUniqueItem
    {
        public RareLevel UniqueLevel
        {
            get
            {
                return RareLevel.UniqueLvl1;
            }
        }
        [Constructable]
        public MiningBoots()
        {
            Hue = 0x082E;
            Name = "Mining Boots";
            SkillBonuses.SetValues(0, Server.SkillName.Mining, 2.0);
        }
        public MiningBoots(Serial serial) : base(serial)
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
    public class MiningKilt : Kilt, IUniqueItem
    {
        public RareLevel UniqueLevel
        {
            get
            {
                return RareLevel.UniqueLvl2;
            }
        }
        [Constructable]
        public MiningKilt()
        {
            Hue = 0x082E;
            Name = "Greater Mining Kilt";
            SkillBonuses.SetValues(0, Server.SkillName.Mining, 4.0);
        }
        public MiningKilt(Serial serial) : base(serial)
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

    public class MiningShirt : Shirt, IUniqueItem
    {
        public RareLevel UniqueLevel
        {
            get
            {
                return RareLevel.UniqueLvl3;
            }
        }
        [Constructable]
        public MiningShirt()
        {
            Hue = 0x082E;
            Name = "Greater Mining Shirt";
            SkillBonuses.SetValues(0, Server.SkillName.Mining, 6.0);
        }
        public MiningShirt(Serial serial) : base(serial)
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

    public class MiningCloak : Cloak, IUniqueItem
    {
        public RareLevel UniqueLevel
        {
            get
            {
                return RareLevel.UniqueLvl4;
            }
        }
        [Constructable]
        public MiningCloak()
        {
            Hue = 0x082E;
            Name = "Cloak of Mining";
            SkillBonuses.SetValues(0, Server.SkillName.Mining, 8.0);
        }
        public MiningCloak(Serial serial) : base(serial)
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

    public class MiningApron : FullApron, IUniqueItem
    {
        public RareLevel UniqueLevel
        {
            get
            {
                return RareLevel.UniqueLvl5;
            }
        }
        [Constructable]
        public MiningApron()
        {
            Hue = 0x082E;
            Name = "Apron of Geology";
            SkillBonuses.SetValues(0, Server.SkillName.Mining, 10.0);
        }
        public MiningApron(Serial serial) : base(serial)
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
