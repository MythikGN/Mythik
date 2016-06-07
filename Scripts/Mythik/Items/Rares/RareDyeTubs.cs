using Server;
using Server.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scripts.Mythik.Items.Rares
{

    abstract class LimitedUseDyeTub : DyeTub , IUniqueItem
    {


        public LimitedUseDyeTub(Serial serial) : base(serial)
        {

        }

        public LimitedUseDyeTub()
        {
           
        }
        private short m_Uses;
        public short Uses { get { return m_Uses; } set { m_Uses = value; this.InvalidateProperties(); } }
        public short UsesMax { get; set; }

        public RareLevel UniqueLevel
        {
            get
            {
                return RareLevel.Rare;
            }
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write(1);
            writer.Write(Uses);
            writer.Write(UsesMax);
        }
        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            var ver = reader.ReadInt();
            Uses = reader.ReadShort();
            UsesMax = reader.ReadShort();
        }
        public override void AddLootTypeProperty(ObjectPropertyList list)
        {
            base.AddLootTypeProperty(list);
            this.PropertyList.Add("Charges " + Uses + "/" + UsesMax);
        }

        
    }
    class RareClothDyeTub : LimitedUseDyeTub, IUniqueItem
    {
        public override bool Redyable
        {
            get
            {
                return false;
            }

            set
            {
                base.Redyable = value;
            }
        }

        public RareLevel UniqueLevel
        {
            get
            {
                return RareLevel.Rare;
            }
        }
        [Constructable]
        public RareClothDyeTub() : base()
        {
            this.DyedHue = MythikStaticValues.RareClothHues[Server.Utility.Random(MythikStaticValues.RareClothHues.Count() - 1)];
            this.Name = "Rare Cloth Dye Tub";
            this.Uses = this.UsesMax = 25;
        }
        public RareClothDyeTub(Serial serial) : base(serial)
        {

        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
        }
        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
        }
    }

    class RareLeatherDyeTub : LimitedUseDyeTub, IUniqueItem
    {
        public override bool Redyable
        {
            get
            {
                return false;
            }

            set
            {
                base.Redyable = value;
            }
        }

        public override bool AllowLeather
        {
            get
            {
                return true;
            }
        }
        public override bool AllowDyables
        {
            get
            {
                return false;
            }
        }
        public RareLevel UniqueLevel
        {
            get
            {
                return RareLevel.Rare;
            }
        }

        [Constructable]
        public RareLeatherDyeTub() : base()
        {
            this.DyedHue = MythikStaticValues.RareClothHues[Server.Utility.Random(MythikStaticValues.RareClothHues.Count() - 1)];
            this.Name = "Rare Leather Dye Tub";
            this.Uses = this.UsesMax = 25;
        }
        public RareLeatherDyeTub(Serial serial) : base(serial)
        {

        }
        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
        }
        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
        }
    }
}
