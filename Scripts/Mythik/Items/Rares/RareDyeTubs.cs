﻿using Server;
using Server.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scripts.Mythik.Items.Rares
{

    abstract class LimitedUseDyeTub : DyeTub
    {


        public LimitedUseDyeTub(Serial serial) : base(serial)
        {

        }

        public LimitedUseDyeTub()
        {
        }

        public short Uses { get; set; }
        public short UsesMax { get; set; }
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
    }
}