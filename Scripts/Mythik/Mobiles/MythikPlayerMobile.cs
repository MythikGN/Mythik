using Server.Mobiles;
using Server.Poker;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Server;
using Scripts.Mythik.Systems.Achievements;

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

        /// <summary>
        /// AcheivID , Progress
        /// </summary>
        internal Dictionary<int, AchieveData> Achievements = new Dictionary<int, AchieveData>();
        public int AchievementPointsTotal { get; set; }
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

        [Constructable]
        public MythikPlayerMobile(Serial serial): base(serial)
        {

        }
        [Constructable]
        public MythikPlayerMobile() : base ()
        {

        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write((int)1);//version
            writer.Write(HasSetLanguageSkills);
            writer.Write(AuctionEnabled);

            writer.Write(AchievementPointsTotal);
            writer.Write(Achievements.Count);
            foreach (var kv in Achievements)
            {
                writer.Write(kv.Key);
                kv.Value.Serialize(writer);
            }
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int ver = reader.ReadInt();
            HasSetLanguageSkills = reader.ReadBool();
            AuctionEnabled = reader.ReadBool();
            AchievementPointsTotal = reader.ReadInt();
            int count = reader.ReadInt();
            if(count > 0)
            {
                for(int i = 0; i < count;i++)
                {
                    Achievements.Add(reader.ReadInt(), new AchieveData(reader));
                }
            }

        }

    }
}
