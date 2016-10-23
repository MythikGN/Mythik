using Server;
using Server.Gumps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Server.Network;
using Scripts.Mythik.Systems;
using Scripts.Mythik.Systems.Achievements.Gumps;
using Scripts.Mythik.Mobiles;
using static Server.Commands.HelpInfo;

namespace Scripts.Mythik.Gumps
{
    public static class NewsSystem
    {
        private static bool Enabled = true;

        public static List<NewsItem> News = new List<NewsItem>() { new NewsItem("23/10/16","Going live in 2016") };
        //git 
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
            this.AddBackground(25, 25, 528, 455, 9270);
            this.AddButton(506, 40, 4017, 4018, 0, GumpButtonType.Reply, 0);
            this.AddBackground(41, 95, 352, 368, 3600);
            this.AddLabel(224, 38, 457, @"Mythik News");

            var html = "";
            foreach (var news in NewsSystem.News)
            {
                html += "<CENTER> <STRONG>" + news.Title + "</STRONG> <BR> <P>" + news.Content + "</P>";
            }

            this.AddHtml(52, 110, 324, 338, html, (bool)false, (bool)true);
            this.AddButton(508, 103, 4005, 4006, 1, GumpButtonType.Reply, 0);
            this.AddLabel(400, 103, 457, @"Commands");
            this.AddButton(508, 129, 4005, 4006, 2, GumpButtonType.Reply, 0);
            this.AddLabel(400, 129, 457, @"Features");
            this.AddButton(508, 155, 4005, 4006, 3, GumpButtonType.Reply, 0);
            this.AddLabel(400, 155, 457, @"Creature Guide");
            this.AddButton(508, 184, 4005, 4006, 4, GumpButtonType.Reply, 0);
            this.AddLabel(400, 184, 457, @"Achievements");


        }
        public override void OnResponse(NetState sender, RelayInfo info)
        {
            switch(info.ButtonID)
            {
                case 1:
                    sender.Mobile.SendGump(new CommandListGump(0, sender.Mobile, null));
                    break;
                case 2:
                    sender.Mobile.SendGump(new FAQGump());
                    break;
                case 3:
                    sender.Mobile.SendGump(new CreatureListGump(CreatureListGump.Buttons.None));
                    break;
                case 4:
                    var pm = sender.Mobile as MythikPlayerMobile;
                    sender.Mobile.SendGump(new AchievementGump(pm.Achievements,pm.AchievementPointsTotal));
                    break;
                default:
                    break;
            }
        }
    }
}
