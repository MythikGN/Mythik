using Scripts.Mythik.Items.Craftables.Inscription;
using Scripts.Mythik.Items.Misc;
using Server;
using Server.Gumps;
using Server.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scripts.Mythik.Gumps
{
    public enum ReagentBookGumpPage
    {
        Regular = 0,
        Pagan,
    }

    public class ReagentBookGump : Gump
    {
        private Mobile m_Mobile;
        private ReagentBook m_Book;

        private int m_defValue;

        private static ReagentEntry[] m_Regular = new ReagentEntry[] {
                                                                         new ReagentEntry( 0, 0xF7A, "Black pearl" ),
                                                                         new ReagentEntry( 1, 0xF7B, "Bloodmoss" ),
                                                                         new ReagentEntry( 2, 0xF84, "Garlic" ),
                                                                         new ReagentEntry( 3, 0xF85, "Ginseng" ),
                                                                         new ReagentEntry( 4, 0xF86, "Mandrake root" ),
                                                                         new ReagentEntry( 5, 0xF88, "Nightshade" ),
                                                                         new ReagentEntry( 6, 0xF8D, "Spider's silk" ),
                                                                         new ReagentEntry( 7, 0xF8C, "Sulfurous ash" )
                                                                     };

        private static ReagentEntry[] m_Pagan = new ReagentEntry[] {
                                                                       new ReagentEntry( 8, 0xF78, "Bat wing" ),
                                                                       new ReagentEntry( 9, 0xF7D, "Daemon blood" ),
                                                                       new ReagentEntry( 10, 0xF80, "Daemon bone" ),
                                                                       new ReagentEntry( 11, 0xF87, "Eye of newt" ),
                                                                       new ReagentEntry( 12, 0xF8F, "Grave dust" ),
                                                                       new ReagentEntry( 13, 0xF8E, "Nox crystal" ),
                                                                       new ReagentEntry( 14, 0xF8A, "Pig iron" )
                                                                   };

        public ReagentBookGump(Mobile mobile, ReagentBook book, ReagentBookGumpPage page, int defValue) : base(150, 200)
        {
            mobile.CloseGump(typeof(ReagentBookGump));

            m_Mobile = mobile;
            m_Book = book;

            m_defValue = defValue;

            ReagentEntry[] list = m_Regular;

            if (page == ReagentBookGumpPage.Pagan)
                list = m_Pagan;

            AddPage(0);

            AddBackground(10, 10, 444, 439, 5054);
            AddImageTiled(18, 20, 444 - 17, 420, 2624);

            AddImageTiled(48, 64, 36, 20, 200);
            AddImageTiled(96, 64, 133, 20, 1416);
            AddImageTiled(231, 64, 80, 20, 200);
            AddImageTiled(313, 64, 75, 20, 1416);

            AddImageTiled(48, 90, 36, 210, 200);
            AddImageTiled(96, 90, 133, 210, 1416);
            AddImageTiled(231, 90, 80, 210, 200);
            AddImageTiled(313, 90, 75, 210, 1416);

            AddAlphaRegion(18, 20, 444 - 17, 420);

            AddImage(5, 5, 10460);
            AddImage(444 - 15, 5, 10460);
            AddImage(5, 424, 10460);
            AddImage(444 - 15, 424, 10460);

            AddHtml(255, 64, 200, 32, "<BASEFONT COLOR=\"#FFFFFF\">Held</BASEFONT>", false, false); // Held
            AddHtml(330, 64, 200, 32, "<BASEFONT COLOR=\"#FFFFFF\">Amount</BASEFONT>", false, false); // Amount

            if (page == ReagentBookGumpPage.Regular)
            {
                AddImage(49, 66, 0x25EA);
                AddButton(67, 66, 0x15E1, 0x15E5, GetButtonID(0, 1), GumpButtonType.Reply, 0);

                AddHtml(96, 64, 133, 32, "<BASEFONT COLOR=\"#FFFFFF\"><CENTER>Regular Reagents</CENTER></BASEFONT>", false, false); // Reagent
            }
            else
            {
                AddButton(49, 66, 0x15E3, 0x15E7, GetButtonID(0, 2), GumpButtonType.Reply, 0);
                AddImage(67, 66, 0x25E6);

                AddHtml(96, 64, 133, 32, "<BASEFONT COLOR=\"#FFFFFF\"><CENTER>Pagan Reagent</CENTER></BASEFONT>", false, false); // Reagent
            }

            for (int i = 0, j = 0; i < list.Length; ++i, ++j)
            {
                ReagentEntry entry = list[i];

                int count = book.Reagents[entry.Index];
                int y = 100 + (25 * i);

                AddItem(45, y + 5, entry.ItemID);
                AddLabel(100, y, 1153, entry.Name);
                AddLabel(235, y, 1153, count.ToString());

                if (count > 0)
                {
                    AddTextBox(325, y, 50, 20, entry.Index, defValue.ToString());
                    AddButton(388, y - 2, 4005, 4007, GetButtonID(1, entry.Index), GumpButtonType.Reply, 0);
                }
            }
        }

        public override void OnResponse(Server.Network.NetState sender, RelayInfo Info)
        {
            int val = Info.ButtonID - 1;

            if (val < 0)
                return;

            int type = val % 10;
            int index = val / 10;

            switch (type)
            {
                case 0:
                    {
                        switch (index)
                        {
                            case 1: { m_Mobile.SendGump(new ReagentBookGump(m_Mobile, m_Book, ReagentBookGumpPage.Pagan, m_defValue)); break; }
                            case 2: { m_Mobile.SendGump(new ReagentBookGump(m_Mobile, m_Book, ReagentBookGumpPage.Regular, m_defValue)); break; }
                        }

                        break;
                    }
                case 1:
                    {
                        if (index >= 0 && index <= 14)
                        {
                            int count = Utility.ToInt32(Info.GetTextEntry(index).Text);

                            if (count > 0)
                            {
                                if (count <= m_Book.Reagents[index])
                                {
                                    BaseReagent reagent;
                                    bool added = false;

                                    try { reagent = (BaseReagent)Activator.CreateInstance(GetReagent(index)); }
                                    catch { reagent = null; }

                                    if (reagent != null)
                                    {
                                        reagent.Amount = count;

                                        if ((added = m_Mobile.AddToBackpack(reagent)))
                                            m_Mobile.SendMessage("The reagents have been added into your backpack.");
                                        else
                                            m_Mobile.SendMessage("The book was unable to give you the reagents. Your backpack may contain too many items.");

                                        if (!added)
                                            reagent.Delete();
                                        else
                                            m_Book.Reagents[index] -= count;

                                        m_Book.InvalidateProperties();
                                    }
                                }
                                else
                                {
                                    m_Mobile.SendMessage("The book does not contain that many of those reagents.");
                                }
                            }
                        }

                        break;
                    }
            }
        }

        public static Type GetReagent(int index)
        {
            switch (index)
            {
                case 0: return typeof(BlackPearl);
                case 1: return typeof(Bloodmoss);
                case 2: return typeof(Garlic);
                case 3: return typeof(Ginseng);
                case 4: return typeof(MandrakeRoot);
                case 5: return typeof(Nightshade);
                case 6: return typeof(SpidersSilk);
                case 7: return typeof(SulfurousAsh);
                case 8: return typeof(BatWing);
                case 9: return typeof(DaemonBlood);
                case 10: return typeof(DaemonBone);
                case 11: return typeof(EyeOfNewt);
                case 12: return typeof(GraveDust);
                case 13: return typeof(NoxCrystal);
                case 14: return typeof(PigIron);
            }

            return typeof(BlackPearl);
        }

        public void AddTextBox(int x, int y, int width, int height, int id, string def)
        {
            AddImageTiled(x - 2, y, width + 4, height - 2, 0xBBC);
            AddImageTiled(x - 1, y + 1, width + 2, height - 4, 2624);
            AddAlphaRegion(x - 1, y + 1, width + 2, height - 4);
            AddTextEntry(x, y, width - 2, height, 0x481, id, def);
        }

        public int GetButtonID(int type, int index)
        {
            return 1 + (index * 10) + type;
        }

        private class ReagentEntry
        {
            private int m_Index;
            private int m_ItemID;
            private string m_Name;

            public int Index { get { return m_Index; } }
            public int ItemID { get { return m_ItemID; } }
            public string Name { get { return m_Name; } }

            public ReagentEntry(int index, int itemID, string name)
            {
                m_Index = index;
                m_ItemID = itemID;
                m_Name = name;
            }
        }
    }
}
