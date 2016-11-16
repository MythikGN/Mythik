using Server;
using Server.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Server.Engines.Harvest;
using Scripts.Mythik.Systems;

namespace Scripts.Mythik.Items
{
    class HerbalismSickle : BaseHarvestTool
    {
        public override HarvestSystem HarvestSystem
        {
            get
            {
                return HerbGathering.System;
            }
        }

        [Constructable]
        public HerbalismSickle() : base(0x26BB)
        {
          
        }

        public HerbalismSickle(Serial serial) : base(serial) { }

      



        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write((int)0);
            
        }
        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            reader.ReadInt(); //ver
            
        }



    }
}
