using System;
using Server;
using Server.Items;
using Server.Multis;
using Server.Multis.Deeds;

namespace Mythik.Systems.Farming
{
	public interface IFarm
    {
        void AddFarmItem(Item item);
    }

    public class SmallFarmDeed : HouseDeed
    {
        public static Rectangle2D[] AreaArray = new Rectangle2D[] { new Rectangle2D(-3, -3, 7, 7), new Rectangle2D(-1, 4, 3, 1) };
        [Constructable]
        public SmallFarmDeed() : base(0x13ec, new Point3D(0, 4, 0))
        {
            Name = "deed to a small farm";
        }

        public SmallFarmDeed(Serial serial) : base(serial)
        {
        }

        public override BaseHouse GetHouse(Mobile owner)
        {
            return new SmallFarm(owner, 0x24);
        }

        public override Rectangle2D[] Area { get { return AreaArray; } }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }


    public class SmallFarm : HouseFoundation , IFarm
    {
        public static Rectangle2D[] AreaArray = new Rectangle2D[] { new Rectangle2D(-3, -3, 7, 7), new Rectangle2D(-1, 4, 3, 1) };

        public override Rectangle2D[] Area { get { return AreaArray; } }
        public override Point3D BaseBanLocation { get { return new Point3D(2, 4, 0); } }

        public override int DefaultPrice { get { return 43800; } }

        public override HousePlacementEntry ConvertEntry { get { return HousePlacementEntry.TwoStoryFoundations[0]; } }

        public SmallFarm(Mobile owner, int id) : base(owner,id, 125, 2)
        {
            uint keyValue = CreateKeys(owner);
            AddSouthDoors(0, 3, 7,true);
           // AddSouthDoor(0, 3, 7, keyValue);
           // SetSign(2, 4, 5);
        }

        public void AddFarmItem(Item item)
        {
            this.LockDowns.Add(item);
        }


        public SmallFarm(Serial serial) : base(serial)
        {
        }

        public override HouseDeed GetDeed()
        {
            return new SmallFarmDeed();
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write((int)0);//version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();
        }

      
    }

}