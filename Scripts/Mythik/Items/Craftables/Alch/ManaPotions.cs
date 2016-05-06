﻿using Server;
using Server.Items;
using Server.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scripts.Mythik.Items.Craftables.Alch
{
    public class ManaPotion : BaseManaPotion
    {
        public override int MinHeal { get { return 25; } }
        public override int MaxHeal { get { return 50; } }
        public override double Delay { get { return 10.0; } }

        [Constructable]
        public ManaPotion() : base(PotionEffect.Mana)
        {
        }

        public ManaPotion(Serial serial) : base(serial)
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
    public class TotalManaPotion : BaseManaPotion
    {
        public override int MinHeal { get { return 50; } }
        public override int MaxHeal { get { return 75; } }
        public override double Delay { get { return 10.0; } }

        [Constructable]
        public TotalManaPotion() : base(PotionEffect.ManaTotal)
        {
        }

        public TotalManaPotion(Serial serial) : base(serial)
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


    public abstract class BaseManaPotion : BasePotion
    {
        public abstract int MinHeal { get; }
        public abstract int MaxHeal { get; }
        public abstract double Delay { get; }

        public BaseManaPotion(PotionEffect effect) : base(0xF0C, effect)
        {
        }

        public BaseManaPotion(Serial serial) : base(serial)
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

        public void DoMana(Mobile from)
        {
            int min = Scale(from, MinHeal);
            int max = Scale(from, MaxHeal);
            var newMana = Utility.RandomMinMax(min, max) + from.Mana;
            from.Mana = Math.Min(newMana, from.ManaMax);
        }

        public override void Drink(Mobile from)
        {
            if (from.Mana < from.ManaMax)
            {
                    if (from.BeginAction(typeof(BaseHealPotion)))
                    {
                        DoMana(from);

                        BasePotion.PlayDrinkEffect(from);

                        if (!Server.Engines.ConPVP.DuelContext.IsFreeConsume(from))
                            this.Consume();

                        Timer.DelayCall(TimeSpan.FromSeconds(Delay), new TimerStateCallback(ReleaseHealLock), from);
                    }
                    else
                    {
                        from.NonlocalOverheadMessage(MessageType.Regular, 0x22, true, "You must wait before using another mana potion.");
                        //from.LocalOverheadMessage(MessageType.Regular, 0x22, 500235); // You must wait 10 seconds before using another healing potion.
                    }
                
            }
            else
            {
                from.NonlocalOverheadMessage(MessageType.Regular, 0x22, true, "You decide against drinking this potion, as you are already at full mana.");
                //from.SendLocalizedMessage(1049547); // 
            }
        }

        private static void ReleaseHealLock(object state)
        {
            ((Mobile)state).EndAction(typeof(BaseManaPotion));
        }
    }
}