using System;
using System.Collections;
using System.Collections.Generic;
using Server;
using Server.Items;
using Server.Mobiles;
using Server.Engines.PartySystem;

namespace Server.Events.DoomSystem
{
    public static class LeverPuzzle
    {
        #region Members
        private static List<DoomStatue> m_Statues
        {
            get
            {
                List<DoomStatue> list = new List<DoomStatue>();

                for (int i = 0; i < 2; i++)
                    foreach (Item item in Map.Malas.GetItemsInRange(m_StatueLocations[i], 0))
                        if (item is DoomStatue)
                            list.Add((DoomStatue)item);                

                return list;
            }
        }
            
        private static List<DoomLever> m_Levers
        {
            get
            {
                List<DoomLever> list = new List<DoomLever>();

                for (int i = 0; i < 4; i++)
                    foreach (Item item in Map.Malas.GetItemsInRange(m_LeverLocations[i], 0))
                        if (item is DoomLever)
                            list.Add((DoomLever)item);

                return list;
            }
        }
        private static int[] m_Answer = new int[4];
        private static int[] m_Attempt = new int[4];
        private static Party m_Party;

        public static Party Party
        {
            get
            {
                if (m_Party != null)
                {
                    foreach (PartyMemberInfo pmi in m_Party.Members)
                    {
                        Mobile m = pmi.Mobile;

                        if (m.InRange(m_PlayerLocations[0], 18) && m.Alive)
                            return m_Party;
                    }
                }

                return null;
            }

            set{m_Party = value;}
        }

        public static Point3D[] m_PlayerLocations = new Point3D[]
			{
                new Point3D( 324, 64, -1 ),// thiefloc
				new Point3D( 316, 65, -1 ), new Point3D( 324, 58, -1 ),//switchlocs
				new Point3D( 332, 64, -1 ), new Point3D( 323, 72, -1 )				
			};

        private static Point3D[] m_LeverLocations = new Point3D[]
			{
                new Point3D( 316, 64, 2 ), new Point3D( 323, 58, 2 ), 
                new Point3D( 332, 63, 2 ), new Point3D( 323, 71, 2 )		
			};

        private static Point3D[] m_StatueLocations = new Point3D[]
			{
                new Point3D(329, 60, 19 ), 
                new Point3D( 319, 70, 19 )		
			};

        private static bool m_LocationsPressed
        {
            get
            {
                int count = 0;
                for (int i = 0; i < 5; i++)
                {
                    IPooledEnumerable ip = Map.Malas.GetMobilesInRange(m_PlayerLocations[i], 0);
                    foreach (Mobile m in ip)
                    {
                        if (m is PlayerMobile && m.CheckAlive())
                        {
                            count++;
                            break;
                        }
                    }
                }

                return count == 5;
            }
        }
        #endregion

        #region Core
        public static void LeverSwitched(int ID)
        {
            for (int i = 0; i < 4; i++)
            {
                if (m_Attempt[i] == 0)
                {
                    m_Attempt[i] = ID;
                    if (i == 3)
                        DoAttempt();

                    break;
                }
            }
        }

        private static void DoAttempt()
        {
            int number = 0;

            for (int i = 0; i < 4; i++)
                if (m_Answer[i] == m_Attempt[i])
                    number++;

            if (number == 4)
            {
                List<Mobile> list = new List<Mobile>();

                IPooledEnumerable ip = Map.Malas.GetMobilesInRange(m_PlayerLocations[0], 0);
                foreach (Mobile m in ip)
                {
                    if (m is PlayerMobile)
                    {
                        if (m_LocationsPressed)
                        {
                            if ((Party == null || Party.Get(m) == Party))
                                list.Add(m);

                            else
                                m.SendLocalizedMessage(1050004); // The circle is the key, the key is incomplete and so the gate remains closed.
                        }

                        else
                            StatuesTalk(4);
                    }
                }

                if (list.Count != 0)
                {
                    Timer.DelayCall(TimeSpan.FromSeconds(1.5), new TimerStateCallback(MoveThief_Callback), list);
                    list[0].BoltEffect(0);
                    GenerateAnswer();
                }                
            }

            else
            {
                StatuesTalk(number);
                List<Mobile> moblist = new List<Mobile>();

                for (int i = 0; i < 5; ++i)
                {
                    IPooledEnumerable ip = Map.Malas.GetMobilesInRange(m_PlayerLocations[i], 1);
                    foreach (Mobile m in ip)
                        if (m is PlayerMobile && !moblist.Contains(m))
                            moblist.Add(m);
                }

                for (int i = 0; i < m_PlayerLocations.Length; i++)
                    Effects.SendMovingEffect(new Entity(Serial.Zero, new Point3D(m_PlayerLocations[i].X, m_PlayerLocations[i].Y, m_PlayerLocations[i].Z + 80), Map.Malas), new Entity(Serial.Zero, new Point3D(m_PlayerLocations[i].X, m_PlayerLocations[i].Y, m_PlayerLocations[i].Z + 2), Map.Malas), 0x11B8, 5, 16, false, false);

                Timer.DelayCall(TimeSpan.FromSeconds(0.75), new TimerCallback(Boulder_Callback));

                for (int i = 0; i < moblist.Count; ++i)
                {
                    Mobile m = moblist[i];

                    m.LocalOverheadMessage(0, 0x3B2, 3000066); // OUCH!
                    if (m == moblist[0])
                        m.SendLocalizedMessage(1050005); // The weight of the boulder pins you to the ground!
                    else
                        m.SendLocalizedMessage(1050006); // A flying rock smashes against your head!
                    AOS.Damage(m, 250 - (ClosestDistance(m) * 30), 100, 0, 0, 0, 0);
                }
            }

            m_Attempt = new int[4];
            foreach (DoomLever lever in m_Levers)
            {
                if (lever != null)
                    lever.Pressed = false;
            }
        }
        #endregion

        #region SupportMethods
        private static void Boulder_Callback()
        {
            for (int i = 0; i < m_PlayerLocations.Length; i++)
            {
                Effects.SendLocationEffect(new Point3D(m_PlayerLocations[i].X, m_PlayerLocations[i].Y, m_PlayerLocations[i].Z + 2), Map.Malas, 0x36B0, 16, 1, 0, 0);
                Effects.PlaySound(m_PlayerLocations[i], Map.Malas, 0x307);
            }
        }

        private static void MoveThief_Callback(object state)
        {
            List<Mobile> list = (List<Mobile>)state;

            list[0].BoltEffect(0);

            foreach (Mobile thief in list)
            {
                thief.MoveToWorld(new Point3D(470, 96, -1), Map.Malas);
            }
        }

        private static int ClosestDistance(Mobile m)
        {
            double dist = m.GetDistanceToSqrt(m_PlayerLocations[0]);

            for (int i = 1; i < m_PlayerLocations.Length; i++)
            {
                double b = m.GetDistanceToSqrt(m_PlayerLocations[i]);
                if (b < dist)
                    dist = b;
            }
            return (int)dist;
        }

        private static void StatuesTalk(int correct)
        {
            foreach (DoomStatue statue in m_Statues)
            {
                if (statue != null)
                {
                    if (correct == 4)
                        statue.PublicOverheadMessage(0, 0x3B2, 1050004); // The circle is the key, the key is incomplete and so the gate remains closed.

                    else
                    {
                        statue.PublicOverheadMessage(0, 0x3B2, 1050009, correct.ToString()); // The circle of souls has failed to turn the key.  The gate remains closed...

                        if (correct == 1)
                            statue.PublicOverheadMessage(0, 0x3B2, 1050007, "1"); // ~1_NUM~ soul has turned the key correctly, but the rest have forsaken the circle...

                        else
                            statue.PublicOverheadMessage(0, 0x3B2, 1050008, correct.ToString()); // ~1_NUM~ souls have turned the key correctly, but the rest have forsaken the circle...                        
                    }
                }
            }
        }

        public static void GenerateAnswer()
        {
            m_Answer = new int[4];

            for (int i = 1; i < 5; i++)
            {
                int rand = 0;
                while (m_Answer[rand = Utility.Random(m_Answer.Length)] != 0)
                    continue;
                m_Answer[rand] = i;
            }
        }
        #endregion
    }
}
