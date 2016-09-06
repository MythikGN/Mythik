using System;
using Server.Misc;
using Server.Items;
using Server.Targeting;
using Server.Network;
using Server.Mobiles;

namespace Server.Spells.Sixth
{
	public class DispelSpell : MagerySpell
	{
		private static SpellInfo m_Info = new SpellInfo(
				"Dispel", "An Ort",
				218,
				9002,
				Reagent.Garlic,
				Reagent.MandrakeRoot,
				Reagent.SulfurousAsh
			);

		public override SpellCircle Circle { get { return SpellCircle.Sixth; } }

		public DispelSpell( Mobile caster, Item scroll ) : base( caster, scroll, m_Info )
		{
		}

		public override void OnCast()
		{
			Caster.Target = new InternalTarget( this );
		}
        public override void OnPlayerCast()
        {
            if (SphereSpellTarget is Mobile)
                Target((Mobile)SphereSpellTarget);
            else
                DoFizzle();
        }

        public void Target(Mobile o)
        {
            Mobile m = (Mobile)o;
            BaseCreature bc = m as BaseCreature;
            var from = Caster;
            if (!from.CanSee(m))
            {
                from.SendLocalizedMessage(500237); // Target can not be seen.
                return;
            }

            if(bc == null)
            {
                var res = SpellHelper.RemoveStatMod(Caster, m, StatType.All, m == Caster);
                if(!res)
                    res = SpellHelper.RemoveStatMod(Caster, m, StatType.Str, m == Caster);
                if (!res)
                    res = SpellHelper.RemoveStatMod(Caster, m, StatType.Int, m == Caster);
                if (!res)
                    res = SpellHelper.RemoveStatMod(Caster, m, StatType.Dex, m == Caster);
                if (!res)
                    res = DisguiseTimers.RemoveTimer(m);
                if(res)
                {
                    Effects.SendLocationParticles(EffectItem.Create(m.Location, m.Map, EffectItem.DefaultDuration), 0x3728, 8, 20, 5042);
                    Effects.PlaySound(m, m.Map, 0x201);
                    from.SendAsciiMessage("You dispel an effect.");
                }
                else
                {
                    m.FixedEffect(0x3779, 10, 20);
                    from.SendAsciiMessage("There is nothing to dispel.");
                }
                return;
            }

            if (bc == null || !bc.IsDispellable)
            {
                from.SendLocalizedMessage(1005049); // That cannot be dispelled.
            }
            else if (CheckHSequence(m))
            {
                SpellHelper.Turn(from, m);

                double dispelChance = (50.0 + ((100 * (from.Skills.Magery.Value - bc.DispelDifficulty)) / (bc.DispelFocus * 2))) / 100;

                if (dispelChance > Utility.RandomDouble())
                {
                    Effects.SendLocationParticles(EffectItem.Create(m.Location, m.Map, EffectItem.DefaultDuration), 0x3728, 8, 20, 5042);
                    Effects.PlaySound(m, m.Map, 0x201);

                    m.Delete();
                }
                else
                {
                    m.FixedEffect(0x3779, 10, 20);
                    from.SendLocalizedMessage(1010084); // The creature resisted the attempt to dispel it!
                }
            }
        }
        public class InternalTarget : Target
		{
			private DispelSpell m_Owner;

			public InternalTarget( DispelSpell owner ) : base( Core.ML ? 10 : 12, false, TargetFlags.Harmful )
			{
				m_Owner = owner;
			}

			protected override void OnTarget( Mobile from, object o )
			{
				if ( o is Mobile )
				{
                    m_Owner.Target((Mobile)o);
				}
			}

			protected override void OnTargetFinish( Mobile from )
			{
				m_Owner.FinishSequence();
			}
		}
	}
}