using System;
using Server;
using Server.Mobiles;
using Server.Spells;
using Server.Spells.Seventh;
using Server.Spells.Fourth;
using Server.Spells.Sixth;

namespace Server.Regions
{
	public class NeutralZone : Region
	{
		public static void Initialize()
		{
            Region.Regions.Add( new NeutralZone( Map.Felucca ) );
		}

		public NeutralZone( Map map ) : base( "Neutral Zone", map,80,new Rectangle2D(5125,1200,100,100) )
		{
            this.Register();
		}

        public override bool AllowBeneficial(Mobile from, Mobile target)
        {
            if (from.AccessLevel == AccessLevel.Player)
                from.SendMessage("You may not do that in Neutral Zone.");

            return (from.AccessLevel > AccessLevel.Player);
        }
      
		public override bool AllowHarmful( Mobile from, Mobile target )
		{
			if ( from.AccessLevel == AccessLevel.Player )
				from.SendMessage( "You may not do that in Neutral Zone." );

			return ( from.AccessLevel > AccessLevel.Player );
		}

		public override bool AllowHousing( Mobile from, Point3D p )
		{
			return false;
		}

		public override void AlterLightLevel( Mobile m, ref int global, ref int personal )
		{
			global = LightCycle.DayLevel;
		}

		public override bool OnBeginSpellCast( Mobile from, ISpell s )
		{
			if ( from.AccessLevel == AccessLevel.Player )
				from.SendLocalizedMessage( 502629 ); // You cannot cast spells here.

			return ( from.AccessLevel > AccessLevel.Player );
		}

		public override bool OnSkillUse( Mobile from, int Skill )
		{
			if ( from.AccessLevel == AccessLevel.Player )
				from.SendMessage( "You may not use skills in Neutral Zone." );

			return ( from.AccessLevel > AccessLevel.Player );
		}

		public override bool OnCombatantChange( Mobile from, Mobile Old, Mobile New )
		{
			return ( from.AccessLevel > AccessLevel.Player );
		}
	}
}