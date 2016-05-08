using Server;
using Server.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scripts.Mythik.Items.Uniques
{
    public class BlacksmithyArms : LeatherArms, IUniqueItem
    {
        public RareLevel UniqueLevel
        {
            get
            {
                return RareLevel.UniqueLvl1;
            }
        }
        [Constructable]
        public BlacksmithyArms()
        {
            Hue = 0x0787;
            Name = "Blacksmithy Arm Guards";
            SkillBonuses.SetValues(0, Server.SkillName.Blacksmith, 2.0);
        }
        public BlacksmithyArms(Serial serial) : base(serial)
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
    public class BlacksmithShoes : Shoes, IUniqueItem
    {
        public RareLevel UniqueLevel
        {
            get
            {
                return RareLevel.UniqueLvl2;
            }
        }
        [Constructable]
        public BlacksmithShoes()
        {
            Hue = 0x0787;
            Name = "Shoes of Greater Blacksmithy";
            SkillBonuses.SetValues(0, Server.SkillName.Blacksmith, 4.0);
        }
        public BlacksmithShoes(Serial serial) : base(serial)
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
    public class BlacksmithBandana : Bandana, IUniqueItem
    {
        public RareLevel UniqueLevel
        {
            get
            {
                return RareLevel.UniqueLvl3;
            }
        }
        [Constructable]
        public BlacksmithBandana()
        {
            Hue = 0x0787;
            Name = "Bandana of Blacksmithy";
            SkillBonuses.SetValues(0, Server.SkillName.Blacksmith, 6.0);
        }
        public BlacksmithBandana(Serial serial) : base(serial)
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
    public class BlacksmithApron : HalfApron, IUniqueItem
    {
        public RareLevel UniqueLevel
        {
            get
            {
                return RareLevel.UniqueLvl4;
            }
        }
        [Constructable]
        public BlacksmithApron()
        {
            Hue = 0x0787;
            Name = "Adept Blacksmiths Apron";
            SkillBonuses.SetValues(0, Server.SkillName.Blacksmith, 8.0);
        }
        public BlacksmithApron(Serial serial) : base(serial)
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

    public class BlacksmithHammer : AncientSmithyHammer, IUniqueItem
    {
        public RareLevel UniqueLevel
        {
            get
            {
                return RareLevel.UniqueLvl5;
            }
        }
        [Constructable]
        public BlacksmithHammer() : base(10)
        {
            Hue = 0x0787;
            Name = "Master Blacksmiths Hammer";
        }
        public BlacksmithHammer(Serial serial) : base(serial)
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
