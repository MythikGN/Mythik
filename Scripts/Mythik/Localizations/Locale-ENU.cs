using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scripts.Mythik.Localizations
{
    class Locale_ENU : Locale
    {

        static Locale m_Current;


        public static Locale Current { get { if (m_Current == null) m_Current = new Locale_ENU(); return m_Current; } }
        public Locale_ENU()
        {
            BANK_GUMP_TITLE = "<BASEFONT COLOR=\"#FFFFFF\"><CENTER>Mythik Bank Menu</CENTER></BASEFONT>";
            BANK_GUMP_HAIL = "<BASEFONT COLOR=\"#FFFFFF\">Hail, {0}!</BASEFONT>";
            BANK_GUMP_BALANCE = "<BASEFONT COLOR=\"#FFFFFF\"><CENTER>* Balance *<BR>{0}</CENTER></BASEFONT>";
            BANK_GUMP_ITEMS = "<BASEFONT COLOR=\"#FFFFFF\"><CENTER>* Items *<BR>{0}</CENTER></BASEFONT>";
            BANK_GUMP_WEIGHT = "<BASEFONT COLOR=\"#FFFFFF\"><CENTER>* Weight *<BR>{0}</CENTER></BASEFONT>";
            BANK_GUMP_OPEN = "<BASEFONT COLOR =\"#FFFFFF\">Open Bank Box<BR></BASEFONT>";
            BANK_GUMP_DEPOSIT = "<BASEFONT COLOR =\"#FFFFFF\">Deposit Gold<BR></BASEFONT>";
            BANK_GUMP_WITHDRAW = "<BASEFONT COLOR=\"#FFFFFF\">Withdraw Gold<BR></BASEFONT>";
            BANK_STONE_DEPOSITED_CHEQUE = "A bank check for {0} gold has been deposited into your bank box.";
            BANK_STONE_DEPOSITED = "{0} gold pieces have been deposited into your bank box";
            BANK_STONE_DEPOSIT_NOT_ENOUGH = "You must despoit gold in amounts higher then 50.";
            BANK_STONE_NO_GOLD = "You do not have any gold in your bank box.";
            BANK_GUMP_WITHDRAW_TITLE = "<BASEFONT COLOR=\"#FFFFFF\"><CENTER>Mythik Bank Withdraw Menu</CENTER></BASEFONT>";
            BANK_GUMP_WITHDRAW_AMOUNT = "How much gold would you like to withdraw?";
            BANK_STONE_WITHDRAW_GOT_CHEQUE = "A check for {0} gold pieces has been dropped in your back pack.";
            BANK_STONE_WITHDRAW_GOT_GOLD = "{0} gold pieces have been dropped in your back pack";
            BANK_STONE_WITHDRAW_NOT_ENOUGH = "You do not have that much gold in your bank box.";
            BANK_STONE_WITHDRAW_INVALID = "You must enter a valid number.";

            TRAVEL_STONE_NO_GOLD = "You do not have {0} gold to pay for this service!";
            TRAVEL_STONE_TRAVELING = "You are already being teleported elsewhere!";


            RESSURRECTION_STONE_NOT_DEAD = "You are not dead.";
            RESSURRECTION_STONE_WAIT = "You must wait {0} second{1} before you can be be resurrected!";
        }
    }
}
