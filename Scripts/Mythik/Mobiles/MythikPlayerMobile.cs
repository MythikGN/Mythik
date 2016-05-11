using Server.Mobiles;
using Server.Poker;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Server;

namespace Scripts.Mythik.Mobiles
{
    public class MythikPlayerMobile : PlayerMobile
    {
        private PokerGame m_PokerGame; //Edit for Poker System
        public PokerGame PokerGame
        {
            get { return m_PokerGame; }
            set { m_PokerGame = value; }
        }


        public bool HasSetLanguageSkills { get; set; }
        public bool AuctionEnabled { get; internal set; }


        protected override bool OnMove(Direction d)
        {
            if (m_PokerGame != null)
            {
                if (!HasGump(typeof(PokerLeaveGump)))
                {
                    SendGump(new PokerLeaveGump(this, m_PokerGame));
                    return false;
                }
            }
            return base.OnMove(d);
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write((int)1);//version
            writer.Write(HasSetLanguageSkills);
            writer.Write(AuctionEnabled);

        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int ver = reader.ReadInt();
            HasSetLanguageSkills = reader.ReadBool();
            AuctionEnabled = reader.ReadBool();
        }

    }
}
