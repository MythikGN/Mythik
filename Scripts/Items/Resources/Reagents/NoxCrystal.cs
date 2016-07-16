using System;
using Server;
using Server.Items;

namespace Server.Items
{
	public class NoxCrystal : BaseReagent, ICommodity
	{
        TextDefinition ICommodity.Description { get { if (LabelNumber != 0) return LabelNumber; else return Name; } }
        bool ICommodity.IsDeedable { get { return true; } }

		[Constructable]
		public NoxCrystal() : this( 1 )
		{
		}

		[Constructable]
		public NoxCrystal( int amount ) : base( 0xF8E, amount )
		{
		}

		public NoxCrystal( Serial serial ) : base( serial )
		{
		}

		

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}
}