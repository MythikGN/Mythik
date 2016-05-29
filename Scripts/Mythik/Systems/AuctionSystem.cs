using Scripts.Mythik.Gumps;
using Scripts.Mythik.Mobiles;
using Server;
using Server.Commands;
using Server.Items;
using Server.Mobiles;
using Server.Targeting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scripts.Mythik.Systems
{
    public class AuctionSystem : Container
    {
        enum AuctionStatus
        {
            Ready,
            Starting,
            Ongoing,
            Finishing,

        }
        private static bool Enabled = true;

        //private bool m_auctionInProgress = false;
        private AuctionStatus m_auctionStatus = AuctionStatus.Ready;
        private int m_StartPrice;
        private Mobile m_ItemOwner;
        private AuctionTimer m_timer;
        private Item m_Item;

        private int m_CurrentBid;
        private Mobile m_CurrentBidder;
        private readonly double TAX = 0.90; // 10% tax

        //item is stored inside
        private List<MythikPlayerMobile> m_ListeningToAuction = new List<MythikPlayerMobile>();

        [Constructable]
        public AuctionSystem() : base(0x00)
        {
            if(Enabled)
            {
                CommandSystem.Register("auction", AccessLevel.Player, new CommandEventHandler(OnAuction));
                CommandSystem.Register("auctionon", AccessLevel.Player, new CommandEventHandler(OnAuctionOn));
                CommandSystem.Register("auctionoff", AccessLevel.Player, new CommandEventHandler(OnAuctionOff));
                CommandSystem.Register("auctionstatus", AccessLevel.Player, new CommandEventHandler(OnAuctionStatus));
                CommandSystem.Register("bid", AccessLevel.Player, new CommandEventHandler(OnBid));
                EventSink.Login += EventSink_Login;
                EventSink.Logout += EventSink_Logout;
            }

        }

        private void EventSink_Logout(LogoutEventArgs e)
        {
            var pm = e.Mobile as MythikPlayerMobile;
            if (m_ListeningToAuction.Contains(pm))
                m_ListeningToAuction.Remove(pm);
        }

        private void EventSink_Login(LoginEventArgs e)
        {
            var pm = e.Mobile as MythikPlayerMobile;
            if (pm.AuctionEnabled && !m_ListeningToAuction.Contains(pm))
                m_ListeningToAuction.Add(pm);
        }
        [Usage("bid")]
        [Description("Bid on a currently running auction.")]
        private void OnBid(CommandEventArgs e)
        {
            var pm = e.Mobile as MythikPlayerMobile;
            if (m_auctionStatus != AuctionStatus.Ongoing)
            {
                e.Mobile.SendMessage("No auctions currently active.");
                return;
            }
            int bid = -1;
            int.TryParse(e.Arguments[0], out bid);
            if(bid < m_CurrentBid + 50)
            {
                e.Mobile.SendMessage("You must bid atleast 50gp more than the current bid.");
                return;
            }
           
            if (pm.Backpack.ConsumeTotal(typeof(Gold), bid, true) || Banker.Withdraw(pm, bid))
            {
                if (m_CurrentBidder != null)
                {
                    m_CurrentBidder.PlaceInBackpack(new BankCheck((int)(m_CurrentBid)));
                    //return current bidders gold.
                }
                Broadcast(e.Mobile.Name + " bids " + e.Arguments[0] + " On the " + m_Item.Name);
                m_CurrentBid = bid;
                m_CurrentBidder = pm;
                this.m_timer = new AuctionTimer(this);
                m_timer.Start();
            }
            //consume the gold.
          
        }
        private void Broadcast(string msg)
        {
            foreach (var pm in m_ListeningToAuction)
                pm.SendMessage(0xBAD,msg);
        }
        [Usage("auction")]
        [Description("Auction an Item using the Mythik Auction system")]
        private void OnAuction(CommandEventArgs e)
        {
            var pm = e.Mobile as MythikPlayerMobile;
            if(m_auctionStatus != AuctionStatus.Ready)
            {
                pm.SendMessage("An Auction is already in progress, please wait for it to finish");
                return;
            }
            m_StartPrice = -1;

            pm.SendMessage("Select an Item to Auction");
            pm.BeginTarget(1, false, Server.Targeting.TargetFlags.None, OnAuctionItemTargeted);
            pm.Target = new AuctionTarget(OnAuctionItemTargeted);
            m_auctionStatus = AuctionStatus.Starting;
            if(e.Arguments.Length > 0)
            {
                int price = -1;
                int.TryParse(e.Arguments[0], out price);
                if (price > -1 && price < 20000000)
                    m_StartPrice = price;
            }
        }
        private class AuctionTarget : Target
        {
            private TargetCallback m_Callback;

            public AuctionTarget(TargetCallback callback)
                : base(1, false, TargetFlags.None)
            {
                m_Callback = callback;
            }

            protected override void OnTarget(Mobile from, object targeted)
            {
                if (m_Callback != null)
                    m_Callback(from, targeted);
            }
            protected override void OnTargetCancel(Mobile from, TargetCancelType cancelType)
            {
                base.OnTargetCancel(from, cancelType);
                if (m_Callback != null)
                    m_Callback(from, null);
            }
        }
        private void OnAuctionItemTargeted(Mobile from, object targeted)

        {
            var item = targeted as Item;
            if(item == null || item is BaseContainer)
            {
                from.SendMessage("Only items can be auctioned.");
                m_auctionStatus = AuctionStatus.Ready;
                return;
            }
            if (!from.Backpack.Items.Contains(item))
            {
                from.SendMessage("The item must be in your backpack to auction.");
                m_auctionStatus = AuctionStatus.Ready;
                return;
            }
            this.AddItem(item);
            this.m_ItemOwner = from;
            //start a timer to timeout if they dont fill in price
            if(m_StartPrice == -1)
            {
                from.SendGump(new AuctionPriceGump(from,item,StartAuction));
            }
            else
            {
                StartAuction(item,m_StartPrice);
            }
        }

        private void StartAuction(Item item, int m_StartPrice)
        {

            m_Item = item;
            Broadcast("Auction Starting!");
            Broadcast(item.Amount + " " + item.Name + " Price: " + m_StartPrice);
            m_CurrentBid = m_StartPrice;
            this.m_timer = new AuctionTimer(this);
            m_timer.Start();
            m_auctionStatus = AuctionStatus.Ongoing;
        }
        private class AuctionTimer : Timer
        {
            private AuctionSystem m_System;
            private Stopwatch _timeLeft =  Stopwatch.StartNew();
            public AuctionTimer(AuctionSystem system) : base(TimeSpan.FromSeconds(0), TimeSpan.FromSeconds(9),4)
            {
                m_System = system;     
            }
            protected override void OnTick()
            {
                if(this.Next == null || new TimeSpan(0, 0, 30) - _timeLeft.Elapsed < TimeSpan.FromSeconds(1))
                {
                    m_System.EndAuction();
                }
                m_System.AnnounceAuctionStatus(new TimeSpan(0,0,30) - _timeLeft.Elapsed);
            }
           
        }

        private void AnnounceAuctionStatus(TimeSpan timeSpan)
        {
            Broadcast(m_Item.Amount + " " + m_Item.Name + " Price: " + m_CurrentBid + "  " + timeSpan.Seconds + " Remaining!");
        }

        private void EndAuction()
        {
            m_auctionStatus = AuctionStatus.Finishing;
            Broadcast("Auction Ended");
            if(m_CurrentBidder == null)
            {
                m_ItemOwner.PlaceInBackpack(m_Item);

            }
            else
            {
                m_CurrentBidder.PlaceInBackpack(m_Item);
                m_ItemOwner.PlaceInBackpack(new BankCheck((int)(m_CurrentBid * TAX)));
            }
            m_ItemOwner = null;
            m_CurrentBid = -1;
            m_CurrentBid = -1;
            m_CurrentBidder = null;
            m_auctionStatus = AuctionStatus.Ready;
        }



        private void OnAuctionStatus(CommandEventArgs e)
        {
            //Send AuctionStatusGump
            e.Mobile.SendGump(new AuctionStatusGump(this, e.Mobile));
        }
        [Usage("auctionoff")]
        [Description("Disable participation in the auction channel.")]
        private void OnAuctionOff(CommandEventArgs e)
        {
            var pm = e.Mobile as MythikPlayerMobile;
            if(m_ItemOwner == pm)
            {
                pm.SendMessage("Please wait for your auction to finish");
                return;
            }
            pm.AuctionEnabled = false;//TODO serialize this
            if(m_ListeningToAuction.Contains(pm))
            m_ListeningToAuction.Remove(pm);
        }
        [Usage("auctionon")]
        [Description("Enable participation in the auction channel")]
        private void OnAuctionOn(CommandEventArgs e)
        {
            var pm = e.Mobile as MythikPlayerMobile;
            if (!pm.AuctionEnabled)
                pm.SendMessage("Welcome to the Mythik auction system.");
            pm.AuctionEnabled = true;
            if (!m_ListeningToAuction.Contains(pm))
                m_ListeningToAuction.Add(pm);
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)1);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();

           
        }
    }
}
