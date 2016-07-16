using System;

namespace Server.Items
{
	public class BlankScroll : Item, ICommodity
	{
		[Constructable]
		public BlankScroll() : this( 1 )
		{
		}

		[Constructable]
		public BlankScroll( int amount ) : base( 0xEF3 )
		{
			Stackable = true;
			Weight = 1.0;
			Amount = amount;
		}

        TextDefinition ICommodity.Description { get { if (LabelNumber != 0) return LabelNumber; else return Name; } }
        bool ICommodity.IsDeedable { get { return (Core.ML); } }

		public BlankScroll( Serial serial ) : base( serial )
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