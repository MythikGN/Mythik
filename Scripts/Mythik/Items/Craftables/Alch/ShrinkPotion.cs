using Server;
using Server.Items;
using Server.Mobiles;
using Server.Network;
using Server.Targeting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scripts.Mythik.Items.Craftables.Alch
{
    //Thanks to Lux for this one too.
    public class ShrinkPotion : BasePotion
    {
        public override int LabelNumber { get { return 0; } }
        [Constructable]
        public ShrinkPotion() : this(1)
        {
            
        }

        [Constructable]
        public ShrinkPotion(int amount) : base(0x0F0E, PotionEffect.Shrink)
        {
            Stackable = true;
            Amount = amount;
            Name = "shrink potion";
        }

        public ShrinkPotion(Serial serial) : base(serial)
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

        public override void OnDoubleClick(Mobile from)
        {
            if (!Movable)
                return;

            if (from.InRange(this.GetWorldLocation(), 1))
            {
                if (BasePotion.HasFreeHand(from))
                {
                    from.BeginTarget(2, false, TargetFlags.None, new TargetCallback(OnTarget));
                    from.SendMessage("Target the creature to shrink.");
                }
                else
                {
                    from.SendMessage("You must have a free hand to use this potion.");
                }
            }
            else
            {
                from.SendLocalizedMessage(502138); // That is too far away for you to use
            }
        }

        public virtual void OnTarget(Mobile from, object obj)
        {
            if (Deleted)
                return;

            if (obj is BaseCreature)
            {
                BaseCreature creature = (BaseCreature)obj;

                if (from.InRange(creature.Location, 2))
                {
                    if (Shrink(from, creature))
                        Drink(from);
                }
                else
                {
                    from.SendLocalizedMessage(500295);
                }
            }
            else
            {
                from.SendMessage("You cannot shrink that!");
            }
        }

        public override void Drink(Mobile from)
        {
            from.AddToBackpack(new Bottle());

            if (Amount > 1)
                Amount -= 1;
            else
                this.Delete();
        }


    

        public static bool Shrink(Mobile from, object target)
        {
            if (from == target)
            {
                from.SendMessage("You cannot shrink yourself!");
                return false;
            }
            else if (!(target is BaseCreature))
            {
                from.SendMessage("You cannot shrink that!");
                return false;
            }
            else
            {
                BaseCreature creature = (BaseCreature)target;

                if (creature.ControlMaster != from)
                {
                    from.SendMessage("You can only shrink creatures that you control.");
                    return false;
                }
                else if (!creature.Tamable)
                {
                    from.SendMessage("You can only shrink creature that are tameable.");
                    return false;
                }
                else if (creature.IsDeadPet)
                {
                    from.SendMessage("You cannot shrink dead creatures!");
                    return false;
                }
                else if (creature.Summoned)
                {
                    from.SendMessage("You cannot shrink summoned creatures!");
                    return false;
                }
                else if (creature.Combatant != null && creature.InRange(creature.Combatant, 12) && creature.Map == creature.Combatant.Map)
                {
                    from.SendMessage("You cannot shrink a creature while it is in combat.");
                    return false;
                }
                else if ((creature is PackLlama || creature is PackHorse || creature is Beetle) && (creature.Backpack != null && creature.Backpack.Items.Count > 0))
                {
                    from.SendMessage("You must unload the creature first.");
                    return false;
                }

                ShrunkenCreature shrunk = new ShrunkenCreature(from, creature);

                if (!from.AddToBackpack(shrunk))
                {
                    shrunk.MoveToWorld(from.Location, from.Map);
                    from.SendMessage("Your backpack is full so the shrunken creature falls to the ground!");
                }

                creature.SetControlMaster(null);

                creature.IsStabled = true;
                creature.Internalize();

                return true;
            }
        }
    }
    public class ShrunkenCreature : Item
    {
        private Mobile m_Owner;
        private BaseCreature m_Creature;

        [CommandProperty(AccessLevel.GameMaster)]
        public Mobile Owner
        {
            get { return m_Owner; }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public BaseCreature Creature
        {
            get { return m_Creature; }
        }

        [Constructable]
        public ShrunkenCreature(Mobile owner, BaseCreature creature) : base(ShrinkTable.Lookup(creature.Body))
        {
            m_Owner = owner;
            m_Creature = creature;
            Name = SphereUtils.GenericComputeName(this);
            Hue = creature.Hue;
        }
        public override void OnSingleClick(Mobile from)
        {
            base.OnSingleClick(from);
            from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0x803, 3, "", "[ " + m_Creature.HitsMax + " Hits ]"));

        }

        public ShrunkenCreature(Serial serial) : base(serial)
        {
        }

        public override void AddNameProperties(ObjectPropertyList list)
        {
            list.Add(m_Creature.Name);
        }

        public override void OnDoubleClick(Mobile from)
        {
            if (!Movable)
                return;

            if (!from.CheckAlive())
            {
                from.SendLocalizedMessage(1060190); // You cannot do that while dead!
                return;
            }
            else if (from.InRange(this.GetWorldLocation(), 2) == false)
            {
                from.SendLocalizedMessage(500486); // That is too far away.
                return;
            }
            else if (m_Creature == null || m_Creature.Deleted)
            {
                from.SendMessage("The magic was unable bring back the shrunken creature.");

                Delete();
                return;
            }
            else if ((from.Followers + m_Creature.ControlSlots) > from.FollowersMax)
            {
                from.SendLocalizedMessage(1049645); // You have too many followers to summon that creature.
                return;
            }
            else
            {

                if (!m_Creature.Owners.Contains(from))
                    m_Creature.Owners.Add(from);

                m_Creature.SetControlMaster(from);

                m_Creature.Location = from.Location;
                m_Creature.Map = from.Map;

                m_Creature.ControlTarget = from;
                m_Creature.ControlOrder = OrderType.Follow;

                if (m_Owner != from)
                {
                    if (m_Creature.IsBondable)
                        m_Creature.IsBonded = false;
                }

                if (!m_Creature.IsBonded)
                    m_Creature.BondingBegin = DateTime.UtcNow;

                Delete();
            }
        }


        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0); // version

            writer.Write(m_Owner);
            writer.Write(m_Creature);

        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();

            switch (version)
            {
                case 0:
                    {
                        m_Owner = reader.ReadMobile();
                        m_Creature = (BaseCreature)reader.ReadMobile();
                        break;
                    }

            }

            if (m_Creature != null)
                m_Creature.IsStabled = true;
        }
    }


}
