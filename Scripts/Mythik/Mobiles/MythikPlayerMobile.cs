using Server.Mobiles;
using Server.Poker;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Server;
using Scripts.Mythik.Systems.Achievements;
using Server.SkillHandlers;
using Server.ContextMenus;
using Server.Gumps;
using Server.Mythik;

namespace Scripts.Mythik.Mobiles
{
    public class MythikPlayerMobile : PlayerMobile
    {

        public PokerGame PokerGame { get; set; }


        public bool HasSetSkills { get; set; }
        public bool AuctionEnabled { get; internal set; }
        public bool ChatEnabled { get; internal set; } = true;

        /// <summary>
        /// AcheivID , Progress
        /// </summary>
        internal Dictionary<int, AchieveData> Achievements = new Dictionary<int, AchieveData>();
        public int AchievementPointsTotal { get; set; }

        #region PolymorphDamageMods
        public int MinDamage { get; internal set; }
        public int MaxDamage { get; internal set; }
        public int SwingSpeed { get; internal set; }
        #endregion

        private Dictionary<int, Tuple<int, int>> m_GumpLocations = new Dictionary<int, Tuple<int, int>>();

        public Tuple<int, int> GetGumpLoc(Type gump)
        {
            if (!m_GumpLocations.ContainsKey(gump.Name.GetHashCode()))
                m_GumpLocations.Add(gump.Name.GetHashCode(), new Tuple<int, int>(800, 10));
            return m_GumpLocations[gump.Name.GetHashCode()];
           
        }
        public void SetGumpLoc(Type gump, int x, int y)
        {
            if (m_GumpLocations.ContainsKey(gump.Name.GetHashCode()))
            {
                var cur = m_GumpLocations[gump.Name.GetHashCode()];
                if (cur.Item1 + x < 0 || cur.Item2 + y < 0)
                    return;
                m_GumpLocations[gump.Name.GetHashCode()] = new Tuple<int, int>(cur.Item1+x, cur.Item2 + y);
            }
            else
                m_GumpLocations.Add(gump.Name.GetHashCode(), new Tuple<int, int>(x, y));
        }
        protected override bool OnMove(Direction d)
        {
            if (PokerGame != null)
            {
                if (!HasGump(typeof(PokerLeaveGump)))
                {
                    SendGump(new PokerLeaveGump(this, PokerGame));
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
        public override void OpenPropsGump(Item targ)
        {
            SendGump(new EquipInfoGump(this, targ));

        }
        public override void ClosePropsGump()
        {
            this.CloseGump(typeof(EquipInfoGump));
        }

        public override void ProcessDelta()
        {
            if (NetState.Version?.Major <= 3)
            {
                Mobile m = this;
                MobileDelta delta;

                delta = m.m_DeltaFlags;

                if (delta == MobileDelta.None)
                    return;

                MobileDelta attrs = delta & MobileDelta.Attributes;
                if ((delta & (MobileDelta.Hits | MobileDelta.Stam | MobileDelta.Mana | MobileDelta.WeaponDamage | MobileDelta.Resistances | MobileDelta.Stat | MobileDelta.Weight | MobileDelta.Gold | MobileDelta.Armor | MobileDelta.StatCap | MobileDelta.Followers)) != 0)
                {
                    SendGump(new StatusGump(this));
                }
            }
                
            base.ProcessDelta();
        }
        public override void GetContextMenuEntries(Mobile from, List<ContextMenuEntry> list)
        {
            base.GetContextMenuEntries(from, list);
            if(from == this)
            {
                list.Add(new Server.Engines.VendorSearhing.SearchVendors(this));
                list.Add(new TitlesMenuEntry(this));
            }
            if(from is PlayerMobile)
                list.Add(new AchievementMenuEntry(from as PlayerMobile, this));
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write((int)2);//version
            writer.Write(HasSetSkills);
            writer.Write(AuctionEnabled);

            writer.Write(AchievementPointsTotal);
            writer.Write(Achievements.Count);
            foreach (var kv in Achievements)
            {
                writer.Write(kv.Key);
                kv.Value.Serialize(writer);
            }

            writer.Write(m_GumpLocations.Count);
            foreach(var g in m_GumpLocations)
            {
                writer.Write(g.Key);
                writer.Write((short)g.Value.Item1);
                writer.Write((short)g.Value.Item2);
            }
            writer.Write(ChatEnabled);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int ver = reader.ReadInt();
            HasSetSkills = reader.ReadBool();
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
            if (ver == 1)
                return;

            count = reader.ReadInt();
            if(count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    m_GumpLocations.Add(reader.ReadInt(), new Tuple<int, int>(reader.ReadShort(), reader.ReadShort()));
                }
            }

            ChatEnabled = reader.ReadBool();

        }

    }
}
