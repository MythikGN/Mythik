using Server;
using Server.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scripts.Mythik.Items.Uniques
{
    public class DamagedMiningHat : WizardsHat, IUniqueItem
    {
        public RareLevel UniqueLevel
        {
            get
            {
                return RareLevel.UniqueLvl1;
            }
        }
        [Constructable]
        public DamagedMiningHat()
        {
            Hue = 0x082E;
            Name = "Damaged Mining Hat";
            SkillBonuses.SetValues(0, Server.SkillName.Mining, 2.0);
        }
        public DamagedMiningHat(Serial serial) : base(serial)
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
    public class Miningboots : ThighBoots, IUniqueItem
    {
        public RareLevel UniqueLevel
        {
            get
            {
                return RareLevel.UniqueLvl2;
            }
        }
        [Constructable]
        public Miningboots()
        {
            Hue = 0x082E;
            Name = "Greater Boots of Mining";
            SkillBonuses.SetValues(0, Server.SkillName.Mining, 4.0);
        }
        public Miningboots(Serial serial) : base(serial)
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

    public class MiningPants : LongPants, IUniqueItem
    {
        public RareLevel UniqueLevel
        {
            get
            {
                return RareLevel.UniqueLvl4;
            }
        }
        [Constructable]
        public MiningPants()
        {
            Hue = 0x082E;
            Name = "Long Pants of Mining";
            SkillBonuses.SetValues(0, Server.SkillName.Mining, 8.0);
        }
        public MiningPants(Serial serial) : base(serial)
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

    public class MiningRobe : FullApron, IUniqueItem
    {
        public RareLevel UniqueLevel
        {
            get
            {
                return RareLevel.UniqueLvl5;
            }
        }
        [Constructable]
        public MiningRobe()
        {
            Hue = 0x082E;
            Name = "Robe of Geology";
            SkillBonuses.SetValues(0, Server.SkillName.Mining, 10.0);
        }
        public MiningRobe(Serial serial) : base(serial)
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
