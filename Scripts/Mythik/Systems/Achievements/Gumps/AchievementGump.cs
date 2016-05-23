using Scripts.Mythik.Mobiles;
using Server;
using Server.Gumps;
using Server.Mobiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scripts.Mythik.Systems.Achievements.Gumps
{
    class AchievementGump : Gump
    {
        int FORWARD = 1;
        int BACK = 2;
        public AchievementGump(PlayerMobile m) : base(25,25)
        {
            var player = m as MythikPlayerMobile;
            this.Closable = true;
            this.Disposable = true;
            this.Dragable = true;
            this.Resizable = false;
            this.AddPage(0);
            this.AddBackground(11, 15, 758, 575, 2600);
            this.AddBackground(57, 92, 666, 478, 9250);
            this.AddBackground(321, 104, 386, 453, 9270);
            this.AddBackground(72, 104, 245, 453, 9270);
            this.AddBackground(72, 34, 635, 53, 9270);
            this.AddBackground(327, 0, 133, 41, 9200);
            this.AddLabel(292, 52, 68, @"Mythik Achievment System");
            this.AddLabel(360, 11, 82, player.AchievementPointsTotal + @" Points");
            this.AddBackground(341, 522, 353, 26, 9200);
            this.AddButton(658, 524, 4005, 248, FORWARD, GumpButtonType.Reply, 0);
            this.AddButton(345, 524, 4014, 248, FORWARD, GumpButtonType.Page, 0);

            this.AddBackground(90, 123, 209, 25, 9200);
           // this.AddButton(102, 129, 1209, 1210, (int)Buttons.catAbtn, GumpButtonType.Reply, 0);
            this.AddLabel(122, 125, 0, @"Category A");

            this.AddBackground(91, 154, 209, 25, 9200);
            //this.AddButton(103, 160, 1210, 1210, (int)Buttons.catBbtn, GumpButtonType.Reply, 0);
            this.AddLabel(123, 156, 249, @"Category B");

            this.AddBackground(123, 187, 155, 25, 9200);
           // this.AddButton(135, 193, 1209, 1210, (int)Buttons.subCatbtn, GumpButtonType.Reply, 0);
            this.AddLabel(155, 189, 0, @"SubCategory C");



        }
    }
}
