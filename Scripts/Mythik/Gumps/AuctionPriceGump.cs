﻿using Server.Gumps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Server;
using Server.Network;

namespace Scripts.Mythik.Gumps
{
    public class AuctionPriceGump : Gump
    {
        private Mobile from;
        private Item item;
        private Action<Item, int> startAuction;

        public AuctionPriceGump(Mobile from, Item item, Action<Item, int> startAuction) : base(25,25)
        {
            this.from = from;
            this.item = item;
            this.startAuction = startAuction;
        }

        public override void OnResponse(NetState sender, RelayInfo info)
        {
            if(info.TextEntries.Length == 0 || string.IsNullOrWhiteSpace(info.TextEntries[0].Text))
            {
                return;
            }
            int price = -1;
            int.TryParse(info.TextEntries[0].Text, out price);
            if (price > -1 && price < 20000000)
                startAuction(item, price);
            base.OnResponse(sender, info);
        }
    }
}