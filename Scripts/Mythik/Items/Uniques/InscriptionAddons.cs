using Server;
using Server.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scripts.Mythik.Items.Uniques
{
    public class InscribeBoots : ThighBoots, IUniqueItem
    {
        public RareLevel UniqueLevel
        {
            get
            {
                return RareLevel.UniqueLvl1;
            }
        }
        [Constructable]
        public InscribeBoots()
        {
            Hue = 0x803;
            Name = "Scribe's Thigh Boots";
            SkillBonuses.SetValues(0, Server.SkillName.Inscribe, 2.0);
        }
        public InscribeBoots(Serial serial) : base(serial)
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
    public class InscribeHat : WizardsHat, IUniqueItem
    {
        public RareLevel UniqueLevel
        {
            get
            {
                return RareLevel.UniqueLvl2;
            }
        }
        [Constructable]
        public InscribeHat()
        {
            Hue = 0x803;
            Name = "Infused Hat of Inscription";
            SkillBonuses.SetValues(0, Server.SkillName.Inscribe, 4.0);
        }
        public InscribeHat(Serial serial) : base(serial)
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
    public class InscribeApron : HalfApron, IUniqueItem
    {
        public RareLevel UniqueLevel
        {
            get
            {
                return RareLevel.UniqueLvl3;
            }
        }
        [Constructable]
        public InscribeApron()
        {
            Hue = 0x803;
            Name = "Scribe's Apron";
            SkillBonuses.SetValues(0, Server.SkillName.Inscribe, 6.0);
        }
        public InscribeApron(Serial serial) : base(serial)
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
    public class InscribeGloves : LeatherGloves, IUniqueItem
    {
        public RareLevel UniqueLevel
        {
            get
            {
                return RareLevel.UniqueLvl4;
            }
        }
        [Constructable]
        public InscribeGloves()
        {
            Hue = 0x803;
            Name = "Scribe's Writing Gloves";
            SkillBonuses.SetValues(0, Server.SkillName.Inscribe, 8.0);
        }
        public InscribeGloves(Serial serial) : base(serial)
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

    public class InscribeShirt : FancyShirt, IUniqueItem
    {
        public RareLevel UniqueLevel
        {
            get
            {
                return RareLevel.UniqueLvl5;
            }
        }
        [Constructable]
        public InscribeShirt()
        {
            Hue = 0x803;
            Name = "Scribe's Ink Proof Shirt";
            SkillBonuses.SetValues(0, Server.SkillName.Inscribe, 10.0);
        }
        public InscribeShirt(Serial serial) : base(serial)
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
