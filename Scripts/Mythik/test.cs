

using System;
using System.Collections;
using Server;
using Server.Items;
using Scripts.Mythik.Items.Stones;

namespace Server.Commands
{
    public class StonesGen
    {
        public static void Initialize()
        {
            CommandSystem.Register("StonesGen", AccessLevel.Administrator, new CommandEventHandler(StonesGen_OnCommand));
        }

        [Usage("StonesGen")]
        [Description("Generate stones over the world.")]
        private static void StonesGen_OnCommand(CommandEventArgs e)
        {
            e.Mobile.SendMessage("Generating stones, please wait.");

            ArrayList items = new ArrayList();

            foreach (Item item in World.Items.Values)
            {
                if (item is TravelStone || item is BankStone || item is ItemStone || item is ResurrectionStone)
                    items.Add(item);
            }

            for (int i = 0; i < items.Count; i++)
                ((Item)items[i]).Delete();

            // Neutral Zone
            CreateStone(new TravelStone(), new Point3D(5190, 1197, 5));
            CreateStone(new BankStone(), new Point3D(5189, 1197, 5));
            CreateStone(new ItemStone(10, 10.0), new Point3D(5188, 1197, 5));
            CreateStone(new ResurrectionStone(), new Point3D(5187, 1197, 5));

            CreateStone(new TravelStone(), new Point3D(5194, 1197, 5));
            CreateStone(new BankStone(), new Point3D(5195, 1197, 5));
            CreateStone(new ItemStone(10, 10.0), new Point3D(5196, 1197, 5));
            CreateStone(new ResurrectionStone(), new Point3D(5197, 1197, 5));

            // Britain
            CreateStone(new TravelStone(), new Point3D(1415, 1699, 0));
            CreateStone(new BankStone(), new Point3D(1415, 1698, 0));
            CreateStone(new ItemStone(5, 5.0), new Point3D(1415, 1697, 0));
            CreateStone(new ResurrectionStone(), new Point3D(1415, 1696, 0));

            // Cove
            CreateStone(new TravelStone(), new Point3D(2270, 1206, 0));
            CreateStone(new BankStone(), new Point3D(2269, 1206, 0));
            CreateStone(new ItemStone(5, 5.0), new Point3D(2268, 1206, 0));
            CreateStone(new ResurrectionStone(), new Point3D(2267, 1206, 0));

            // Jhelom
            CreateStone(new TravelStone(), new Point3D(1383, 3810, 0));
            CreateStone(new BankStone(), new Point3D(1383, 3809, 0));
            CreateStone(new ItemStone(5, 5.0), new Point3D(1383, 3808, 0));
            CreateStone(new ResurrectionStone(), new Point3D(1383, 3807, 0));

            // Moonglow
            CreateStone(new TravelStone(), new Point3D(4463, 1177, 0));
            CreateStone(new BankStone(), new Point3D(4463, 1176, 0));
            CreateStone(new ItemStone(5, 5.0), new Point3D(4463, 1175, 0));
            CreateStone(new ResurrectionStone(), new Point3D(4463, 1174, 0));

            // Nujel'm
            CreateStone(new TravelStone(), new Point3D(3754, 1241, 0));
            CreateStone(new BankStone(), new Point3D(3755, 1241, 0));
            CreateStone(new ItemStone(5, 5.0), new Point3D(3756, 1241, 0));
            CreateStone(new ResurrectionStone(), new Point3D(3757, 1241, 0));

            // Occlo
            CreateStone(new TravelStone(), new Point3D(3683, 2525, 0));
            CreateStone(new BankStone(), new Point3D(3682, 2525, 0));
            CreateStone(new ItemStone(5, 5.0), new Point3D(3681, 2525, 0));
            CreateStone(new ResurrectionStone(), new Point3D(3680, 2525, 0));

            // Trinsic
            CreateStone(new TravelStone(), new Point3D(1952, 2775, 10));
            CreateStone(new BankStone(), new Point3D(1952, 2774, 10));
            CreateStone(new ItemStone(5, 5.0), new Point3D(1952, 2773, 10));
            CreateStone(new ResurrectionStone(), new Point3D(1952, 2772, 10));

            // Vesper
            CreateStone(new TravelStone(), new Point3D(2895, 680, 0));
            CreateStone(new BankStone(), new Point3D(2894, 680, 0));
            CreateStone(new ItemStone(5, 5.0), new Point3D(2893, 680, 0));
            CreateStone(new ResurrectionStone(), new Point3D(2892, 680, 0));

            // Yew
            CreateStone(new TravelStone(), new Point3D(542, 994, 0));
            CreateStone(new BankStone(), new Point3D(542, 993, 0));
            CreateStone(new ItemStone(5, 5.0), new Point3D(542, 992, 0));
            CreateStone(new ResurrectionStone(), new Point3D(542, 991, 0));

            // Buc's Den
            CreateStone(new TravelStone(), new Point3D(2661, 2107, 0));
            CreateStone(new BankStone(), new Point3D(2660, 2107, 0));
            CreateStone(new ItemStone(), new Point3D(2659, 2107, 0));
            CreateStone(new ResurrectionStone(), new Point3D(2658, 2107, 0));

            CreateStone(new TravelStone(), new Point3D(2705, 2230, 0));
            CreateStone(new BankStone(), new Point3D(2706, 2230, 0));
            CreateStone(new ItemStone(), new Point3D(2707, 2230, 0));
            CreateStone(new ResurrectionStone(), new Point3D(2708, 2230, 0));

            CreateStone(new TravelStone(), new Point3D(2704, 2167, 0));
            CreateStone(new BankStone(), new Point3D(2705, 2167, 0));
            CreateStone(new ItemStone(), new Point3D(2706, 2167, 0));
            CreateStone(new ResurrectionStone(), new Point3D(2707, 2167, 0));

            // Minoc
            CreateStone(new TravelStone(), new Point3D(2456, 434, 15));
            CreateStone(new BankStone(), new Point3D(2456, 435, 15));
            CreateStone(new ItemStone(), new Point3D(2456, 436, 15));
            CreateStone(new ResurrectionStone(), new Point3D(2456, 437, 15));

            CreateStone(new TravelStone(), new Point3D(2493, 486, 15));
            CreateStone(new BankStone(), new Point3D(2494, 486, 15));
            CreateStone(new ItemStone(), new Point3D(2495, 486, 15));
            CreateStone(new ResurrectionStone(), new Point3D(2496, 486, 15));

            CreateStone(new TravelStone(), new Point3D(2467, 542, 0));
            CreateStone(new BankStone(), new Point3D(2468, 542, 0));
            CreateStone(new ItemStone(), new Point3D(2469, 542, 0));
            CreateStone(new ResurrectionStone(), new Point3D(2470, 542, 0));

            // Serpent's Hold
            CreateStone(new TravelStone(), new Point3D(3016, 3404, 15));
            CreateStone(new BankStone(), new Point3D(3016, 3403, 15));
            CreateStone(new ItemStone(), new Point3D(3016, 3402, 15));
            CreateStone(new ResurrectionStone(), new Point3D(3016, 3401, 15));

            // Skara Brae
            CreateStone(new TravelStone(), new Point3D(594, 2152, 0));
            CreateStone(new BankStone(), new Point3D(593, 2152, 0));
            CreateStone(new ItemStone(), new Point3D(592, 2152, 0));
            CreateStone(new ResurrectionStone(), new Point3D(591, 2152, 0));

            // Wind
            CreateStone(new TravelStone(), new Point3D(1358, 898, 0));
            CreateStone(new BankStone(), new Point3D(1358, 897, 0));
            CreateStone(new ItemStone(), new Point3D(1358, 896, 0));
            CreateStone(new ResurrectionStone(), new Point3D(1358, 895, 0));

            // Britain Bridge
            CreateStone(new TravelStone(), new Point3D(1363, 1760, 13));
            CreateStone(new BankStone(), new Point3D(1363, 1759, 13));
            CreateStone(new ItemStone(2, 2.5), new Point3D(1363, 1758, 13));
            CreateStone(new ResurrectionStone(), new Point3D(1363, 1757, 13));

            CreateStone(new TravelStone(), new Point3D(1371, 1806, 0));
            CreateStone(new BankStone(), new Point3D(1371, 1805, 0));
            CreateStone(new ItemStone(2, 2.5), new Point3D(1371, 1804, 0));
            CreateStone(new ResurrectionStone(), new Point3D(1371, 1803, 0));

            CreateStone(new TravelStone(), new Point3D(1316, 1754, 10));
            CreateStone(new BankStone(), new Point3D(1316, 1753, 10));
            CreateStone(new ItemStone(2, 2.5), new Point3D(1316, 1752, 10));
            CreateStone(new ResurrectionStone(), new Point3D(1316, 1751, 10));

            // Outside Cove
            CreateStone(new TravelStone(), new Point3D(2284, 1187, 0));
            CreateStone(new BankStone(), new Point3D(2284, 1188, 0));
            CreateStone(new ItemStone(2, 2.5), new Point3D(2284, 1189, 0));
            CreateStone(new ResurrectionStone(), new Point3D(2284, 1190, 0));

            CreateStone(new TravelStone(), new Point3D(2314, 1205, 0));
            CreateStone(new BankStone(), new Point3D(2314, 1204, 0));
            CreateStone(new ItemStone(2, 2.5), new Point3D(2314, 1203, 0));
            CreateStone(new ResurrectionStone(), new Point3D(2314, 1202, 0));

            CreateStone(new TravelStone(), new Point3D(2290, 1236, 0));
            CreateStone(new BankStone(), new Point3D(2290, 1237, 0));
            CreateStone(new ItemStone(2, 2.5), new Point3D(2290, 1238, 0));
            CreateStone(new ResurrectionStone(), new Point3D(2290, 1239, 0));

            // Vendor Mall
            CreateStone(new TravelStone(), new Point3D(5197, 434, 15));
            CreateStone(new BankStone(), new Point3D(5198, 434, 15));
            CreateStone(new ItemStone(10, 10.0), new Point3D(5199, 434, 15));
            CreateStone(new ResurrectionStone(), new Point3D(5200, 434, 15));
        }

        private static void CreateStone(Item item, Point3D location)
        {
            item.MoveToWorld(location, Map.Felucca);
        }
    }
}

