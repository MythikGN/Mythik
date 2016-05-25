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
using Scripts.Mythik.Systems.Achievements.Items;

namespace Scripts.Mythik.Systems.Achievements
{
    //TODO
    // Achievement prereq achieve before showing
    //TODO Skill gain achieves needs event
    //TODO ITEM crafted event sink
    // 
    public class AchievmentSystem
    {
        public static List<Achievement> Achievements = new List<Achievement>();
        public static string[] Categories = new string[] { "Exploration", "Resource Gathering", "Character Development", "Hunting", "Other" };

        public static void Initialize()
        {
            Achievements.Add(new DiscoveryAchievement(0, 0, 0x96E, false, "Minoc!", "Discover Minoc Township", 5, "Minoc"));
            Achievements.Add(new DiscoveryAchievement(1, 0, 0x96E, false, "Yew!", "Discover the Yew Township", 5, "Yew"));
            Achievements.Add(new DiscoveryAchievement(2, 0, 0x96E, false, "Trinsic!", "Discover the Trinsic Township", 5, "Trinsic"));
            Achievements.Add(new DiscoveryAchievement(3, 0, 0x96E, false, "Cove!", "Discover the Cove Township", 5, "Cove"));
            Achievements.Add(new DiscoveryAchievement(4, 0, 0x96E, false, "Wrong!", "Discover the dungeon of Wrong", 5, "Wrong"));
            Achievements.Add(new DiscoveryAchievement(5, 0, 0x96E, false, "Shame!", "Discover the dungeon of Shame", 5, "Shame"));

            Achievements.Add(new HarvestAchievement(100, 1, 0, false, 500, "500 Iron Ore", "Mine 500 Iron Ore", 5, typeof(IronOre), typeof(AncientSmithyHammer)));
            Achievements.Add(new HarvestAchievement(100, 1, 0, false, 500, "50000 Iron Ore", "Mine 500 Iron Ore", 5, typeof(IronOre), typeof(AncientSmithyHammer)));

            Achievements.Add(new HunterAchievement(300, 3, 0x25D1, false, 5, "Dog Slayer", "Slay 5 Dogs", 5, typeof(Dog)));
            Achievements.Add(new HunterAchievement(301, 3, 0x25D1, false, 50, "Dragon Slayer", "Slay 50 Dragon", 5, typeof(Dragon), typeof(Mythik.Items.Rares.RareClothDyeTub)));




            CommandSystem.Register("feats", AccessLevel.Player, new CommandEventHandler(OpenGump));

        }

        private static void OpenGump(CommandEventArgs e)
        {
            var player = e.Mobile as PlayerMobile;
            if(player != null)
            {
#if STOREONITEM
           if (!AcheivmentSystemMemoryStone.GetInstance().Achievements.ContainsKey(player.Serial))
                AcheivmentSystemMemoryStone.GetInstance().Achievements.Add(player.Serial, new Dictionary<int, AcheiveData>());
            var achieves = AcheivmentSystemMemoryStone.GetInstance().Achievements[player.Serial];
                var total = AcheivmentSystemMemoryStone.GetInstance().PointsTotals[player.Serial];
#else
                var achieves = (player as MythikPlayerMobile).Achievements;
                var total = (player as MythikPlayerMobile).AchievementPointsTotal;
#endif
                e.Mobile.SendGump(new AchievementGump(achieves, total));
            }
            
        }

        internal static void SetAchievementStatus(PlayerMobile player, Achievement ach, int progress)
        {

#if STOREONITEM
           if (!AcheivmentSystemMemoryStone.GetInstance().Achievements.ContainsKey(player.Serial))
                AcheivmentSystemMemoryStone.GetInstance().Achievements.Add(player.Serial, new Dictionary<int, AcheiveData>());
            var achieves = AcheivmentSystemMemoryStone.GetInstance().Achievements[player.Serial];
            
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
                if (!AcheivmentSystemMemoryStone.GetInstance().PointsTotals.ContainsKey(player.Serial))
                    AcheivmentSystemMemoryStone.GetInstance().PointsTotals.Add(player.Serial, 0);
                AcheivmentSystemMemoryStone.GetInstance().PointsTotals[player.Serial] += ach.RewardPoints;
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
