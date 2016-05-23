//#define STOREONITEM

using Server;
using System;
using System.Collections.Generic;

using Scripts.Mythik.Mobiles;
using Scripts.Mythik.Systems.Achievements.Gumps;
using Server.Mobiles;
using Server.Items;
using Server.Commands;
using Server.Misc;


namespace Scripts.Mythik.Systems.Achievements
{

    public class AcheivmentSystem
    {
        public static List<Achievement> Achievements = new List<Achievement>();
        public static void Initialize()
        {
            Achievements.Add(new HunterAchievement(1, 0, 0, false, 5, "Dog Slayer", "Slay 5 Dogs", 5, typeof(Dog), typeof(Mythik.Items.Rares.RareClothDyeTub)));
            Achievements.Add(new DiscoveryAchievement(2, 0, 0x96E, false, 1, "Minoc!", "Discover Minoc Township", 5, "Minoc"));
            Achievements.Add(new HarvestAchievement(3, 0, 0, false, 500, "500 Iron Ore", "Mine 500 Iron Ore", 5, typeof(IronOre), typeof(AncientSmithyHammer)));
            Achievements.Add(new DiscoveryAchievement(4, 0, 0, false, 1, "Yew!", "Discover the Yew Township", 5, "Yew"));

            CommandSystem.Register("feats", AccessLevel.Player, new CommandEventHandler(OpenGump));

        }

        private static void OpenGump(CommandEventArgs e)
        {
            var player = e.Mobile as PlayerMobile;
            if(player != null)
                e.Mobile.SendGump(new AchievementGump(player));
        }

        internal static void SetAchievementStatus(PlayerMobile player, Achievement ach, int progress)
        {

#if STOREONITEM
           if (!AcheivmentSystemStone.GetInstance().Achievements.ContainsKey(player.Serial))
                AcheivmentSystemStone.GetInstance().Achievements.Add(player.Serial, new Dictionary<int, AcheiveData>());
            var achieves = AcheivmentSystemStone.GetInstance().Achievements[player.Serial];
#else
            var achieves = (player as MythikPlayerMobile).Achievements;
#endif


            if (achieves.ContainsKey(ach.ID))
            {
                if (achieves[ach.ID].Progress >= ach.CompletionTotal)
                    return;
                achieves[ach.ID].Progress += progress;
            }
            else
            {
                achieves.Add(ach.ID, new AcheiveData() { Progress = progress });
            }

            if (achieves[ach.ID].Progress >= ach.CompletionTotal) {
                player.SendGump(new AchievementObtainedGump(ach),false);
                achieves[ach.ID].CompletedOn = DateTime.Now;


#if STOREONITEM
                if (!AcheivmentSystemStone.GetInstance().PointsTotals.ContainsKey(player.Serial))
                    AcheivmentSystemStone.GetInstance().PointsTotals.Add(player.Serial, 0);
                AcheivmentSystemStone.GetInstance().PointsTotals[player.Serial] += ach.RewardPoints;
#else
                (player as MythikPlayerMobile).AchievementPointsTotal += ach.RewardPoints;
#endif
                if (ach.RewardItems?.Length > 0)
                {
                    try
                    {
                        var item = (Item)Activator.CreateInstance(ach.RewardItems[0]);
                        if (!WeightOverloading.IsOverloaded(player))
                            player.Backpack.DropItem(item);
                        else
                            player.BankBox.DropItem(item);
                    }
                    catch { }
                }
            }
        }
    }
}
