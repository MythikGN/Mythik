using Server;
using Server.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scripts.Mythik.Items.Uniques
{
    public class BlacksmithySash : BodySash, IUniqueItem
    {
        public RareLevel UniqueLevel
        {
            get
            {
                return RareLevel.UniqueLvl1;
            }
        }
        [Constructable]
        public BlacksmithySash()
        {
            Hue = 0x0787;
            Name = "Blacksmithy Sash";
            SkillBonuses.SetValues(0, Server.SkillName.Blacksmith, 1.0);
        }
        public BlacksmithySash(Serial serial) : base(serial)
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
            SkillBonuses.SetValues(0, Server.SkillName.Blacksmith, 2.0);
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
    public class BlacksmithShirt : Shirt, IUniqueItem
    {
        public RareLevel UniqueLevel
        {
            get
            {
                return RareLevel.UniqueLvl3;
            }
        }
        [Constructable]
        public BlacksmithShirt()
        {
            Hue = 0x0787;
            Name = "Shirt of Blacksmithy";
            SkillBonuses.SetValues(0, Server.SkillName.Blacksmith, 4.0);
        }
        public BlacksmithShirt(Serial serial) : base(serial)
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
            SkillBonuses.SetValues(0, Server.SkillName.Blacksmith, 5.0);
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
        public BlacksmithHammer() : base(8,0)
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
