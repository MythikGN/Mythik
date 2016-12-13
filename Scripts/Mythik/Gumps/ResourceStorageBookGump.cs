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


    public class ResourceStorageBookGump : Gump
    {
        private Mobile m_Mobile;
        private ResourceBook m_Book;

        private int m_defValue;

     
        public ResourceStorageBookGump(Mobile mobile, ResourceBook book, int defValue) : base(150, 200)
        {
            mobile.CloseGump(typeof(ResourceStorageBookGump));

            m_Mobile = mobile;
            m_Book = book;

            m_defValue = defValue;

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


                AddImage(49, 66, 0x25EA);
                AddButton(67, 66, 0x15E1, 0x15E5, GetButtonID(0, 1), GumpButtonType.Reply, 0);

                AddHtml(96, 64, 133, 32, "<BASEFONT COLOR=\"#FFFFFF\"><CENTER>Resources</CENTER></BASEFONT>", false, false); // Reagent

            int i = 0, j = 0, pageCnt = 1 ;
            foreach (var kv in m_Book.StoredItems)
            {
                if(j % 7 == 0)
                {
                    AddPage(pageCnt++);
                    i = 0;
                    this.AddButton(400, 420, 4005, 4006, 4, GumpButtonType.Page, (j / 7) + 1);
                    this.AddButton(25, 420, 4014, 4015, 4, GumpButtonType.Page, (j / 7));

                }
                int y = 100 + (25 * i);

                var item = (Item)Activator.CreateInstance(kv.Key);

                AddItem(45, y + 5, item.ItemID);
                if (string.IsNullOrWhiteSpace(item.Name))
                    AddHtmlLocalized(100, y, 150, 15, item.LabelNumber, 1153, false, false);
                else
                    AddLabel(100, y, 1153, item.Name);
                AddLabel(235, y, 1153, kv.Value.m_Count.ToString());

                if (kv.Value.m_Count > 0)
                {
                    AddTextBox(325, y, 50, 20, j + 100, defValue.ToString());
                    AddButton(388, y - 2, 4005, 4007, GetButtonID(1, j + 100), GumpButtonType.Reply, 0);
                }

                i++;
                j++;
            }

        }

        public override void OnResponse(Server.Network.NetState sender, RelayInfo Info)
        {
            int val = Info.ButtonID - 1;
            Item created;
            if (val <= 0)
                return;

            var index = val - 100;
            if (m_Book.StoredItems.Count < index)
            {
                var item = m_Book.StoredItems.Values.ToArray()[index];
                uint requested = 0;
                uint.TryParse(Info.GetTextEntry(val + 100).Text,out requested);
                if(requested > 0 && requested < item.m_Count && requested < int.MaxValue)
                {
                    try { created = (Item)Activator.CreateInstance(item.m_Type); }
                    catch { created = null; }

                    if (created != null)
                    {
                        bool added = false;
                        created.Amount = (int)requested;

                        if ((added = m_Mobile.AddToBackpack(created)))
                            m_Mobile.SendMessage("The resources have been added into your backpack.");
                        else
                            m_Mobile.SendMessage("The book was unable to give you the resources. Your backpack may contain too many items.");

                        if (!added)
                            created.Delete();
                        else
                            item.m_Count -= requested;

                        //m_Book.InvalidateProperties();
                    }
                }
            }

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

       
    }
}
