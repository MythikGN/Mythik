using Scripts.Mythik.Items.Craftables.Alch;
using Server;
using Server.Commands;
using Server.Items;
using Server.Mobiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scripts.Mythik.Commands
{
    public class PlayerCommands
    {
        public static void Initialize()
        {
            CommandSystem.Register("mp", AccessLevel.Player, new CommandEventHandler(DrinkManaPotion));
            CommandSystem.Register("hp", AccessLevel.Player, new CommandEventHandler(DrinkHealPotion));
            CommandSystem.Register("rp", AccessLevel.Player, new CommandEventHandler(DrinkRefreshPotion));
            CommandSystem.Register("ip", AccessLevel.Player, new CommandEventHandler(DrinkInvisPotion));
            CommandSystem.Register("cp", AccessLevel.Player, new CommandEventHandler(DrinkCurePotion));
            CommandSystem.Register("rt", AccessLevel.Player, new CommandEventHandler(DrinkRestoPotion));
            CommandSystem.Register("fs", AccessLevel.Player, new CommandEventHandler(UseFSScroll));
            CommandSystem.Register("heal", AccessLevel.Player, new CommandEventHandler(UseBandage));


        }

        private static void UseBandage(CommandEventArgs e)
        {
            var player = e.Mobile as PlayerMobile;
            if (player != null)
            {
                var pot = player.Backpack.FindItemByType<Bandage>(true);
                if (pot != null)
                    pot.OnDoubleClick(player);
            }
        }

        private static void UseFSScroll(CommandEventArgs e)
        {
            var player = e.Mobile as PlayerMobile;
            if (player != null)
            {
                var pot = player.Backpack.FindItemByType<FlamestrikeScroll>(true);
                if (pot != null)
                    pot.OnDoubleClick(player);
            }
        }

        private static void DrinkRestoPotion(CommandEventArgs e)
        {
           /* var player = e.Mobile as PlayerMobile;
            if (player != null)
            {
                var pot = player.Backpack.FindItemByType<Rest>(true);
                if (pot != null)
                    pot.OnDoubleClick(player);
            }*/
        }

        private static void DrinkCurePotion(CommandEventArgs e)
        {
            var player = e.Mobile as PlayerMobile;
            if (player != null)
            {
                var pot = player.Backpack.FindItemByType<BaseCurePotion>(true);
                if (pot != null)
                    pot.OnDoubleClick(player);
            }
        }

        private static void DrinkInvisPotion(CommandEventArgs e)
        {
            var player = e.Mobile as PlayerMobile;
            if (player != null)
            {
                var pot = player.Backpack.FindItemByType<InvisibilityPotion>(true);
                if (pot != null)
                    pot.OnDoubleClick(player);
            }
        }

        private static void DrinkRefreshPotion(CommandEventArgs e)
        {
            var player = e.Mobile as PlayerMobile;
            if (player != null)
            {
                var pot = player.Backpack.FindItemByType<BaseRefreshPotion>(true);
                if (pot != null)
                    pot.OnDoubleClick(player);
            }
        }

        private static void DrinkHealPotion(CommandEventArgs e)
        {
            var player = e.Mobile as PlayerMobile;
            if (player != null)
            {
                var pot = player.Backpack.FindItemByType<BaseHealPotion>(true);
                if (pot != null)
                    pot.OnDoubleClick(player);
            }
        }

        private static void DrinkManaPotion(CommandEventArgs e)
        {
            var player = e.Mobile as PlayerMobile;
            if(player != null)
            {
                var pot = player.Backpack.FindItemByType<BaseManaPotion>(true);
                if (pot != null)
                    pot.OnDoubleClick(player);
            }
        }
    }
}
