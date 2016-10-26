using Server;
using Server.Mobiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scripts.Mythik.Systems.CityLoyalty
{
    class CityLoyaltySystem
    {
        public static bool ApplyCityTitle(PlayerMobile pm, ObjectPropertyList list, string prefix, int loc)
        {
            return false;
        }

        internal static bool HasCustomTitle(PlayerMobile user, out string cust)
        {
            cust = "";
            return false;
        }
    }
}
