using System;
using Server.Misc;
using Server.Items;
using Server.Targeting;
using Server.Network;
using Server.Mobiles;
using Server.Spells.Seventh;
using Server.Spells.Fifth;

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

        public static bool DoDispell(Mobile from, Mobile m)
        {
            var res = SpellHelper.RemoveStatMod(from, m, StatType.All, m != from);
            if (!res)
                res = SpellHelper.RemoveStatMod(from, m, StatType.Str, m != from);
            if (!res)
                res = SpellHelper.RemoveStatMod(from, m, StatType.Int, m != from);
            if (!res)
                res = SpellHelper.RemoveStatMod(from, m, StatType.Dex, m != from);
            if (!res)
                res = Second.ProtectionSpell.EndProtection(m);
            if (!res)
                res = First.ReactiveArmorSpell.EndArmor(m);
            if (!res)
            {
                res = DisguiseTimers.RemoveTimer(m);
                m.EndAction(typeof(IncognitoSpell));
            }

            if (!res)
            {
                if (!m.CanBeginAction(typeof(PolymorphSpell)))
                {
                    PolymorphSpell.StopTimer(m);
                    res = true;
                }
            }
            return res;
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

            if(bc == null || !bc.IsDispellable)
            {
                var res = DoDispell(from, m);
                if (res)
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