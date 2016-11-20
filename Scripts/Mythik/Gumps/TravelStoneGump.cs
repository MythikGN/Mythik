using Scripts.Mythik.Items.Stones;
using Server;
using Server.Gumps;
using Server.Mobiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scripts.Mythik.Gumps
{
    public class TravelStoneGump : Gump
    {
        private static TownEntry[] m_GuardedTowns =
            {
                new TownEntry( "Britain",   new Point3D( 1428, 1696, 0 ) ),
                new TownEntry( "Cove",      new Point3D( 2272, 1209, 0 ) ),
                new TownEntry( "Jhelom",    new Point3D( 1381, 3817, 0 ) ),
                new TownEntry( "Moonglow",  new Point3D( 4471, 1178, 0 ) ),
                new TownEntry( "Nujel'm",   new Point3D( 3756, 1244, 0 ) ),
                new TownEntry( "Occlo",     new Point3D( 3173,1730, 0 ) ),
                new TownEntry( "Trinsic",   new Point3D( 1956, 2780, 10 ) ),
                new TownEntry( "Vesper",    new Point3D( 2895, 688, 0 ) ),
                new TownEntry( "Yew",       new Point3D( 546, 991, 0 ) ),
                new TownEntry("Magincia",   new Point3D(3205,1419,0)),
                new TownEntry( "Serp's Hold",       new Point3D( 1874,3822, 15 ) ),
            };

        private static TownEntry[] m_UnguardedTowns =
            {
                new TownEntry( "Buc's Den",         new Point3D[] { new Point3D( 348,2764, 0 ), new Point3D( 338,2700, 0 ), new Point3D( 375,2699, 0 ) } ),
                new TownEntry( "Minoc",             new Point3D[] { new Point3D( 2466, 443, 15 ), new Point3D( 2465, 544, 0 ), new Point3D( 2490, 486, 15 ) } ),
                //new TownEntry( "Serp's Hold",       new Point3D( 3021, 3397, 15 ) ),
                new TownEntry( "Skara Brae",        new Point3D( 592, 2155, 0 ) ),
                //new TownEntry( "Wind",              new Point3D( 1361, 895, 0 ) ),
                new TownEntry( "Britain Bridge",    new Point3D[] { new Point3D( 1365, 1755, 13 ), new Point3D( 1321, 1752, 10 ), new Point3D( 1374, 1808, 0 ) } ),
                new TownEntry( "Cove Outskirts",    new Point3D[] { new Point3D( 2298, 1238, 0 ), new Point3D( 2291, 1188, 0 ), new Point3D( 2310, 1204, 0 ) } )
            };

        private static TownEntry[] m_NeutralTowns =
            {
                new TownEntry( "Neutral Zone",MythikStaticValues.NeutralZone ),
                new TownEntry( "Donater Mall",MythikStaticValues.Mall ),
                new TownEntry( "Boxing Arena",MythikStaticValues.ArenaBoxing ),
                new TownEntry( "1v1 Arena",MythikStaticValues.ArenaOneVOne ),
                new TownEntry( "FFA Arena",MythikStaticValues.ArenaFFA ),
            };

        private PlayerMobile m_Mobile;
        private TravelStone m_Stone;

        public TravelStoneGump(PlayerMobile apm, TravelStone stone) : base(110, 90)
        {
            apm.CloseGump(typeof(TravelStoneGump));

            m_Mobile = apm;
            m_Stone = stone;

            AddPage(0);

            AddBackground(0, 0, 420, 350, 5054);

            AddImageTiled(10, 10, 400, 20, 2624);
            AddAlphaRegion(10, 10, 400, 20);

            AddHtml(10, 10, 400, 20, "<BASEFONT COLOR=\"#FFFFFF\"><CENTER>Mythik Travel Menu</CENTER></BASEFONT>", false, false);

            CreateTable("Guarded", stone.GuardedPrice, 0x8888FF, 10);
            CreateTable("Unguarded", stone.UnguardedPrice, 0xFF6666, 145);
            CreateTable("Neutral", stone.NeutralPrice, 0x66FF66, 280);

            ListTable(m_GuardedTowns, 10, 1);
            ListTable(m_UnguardedTowns, 145, 2);
            ListTable(m_NeutralTowns, 280, 3);
        }

        public override void OnResponse(Server.Network.NetState sender, RelayInfo info)
        {
            m_Mobile.Frozen = false;

            int buttonID = info.ButtonID - 1;

            if (buttonID > 0)
            {
                int index = buttonID / 4;
                int type = buttonID % 4;

                TownEntry[] table = null;
                int price = 0;

                switch (type)
                {
                    case 1: { table = m_GuardedTowns; price = m_Stone.GuardedPrice; break; }
                    case 2: { table = m_UnguardedTowns; price = m_Stone.UnguardedPrice; break; }
                    case 3: { table = m_NeutralTowns; price = m_Stone.NeutralPrice; break; }
                }

                if (index < table.Length)
                {
                    TownEntry te = (TownEntry)table.GetValue(index);

                    var timer = new TravelTimer(m_Mobile, te.GetLocation(), price);
                    timer.Start();
                    m_Mobile.BeginAction(timer);
                    m_Mobile.RevealingAction();
                }
            }
        }

        private void CreateTable(string name, int price, int color, int x)
        {
            AddImageTiled(x, 40, 130, 40, 2624);
            AddAlphaRegion(x, 40, 130, 40);

            AddImageTiled(x, 85, 130, 205, 2624);
            AddAlphaRegion(x, 85, 130, 205);

            AddHtml(x, 40, 130, 40, String.Format("<BASEFONT COLOR=\"#{0:X6}\"><CENTER>{1}<BR>[ {2} gps ]</CENTER></BASEFONT>", color, name, price), false, false);
        }

        private void ListTable(TownEntry[] table, int x, int type)
        {
            int offset = 0;

            for (int i = 0; i < table.Length; i++)
            {
                TownEntry te = (TownEntry)table.GetValue(i);

                if (te != null)
                {
                    AddLabel(x + 23, 85 + (i * 20) + offset, 0x481, te.Name);
                    AddButton(x + 5, 88 + (i * 20) + offset, 0x15E1, 0x15E5, GetButtonID(type, i), GumpButtonType.Reply, 0);
                }
            }
        }

        public int GetButtonID(int type, int index)
        {
            return 1 + (index * 4) + type;
        }

        private class TownEntry
        {
            private string m_Name;
            private Point3D[] m_Locations;

            public string Name { get { return m_Name; } }
            public Point3D[] Locations { get { return m_Locations; } }

            public TownEntry(string name, Point3D location) : this(name, new Point3D[] { location })
            {
            }

            public TownEntry(string name, Point3D[] locations)
            {
                m_Name = name;
                m_Locations = locations;
            }

            public Point3D GetLocation()
            {
                int index = Utility.Random(m_Locations.Length);

                return m_Locations[index];
            }
        }
    }
}
