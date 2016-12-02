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

        public LimitedUseDyeTub(int ItemId) :base(ItemId)
        {

        }
        public LimitedUseDyeTub(Serial serial) : base(serial)
        {

        }

        public LimitedUseDyeTub()
        {
           
        }
        private short m_Uses;
        public short Uses { get { return m_Uses; } set { m_Uses = value; this.InvalidateProperties(); } }
        public short UsesMax { get; set; }
        public abstract RareLevel UniqueLevel { get; }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write(1);
            writer.Write(Uses);
            writer.Write(UsesMax);
        }
        public override void OnDoubleClick(Mobile from)
        {
            if (Uses <= 0)
            {
                from.SendAsciiMessage("The dye tub has no more charges.");
                return;
            }
            base.OnDoubleClick(from);
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
    class RareClothDyeTub : LimitedUseDyeTub
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

        public override RareLevel UniqueLevel
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

    class RareLeatherDyeTub : LimitedUseDyeTub
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
        public override RareLevel UniqueLevel
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



    /// <summary>
    /// Empty dye deed, when you fill it your actually duping the tub and setting its uses to 1 and graphic to a deed.
    /// </summary>
    class DyeDeed : LimitedUseDyeTub
    {
        [Constructable]
        public DyeDeed(Serial serial) : base(serial)
        {

        }
        public DyeDeed() : base(0xFF4)
        {
            Name = "empty dye deed";
            this.Uses = this.UsesMax = 0;
            this.Redyable = true;
        }

        public override RareLevel UniqueLevel
        {
            get
            {
                return RareLevel.Rare;
            }
        }


        public bool Dye(Mobile from, DyeTub sender)
        {

            if (sender is LimitedUseDyeTub)
            {
                this.Consume();
                var newDeed = (LimitedUseDyeTub)Activator.CreateInstance(sender.GetType());
                newDeed.Uses = newDeed.UsesMax = 1;
                newDeed.Name = "rare dye deed";
                newDeed.ItemID = 0xFF4;
                if (sender.AllowDyables)
                    newDeed.Name = "rare cloth dye deed";
                else if (sender.AllowFurniture)
                    newDeed.Name = "rare furniture dye deed";
                else if (sender.AllowLeather)
                    newDeed.Name = "rare leather dye deed";
                else if (sender.AllowRunebooks)
                    newDeed.Name = "rare runebook dye deed";
                from.PlaceInBackpack(newDeed);
            }
            else
            {
                from.SendAsciiMessage("You may only dye this with a limited use tub.");
                return false;
            }
            return true;
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
    /// These recharge 10 charges currently
    /// </summary>
    class DyeTubRecharger : Item
    {
        [Constructable]
        public DyeTubRecharger(Serial serial) : base(serial)
        {

        }
        public DyeTubRecharger() : base(0xFF4)
        {
            Name = "+10 Dye Tub Recharge";

        }
        public override void OnDoubleClick(Mobile from)
        {
            from.BeginTarget(2, false, Server.Targeting.TargetFlags.None, OnTargeted);
        }

        private void OnTargeted(Mobile from, object targeted)
        {
            if(targeted is LimitedUseDyeTub)
            {
                var tub = targeted as LimitedUseDyeTub;
                this.Consume();
                tub.Uses = (short)Math.Min(tub.Uses + 10, tub.UsesMax);
            }
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
