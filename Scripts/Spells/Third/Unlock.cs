using System;
using Server.Targeting;
using Server.Network;
using Server.Items;

namespace Server.Spells.Third
{
	public class UnlockSpell : MagerySpell
	{
		private static SpellInfo m_Info = new SpellInfo(
				"Unlock Spell", "Ex Por",
				215,
				9001,
				Reagent.Bloodmoss,
				Reagent.SulfurousAsh
			);

		public override SpellCircle Circle { get { return SpellCircle.Third; } }

		public UnlockSpell( Mobile caster, Item scroll ) : base( caster, scroll, m_Info )
		{
		}

		public override void OnCast()
		{
			Caster.Target = new InternalTarget( this );
		}
        public override void OnPlayerCast()
        {
            if (SphereSpellTarget is LockableContainer)
                Target((LockableContainer)SphereSpellTarget);
            else
                DoFizzle();
        }
        public void Target(IPoint3D loc)
        {
            var from = Caster;
            if (CheckSequence())
            {
                SpellHelper.Turn(from, loc);

                Effects.SendLocationParticles(EffectItem.Create(new Point3D(loc), from.Map, EffectItem.DefaultDuration), 0x376A, 9, 32, 5024);

                Effects.PlaySound(loc, from.Map, 0x1FF);

                if (loc is Mobile)
                    from.LocalOverheadMessage(MessageType.Regular, 0x3B2, 503101); // That did not need to be unlocked.
                else if (!(loc is LockableContainer))
                    from.SendLocalizedMessage(501666); // You can't unlock that!
                else {
                    LockableContainer cont = (LockableContainer)loc;

                    if (Multis.BaseHouse.CheckSecured(cont))
                        from.SendLocalizedMessage(503098); // You cannot cast this on a secure item.
                    else if (!cont.Locked)
                        from.LocalOverheadMessage(MessageType.Regular, 0x3B2, 503101); // That did not need to be unlocked.
                    else if (cont.LockLevel == 0)
                        from.SendLocalizedMessage(501666); // You can't unlock that!
                    else {
                        int level = (int)(from.Skills[SkillName.Magery].Value * 0.8) - 4;

                        if (level >= cont.RequiredSkill && !(cont is TreasureMapChest && ((TreasureMapChest)cont).Level > 2))
                        {
                            cont.Locked = false;

                            if (cont.LockLevel == -255)
                                cont.LockLevel = cont.RequiredSkill - 10;
                        }
                        else
                            from.LocalOverheadMessage(MessageType.Regular, 0x3B2, 503099); // My spell does not seem to have an effect on that lock.
                    }
                }
            }

            FinishSequence();
        }
        private class InternalTarget : Target
		{
			private UnlockSpell m_Owner;

			public InternalTarget( UnlockSpell owner ) : base( Core.ML ? 10 : 12, false, TargetFlags.None )
			{
				m_Owner = owner;
			}
            

            protected override void OnTarget( Mobile from, object o )
			{
                if (o is LockableContainer)
                    m_Owner.Target((LockableContainer)o);
                else
                    from.SendLocalizedMessage(501666); // You can't unlock that!

            }

			protected override void OnTargetFinish( Mobile from )
			{
				m_Owner.FinishSequence();
			}
		}
	}
}