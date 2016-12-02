using Scripts.Mythik.Items.Rares;
using Scripts.Mythik.Items.Stones;
using Server;
using Server.Gumps;
using Server.Items;
using Server.Misc;
using Server.Mobiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scripts.Mythik.Gumps
{
    public class ItemStoneGump : Gump
    {
        private Mobile m_Mobile;
        private ItemStone m_Stone;

        private ItemStoneGroup[] m_Groups = new ItemStoneGroup[]
            {
                new ItemStoneGroup( "Miscellneous", new ItemStoneInfo[]
                                                        {
                                                            new ItemStoneInfo( typeof( Bag ), "Bag", 300, 0, false ),
															//new ItemStoneInfo( typeof( BookDyeTub ), "Book Dye Tub", 250000, 0, false ),
															//new ItemStoneInfo( typeof( BroadcastTicket ), "Broadcast Ticket", 5000, 0, true ),
															new ItemStoneInfo( typeof( DyeTubRecharger ), "+10 Dyetub Charger", 10000, 0, false ),
															//new ItemStoneInfo( typeof( FullSpellBook ), "Full Spellbook", 5000, 0, false ),
                                                            new ItemStoneInfo(typeof(DyeDeed),"Empty Dye Deed",5000,0,false),
															new ItemStoneInfo( typeof( Runebook ), "Runebook", 20000, 0, false )
															//new ItemStoneInfo( typeof( ShipRune ), "Ship Rune", 200, 0, false ),
															
														} ),
                new ItemStoneGroup( "Reagents", new ItemStoneInfo[]
                                                        {
                                                            new ItemStoneInfo( typeof( Bloodmoss ), "Bloodmoss", 10, 0, true ),
                                                            new ItemStoneInfo( typeof( BlackPearl ), "Black Pearl", 10, 0, true ),
                                                            new ItemStoneInfo( typeof( Garlic ), "Garlic", 10, 0, true ),
                                                            new ItemStoneInfo( typeof( Ginseng ), "Ginseng", 10, 0, true ),
                                                            new ItemStoneInfo( typeof( MandrakeRoot ), "Mandrake Root", 10, 0, true ),
                                                            new ItemStoneInfo( typeof( Nightshade ), "Nightshade", 10, 0, true ),
                                                            new ItemStoneInfo( typeof( SulfurousAsh ), "Sulfurous Ash", 10, 0, true ),
                                                            new ItemStoneInfo( typeof( SpidersSilk  ), "Spider's Silk", 10, 0, true )
                                                        } ),
                new ItemStoneGroup( "Resources", new ItemStoneInfo[]
                                                        {
                                                            new ItemStoneInfo( typeof( Arrow ), "Arrows", 40, 0, true ),
                                                            new ItemStoneInfo( typeof( Bandage ), "Bandages", 10, 0, true ),
                                                            new ItemStoneInfo( typeof( Bottle ), "Bottles", 60, 0, true ),
                                                            new ItemStoneInfo( typeof( Bolt ), "Bolts", 20, 0, true ),
                                                            new ItemStoneInfo( typeof( Feather ), "Feathers", 10, 0, true ),
                                                            new ItemStoneInfo( typeof( BlankMap ), "Blank Maps", 50, 0, true ),
                                                            new ItemStoneInfo( typeof( BlankScroll ), "Blank Scrolls", 50, 0, true ),
                                                            new ItemStoneInfo( typeof( Wool ), "Wool", 100, 0, true )
                                                        } ),
                new ItemStoneGroup( "Tools", new ItemStoneInfo[]
                                                        {
                                                            new ItemStoneInfo( typeof( Hatchet ), "Hatchet", 100, 0, false ),
                                                            new ItemStoneInfo( typeof( Pickaxe ), "Pickaxe", 100, 0, false ),
                                                            new ItemStoneInfo( typeof( MortarPestle  ), "Mortar and Pestle", 100, 0, false ),
                                                            new ItemStoneInfo( typeof( Saw ), "Saw", 100, 0, false ),
                                                            new ItemStoneInfo( typeof( SewingKit ), "Sewing Kit", 100, 0, false ),
                                                            new ItemStoneInfo( typeof( SmithHammer ), "Smith's Hammer", 100, 0, false ),
                                                            new ItemStoneInfo( typeof( TinkerTools ), "Tinker's Tools", 100, 0, false )

                                                        } )
            };

        private ItemStoneGroup m_Selected;

        public ItemStoneGump(Mobile from, ItemStone stone) : this(from, stone, null, null)
        {
        }

        public ItemStoneGump(Mobile from, ItemStone stone, ItemStoneGroup[] usergroup, ItemStoneGroup selected) : base(155, 50)
        {
            m_Mobile = from;
            m_Stone = stone;

            if (usergroup != null)
                m_Groups = usergroup;

            m_Selected = selected;

            m_Mobile.CloseGump(typeof(ItemStoneGump));

            AddPage(0);

            int headerSpace = 65;
            int bodySpace = (m_Groups.Length * 25);

            if (selected != null)
                bodySpace += (selected.Items.Length * 20);

            int footerSpace = 110;

            int totalHeight = headerSpace + bodySpace + footerSpace;

            AddBackground(0, 0, 330, totalHeight, 5054);

            AddImageTiled(10, 10, 310, 20, 2624);
            AddAlphaRegion(10, 10, 310, 20);

            AddHtml(10, 10, 310, 20, "<BASEFONT COLOR=\"#FFFFFF\"><CENTER>Mythik Item Menu</CENTER></BASEFONT>", false, false);

            AddImageTiled(10, 35, 310, 20, 2624);
            AddAlphaRegion(10, 35, 310, 20);

            AddHtml(195, 35, 60, 20, "<BASEFONT COLOR=\"#FFFFFF\"><CENTER>Amount</CENTER></BASEFONT>", false, false);
            AddHtml(260, 35, 60, 20, "<BASEFONT COLOR=\"#FFFFFF\"><CENTER>Price</CENTER></BASEFONT>", false, false);

            AddImageTiled(10, 60, 180, bodySpace, 2624);
            AddAlphaRegion(10, 60, 180, bodySpace);

            AddImageTiled(195, 60, 60, bodySpace, 2624);
            AddAlphaRegion(195, 60, 60, bodySpace);

            AddImageTiled(260, 60, 60, bodySpace, 2624);
            AddAlphaRegion(260, 60, 60, bodySpace);

            AddImageTiled(10, headerSpace + bodySpace, 310, 75, 2624);
            AddAlphaRegion(10, headerSpace + bodySpace, 310, 75);

            AddImageTiled(10, headerSpace + bodySpace + 80, 180, 20, 2624);
            AddAlphaRegion(10, headerSpace + bodySpace + 80, 180, 20);

            AddImageTiled(195, headerSpace + bodySpace + 80, 125, 20, 2624);
            AddAlphaRegion(195, headerSpace + bodySpace + 80, 125, 20);

            int y = 65;

            for (int i = 0; i < m_Groups.Length; ++i)
            {
                ItemStoneGroup group = m_Groups[i];

                if (group != selected)
                    AddButton(15, y + 2, 0x15E1, 0x15E5, GetButtonID(0, i), GumpButtonType.Reply, 0);
                else
                    AddLabel(15, y, 0x1153, "*");

                AddHtml(35, y, 180, 25, String.Format("<BASEFONT COLOR=\"#FFFFFF\">{0}</BASEFONT>", group.Name), false, false);

                if (group == selected)
                {
                    for (int j = 0; j < group.Items.Length; ++j)
                    {
                        y += 20;

                        ItemStoneInfo info = group.Items[j];

                        AddLabel(40, y, 0x481, info.Name);
                        TextEntry(200, y, 50, 20, j, info.Amount.ToString());
                        AddLabel(265, y, 0x481, String.Format("{0}", info.Price + m_Stone.Markup));
                    }
                }

                y += 25;
            }

            int price = GetCost();
            int tax = GetTax(price);
            int total = price + tax;

            AddHtml(200, headerSpace + bodySpace, 110, 20, String.Format("<BASEFONT COLOR=\"#FFFFFF\">Cost: {0}</CENTER></BASEFONT>", price), false, false);
            AddHtml(200, headerSpace + bodySpace + 20, 110, 20, String.Format("<BASEFONT COLOR=\"#FFFFFF\">Tax: {0}</CENTER></BASEFONT>", tax), false, false);
            AddHtml(200, headerSpace + bodySpace + 55, 110, 20, String.Format("<BASEFONT COLOR=\"#FFFFFF\">Total: {0}</CENTER></BASEFONT>", total), false, false);

            AddHtml(15, headerSpace + bodySpace + 80, 170, 20, String.Format("<BASEFONT COLOR=\"#FFFFFF\">Tax: {0}%    Markup: +{1}</BASEFONT>", m_Stone.Tax, m_Stone.Markup), false, false);

            AddHtml(230, headerSpace + bodySpace + 80, 70, 20, "<BASEFONT COLOR=\"#FFFFFF\"><CENTER>Purchase</CENTER></BASEFONT>", false, false);
            AddButton(300, headerSpace + bodySpace + 82, 0x15E1, 0x15E5, GetButtonID(0, 100), GumpButtonType.Reply, 0);

            AddHtml(35, headerSpace + bodySpace, 155, 20, "<BASEFONT COLOR=\"#FFFFFF\">50 Reagents</BASEFONT>", false, false);
            AddButton(15, headerSpace + bodySpace + 2, 0x15E1, 0x15E5, GetButtonID(0, 101), GumpButtonType.Reply, 0);

            AddHtml(35, headerSpace + bodySpace + 20, 155, 20, "<BASEFONT COLOR=\"#FFFFFF\">100 Reagents</BASEFONT>", false, false);
            AddButton(15, headerSpace + bodySpace + 22, 0x15E1, 0x15E5, GetButtonID(0, 102), GumpButtonType.Reply, 0);
        }

        public int GetButtonID(int type, int index)
        {
            return 1 + (index * m_Groups.Length) + type;
        }

        public int GetCost()
        {
            int amount = 0;

            for (int i = 0; i < m_Groups.Length; ++i)
            {
                ItemStoneGroup group = m_Groups[i];

                for (int j = 0; j < group.Items.Length; ++j)
                {
                    ItemStoneInfo info = group.Items[j];

                    amount += ((info.Price + m_Stone.Markup) * info.Amount);
                }
            }

            return amount;
        }

        public int GetTax(int amount)
        {
            return (int)((amount * m_Stone.Tax) / 100);
        }

        private void TextEntry(int x, int y, int width, int height, int id, string initial)
        {
            AddImageTiled((x - 2), y, (width + 4), (height - 2), 0xBBC);
            AddImageTiled((x - 1), (y + 1), (width + 2), (height - 4), 2624);
            AddAlphaRegion((x - 1), (y + 1), (width + 2), (height - 4));
            AddTextEntry(x, y, width - 1, height, 1153, id, initial);
        }

        public override void OnResponse(Server.Network.NetState sender, RelayInfo info)
        {
            m_Mobile.Frozen = false;

            int buttonID = info.ButtonID - 1;

            if (buttonID != -1)
                UpdateTotals(info);

            int index = buttonID / m_Groups.Length;
            int type = buttonID % m_Groups.Length;

            switch (type)
            {
                case 0:
                    {
                        if (index > m_Groups.Length)
                        {
                            switch (index)
                            {
                                case 100:
                                    {
                                        int price = GetCost();
                                        int tax = GetTax(price);
                                        int total = price + tax;

                                        if (m_Mobile.Backpack.ConsumeTotal(typeof(Gold), total, true) || Banker.Withdraw(m_Mobile, total))
                                        {
                                            for (int i = 0; i < m_Groups.Length; ++i)
                                            {
                                                ItemStoneGroup group = m_Groups[i];

                                                for (int j = 0; j < group.Items.Length; ++j)
                                                {
                                                    ItemStoneInfo iteminfo = group.Items[j];

                                                    if (iteminfo.Amount > 0)
                                                    {
                                                        if (iteminfo.Stackable)
                                                        {
                                                            Item item = iteminfo.Create();
                                                            item.Amount = iteminfo.Amount;

                                                            GiveItem(m_Mobile, item);
                                                        }
                                                        else
                                                        {
                                                            for (int k = 0; k < iteminfo.Amount; ++k)
                                                            {
                                                                Item item = iteminfo.Create();
                                                                GiveItem(m_Mobile, item);
                                                            }
                                                        }
                                                    }
                                                }
                                            }

                                            m_Mobile.SendMessage("The items you have purchased may have been placed in either your backpack or bankbox!");
                                        }
                                        else
                                        {
                                            m_Mobile.SendMessage("You do not have {0} gold pieces to pay for these items.");
                                        }

                                        break;
                                    }
                                case 101:
                                    {
                                        SetReagents(50);
                                        m_Mobile.SendMessage("All reagents purchase amounts have been set to 50.");
                                        m_Mobile.SendGump(new ItemStoneGump(m_Mobile, m_Stone, m_Groups, m_Groups[1]));

                                        break;
                                    }
                                case 102:
                                    {
                                        SetReagents(100);
                                        m_Mobile.SendMessage("All reagents purchase amounts have been set to 100.");
                                        m_Mobile.SendGump(new ItemStoneGump(m_Mobile, m_Stone, m_Groups, m_Groups[1]));

                                        break;
                                    }
                            }
                        }
                        else
                        {
                            m_Mobile.SendGump(new ItemStoneGump(m_Mobile, m_Stone, m_Groups, m_Groups[index]));
                        }

                        break;
                    }
            }
        }

        private void UpdateTotals(RelayInfo info)
        {
            if (m_Selected != null)
            {
                ItemStoneInfo[] items = m_Selected.Items;

                for (int i = 0; i < items.Length; ++i)
                {
                    int amount = Utility.ToInt32(info.GetTextEntry(i).Text);

                    if (amount < 0)
                        amount = 0;

                    ItemStoneInfo iteminfo = items[i];
                    iteminfo.Amount = amount;
                }
            }
        }

        private void SetReagents(int amount)
        {
            ItemStoneGroup group = m_Groups[1];

            for (int i = 0; i < group.Items.Length; ++i)
            {
                ItemStoneInfo info = group.Items[i];
                info.Amount = amount;
            }
        }

        private void GiveItem(Mobile m, Item item)
        {
            if (!WeightOverloading.IsOverloaded(m))
                m.Backpack.DropItem(item);
            else
                m.BankBox.DropItem(item);
        }
    }

    public class ItemStoneGroup
    {
        private string m_Name;
        private ItemStoneInfo[] m_Items;

        public string Name { get { return m_Name; } }
        public ItemStoneInfo[] Items { get { return m_Items; } }

        public ItemStoneGroup(string name, ItemStoneInfo[] items)
        {
            m_Name = name;
            m_Items = items;
        }
    }

    public class ItemStoneInfo
    {
        private Type m_Type;
        private string m_Name;
        private int m_Price;
        private int m_Amount;
        private bool m_Stackable;

        public Type Type { get { return m_Type; } }
        public string Name { get { return m_Name; } }
        public int Price { get { return m_Price; } }
        public int Amount { get { return m_Amount; } set { m_Amount = value; } }
        public bool Stackable { get { return m_Stackable; } }

        public ItemStoneInfo(Type type, string name, int price, int amount, bool stackable)
        {
            m_Type = type;
            m_Name = name;
            m_Price = price;
            m_Amount = amount;
            m_Stackable = stackable;
        }

        public Item Create()
        {
            Item item;

            try
            {
                item = (Item)Activator.CreateInstance(m_Type);
            }
            catch
            {
                item = null;
            }

            return item;
        }
    }
}
