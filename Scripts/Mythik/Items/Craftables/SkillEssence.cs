using Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scripts.Mythik.Items.Craftables
{
    public class AlchemyEssence : Item
    {
        [Constructable]
        public AlchemyEssence() : base(0x3193)
        {
            Weight = 1;
            Hue = 0x79c;
            Name = "Alchemy Essence";
        }
        public AlchemyEssence(Serial serial) : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0); // version 
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }

    public class BlacksmithyEssence : Item
    {
        [Constructable]
        public BlacksmithyEssence() : base(0x3193)
        {
            Weight = 1;
            Hue = 0x0787;
            Name = "Blacksmithy Essence";
        }
        public BlacksmithyEssence(Serial serial) : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0); // version 
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }

    public class BowcraftEssence : Item
    {
        [Constructable]
        public BowcraftEssence() : base(0x3193)
        {
            Weight = 1;
            Hue = 0x783;
            Name = "Bowcraft Essence";
        }
        public BowcraftEssence(Serial serial) : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0); // version 
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }

    public class CarpentryEssence : Item
    {
        [Constructable]
        public CarpentryEssence() : base(0x3193)
        {
            Weight = 1;
            Hue = 0x803;
            Name = "Carpentry Essence";
        }
        public CarpentryEssence(Serial serial) : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0); // version 
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }

    public class FishingEssence : Item
    {
        [Constructable]
        public FishingEssence() : base(0x3193)
        {
            Weight = 1;
            Hue = 0x794;
            Name = "Fishing Essence";
        }
        public FishingEssence(Serial serial) : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0); // version 
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }

    public class InscriptionEssence : Item
    {
        [Constructable]
        public InscriptionEssence() : base(0x3193)
        {
            Weight = 1;
            Hue = 0x7FC;
            Name = "Inscription Essence";
        }
        public InscriptionEssence(Serial serial) : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0); // version 
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }

    public class LumberjackingEssence : Item
    {
        [Constructable]
        public LumberjackingEssence() : base(0x3193)
        {
            Weight = 1;
            Hue = 0x7FF;
            Name = "Lumberjacking Essence";
        }
        public LumberjackingEssence(Serial serial) : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0); // version 
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }

    public class MiningEssence : Item
    {
        [Constructable]
        public MiningEssence() : base(0x3193)
        {
            Weight = 1;
            Hue = 0x082E;
            Name = "Mining Essence";
        }
        public MiningEssence(Serial serial) : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0); // version 
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }

    public class TailoringEssence : Item
    {
        [Constructable]
        public TailoringEssence() : base(0x3193)
        {
            Weight = 1;
            Hue = 0x798;
            Name = "Tailoring Essence";
        }
        public TailoringEssence(Serial serial) : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0); // version 
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }

    public class TamingEssence : Item
    {
        [Constructable]
        public TamingEssence() : base(0x3193)
        {
            Weight = 1;
            Hue = 0x7a1;
            Name = "Taming Essence";
        }
        public TamingEssence(Serial serial) : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0); // version 
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }

    public class TinkeringEssence : Item
    {
        [Constructable]
        public TinkeringEssence() : base(0x3193)
        {
            Weight = 1;
            Hue = 0x07AA;
            Name = "Tinkering Essence";
        }
        public TinkeringEssence(Serial serial) : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0); // version 
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }
}