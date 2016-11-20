using Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scripts.Mythik.Systems.Farming.Items
{
    public class FarmableDirt : Item
    {

        public FarmableDirt() : base(0x0914)
        {
            Movable = false;
            this.Name = "tilled soil";
        }
    }
}
