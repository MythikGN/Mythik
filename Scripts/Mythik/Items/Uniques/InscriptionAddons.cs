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
            Hue = 0x7FC;
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
    public class InscribeKilt : Kilt, IUniqueItem
    {
        public RareLevel UniqueLevel
        {
            get
            {
                return RareLevel.UniqueLvl2;
            }
        }
        [Constructable]
        public InscribeKilt()
        {
            Hue = 0x7FC;
            Name = "Infused Kilt of Inscription";
            SkillBonuses.SetValues(0, Server.SkillName.Inscribe, 4.0);
        }
        public InscribeKilt(Serial serial) : base(serial)
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
                return RareLevel.UniqueLvl3;
            }
        }
        [Constructable]
        public InscribeShirt()
        {
            Hue = 0x7FC;
            Name = "Scribe's Ink Proof Shirt";
            SkillBonuses.SetValues(0, Server.SkillName.Inscribe, 6.0);
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
    public class InscribeDoublet : Doublet, IUniqueItem
    {
        public RareLevel UniqueLevel
        {
            get
            {
                return RareLevel.UniqueLvl4;
            }
        }
        [Constructable]
        public InscribeDoublet()
        {
            Hue = 0x7FC;
            Name = "Scribe's Ink Proof Doublet";
            SkillBonuses.SetValues(0, Server.SkillName.Inscribe, 8.0);
        }
        public InscribeDoublet(Serial serial) : base(serial)
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

    public class InscribeSpellBook : Spellbook, IUniqueItem
    {
        public RareLevel UniqueLevel
        {
            get
            {
                return RareLevel.UniqueLvl5;
            }
        }
        [Constructable]
        public InscribeSpellBook()
        {
            Hue = 0x7FC;
            Name = "Scribe's Enchanted SpellBook";
            SkillBonuses.SetValues(0, Server.SkillName.Inscribe, 10.0);
			Content = ulong.MaxValue;
            LootType = LootType.Regular;
        }
        public InscribeSpellBook(Serial serial) : base(serial)
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
