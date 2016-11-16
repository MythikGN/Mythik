using Server;
using Server.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Server.Engines.Harvest;
using Server.Engines.Craft;
using Server.Regions;
using Mythik.Systems.Farming;
using Scripts.Mythik.Systems.Farming.Items;
using Server.Misc;

namespace Scripts.Mythik.Systems.Farming
{
    public class FarmingTool : Item
    {
        private int m_UsesRemaining;
        public int UsesRemaining { get { return m_UsesRemaining; } set { m_UsesRemaining = value; InvalidateProperties(); } }

        public int UsesMax;
        [Constructable]
        public FarmingTool() : base(0xE87)
        {

        }



        public override void OnDoubleClick(Mobile from)
        {
            base.OnDoubleClick(from);
            from.BeginTarget(1, false, Server.Targeting.TargetFlags.None, OnTarget);
        }
        private void OnTarget(Mobile from, object targeted)
        {
            var tile = targeted as Server.Targeting.StaticTarget;
            if (tile == null)
                return;
            var region = Region.Find(tile.Location, Map);
            if (region == null)
                return;
            var houseRegion = region as HouseRegion;
            if (houseRegion == null || !(houseRegion.House is IFarm))
                return;

            if(!from.CheckSkill(SkillName.Begging, 0.75))
            {
                from.SendMessage("You fail to till the soil.");
                return;
            }
            var dirt = new FarmableDirt();
            var loc = new Point3D(tile.X, tile.Y, tile.Z + 1);
            dirt.MoveToWorld(loc, from.Map);
            (houseRegion.House as IFarm).AddFarmItem(dirt);
            from.SendMessage("You till the soil.");
        }

        public override void AddLootTypeProperty(ObjectPropertyList list)
        {
            base.AddLootTypeProperty(list);
            this.PropertyList.Add("Uses Remaining " + m_UsesRemaining + "/" + UsesMax);
        }



        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write((int)0);
            writer.Write(UsesRemaining);
        }
        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            reader.ReadInt(); //ver
            m_UsesRemaining = reader.ReadInt();
        }

    }
}
