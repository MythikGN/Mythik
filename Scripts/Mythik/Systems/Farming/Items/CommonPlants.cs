using Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scripts.Mythik.Systems.Farming.Items
{
    public abstract class FarmPlant : Item
    {

        public void Plant(Mobile from)
        {

        }
    }
    public class SmallPlant : FarmPlant
    {
        private PlantType nightShade;

        public SmallPlant(PlantType nightShade)
        {
            this.nightShade = nightShade;
        }
    }
}
