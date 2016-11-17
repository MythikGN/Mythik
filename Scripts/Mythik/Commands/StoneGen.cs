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
            e.Mobile.SendMessage("Removing all existing stones and generating new ones...");

            ArrayList items = new ArrayList();

            foreach (Item item in World.Items.Values)
            {
                if (item is TravelStone || item is BankStone || item is ItemStone || item is ResurrectionStone)
                    items.Add(item);
            }

            for (int i = 0; i < items.Count; i++)
                ((Item)items[i]).Delete();

            // Neutral Zone - Update when NZ is built.
            CreateStone(new TravelStone(), new Point3D(5190, 1197, 5));
            CreateStone(new BankStone(), new Point3D(5189, 1197, 5));
            CreateStone(new ItemStone(10, 10.0), new Point3D(5188, 1197, 5));
            CreateStone(new ResurrectionStone(), new Point3D(5187, 1197, 5));

            CreateStone(new TravelStone(), new Point3D(5194, 1197, 5));
            CreateStone(new BankStone(), new Point3D(5195, 1197, 5));
            CreateStone(new ItemStone(10, 10.0), new Point3D(5196, 1197, 5));
            CreateStone(new ResurrectionStone(), new Point3D(5197, 1197, 5));

            // Britain - Correct
            CreateStone(new TravelStone(), new Point3D(1415, 1699, 0));
            CreateStone(new BankStone(), new Point3D(1415, 1698, 0));
            CreateStone(new ItemStone(5, 5.0), new Point3D(1415, 1697, 0));
            CreateStone(new ResurrectionStone(), new Point3D(1415, 1696, 0));

            // Cove - Correct
            CreateStone(new TravelStone(), new Point3D(2270, 1206, 0));
            CreateStone(new BankStone(), new Point3D(2269, 1206, 0));
            CreateStone(new ItemStone(5, 5.0), new Point3D(2268, 1206, 0));
            CreateStone(new ResurrectionStone(), new Point3D(2267, 1206, 0));

            // Jhelom - Correct
            CreateStone(new TravelStone(), new Point3D(1383, 3810, 0));
            CreateStone(new BankStone(), new Point3D(1383, 3809, 0));
            CreateStone(new ItemStone(5, 5.0), new Point3D(1383, 3808, 0));
            CreateStone(new ResurrectionStone(), new Point3D(1383, 3807, 0));

            // Moonglow - Correct
            CreateStone(new TravelStone(), new Point3D(4448, 1165, 0));
            CreateStone(new BankStone(), new Point3D(4448, 1164, 0));
            CreateStone(new ItemStone(5, 5.0), new Point3D(4448, 1163, 0));
            CreateStone(new ResurrectionStone(), new Point3D(4448, 1162, 0));

            // Nujel'm - Correct
            CreateStone(new TravelStone(), new Point3D(3752, 1253, 0));
            CreateStone(new BankStone(), new Point3D(3752, 1252, 0));
            CreateStone(new ItemStone(5, 5.0), new Point3D(3752, 1251, 0));
            CreateStone(new ResurrectionStone(), new Point3D(3752, 1250, 0));

            // Occlo - Correct
            CreateStone(new TravelStone(), new Point3D(3171, 1733, 0));
            CreateStone(new BankStone(), new Point3D(3170, 1733, 0));
            CreateStone(new ItemStone(5, 5.0), new Point3D(3169, 1733, 0));
            CreateStone(new ResurrectionStone(), new Point3D(3168, 1733, 0));

            // Trinsic - Correct - Should another set be added? Trinsic is big.
            CreateStone(new TravelStone(), new Point3D(1952, 2775, 10));
            CreateStone(new BankStone(), new Point3D(1952, 2774, 10));
            CreateStone(new ItemStone(5, 5.0), new Point3D(1952, 2773, 10));
            CreateStone(new ResurrectionStone(), new Point3D(1952, 2772, 10));

            // Vesper - Correct - Should another set be added further south?
            CreateStone(new TravelStone(), new Point3D(2894, 680, 0));
            CreateStone(new BankStone(), new Point3D(2893, 680, 0));
            CreateStone(new ItemStone(5, 5.0), new Point3D(2892, 680, 0));
            CreateStone(new ResurrectionStone(), new Point3D(2891, 680, 0));

            // Yew - Correct
            CreateStone(new TravelStone(), new Point3D(542, 994, 0));
            CreateStone(new BankStone(), new Point3D(542, 993, 0));
            CreateStone(new ItemStone(5, 5.0), new Point3D(542, 992, 0));
            CreateStone(new ResurrectionStone(), new Point3D(542, 991, 0));

            // Buc's Den - Correct
            CreateStone(new TravelStone(), new Point3D(317, 2641, 0));
            CreateStone(new BankStone(), new Point3D(316, 2641, 0));
            CreateStone(new ItemStone(), new Point3D(315, 2641, 0));
            CreateStone(new ResurrectionStone(), new Point3D(314, 2641, 0));

            CreateStone(new TravelStone(), new Point3D(363, 2765, 0));
            CreateStone(new BankStone(), new Point3D(362, 2765, 0));
            CreateStone(new ItemStone(), new Point3D(361, 2765, 0));
            CreateStone(new ResurrectionStone(), new Point3D(360, 2765, 0));

            CreateStone(new TravelStone(), new Point3D(372, 2696, 0));
            CreateStone(new BankStone(), new Point3D(371, 2696, 0));
            CreateStone(new ItemStone(), new Point3D(370, 2696, 0));
            CreateStone(new ResurrectionStone(), new Point3D(369, 2696, 0));

            // Magincia - Correct, but might want to move.
            CreateStone(new TravelStone(), new Point3D(3208, 1367, 20));
            CreateStone(new BankStone(), new Point3D(3207, 1367, 20));
            CreateStone(new ItemStone(), new Point3D(3206, 1367, 20));
            CreateStone(new ResurrectionStone(), new Point3D(3205, 1367, 20));

            // Minoc - Correct
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

            // Serpent's Hold - Correct - Not 100% happy with positions.
            CreateStone(new TravelStone(), new Point3D(1872, 3820, 15));
            CreateStone(new BankStone(), new Point3D(1872, 3819, 15));
            CreateStone(new ItemStone(), new Point3D(1872, 3818, 15));
            CreateStone(new ResurrectionStone(), new Point3D(1872, 3817, 15));

            // Skara Brae - Correct
            CreateStone(new TravelStone(), new Point3D(593, 2152, 0));
            CreateStone(new BankStone(), new Point3D(592, 2152, 0));
            CreateStone(new ItemStone(), new Point3D(591, 2152, 0));
            CreateStone(new ResurrectionStone(), new Point3D(590, 2152, 0));

            // Wind - Correct
            CreateStone(new TravelStone(), new Point3D(1365, 891, 0));
            CreateStone(new BankStone(), new Point3D(1365, 890, 0));
            CreateStone(new ItemStone(), new Point3D(1365, 889, 0));
            CreateStone(new ResurrectionStone(), new Point3D(1365, 888, 0));

            // Britain Bridge - Correct
            CreateStone(new TravelStone(), new Point3D(1362, 1760, 13));
            CreateStone(new BankStone(), new Point3D(1362, 1759, 13));
            CreateStone(new ItemStone(2, 2.5), new Point3D(1362, 1758, 13));
            CreateStone(new ResurrectionStone(), new Point3D(1362, 1757, 13));

            CreateStone(new TravelStone(), new Point3D(1371, 1806, 0));
            CreateStone(new BankStone(), new Point3D(1371, 1805, 0));
            CreateStone(new ItemStone(2, 2.5), new Point3D(1371, 1804, 0));
            CreateStone(new ResurrectionStone(), new Point3D(1371, 1803, 0));

            CreateStone(new TravelStone(), new Point3D(1314, 1753, 10));
            CreateStone(new BankStone(), new Point3D(1314, 1752, 10));
            CreateStone(new ItemStone(2, 2.5), new Point3D(1314, 1751, 10));
            CreateStone(new ResurrectionStone(), new Point3D(1314, 1750, 10));

            // Outside Cove - Correct
            //CreateStone(new TravelStone(), new Point3D(2284, 1187, 0));
            //CreateStone(new BankStone(), new Point3D(2284, 1188, 0));
            //CreateStone(new ItemStone(2, 2.5), new Point3D(2284, 1189, 0));
            //CreateStone(new ResurrectionStone(), new Point3D(2284, 1190, 0));

            //CreateStone(new TravelStone(), new Point3D(2314, 1205, 0));
            //CreateStone(new BankStone(), new Point3D(2314, 1204, 0));
            //CreateStone(new ItemStone(2, 2.5), new Point3D(2314, 1203, 0));
            //CreateStone(new ResurrectionStone(), new Point3D(2314, 1202, 0));

            CreateStone(new TravelStone(), new Point3D(2290, 1238, 0));
            CreateStone(new BankStone(), new Point3D(2290, 1239, 0));
            CreateStone(new ItemStone(2, 2.5), new Point3D(2290, 1240, 0));
            CreateStone(new ResurrectionStone(), new Point3D(2290, 1241, 0));

            // Vendor Mall - Update when mall is built.
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
