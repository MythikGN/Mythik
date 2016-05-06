using Scripts.Mythik.Gumps;
using Scripts.Mythik.Items.Misc;
using Server;
using Server.Items;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scripts.Mythik.Items.Craftables.Inscription
{
    //Thanks lux
    public class ReagentBook : Item
    {
        private int[] m_Reagents;

        public int BlackPearl { get { return m_Reagents[0]; } set { m_Reagents[0] = value; InvalidateProperties(); } }
        public int Bloodmoss { get { return m_Reagents[1]; } set { m_Reagents[1] = value; InvalidateProperties(); } }
        public int Garlic { get { return m_Reagents[2]; } set { m_Reagents[2] = value; InvalidateProperties(); } }
        public int Ginseng { get { return m_Reagents[3]; } set { m_Reagents[3] = value; InvalidateProperties(); } }
        public int MandrakeRoot { get { return m_Reagents[4]; } set { m_Reagents[4] = value; InvalidateProperties(); } }
        public int Nightshade { get { return m_Reagents[5]; } set { m_Reagents[5] = value; InvalidateProperties(); } }
        public int SpidersSilk { get { return m_Reagents[6]; } set { m_Reagents[6] = value; InvalidateProperties(); } }
        public int SulfurousAsh { get { return m_Reagents[7]; } set { m_Reagents[7] = value; InvalidateProperties(); } }

        public int BatWing { get { return m_Reagents[8]; } set { m_Reagents[8] = value; InvalidateProperties(); } }
        public int DaemonBlood { get { return m_Reagents[9]; } set { m_Reagents[9] = value; InvalidateProperties(); } }
        public int DaemonBone { get { return m_Reagents[10]; } set { m_Reagents[10] = value; InvalidateProperties(); } }
        public int EyeOfNewt { get { return m_Reagents[11]; } set { m_Reagents[11] = value; InvalidateProperties(); } }
        public int GraveDust { get { return m_Reagents[12]; } set { m_Reagents[12] = value; InvalidateProperties(); } }
        public int NoxCrystal { get { return m_Reagents[13]; } set { m_Reagents[13] = value; InvalidateProperties(); } }
        public int PigIron { get { return m_Reagents[14]; } set { m_Reagents[14] = value; InvalidateProperties(); } }

        public int[] Reagents { get { return m_Reagents; } set { m_Reagents = value; } }


        [Constructable]
        public ReagentBook(int amount) : base(0x2253)
        {
            this.Name = "reagent book";
            m_Reagents = new int[15];

            for (int i = 0; i < 15; ++i)
                m_Reagents[i] = amount;
        }

        public ReagentBook(Serial serial) : base(serial)
        {
        }

        public override void OnDoubleClick(Mobile from)
        {
            if (from.InRange(GetWorldLocation(), 1))
                from.SendGump(new ReagentBookGump(from, this, ReagentBookGumpPage.Regular, 0));

        }

        public override bool OnDragDrop(Mobile from, Item dropped)
        {
            if (dropped is BaseReagent)
            {
                return AddReagent(from, (BaseReagent)dropped, false);
            }
            else if (dropped is Container)
            {
                Container container = (Container)dropped;

                ArrayList toDelete = new ArrayList();
                bool added = false;

                for (int i = 0; i < container.Items.Count; ++i)
                {
                    Item item = (Item)container.Items[i];

                    if (item is BaseReagent)
                    {
                        if ((added = AddReagent(from, (BaseReagent)item, true)))
                            toDelete.Add(item);
                    }
                }

                if (added)
                    from.SendMessage("You add reagents from the container into the book.");

                for (int j = 0; j < toDelete.Count; ++j)
                    ((Item)toDelete[j]).Delete();

                if (container.Items.Count == 0)
                    return true;
            }
            else
            {
                from.SendLocalizedMessage(1042276); // You cannot drop that there.
            }

            return base.OnDragDrop(from, dropped);
        }

        public bool AddReagent(Mobile from, BaseReagent reagent, bool passive)
        {
            int index = GetIndex(reagent);

            int count = m_Reagents[index];
            int amount = reagent.Amount;

            int total = count + amount;

            if (count >= 50000)
            {
                if (!passive)
                    from.SendMessage("This book cannot hold any more of that reagent.");
            }
            else
            {
                bool action;

                if (total > 50000)
                {
                    int excess = total - 50000;
                    int toAdd = amount - excess;

                    m_Reagents[index] += toAdd;
                    reagent.Amount -= toAdd;

                    if (!passive)
                        from.SendMessage("You were only able to add {0} of the reagents.", toAdd);

                    action = false;
                }
                else
                {
                    m_Reagents[index] += amount;

                    if (!passive)
                        from.SendMessage("You add all of the reagents into the book.");

                    action = true;
                }

                InvalidateProperties();

                return action;
            }

            return false;
        }

        public static int GetIndex(BaseReagent reagent)
        {
            if (reagent is BlackPearl) { return 0; }
            else if (reagent is Bloodmoss) { return 1; }
            else if (reagent is Garlic) { return 2; }
            else if (reagent is Ginseng) { return 3; }
            else if (reagent is MandrakeRoot) { return 4; }
            else if (reagent is Nightshade) { return 5; }
            else if (reagent is SpidersSilk) { return 6; }
            else if (reagent is SulfurousAsh) { return 7; }

            else if (reagent is BatWing) { return 8; }
            else if (reagent is DaemonBlood) { return 9; }
            else if (reagent is DaemonBone) { return 10; }
            else if (reagent is EyeOfNewt) { return 11; }
            else if (reagent is GraveDust) { return 12; }
            else if (reagent is NoxCrystal) { return 13; }
            else if (reagent is PigIron) { return 14; }

            return 0;
        }

        public override void GetProperties(ObjectPropertyList list)
        {
            base.GetProperties(list);

            for (int i = 0; i < 15; ++i)
                if (m_Reagents[i] > 0)
                    list.Add(1080521 + i, "{0}", m_Reagents[i]);
        }


        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0); // version

            for (int i = 0; i < 15; ++i)
                writer.Write(m_Reagents[i]);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();

            switch (version)
            {
                case 0:
                    {
                        m_Reagents = new int[15];

                        for (int i = 0; i < 15; ++i)
                            m_Reagents[i] = reader.ReadInt();

                        break;
                    }
            }
        }
    }
}
