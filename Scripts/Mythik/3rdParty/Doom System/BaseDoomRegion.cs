using System;
using System.Collections.Generic;
using Server.Regions;
using Server.Mobiles;
using Server.Spells.Sixth;
using Server.Spells.Seventh;

namespace Server.Events.DoomSystem
{
    public class BaseDoomSystemRegion : BaseRegion
    {
        public bool Active;
        public int MaxPoison;
        public int MobilesAlive { get { return AliveMobiles.Count; } }
        public bool NoMobilesAlive { get { return MobilesAlive == 0; } }
        public override bool OnResurrect(Mobile from) { return false; }
        public override bool AllowHousing(Mobile from, Point3D p) { return false; }        

        public List<Mobile> AliveMobiles
        {
            get
            {
                List<Mobile> moblist = new List<Mobile>();

                foreach (Mobile m in GetMobiles())
                    if (DoomSystem.CanActivate(m))
                        moblist.Add(m);

                return moblist;
            }
        }

        public BaseDoomSystemRegion(string name, Map map, int priority, Rectangle2D[] coords, int poison) : base(name, map, priority, coords)
        {
            MaxPoison = poison;
            Register();
        }

        public override void AlterLightLevel(Mobile m, ref int global, ref int personal) { global = LightCycle.DungeonLevel; }       

        public override bool OnBeginSpellCast(Mobile m, ISpell s)
        {
            if (s is MarkSpell || s is GateTravelSpell)
            {
                m.SendMessage("You can not cast that here");
                return false;
            }
            return base.OnBeginSpellCast(m, s);
        }

        public override void OnDeath(Mobile m)
        {
            base.OnDeath(m);

            if (m.Poison != null)
                m.SendLocalizedMessage(1050057); //The end is near. You feel hopeless and desolate.  The poison is beginning to stiffen your muscles.            
        }

        public virtual void Activate()
        {
            Active = true;
            PoisonTimer timer = new PoisonTimer(this);
            timer.Start();
        }

        public virtual void Deactivate()
        {
            Active = false;
        } 

        private class PoisonTimer : Timer
        {
            private BaseDoomSystemRegion m_Region;
            private Point3D[] LocationArray;
            private int m_Count = 0;
            private int PoisonLevel
            {
                get
                {
                    int level = (int)(m_Count / 5);
                    return ((level > m_Region.MaxPoison) ? m_Region.MaxPoison : level);
                }
            }

            public PoisonTimer(BaseDoomSystemRegion region) : base(TimeSpan.FromSeconds(0), TimeSpan.FromSeconds(6.0))
            {
                m_Region = region;

                if(region is PoisonRoomRegion)
                    LocationArray = new Point3D[]
                    {
                        new Point3D(356 + 5616, 22 + 3544, 15),
                        new Point3D(356 + 5616, 16 + 3544, 15),
                        new Point3D(356 + 5616, 13 + 3544, 15),
                        new Point3D(356 + 5616, 7 + 3544, 15),
                        new Point3D(358 + 5616, 6 + 3544, 15),
                        new Point3D(363 + 5616, 6 + 3544, 15),
                        new Point3D(368 + 5616, 6 + 3544, 15),
                        new Point3D(373 + 5616, 6 + 3544, 15)
                    };
            }

            protected override void OnTick()
			{
                m_Count++;
                List<Mobile> list = m_Region.AliveMobiles;

                if(m_Region is SecretRoomRegion)
                {
                    SecretRoomRegion region = (SecretRoomRegion)m_Region;

                    if (region.NoMobilesAlive)
                    {
                        region.Deactivate();
                        Stop();
                    }

                    Point3D[] locarray = new Point3D[4];

                    for (int i = 0; i < 4; i++)
                        locarray[i] = new Point3D(465 + Utility.Random(9) + 5616, 92 + Utility.Random(9) + 3544, 6);

                    LocationArray = locarray;
                }

                else if (m_Region is PoisonRoomRegion)
                {
                    PoisonRoomRegion region = (PoisonRoomRegion)m_Region;

                    if (region.NoMobilesAlive || region.GetDarkGuardians.Count == 0)
                    {
                        region.Deactivate();
                        Stop();
                    }
                }

                foreach (Point3D point in LocationArray)
                {
                    Effects.SendLocationEffect(point, Map.Felucca, 4518, 16, 1, 1166, 0);
                    Effects.PlaySound(point, Map.Felucca, 0x231);
                }

                foreach (Mobile m in list)
                {
                    if (m.Poison == null || m.Poison.Level < PoisonLevel)
                    {
                        m.Poison = Poison.GetPoison(PoisonLevel);

                        int message = 0;

                        switch (PoisonLevel)
                        {
                            case 0: case 1: message = 1050001; break; // It is becoming more difficult for you to breathe as the poisons in the room become more concentrated.
                            case 2: case 3: message = 1050002; break; // You have trouble breathing...
                            case 4: message = 1050003; break; // You begin to panic as the poison clouds thicken.
                        }

                        if(message != 0)
                            m.SendLocalizedMessage(message);
                    }

                    if (m.Hits < 20 && Utility.RandomDouble() < 0.33)
                       m.SendLocalizedMessage(1050055); // Terror grips your spirit as you realize you may never leave this room alive.
                }
            }
        }
    }
}