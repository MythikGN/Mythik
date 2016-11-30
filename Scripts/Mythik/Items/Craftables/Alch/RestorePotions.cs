using Server;
using Server.Items;
using Server.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scripts.Mythik.Items.Craftables.Alch
{
    public class RestorePotion : BaseRestorePotion
    {
        public override int MinHeal { get { return 5; } }
        public override int MaxHeal { get { return 10; } }
        public override double Delay { get { return 14.0; } }

        public override Tuple<int, int> StamHeal
        {
            get
            {
                return new Tuple<int, int>(10, 25);
            }
        }

        [Constructable]
        public RestorePotion() : base(PotionEffect.Restore)
        {
          }

        public RestorePotion(Serial serial) : base(serial)
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
    public class GreaterRestorePotion : BaseRestorePotion
    {
        public override int MinHeal { get { return 15; } }
        public override int MaxHeal { get { return 30; } }
        public override double Delay { get { return 14.0; } }

        public override Tuple<int, int> StamHeal
        {
            get
            {
                return new Tuple<int, int>(25, 35);
            }
        }

        [Constructable]
        public GreaterRestorePotion() : base(PotionEffect.RestoreGreater)
        {
        }

        public GreaterRestorePotion(Serial serial) : base(serial)
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


    public abstract class BaseRestorePotion : BasePotion
    {
        public abstract int MinHeal { get; }
        public abstract int MaxHeal { get; }

        public abstract Tuple<int,int> StamHeal { get; }
        public abstract double Delay { get; }
        public override int LabelNumber { get { return 0; } }
        public BaseRestorePotion(PotionEffect effect) : base(0x0F0E, effect)
        {
            switch (effect)
            {
                case PotionEffect.Restore:
                    Name = "restore potion";
                    break;
                case PotionEffect.RestoreGreater:
                    Name = "greater restore potion";
                    break;
            }
        }

        public BaseRestorePotion(Serial serial) : base(serial)
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

        public void DoRestore(Mobile from)
        {
            int min = Scale(from, MinHeal);
            int max = Scale(from, MaxHeal);
            var newHits = Utility.RandomMinMax(min, max) + from.Hits;
            from.Hits = Math.Min(newHits, from.HitsMax);


            min = Scale(from, StamHeal.Item1);
            max = Scale(from, StamHeal.Item2);
            var newStam = Utility.RandomMinMax(min, max) + from.Stam;
            from.Stam = Math.Min(newStam, from.StamMax);

        }

        public override void Drink(Mobile from)
        {
            if (from.Hits < from.HitsMax || from.Stam < from.StamMax)
            {
                if (from.BeginAction(typeof(BasePotion)))
                {
                    DoRestore(from);

                    BasePotion.PlayDrinkEffect(from);

                    if (!Server.Engines.ConPVP.DuelContext.IsFreeConsume(from))
                        this.Consume();

                    Timer.DelayCall(TimeSpan.FromSeconds(Delay), new TimerStateCallback(ReleaseManaLock), from);
                }
                else
                {
                    from.NonlocalOverheadMessage(MessageType.Regular, 0x22, true, "You must wait before using another restore potion.");
                    //from.LocalOverheadMessage(MessageType.Regular, 0x22, 500235); // You must wait 10 seconds before using another healing potion.
                }

            }
            else
            {
                from.NonlocalOverheadMessage(MessageType.Regular, 0x22, true, "You decide against drinking this potion, as you are already at full health and stamina.");
                //from.SendLocalizedMessage(1049547); // 
            }
        }

        private static void ReleaseManaLock(object state)
        {
            ((Mobile)state).EndAction(typeof(BasePotion));
        }
    }
}
