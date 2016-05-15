using Server;
using Server.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scripts.Mythik.Items.Craftables.Inscription
{

    public class EnemyOfOneScroll : SpellScroll
    {
        [Constructable]
        public EnemyOfOneScroll() : this(1)
        {
        }

        [Constructable]
        public EnemyOfOneScroll(int amount) : base(205, 0x226B, amount)
        {
        }

        public EnemyOfOneScroll(Serial serial) : base(serial)
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
