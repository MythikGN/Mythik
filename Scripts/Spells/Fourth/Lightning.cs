using System;
using Server.Targeting;
using Server.Network;
using Server.Mobiles;

namespace Server.Spells.Fourth
{
	public class LightningSpell : MagerySpell
	{
		private static SpellInfo m_Info = new SpellInfo(
				"Lightning", "Por Ort Grav",
				239,
				9021,
				Reagent.MandrakeRoot,
				Reagent.SulfurousAsh
			);
        public override Tuple<int, int> SphereDamage
        {
            get
            {
                return new Tuple<int, int>(10,20);
            }
        }
        public override int Sound
        {
            get
            {
                return base.Sound;
            }
        }
        public override SpellCircle Circle { get { return SpellCircle.Fourth; } }

		public LightningSpell( Mobile caster, Item scroll ) : base( caster, scroll, m_Info )
		{
		}

		public override void OnCast()
		{
			Caster.Target = new InternalTarget( this );
		}

		public override bool DelayedDamage{ get{ return false; } }
        public override void OnPlayerCast()
        {
            if (SphereSpellTarget is Mobile)
                 Target((Mobile)SphereSpellTarget);
             else
                 DoFizzle();
        }
        public void Target( Mobile m )
		{
			if ( !Caster.CanSee( m ) )
			{
				Caster.SendLocalizedMessage( 500237 ); // Target can not be seen.
			}
			else if ( CheckHSequence( m ) )
			{
				SpellHelper.Turn( Caster, m );

				SpellHelper.CheckReflect( (int)this.Circle, Caster, ref m );

				double damage;

				if ( Core.AOS )
				{
					damage = GetNewAosDamage( 23, 1, 4, m );
				}
				else
				{
					damage = Utility.Random( 12, 9 );

					if ( CheckResisted( m ) )
					{
						damage *= 0.75;

						m.SendLocalizedMessage( 501783 ); // You feel yourself resisting magical energy.
					}

					damage *= GetDamageScalar( m );
				}

				m.BoltEffect( 0 );
                if (Sound != 0)
                    m.PlaySound(this.Sound);
                else
                    Console.WriteLine("Missing sound for spell: " + this.GetType().Name);
                if (m is PlayerMobile && Caster is PlayerMobile)
                    damage = GetSphereDamage(Caster, m, SphereDamage);
                SpellHelper.Damage( this, m, damage, 0, 0, 0, 0, 100 );
			}

			FinishSequence();
		}

		private class InternalTarget : Target
		{
			private LightningSpell m_Owner;

			public InternalTarget( LightningSpell owner ) : base( Core.ML ? 10 : 12, false, TargetFlags.Harmful )
			{
				m_Owner = owner;
			}

			protected override void OnTarget( Mobile from, object o )
			{
				if ( o is Mobile )
					m_Owner.Target( (Mobile)o );
			}

			protected override void OnTargetFinish( Mobile from )
			{
				m_Owner.FinishSequence();
			}
		}
	}
}