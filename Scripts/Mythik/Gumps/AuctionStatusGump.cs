using Server.Gumps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Scripts.Mythik.Systems;
using Server;

namespace Scripts.Mythik.Gumps
{
    public class AuctionStatusGump : Gump
    {
        private AuctionSystem auctionSystem;
        private Mobile mobile;


        public AuctionStatusGump(AuctionSystem auctionSystem, Mobile mobile) : base(25, 25)
        {
            this.auctionSystem = auctionSystem;
            this.mobile = mobile;
        }
    }
}
