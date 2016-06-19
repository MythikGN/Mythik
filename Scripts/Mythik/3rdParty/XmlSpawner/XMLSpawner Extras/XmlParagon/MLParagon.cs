using System;
using Server;
using Server.Items;
using Server.Mobiles;

namespace Server.Engines.XmlSpawner2
{
    public class MLParagon : XmlParagon
    {

        public override string OnIdentify(Mobile from)
        {
            return String.Format("ML {0}", base.OnIdentify(from));
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0);
            // version 0
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
            // version 0
        }

        #region constructors
        public MLParagon(ASerial serial)
            : base(serial)
        {
        }

        [Attachable]
        public MLParagon()
            : base()
        {
        }
        #endregion
    }
}