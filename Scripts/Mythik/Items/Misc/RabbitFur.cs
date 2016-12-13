using Server;
using Server.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Items
{
    class RabbitFur : BaseLeather, ICommodity
    {
        public TextDefinition Description
        {
            get
            {
                return "rabbit fur";
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
        public RabbitFur(int num) : base(CraftResource.RegularLeather, num)
        {
            Hue = 0x7A1;//Ancient hue
            Name = "rabbit fur";
        }
        [Constructable]
        public RabbitFur(Serial serial) : base(serial)
        {

        }
        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
        }
        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
        }
    }
}
