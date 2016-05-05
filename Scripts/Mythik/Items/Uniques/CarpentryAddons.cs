using Server;
using Server.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scripts.Mythik.Items.Uniques
{
    public class CarpentryBoots : Boots, IUniqueItem
    {
        public int UniqueLevel
        {
            get
            {
                return 1;
            }
        }
        [Constructable]
        public CarpentryBoots()
        {
            Hue = 0x803;
            Name = "boots of better carpentry";
            SkillBonuses.SetValues(0, Server.SkillName.Carpentry, 2.0);
        }
        public CarpentryBoots(Serial serial) : base(serial)
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
    public class CarpentryCap : Cap, IUniqueItem
    {
        public int UniqueLevel
        {
            get
            {
                return 2;
            }
        }
        [Constructable]
        public CarpentryCap()
        {
            Hue = 0x803;
            Name = "cap of greater carpentry";
            SkillBonuses.SetValues(0, Server.SkillName.Carpentry, 4.0);
        }
        public CarpentryCap(Serial serial) : base(serial)
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
    public class CarpentryApron : FullApron, IUniqueItem
    {
        public int UniqueLevel
        {
            get
            {
                return 3;
            }
        }
        [Constructable]
        public CarpentryApron()
        {
            Hue = 0x803;
            Name = "carpenter's apron";
            SkillBonuses.SetValues(0, Server.SkillName.Carpentry, 6.0);
        }
        public CarpentryApron(Serial serial) : base(serial)
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
    public class CarpentryGloves : LeatherGloves, IUniqueItem
    {
        public int UniqueLevel
        {
            get
            {
                return 4;
            }
        }
        [Constructable]
        public CarpentryGloves()
        {
            Hue = 0x803;
            Name = "carpenters's work gloves";
            SkillBonuses.SetValues(0, Server.SkillName.Carpentry, 8.0);
        }
        public CarpentryGloves(Serial serial) : base(serial)
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

    public class CarpentryShirt : Shirt, IUniqueItem
    {
        public int UniqueLevel
        {
            get
            {
                return 5;
            }
        }
        [Constructable]
        public CarpentryShirt()
        {
            Hue = 0x803;
            Name = "carpenter's work shirt";
            SkillBonuses.SetValues(0, Server.SkillName.Carpentry, 10.0);
        }
        public CarpentryShirt(Serial serial) : base(serial)
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
