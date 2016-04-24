using Server;
using Server.Gumps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scripts.Mythik.Gumps
{
    public class ResurrectionStoneGump : Gump
    {
        Mobile m_Mobile;

        public ResurrectionStoneGump(Mobile from) : base(130, 155)
        {
            m_Mobile = from;
            m_Mobile.CloseGump(typeof(ResurrectionStoneGump));

            AddPage(0);

            AddBackground(0, 0, 380, 170, 5054);

            AddImageTiled(10, 10, 360, 20, 2624);
            AddAlphaRegion(10, 10, 360, 20);

            AddHtml(10, 10, 360, 20, "<BASEFONT COLOR=\"#FFFFFF\"><CENTER>Mythik Resurrection Menu</CENTER></BASEFONT>", false, false);

            AddImageTiled(10, 40, 360, 120, 2624);
            AddAlphaRegion(10, 40, 360, 120);

            AddHtml(160, 50, 190, 40, "<BASEFONT COLOR=\"#FFFFFF\">I wish to be resurrected now!</BASEFONT>", false, false);
            AddButton(350, 52, 0x15E1, 0x15E5, 1, GumpButtonType.Reply, 0);

            AddHtml(130, 110, 220, 40, "<BASEFONT COLOR=\"#FFFFFF\">Resurrect, then send me to safty!</BASEFONT>", false, false);
            AddButton(350, 112, 0x15E1, 0x15E5, 2, GumpButtonType.Reply, 0);

            AddItem(20, 60, 0x0002);
            AddItem(42, 60, 0x0003);
        }

        public override void OnResponse(Server.Network.NetState sender, RelayInfo info)
        {
            m_Mobile.Frozen = false;

            int ButtonID = info.ButtonID;

            switch (ButtonID)
            {
                case 1:
                    {       
                        m_Mobile.PlaySound(0x214);
                        m_Mobile.FixedEffect(0x376A, 10, 16);
                        m_Mobile.Resurrect();
                        m_Mobile.SendMessage("You have been resurrected.");
                        break;
                    }
                case 2:
                    {
                        m_Mobile.PlaySound(0x214);
                        m_Mobile.FixedEffect(0x376A, 10, 16);
                        m_Mobile.Resurrect();
                        m_Mobile.MoveToWorld(MythikStaticValues.NeutralZone, Map.Felucca);
                        m_Mobile.SendMessage("You have been resurrected and sent to safety.");
                        break;
                    }
            }
        }

    }
}
