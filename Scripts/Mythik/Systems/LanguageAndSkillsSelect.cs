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
using Server.Items;

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
            if (!(e.Mobile as MythikPlayerMobile).HasSetSkills)
            {
                e.Mobile.CloseGump(typeof(WelcomeInfoGump));
                e.Mobile.SendGump(new WelcomeInfoGump());
            }
        }
        private class WelcomeInfoGump : Gump
        {
            public WelcomeInfoGump() : base(50, 50)
            {
                this.Closable = true;
                this.Disposable = true;
                this.Dragable = true;
                this.Resizable = false;
                this.AddPage(0);
                this.AddBackground(40, 45, 399, 370, 9270);
                this.AddBackground(57, 61, 365, 59, 3600);
                this.AddLabel(212, 80, 1152, @"Mythik");
                this.AddBackground(58, 130, 365, 270, 3600);
                this.AddHtml(78, 151, 323, 200, @"<CENTER><COLOR=WHITE>Welcome to Mythik<BR> On the next screen you will be able to choose two skills at 60.<BR>You will then be able to choose two further skills at 50.", (bool)false, (bool)false);
                this.AddButton(335, 357, 247, 248, 1, GumpButtonType.Reply, 0);
            }

            public override void OnResponse(NetState sender, RelayInfo info)
            {
                foreach (Skill s in sender.Mobile.Skills)
                {
                    s.BaseFixedPoint = 0;
                }
                sender.Mobile.SendGump(new SkillsSelectGump(60));
                base.OnResponse(sender, info);
            }
        }



        private class SkillsSelectGump : Gump
        {
            private int m_value;
            public SkillsSelectGump(int val) : base(25, 25)
            {
                this.m_value = val;
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
                this.AddLabel(177, 63, 457, @"Please select two skills to start at 60.");
                this.AddLabel(428, 63, 457, @"Magery will start at 80.");

                int off = 23;//label offset
                int colStart = 61;
                int colOffset = 162;
                int rowStart = 105;
                int cnt = 0;

                for (int col = 0; col <= 3; col++)
                {
                    for (int row = 0; row <= 12; row++)
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
                if (info.Switches.Length != 2)
                {
                    sender.Mobile.SendMessage("Please select two Skills.");
                    sender.Mobile.SendGump(this);
                    return;
                }
                var skillA = (SkillName)info.Switches[0];
                var skillB = (SkillName)info.Switches[1];

                sender.Mobile.Skills[skillA].BaseFixedPoint = m_value * 10;
                sender.Mobile.Skills[skillB].BaseFixedPoint = m_value * 10;
                if (m_value == 50)
                {
                    sender.Mobile.Skills[SkillName.Magery].BaseFixedPoint = 800;
                    (sender.Mobile as MythikPlayerMobile).HasSetSkills = true;
                    AddSkillStarterItems(sender.Mobile, skillA);
                    AddSkillStarterItems(sender.Mobile, skillB);
                }
                else
                {
                    sender.Mobile.SendGump(new SkillsSelectGump(50));
                }


                base.OnResponse(sender, info);
            }


            private void AddSkillStarterItems(Mobile player, SkillName skill)
            {
                switch (skill)
                {
                    case SkillName.Alchemy:
                        player.AddToBackpack(new MortarPestle());
                        player.AddToBackpack(new Bottle(10));
                        break;

                    case SkillName.Blacksmith:
                        player.AddToBackpack(new SmithHammer());
                        player.AddToBackpack(new IronIngot(50));
                        break;

                    case SkillName.Carpentry:
                        player.AddToBackpack(new Saw());
                        player.AddToBackpack(new Log(50));
                        break;

                    case SkillName.Cartography:
                        player.AddToBackpack(new BlankMap());
                        player.AddToBackpack(new BlankMap());
                        player.AddToBackpack(new PenAndInk());
                        break;

                    case SkillName.Inscribe:
                        player.AddToBackpack(new BlankScroll(10));
                        player.AddToBackpack(new ScribesPen());
                        break;

                    case SkillName.Fishing:
                        player.AddToBackpack(new FishingPole());
                        break;

                    case SkillName.Fletching:
                        player.AddToBackpack(new FletcherTools());
                        player.AddToBackpack(new Log(50));
                        break;

                    case SkillName.Veterinary:
                    case SkillName.Healing:
                        player.AddToBackpack(new Bandage(50));
                        break;

                    case SkillName.Lumberjacking:
                        player.AddToBackpack(new Axe());

                        break;
                    case SkillName.Mining:
                        player.AddToBackpack(new Pickaxe());

                        break;
                    case SkillName.Tinkering:
                        player.AddToBackpack(new TinkerTools());

                        break;

                    case SkillName.Discordance:
                    case SkillName.Provocation:
                    case SkillName.Peacemaking:
                        player.AddToBackpack(new Tambourine());
                        break;

                    case SkillName.Archery:
                        EquipAnItem(player, new Bow() { Quality = WeaponQuality.Exceptional });
                        player.AddToBackpack(new Arrow(50));
                        goto case SkillName.Wrestling; // This craziness adds armor as well from wrestling case
                        break;

                    case SkillName.Swords:
                        EquipAnItem(player, new Longsword() { Quality = WeaponQuality.Exceptional });
                        goto case SkillName.Wrestling;

                    case SkillName.Fencing:
                        EquipAnItem(player, new Kryss() { Quality = WeaponQuality.Exceptional });
                        goto case SkillName.Wrestling;

                    case SkillName.Macing:
                        EquipAnItem(player, new Mace() { Quality = WeaponQuality.Exceptional });
                        goto case SkillName.Wrestling;

                    case SkillName.Wrestling:
                        EquipAnItem(player, new RingmailLegs());
                        EquipAnItem(player, new RingmailArms());
                        EquipAnItem(player, new RingmailChest());
                        EquipAnItem(player, new RingmailGloves());
                        EquipAnItem(player, new CloseHelm());
                        EquipAnItem(player, new MetalShield());
                        break;

                }
            }

            private void EquipAnItem(Mobile p, Item a)
            {
                Item equippedItem = null;
                if ((equippedItem = p.FindItemOnLayer(a.Layer)) != null)
                {
                    p.AddToBackpack(equippedItem);
                }
                p.EquipItem(a);
            }
        }





        //Disabled, no need to set language as language is set during login, and also each time client sends a unicode chat message.
        /*private class LanguageSelectGump : Gump
        {
            public LanguageSelectGump(): base(25,25)
            {
                this.Closable = false;
                this.Disposable = true;
                this.Dragable = false;
                this.Resizable = false;
                this.AddPage(0);
                this.AddBackground(23, 18, 272, 270, 9250);
                this.AddLabel(92, 42, 0, @"Welcome to Mythik!");
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
        }*/
    }
}
