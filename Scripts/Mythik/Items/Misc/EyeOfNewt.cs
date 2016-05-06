using Server;
using Server.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scripts.Mythik.Items.Misc
{
    public class EyeOfNewt : BaseReagent, ICommodity
    {
      
        public int DescriptionNumber
        {
            get
            {
                return 1023975;
            }
        }

        public bool IsDeedable
        {
            get
            {
                return true;
            }
        }

 
        [Constructable]
        public EyeOfNewt(int amount) : base(0xF87, amount)
        {
        }

        public EyeOfNewt(Serial serial) : base(serial)
        {
        }

       

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
}
