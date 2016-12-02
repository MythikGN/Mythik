using Server;
using Server.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scripts.Mythik.Items.Rares
{
    public class RuneBookChargeDeed5: BaseRuneBookChargeDeed
    {
        [Constructable]
        public RuneBookChargeDeed5() : base(5)
        {

        }
        [Constructable]
        public RuneBookChargeDeed5(Serial serial) : base(serial)
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
    public class RuneBookChargeDeed10 : BaseRuneBookChargeDeed
    {
        [Constructable]
        public RuneBookChargeDeed10() : base(10)
        {

        }
        [Constructable]
        public RuneBookChargeDeed10(Serial serial) : base(serial)
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
    public class RuneBookChargeDeed15 : BaseRuneBookChargeDeed
    {
        [Constructable]
        public RuneBookChargeDeed15() : base(15)
        {

        }
        [Constructable]
        public RuneBookChargeDeed15(Serial serial) : base(serial)
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
    public class RuneBookChargeDeed20 : BaseRuneBookChargeDeed
    {
        [Constructable]
        public RuneBookChargeDeed20() : base(20)
        {

        }
        [Constructable]
        public RuneBookChargeDeed20(Serial serial) : base(serial)
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


    public abstract class BaseRuneBookChargeDeed : Item, IUniqueItem
    {
        private int m_ChargeAmount = 0;

        public RareLevel UniqueLevel
        {
            get
            {
                return RareLevel.Rare;
            }
        }

        [Constructable]
        public BaseRuneBookChargeDeed(int amount) : base(0x1F1D)
        {
            m_ChargeAmount = amount;
            Name = string.Format("+{0} Runebook Charge Crystal", m_ChargeAmount);
        }
        [Constructable]
        public BaseRuneBookChargeDeed(Serial serial) : base(serial)
        {

        }
        public override void OnDoubleClick(Mobile from)
        {
            from.SendAsciiMessage("Select a Runebook to imbue with the crystals power.");
            from.BeginTarget(1, false, Server.Targeting.TargetFlags.None, OnTarget);
        }

        private void OnTarget(Mobile from, object targeted)
        {
            if (targeted == null)
                return;
            var rb = targeted as Runebook;
            if(rb == null)
            {
                from.SendAsciiMessage("That is not a Runebook.");
                return;
            }
            if(rb.MaxCharges > 1000)
            {
                from.SendAsciiMessage("You cannot imbue that Runebook further.");
                return;
            }
            rb.MaxCharges += m_ChargeAmount;
            if (rb.MaxCharges > 1000)
                rb.MaxCharges = 1000;
            from.SendAsciiMessage("You imbue the Runebook with power.");
            this.Consume();
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write((int)1);
            writer.Write(m_ChargeAmount);
        }
        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            var version = reader.ReadInt();
            m_ChargeAmount = reader.ReadInt();
        }
    }
}
