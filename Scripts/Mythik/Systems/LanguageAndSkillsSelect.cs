using Server;
using Server.Gumps;
using Server.Mobiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Server.Network;
using Scripts.Mythik.Mobiles;

namespace Scripts.Mythik.Systems
{
    public class LanguageAndSkillsSelect
    {
        private static bool Enabled = true;

        public static void Initialize()
        {
            if (Enabled)
                EventSink.Login += new LoginEventHandler(EventSink_Login);
        }

        private static void EventSink_Login(LoginEventArgs e)
        {
            if(!(e.Mobile as MythikPlayerMobile).HasSetLanguageSkills)
            {
                e.Mobile.SendGump(new LanguageSelectGump());
            }
        }

        private class SkillsSelectGump : Gump
        {
            public SkillsSelectGump() : base(25,25)
            {
                this.Closable = false;
                this.Disposable = true;
                this.Dragable = true;
                this.Resizable = false;
                this.AddPage(0);
                this.AddBackground(25, 25, 677, 455, 9270);
                this.AddBackground(40, 85, 155, 377, 3600);
                this.AddBackground(203, 85, 155, 377, 3600);
                this.AddBackground(366, 85, 155, 377, 3600);
                this.AddBackground(528, 85, 155, 377, 3600);
                this.AddButton(616, 55, 247, 248, 1, GumpButtonType.Reply, 0);

                this.AddLabel(280, 40, 457, @"Mythik Skills Select");
                this.AddLabel(177, 63, 0, @"Please select two skills to start at 60.");
                this.AddLabel(428, 63, 0, @"Magery will start at 80.");

                int off = 23;//label offset
                int colStart = 61;
                int colOffset = 162;
                int rowStart = 105;
                int cnt = 0;
                
                for(int col = 0;col <= 3; col++)
                {
                    for(int row = 0;row <= 12;row++)
                    {
                        if (cnt >= (int)SkillName.RemoveTrap)
                            break;
                        this.AddCheck(colStart + (col * colOffset), rowStart + (row * 25), 210, 211, false, cnt);
                        this.AddLabel(colStart + off + (col * colOffset), rowStart + (row * 25), 1952, ((SkillName)cnt).ToString());
                        cnt++;
                        if (cnt == (int)SkillName.Magery)
                            cnt++;
                    }
                }
            }
            public override void OnResponse(NetState sender, RelayInfo info)
            {
                if(info.Switches.Length != 2)
                {
                    sender.Mobile.SendMessage("Please select two Skills.");
                    sender.Mobile.SendGump(this);
                    return;
                }
                var skillA = (SkillName)info.Switches[0];
                var skillB = (SkillName)info.Switches[1];
                sender.Mobile.Skills[skillA].BaseFixedPoint = 600;
                sender.Mobile.Skills[skillB].BaseFixedPoint = 600;
                sender.Mobile.Skills[SkillName.Magery].BaseFixedPoint = 800;
                (sender.Mobile as MythikPlayerMobile).HasSetLanguageSkills = true;
                //base.OnResponse(sender, info);
            }
        }

        private class LanguageSelectGump : Gump
        {
            public LanguageSelectGump(): base(25,25)
            {
                this.Closable = false;
                this.Disposable = true;
                this.Dragable = false;
                this.Resizable = false;
                this.AddPage(0);
                this.AddBackground(23, 18, 272, 270, 9250);
                this.AddLabel(82, 42, 0, @"Welcome to Mythik!");
                this.AddLabel(78, 92, 0, @"");
                this.AddHtml(43, 79, 230, 80, @"Please select your language so we can<BR> localize text to your native language", (bool)false, (bool)false);
                this.AddGroup(1);
                this.AddRadio(60, 160, 209, 208, false, 3);
                this.AddRadio(60, 180, 209, 208, false, 4);
                this.AddRadio(60, 200, 209, 208, false, 5);
                this.AddRadio(60, 220, 209, 208, false, 6);
                this.AddRadio(60, 240, 209, 208, false, 7);
                this.AddLabel(85, 160, 0, @"English");
                this.AddLabel(85, 180, 0, @"Deutch");
                this.AddLabel(85, 200, 0, @"Japanese");
                this.AddLabel(85, 220, 0, @"Español");
                this.AddLabel(85, 240, 0, @"Français");
                this.AddButton(244, 240, 1153, 1154, 1, GumpButtonType.Reply, 0);
            }
            public override void OnResponse(NetState sender, RelayInfo info)
            {
                switch(info.Switches[0] - 2)
                {
                    case 1: //english
                        break;
                    case 2:
                        sender.Mobile.Language = "DEU";
                        break;
                    case 3:
                        sender.Mobile.Language = "JPN";
                        break;
                    case 4:
                        sender.Mobile.Language = "ESP";
                        break;
                    case 5:
                        sender.Mobile.Language = "FRA";
                        break;

                }
                sender.Mobile.SendGump(new SkillsSelectGump());
                base.OnResponse(sender, info);
            }
        }
    }
}
