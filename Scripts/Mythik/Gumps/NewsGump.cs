using Server;
using Server.Gumps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            this.AddBackground(25, 25, 375, 450, 9300);
            this.AddButton(376, 35, 11410, 11412, 1, GumpButtonType.Reply, 0);
            this.AddLabel(156, 33, 1151, @"Mythik News");
            var html = "";
            foreach (var news in NewsSystem.News)
            {
                html += "<STRONG>" + news.Title + "</STRONG> <BR> <P>" + news.Content +"</P>";
            }

            this.AddHtml(43, 69, 340, 389, html, (bool)false, (bool)true);

        }
    }
}
