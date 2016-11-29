using Server;
using Server.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scripts.Mythik.Items.Rares
{
    // Replace these with ML rare crafting mats?

    /// <summary>
    /// Thinking odds around 1:100 or 1:200 for like a level 1-2
    /// require 1-5 for an item?
    /// </summary>
    public class RareCraftingMaterial1 : Item
    {
        [Constructable]
        public RareCraftingMaterial1(int amount) : base(0x1F1D)
        {
            Name = "Rare Crafting Material 1";
        }
        [Constructable]
        public RareCraftingMaterial1(Serial serial) : base(serial)
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
    public class RareCraftingMaterial2 : Item
    {
        [Constructable]
        public RareCraftingMaterial2(int amount) : base(0x1F1D)
        {
            Name = "Rare Crafting Material 2";
        }
        [Constructable]
        public RareCraftingMaterial2(Serial serial) : base(serial)
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

    /// <summary>
    /// Thinking 1:50 or so, fairly common require 10-20 per item?
    /// </summary>
    public class UncommonCraftingMaterial1 : Item
    {
        [Constructable]
        public UncommonCraftingMaterial1(int amount) : base(0x1F1D)
        {
            Name = "Uncommon Crafting Material 1";
        }
        [Constructable]
        public UncommonCraftingMaterial1(Serial serial) : base(serial)
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
    public class UncommonCraftingMaterial2 : Item
    {
        [Constructable]
        public UncommonCraftingMaterial2(int amount) : base(0x1F1D)
        {
            Name = "Uncommon Crafting Material 2";
        }
        [Constructable]
        public UncommonCraftingMaterial2(Serial serial) : base(serial)
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
