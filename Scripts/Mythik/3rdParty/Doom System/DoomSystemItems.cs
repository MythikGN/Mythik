using Server.Items;
using Server.Mobiles;
using Server.Engines.PartySystem;

namespace Server.Events.DoomSystem
{
    #region DoomPuzzle
    public class TeleportSpot : Item
    {
        public TeleportSpot() : base(6178)
        {
            Movable = false;
            Hue = 375;
        }

        public TeleportSpot(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); }
    }

    public class DoomStatue : Item
    {
        public DoomStatue(bool south) : base(south ? 4824 : 4825)
        {
            Movable = false;
            Hue = 706;
        }

        public DoomStatue(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); }
    }

    public class DoomLever : Item
    {
        public int ID;
        private bool m_Pressed;

        public bool Pressed
        {
            get
            {
                return m_Pressed;
            }

            set
            {
                if(m_Pressed && value == false)
                    ItemID = 0x108C;

                else if(!m_Pressed && value == true)
                    ItemID = 0x108E;

                m_Pressed = value;
            }
        }   

        public DoomLever(int LeverID) : base(0x108C)
        {
            ID = LeverID;
            Movable = false;
            Hue = 437;
        }

        public override void OnDoubleClick(Mobile from)
        {
            Party party = LeverPuzzle.Party;

            if (from.Location == LeverPuzzle.m_PlayerLocations[ID] && !m_Pressed)
            {
                if (party != null)
                {
                        if (Party.Get(from) == party)
                        {
                            PublicOverheadMessage(0, 0, false, "*Click*");
                            Pressed = true;
                            LeverPuzzle.LeverSwitched(ID);
                        }

                        else
                            from.SendLocalizedMessage(1062054, party.Leader.Name); //You are not in a party with ~1_NAME~ and can therefore not assist with the puzzle at this time.
                }

                else
                {
                    LeverPuzzle.Party = Party.Get(from);
                    PublicOverheadMessage(0, 0, false, "*Click*");
                    Pressed = true;
                    LeverPuzzle.LeverSwitched(ID);
                }
            }
        }

        public DoomLever(Serial serial) : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write(ID);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            ID = reader.ReadInt();
        }
    }
    #endregion

    #region SecretRoom
    public class Pedestal : Item
    {
        public Pedestal() : base(7978)
        {
            Movable = false;
        }

        public Pedestal(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); }
    }

    public class DoomBox : Item
    {
        public DoomBox() : base(3712)
        {
            Movable = false;
        }

        public override void OnDoubleClick(Mobile from)
        {
            if (from.InRange(Location, 1))
            {
                if (DoomSystem.SecretRoom.Wanderer == null)
                {
                    WandererOfTheVoid wanderer = new WandererOfTheVoid();
                    wanderer.MoveToWorld(new Point3D(470, 96, -1), Map.Malas);
                    wanderer.Combatant = from;
                }
            }
        }

        public DoomBox(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); }
    }

    public class DoomPorter : Item
    {
        public DoomPorter(int itemID, int hue)  : base(itemID)
        {
            Hue = hue;
            Movable = false;
        }

        public override bool OnMoveOver(Mobile from)
        {
            if (!(from is PlayerMobile))
                return true;

            from.MoveToWorld(new Point3D(349, 176, 14), Map.Malas);
            return false;
        }

        public DoomPorter(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); }
    }
    #endregion

    #region PoisonRoom
    public class Penta : Item
    {
        public Penta() : base(0x0FEA)
        {
            Movable = false;
        }

        public override bool HandlesOnMovement { get { return true; } }

        public override void OnMovement(Mobile m, Point3D oldLocation)
        {
            if (!DoomSystem.PoisonRoom.Active && DoomSystem.CanActivate(m) && Utility.InRange(Location, m.Location, 3))
            {
                DoomSystem.PoisonRoom.Activate();
            }
        }

        public Penta(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); }
    }
    #endregion
}