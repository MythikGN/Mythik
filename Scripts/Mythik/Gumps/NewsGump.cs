using Server;
using Server.Gumps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Server.Network;
using Scripts.Mythik.Systems;

namespace Scripts.Mythik.Gumps
{
    public static class NewsSystem
    {
        private static bool Enabled = true;

        public static List<NewsItem> News = new List<NewsItem>() { new NewsItem("Mythik 2016","Going live in 2016") };
        
        public static void Initialize()
        {
            if (Enabled)
                EventSink.Login += new LoginEventHandler(EventSink_Login);
        }

        private static void EventSink_Login(LoginEventArgs e)
        {
            e.Mobile.SendGump(new NewsGump());
        }
    }

    public class NewsItem
    {
        public NewsItem(string title, string content)
        {
            this.Title = title;
            this.Content = content;
        }

        public string Content { get; private set; }
        public string Title { get; private set; }
    }
    public class NewsGump : Gump
    {
        public NewsGump() : base(25,25)
        {
            this.Closable = true;
            this.Disposable = true;
            this.Dragable = true;
            this.Resizable = false;
            this.AddPage(0);
            this.AddBackground(25, 25, 528, 455, 9300);
            this.AddButton(524, 36, 11410, 11412, 0, GumpButtonType.Reply, 0);
            this.AddLabel(156, 33, 1151, @"Mythik News");
            var html = "";
            foreach (var news in NewsSystem.News)
            {
                html += "<STRONG>" + news.Title + "</STRONG> <BR> <P>" + news.Content +"</P>";
            }

            this.AddHtml(43, 69, 340, 389, html, (bool)false, (bool)true);


            this.AddButton(508, 70, 4005, 4006, 1, GumpButtonType.Reply, 0);
            this.AddLabel(400, 70, 0, @"Commands");
            this.AddButton(508, 96, 4005, 4006, 2, GumpButtonType.Reply, 0);
            this.AddLabel(400, 96, 0, @"FAQ");
            this.AddButton(508, 122, 4005, 4006, 3, GumpButtonType.Reply, 0);
            this.AddLabel(400, 122, 0, @"Creature Guide");


        }
        public override void OnResponse(NetState sender, RelayInfo info)
        {
            switch(info.ButtonID)
            {
                case 1:
                    sender.Mobile.SendGump(new CommandsListGump());
                    break;
                case 2:
                    sender.Mobile.SendGump(new FAQGump());
                    break;
                case 3:
                    sender.Mobile.SendGump(new CreatureListGump(CreatureListGump.Buttons.None));
                    break;
                default:
                    break;
            }
        }
    }
}
