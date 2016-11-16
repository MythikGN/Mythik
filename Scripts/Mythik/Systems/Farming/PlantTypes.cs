using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scripts.Mythik.Systems.Farming
{
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
        Hedge = 100,


        //Large

        TreeApple = 200,
    }
    public enum PlantSize
    {
        Small,
        Medium,
        Tree
    }
    public class PlantTypes
    {

        public PlantSize GetSize(PlantType type)
        {
            if ((int)type < 100)
                return PlantSize.Small;
            if ((int)type < 200)
                return PlantSize.Medium;
            return PlantSize.Tree;
        }
    }
}
