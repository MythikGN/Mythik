using Mythik.Systems.Farming;
using Server;
using Server.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scripts.Mythik.Systems.Farming.Items
{
    public class FarmPlant : Item
    {
        private PlantLifeStage m_GrowthStage;
        public PlantLifeStage GrowthStage { get { return m_GrowthStage; } set { m_GrowthStage = value; Name = PlantType.ToString() + "(" + GrowthStage.ToString() + ")"; ItemID = PlantInfo.GetGraphic(GrowthStage);

            }
        }
        public PlantType PlantType { get; set; }
        public PlantInfo PlantInfo { get { return PlantTypes.GetPlantInfo(PlantType); } }
        public PlantSize PlantSize { get { return PlantTypes.GetSize(PlantType); } }

        public DateTime NextGrowth { get; set; }

        private Timer m_GrowthTimer;

        public DateTime NextResourceSpawn { get; set; }

        private Timer m_ResourceTimer;
        private int m_NumSpawned = 0;

        public Mobile Owner { get; set; }


        [Constructable]
        public FarmPlant(Mobile from, PlantType type):base(0x0D2D)
        {
            Owner = from;
            PlantType = type;
            Movable = false;
            GrowthStage = PlantLifeStage.Sprout;
            m_GrowthTimer = Timer.DelayCall(PlantInfo.GrowthTime, TimeSpan.FromMinutes(1.0), new TimerCallback(CheckGrowth));
        }
        [Constructable]
        public FarmPlant(Serial serial) : base(serial) { }

        private void CheckResourceSpawn()
        {
            if (DateTime.UtcNow > NextResourceSpawn)
            {
                m_NumSpawned++;

                var resources = (Item)Activator.CreateInstance(PlantInfo.Harvestable);
                resources.Amount = PlantInfo.ReapAmount;
                resources.MoveToWorld(this.Location);
                resources.Z++;


                if(m_NumSpawned == PlantInfo.MaxRespawns)
                {
                    m_GrowthTimer.Stop();
                    m_GrowthTimer = null;
                    GrowthStage = PlantLifeStage.Dead;
                    var houseRegion = Region.Find(this.Location, Map) as HouseRegion;
                    if (houseRegion == null || houseRegion.House as IFarm == null)
                        return;
                    (houseRegion.House as IFarm).RemoveFarmitem(this);
                    this.Consume();
                }
            }
            }
        private void CheckGrowth()
        {
            if(DateTime.UtcNow > NextGrowth)
            {
                if (Owner.CheckSkill(SkillName.Camping, PlantInfo.MinSkill,PlantInfo.MaxSkill))
                {
                    GrowthStage++;
                }
                else
                {
                    GrowthStage = PlantLifeStage.Dead;
                }
                if (GrowthStage != PlantLifeStage.Dead && GrowthStage != PlantLifeStage.Mature)
                    NextGrowth = DateTime.UtcNow + PlantInfo.GrowthTime; 
                else
                {
                    m_GrowthTimer.Stop();
                    m_GrowthTimer = null;
                    if(PlantInfo.Harvestable == null)
                    {
                        var houseRegion = Region.Find(this.Location, Map) as HouseRegion;
                        if (houseRegion == null || houseRegion.House as IFarm == null)
                            return;
                        (houseRegion.House as IFarm).RemoveFarmitem(this);
                        //Move it to lockdown?
                        this.Movable = true;
                    }
                    else
                    {
                        m_ResourceTimer = Timer.DelayCall(PlantInfo.ResourceRespawnTime, TimeSpan.FromMinutes(1.0), new TimerCallback(CheckResourceSpawn));
                    }


                } 
            }
        }

       
        public override void OnDelete()
        {
            base.OnDelete();

            if (m_GrowthTimer != null)
            {
                m_GrowthTimer.Stop();
                m_GrowthTimer = null;
            }
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write((int)0);
            writer.Write((int)GrowthStage);
            writer.Write((int)PlantType);
            writer.Write(Owner);
            writer.Write(NextGrowth);
            
        }
        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            reader.ReadInt(); //ver
            GrowthStage = (PlantLifeStage)reader.ReadInt();
            PlantType = (PlantType)reader.ReadInt();
            Owner = reader.ReadMobile();
            NextGrowth = reader.ReadDateTime();

            m_GrowthTimer = Timer.DelayCall(PlantInfo.GrowthTime, TimeSpan.FromMinutes(1.0), new TimerCallback(CheckGrowth));
            if(GrowthStage == PlantLifeStage.Mature && PlantInfo.Harvestable != null)
            {
                m_ResourceTimer = Timer.DelayCall(PlantInfo.ResourceRespawnTime, TimeSpan.FromMinutes(1.0), new TimerCallback(CheckResourceSpawn));
            }
        }
    }
}
