using System;
using System.Collections;
using System.Collections.Generic;
using Server;
using Server.Network;
using Server.Gumps;

namespace Knives.TownHouses
{
    public class GumpResponse
    {
        public static void Initialize()
        {
            Timer.DelayCall(TimeSpan.Zero, new TimerCallback(AfterInit));
        }

        private static void AfterInit()
        {
            //PacketHandlers.Register(0xB1, 0, true, new OnPacketReceive(DisplayGumpResponse));
            EventSink.GumpResponseHandler += EventSink_GumpResponseHandler;
        }

        private static void EventSink_GumpResponseHandler(GumpResponseArgs e)
        {
            CheckResponse(e.Version, e.State.Mobile, e.ButtonID);
        }

      

        private static bool CheckResponse(Gump gump, Mobile m, int id)
        {
            if (m == null || !m.Player)
                return true;

            TownHouse th = null;

            ArrayList list = new ArrayList();
            foreach (Item item in m.GetItemsInRange(20))
                if (item is TownHouse)
                    list.Add(item);

            foreach (TownHouse t in list)
                if (t.Owner == m)
                {
                    th = t;
                    break;
                }

            if (th == null || th.ForSaleSign == null)
                return true;

            if (gump is HouseGumpAOS)
            {
                int val = id - 1;

                if (val < 0)
                    return true;

                int type = val % 15;
                int index = val / 15;

                if (th.ForSaleSign.ForcePublic && type == 3 && index == 12 && th.Public)
                {
                    m.SendMessage("This house cannot be private.");
                    m.SendGump(gump);
                    return false;
                }

                if (th.ForSaleSign.ForcePrivate && type == 3 && index == 13 && !th.Public)
                {
                    m.SendMessage("This house cannot be public.");
                    m.SendGump(gump);
                    return false;
                }

                if (th.ForSaleSign.NoTrade && type == 6 && index == 1)
                {
                    m.SendMessage("This house cannot be traded.");
                    m.SendGump(gump);
                    return false;
                }
            }
            else if (gump is HouseGump)
            {
                if (th.ForSaleSign.ForcePublic && id == 17 && th.Public)
                {
                    m.SendMessage("This house cannot be private.");
                    m.SendGump(gump);
                    return false;
                }

                if (th.ForSaleSign.ForcePrivate && id == 17 && !th.Public)
                {
                    m.SendMessage("This house cannot be public.");
                    m.SendGump(gump);
                    return false;
                }

                if (th.ForSaleSign.NoTrade && id == 14)
                {
                    m.SendMessage("This house cannot be traded.");
                    m.SendGump(gump);
                    return false;
                }
            }

            return true;
        }
    }
}