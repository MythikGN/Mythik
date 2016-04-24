using Server;
using Server.Mobiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scripts.Mythik.Localizations
{
    public abstract class Locale
    {
        public static Locale GetLocale(Mobile pm)
        {
            switch(pm.Language)
            {
                case "ENU":
                    return Locale_ENU.Current;
                    break;
            }
            return Locale_ENU.Current;
        }

        public string RESSURRECTION_STONE_NOT_DEAD;
        public string RESSURRECTION_STONE_WAIT;

        public string BANK_GUMP_TITLE;
        public string BANK_GUMP_HAIL;
        public string BANK_GUMP_BALANCE;
        public string BANK_GUMP_OPEN;
        public string BANK_GUMP_ITEMS;
        public string BANK_GUMP_WEIGHT;
        public string BANK_GUMP_DEPOSIT;
        public string BANK_GUMP_WITHDRAW;
        public string BANK_GUMP_WITHDRAW_AMOUNT;
        public string BANK_GUMP_WITHDRAW_TITLE;
        public string BANK_STONE_DEPOSITED;
        public string BANK_STONE_DEPOSITED_CHEQUE;
        public string BANK_STONE_DEPOSIT_NOT_ENOUGH;
        public string BANK_STONE_NO_GOLD;
        public string BANK_STONE_WITHDRAW_GOT_GOLD;
        public string BANK_STONE_WITHDRAW_GOT_CHEQUE;
        public string BANK_STONE_WITHDRAW_NOT_ENOUGH;
        public string BANK_STONE_WITHDRAW_INVALID;

        public string TRAVEL_STONE_TRAVELING;
        public string TRAVEL_STONE_NO_GOLD;
    }
}
