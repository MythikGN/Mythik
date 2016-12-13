using Scripts.Mythik.Systems.Farming.Items;
using Server;
using Server.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scripts.Mythik.Systems.Farming
{
    public class PlantInfo
    {
        public Type Harvestable { get; private set; }
        public int MaxRespawns { get; private set; }
        public PlantType Type { get; private set; }
        public double MinSkill { get; set; }
        public double MaxSkill { get; set; }
        public int ReapAmount { get; private set; }
        public TimeSpan GrowthTime { get; private set; }

        private int[] m_Graphics;
        public TimeSpan ResourceRespawnTime;

        public PlantInfo(PlantType ptype,int Stage1Graphic, int Stage2Graphic, int Stage3Graphic,TimeSpan growthTime,Type harvestableResource,int numPerReap, TimeSpan respawnTimer,int numRespawns,double minSkill, double maxSkill)
        {
            this.Type = ptype;
            this.m_Graphics = new int[] { Stage1Graphic, Stage2Graphic, Stage3Graphic,0x0D40 };
            if(harvestableResource != null)
            this.Harvestable = harvestableResource;
            this.ResourceRespawnTime = respawnTimer;
            this.MaxRespawns = numRespawns;
            this.MinSkill = minSkill;
            this.MaxSkill = maxSkill;
            this.ReapAmount = numPerReap;
            this.GrowthTime = growthTime;
        }

        internal int GetGraphic(PlantLifeStage growthStage)
        {
            return m_Graphics[(int)growthStage];
        }
    }

 
    public enum PlantType
    {
        BlackPearl,
        Bloodmoss,
        Garlic,
        Ginseng,
        MandrakeRoot,
        NightShade,
        SpidersSilk,
        SulfAsh,
        

        //Med plants
        MediumPlants,
        Hedge = 100,


        //Large
        LargePlants,
        TreeApple = 200,
    }
    public enum PlantLifeStage
    {
        Sprout,
        Young,
        Mature,
        Dead
    }
    public enum PlantSize
    {
        Small,
        Medium,
        Tree
    }
    public static class PlantTypes
    {
        public static Dictionary<PlantType,PlantInfo> AllPlants { get; private set; }
        public static IEnumerable<PlantInfo> SmallPlants { get { return AllPlants.Values.Where(p => p.Type < PlantType.MediumPlants); } }
        public static IEnumerable<PlantInfo> MediumPlants { get { return AllPlants.Values.Where(p => p.Type > PlantType.MediumPlants && p.Type < PlantType.LargePlants); } }
        public static IEnumerable<PlantInfo> LargePlants { get { return AllPlants.Values.Where(p => p.Type > PlantType.LargePlants); } }

        public static void Initialize()
        {
            AllPlants = new Dictionary<PlantType, PlantInfo>();
            AddPlant(new PlantInfo(PlantType.BlackPearl, 0x0C68, 0x0C69, 0x18E9, TimeSpan.FromMinutes(1), typeof(BlackPearl), 10,TimeSpan.FromMinutes(10), 2, 0, 50));
            AddPlant(new PlantInfo(PlantType.Bloodmoss, 0x0C68, 0x0C69, 0x18E9, TimeSpan.FromMinutes(1), typeof(Bloodmoss), 10, TimeSpan.FromMinutes(10), 2, 0, 50));
            AddPlant(new PlantInfo(PlantType.Garlic, 0x0C68, 0x0C69, 0x18E9, TimeSpan.FromMinutes(1), typeof(Garlic), 10, TimeSpan.FromMinutes(10), 2, 0, 50));
            AddPlant(new PlantInfo(PlantType.Ginseng, 0x0C68, 0x0C69, 0x18E9, TimeSpan.FromMinutes(1), typeof(Ginseng), 10, TimeSpan.FromMinutes(10), 2, 0, 50));

            AddPlant(new PlantInfo(PlantType.Hedge,0x0C68, 0x0C69, 0x18E9,TimeSpan.FromMinutes(1),null,0,TimeSpan.Zero,0,50,100));

            //TODO More plants
        }

        private static void AddPlant(PlantInfo plant)
        {
            AllPlants.Add(plant.Type, plant);
        }

        public static PlantSize GetSize(PlantType type)
        {
            if ((int)type < 100)
                return PlantSize.Small;
            if ((int)type < 200)
                return PlantSize.Medium;
            return PlantSize.Tree;
        }

        internal static PlantInfo GetPlantInfo(PlantType plantType)
        {
            return AllPlants?[plantType];
        }
    }
}
