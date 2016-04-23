using Server;
using Server.Gumps;
using Server.Items;
using Server.Mobiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//BankStone Script by Lux
//Modified to not cost Gold on use by Lotus
namespace Scripts.Mythik.Gumps
{
    public class BankStoneGump : Gump
    {
        private Mobile m_Mobile;

        public BankStoneGump(Mobile from) : base(110, 90)
        {
            m_Mobile = from;
            m_Mobile.CloseGump(typeof(BankStoneGump));
            m_Mobile.CloseGump(typeof(BankStoneWithdrawGump));

            AddPage(0);

            AddBackground(0, 0, 420, 300, 5054);

            AddImageTiled(10, 10, 400, 20, 2624);
            AddAlphaRegion(10, 10, 400, 20);

            AddHtml(10, 10, 400, 20, "<BASEFONT COLOR=\"#FFFFFF\"><CENTER>Mythik Bank Menu</CENTER></BASEFONT>", false, false);

            AddImageTiled(10, 40, 400, 250, 2624);
            AddAlphaRegion(10, 40, 400, 250);

            AddImage(20, 40, 66);

            AddItem(30, 85, 3823);
            AddItem(25, 145, 0x1BF0);
            AddItem(30, 185, 0xE76);

            AddHtml(210, 60, 190, 20, String.Format("<BASEFONT COLOR=\"#FFFFFF\">Hail, {0}!</BASEFONT>", m_Mobile.Name), false, false);

            AddHtml(35, 85, 150, 40, String.Format("<BASEFONT COLOR=\"#FFFFFF\"><CENTER>* Balance *<BR>{0}</CENTER></BASEFONT>", m_Mobile.BankBox.TotalGold), false, false);
            AddHtml(35, 145, 150, 40, String.Format("<BASEFONT COLOR=\"#FFFFFF\"><CENTER>* Items *<BR>{0}</CENTER></BASEFONT>", m_Mobile.BankBox.TotalItems), false, false);
            AddHtml(35, 185, 150, 40, String.Format("<BASEFONT COLOR=\"#FFFFFF\"><CENTER>* Weight *<BR>{0}</CENTER></BASEFONT>", m_Mobile.BankBox.TotalWeight), false, false);

            AddHtml(270, 100, 100, 40, "<BASEFONT COLOR=\"#FFFFFF\">Open Bank Box<BR></BASEFONT>", false, false);
            AddButton(380, 102, 0x15E1, 0x15E5, 1, GumpButtonType.Reply, 0);

            AddHtml(270, 160, 100, 40, "<BASEFONT COLOR=\"#FFFFFF\">Deposit Gold<BR></BASEFONT>", false, false);
            AddButton(380, 162, 0x15E1, 0x15E5, 2, GumpButtonType.Reply, 0);

            AddHtml(270, 220, 100, 40, "<BASEFONT COLOR=\"#FFFFFF\">Withdraw Gold<BR></BASEFONT>", false, false);
            AddButton(380, 222, 0x15E1, 0x15E5, 3, GumpButtonType.Reply, 0);
        }

        public override void OnResponse(Server.Network.NetState sender, RelayInfo info)
        {
            m_Mobile.Frozen = false;

            int ButtonID = info.ButtonID;

            switch (ButtonID)
            {
                case 1:
                    {
                        m_Mobile.BankBox.Open();
                        break;
                    }
                case 2:
                    {
                        if (m_Mobile.Backpack.TotalGold > 50)
                        {
                            int amount = m_Mobile.Backpack.TotalGold;

                            if (m_Mobile.Backpack.ConsumeTotal(typeof(Gold), amount, true))
                            {
                                if (amount > 5000)
                                {
                                    BankCheck bc = new BankCheck(amount);
                                    m_Mobile.BankBox.DropItem(bc);

                                    m_Mobile.SendMessage("A bank check for {0} gold has been deposited into your bank box.", amount);
                                }
                                else
                                {
                                    Gold g = new Gold(amount);
                                    m_Mobile.BankBox.DropItem(g);

                                    m_Mobile.SendMessage("{0} gold pieces have been desposited into your bank box", amount);
                                }
                            }
                        }
                        else
                        {
                            m_Mobile.SendMessage("You must despoit gold in amounts higher then 50.");
                        }

                        break;
                    }
                case 3:
                    {
                        if (m_Mobile.BankBox.TotalGold > 0)
                        {
                            m_Mobile.Frozen = true;
                            m_Mobile.SendGump(new BankStoneWithdrawGump(m_Mobile));
                        }
                        else
                        {
                            m_Mobile.SendMessage("You do not have any gold in your bank box.");
                        }
                        break;
                    }
            }
        }
    }

    public class BankStoneWithdrawGump : Gump
    {
        private Mobile m_Mobile;

        public BankStoneWithdrawGump(Mobile from) : base(150, 185)
        {
            m_Mobile = from;
            m_Mobile.CloseGump(typeof(BankStoneGump));
            m_Mobile.CloseGump(typeof(BankStoneWithdrawGump));

            AddPage(0);

            AddBackground(0, 0, 340, 110, 5054);

            AddImageTiled(10, 10, 320, 20, 2624);
            AddAlphaRegion(10, 10, 320, 20);

            AddHtml(15, 10, 320, 20, "<BASEFONT COLOR=\"#FFFFFF\"><CENTER>Mythik Bank Withdraw Menu</CENTER></BASEFONT>", false, false);

            AddImageTiled(10, 40, 320, 60, 2624);
            AddAlphaRegion(10, 40, 320, 60);

            AddLabel(30, 40, 0x481, "How much gold would you like to withdraw?");

            TextEntry(75, 70, 180, 20, 1, null);

            AddButton(300, 72, 0x15E1, 0x15E5, 1, GumpButtonType.Reply, 0);
        }

        public override void OnResponse(Server.Network.NetState sender, RelayInfo info)
        {
            m_Mobile.Frozen = false;

            int ButtonID = info.ButtonID;

            switch (ButtonID)
            {
                case 1:
                    {
                        int amount = Utility.ToInt32(info.GetTextEntry(1).Text);

                        if (amount > 0)
                        {
                            if (m_Mobile.BankBox.TotalGold >= amount)
                            {
                                if (amount > 5000)
                                {
                                    BankCheck bc = new BankCheck(amount);
                                    m_Mobile.Backpack.DropItem(bc);

                                    m_Mobile.SendMessage("A check for {0} gold pieces has been dropped in your back pack.", amount);
                                }
                                else
                                {
                                    Gold g = new Gold(amount);
                                    m_Mobile.Backpack.DropItem(g);

                                    m_Mobile.SendMessage("{0} gold pieces have been dropped in your back pack", amount);
                                }
                            }
                            else
                            {
                                m_Mobile.SendMessage("You do not have that much gold in your bank box.");
                            }
                        }
                        else
                        {
                            m_Mobile.SendMessage("You must enter a valid number.");
                        }

                        break;
                    }
            }
        }

        private void TextEntry(int x, int y, int width, int height, int id, string initial)
        {
            AddImageTiled((x - 2), y, (width + 4), (height - 2), 0xBBC);
            AddImageTiled((x - 1), (y + 1), (width + 2), (height - 4), 2624);
            AddAlphaRegion((x - 1), (y + 1), (width + 2), (height - 4));
            AddTextEntry(x, y, width - 1, height, 1153, id, initial);
        }
    }
}
