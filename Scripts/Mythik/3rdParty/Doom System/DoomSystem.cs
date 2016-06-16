using System;
using System.Collections;
using Server.Items;
using Server.Mobiles;
using Server.Commands;
using Scripts.Mythik;

namespace Server.Events.DoomSystem
{
    public static class DoomSystem
    {
        public static void Initialize()
        {
            SecretRoom.Deactivate();
            PoisonRoom.Deactivate();
            LeverPuzzle.GenerateAnswer();
            EventSink.Disconnected += new DisconnectedEventHandler(EventSink_Disconnected);
            CommandSystem.Register("GenDoomSystem", AccessLevel.Administrator, new CommandEventHandler(GenDoomSystem_OnCommand));
        }

        public static SecretRoomRegion SecretRoom = new SecretRoomRegion();
        public static PoisonRoomRegion PoisonRoom = new PoisonRoomRegion();

        private static void EventSink_Disconnected(DisconnectedEventArgs e)
        {
            Mobile m = e.Mobile;
            if (m.Region == SecretRoom && m.AccessLevel == AccessLevel.Player)
                m.Kill();
        }

        [Usage("GenDoomSystem")]
        [Description("creates all items needed for the doomsystem.")]
        private static void GenDoomSystem_OnCommand(CommandEventArgs e)
        {
            #region PoisonRoom
            CreateDoor(334, 14, -1);
            CreateDoor(344, 14, -1);
            CreateDoor(355, 14, -1);
            CreateType(365, 15, -1, typeof(Penta));
            #endregion

            #region LeverPuzzle
            CreateDoomLever(316, 64, 2, 1);
            CreateDoomLever(323, 58, 2, 2);
            CreateDoomLever(332, 63, 2, 3);
            CreateDoomLever(323, 71, 2, 4);
            CreateDoomStatue(329, 60, 19, false);
            CreateDoomStatue(319, 70, 19, true);
            CreateType(324, 64, -1, typeof(TeleportSpot));
            #endregion

            #region SecretRoom
            CreateDoomPorter(468, 92, -1, 6173, 696);
            CreateDoomPorter(469, 92, -1, 6177, 638);
            CreateDoomPorter(470, 92, -1, 6175, 133);
            CreateType(467, 92, -1, typeof(Pedestal));
            CreateType(469, 96, 5, typeof(DoomBox));
            #endregion
        }

        private static void CreateDoor(int x, int y, int z)
        {
            var loc = new Point3D(x, y, z);
            var map = Map.Malas;
            if (MythikStaticValues.UpdateLoc(ref loc, ref map))
                CreateDoor(loc, map);
        }

        private static void CreateDoor(Point3D loc, Map map)
        {
            ArrayList removelist = new ArrayList();

            for (int i = 0; i < 2; i++)
            {
                IPooledEnumerable ipe = map.GetItemsInRange(new Point3D(loc.X, loc.Y + i, loc.Z), 0);

                foreach (Item item in ipe)
                    if (item is DarkWoodDoor)
                        removelist.Add(item);                
            }

            foreach (Item item in removelist)
                item.Delete();

            DarkWoodDoor door1 = new DarkWoodDoor(DoorFacing.NorthCCW);
            DarkWoodDoor door2 = new DarkWoodDoor(DoorFacing.SouthCW);

            door1.Link = door2;
            door2.Link = door1;

            door1.MoveToWorld(loc, map);
            door2.MoveToWorld(new Point3D(loc.X, loc.Y + 1, loc.Z), map);
        }


        private static void CreateType(int x, int y, int z, Type type)
        {
            var loc = new Point3D(x, y, z);
            var map = Map.Malas;
            if (MythikStaticValues.UpdateLoc(ref loc, ref map))
                CreateType(loc, map,type);
        }

        private static void CreateType(Point3D point,Map map, Type type)
        {

            if (!Exists(point, map, type))
            {
                ((Item)Activator.CreateInstance(type)).MoveToWorld(point, map);
            }
        }

        private static void CreateDoomLever(int x, int y, int z, int id)
        {
            var loc = new Point3D(x, y, z);
            var map = Map.Malas;
            if (MythikStaticValues.UpdateLoc(ref loc, ref map))
                CreateDoomLever(loc, map, id);
        }
        private static void CreateDoomLever(Point3D point, Map map, int id)
        {

            if (!Exists(point, map, typeof(DoomLever)))
            {
                new DoomLever(id).MoveToWorld(point, map);
            }
        }

        private static void CreateDoomStatue(int x, int y, int z, bool south)
        {
            var loc = new Point3D(x, y, z);
            var map = Map.Malas;
            if (MythikStaticValues.UpdateLoc(ref loc, ref map))
                CreateDoomStatue(loc, map, south);
        }

        private static void CreateDoomStatue(Point3D point, Map map, bool south)
        {

            if (!Exists(point, map, typeof(DoomStatue)))
            {
                new DoomStatue(south).MoveToWorld(point, map);
            }
        }

        private static void CreateDoomPorter(int x, int y, int z, int itemid, int hue)
        {
            var loc = new Point3D(x, y, z);
            var map = Map.Malas;
            if (MythikStaticValues.UpdateLoc(ref loc, ref map))
                CreateDoomPorter(loc, map, itemid, hue);
        }
        private static void CreateDoomPorter(Point3D point, Map map, int itemid, int hue)
        {

            if (!Exists(point, map, typeof(DoomPorter)))
            {
                new DoomPorter(itemid, hue).MoveToWorld(point, map);
            }
        }

        private static bool Exists(Point3D point,Map map, Type type)
        {
            IPooledEnumerable ipe = map.GetItemsInRange(point, 0);

            foreach (Item item in ipe)
            {
                if (item.GetType() == type)
                {
                    return true;
                }
            }

            return false;
        }

        public static bool CanActivate(Mobile m)
        {
            if (m.AccessLevel == AccessLevel.Player && (m.Player || ((m is BaseCreature && ((BaseCreature)m).Controlled)) && !((BaseCreature)m).IsDeadPet) && m.Alive)
                return true;

            return false;
        }
    }
}