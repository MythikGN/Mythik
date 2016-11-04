using System;
using Server;
using Server.Spells;
using Server.Network;

namespace Server.Items
{
	public class CureLevelInfo
	{
		private Poison m_Poison;
		private double m_Chance;

		public Poison Poison
		{
			get{ return m_Poison; }
		}

		public double Chance
		{
			get{ return m_Chance; }
		}

		public CureLevelInfo( Poison poison, double chance )
		{
			m_Poison = poison;
			m_Chance = chance;
		}
	}

	public abstract class BaseCurePotion : BasePotion
	{
		public abstract CureLevelInfo[] LevelInfo{ get; }
        public double Delay { get { return 14; } }
        public BaseCurePotion( PotionEffect effect ) : base(0x0F0E, effect )
		{
		}

		public BaseCurePotion( Serial serial ) : base( serial )
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

		public void DoCure( Mobile from )
		{
			bool cure = false;

			CureLevelInfo[] info = LevelInfo;

			for ( int i = 0; i < info.Length; ++i )
			{
				CureLevelInfo li = info[i];

				if ( li.Poison == from.Poison && Scale( from, li.Chance ) > Utility.RandomDouble() )
				{
					cure = true;
					break;
				}
			}

			if ( cure && from.CurePoison( from ) )
			{
				from.SendLocalizedMessage( 500231 ); // You feel cured of poison!

				from.FixedEffect( 0x373A, 10, 15 );
				from.PlaySound( 0x1E0 );
			}
			else if ( !cure )
			{
				from.SendLocalizedMessage( 500232 ); // That potion was not strong enough to cure your ailment!
			}
		}

		public override void Drink( Mobile from )
		{
			if ( TransformationSpellHelper.UnderTransformation( from, typeof( Spells.Necromancy.VampiricEmbraceSpell ) ) )
			{
				from.SendLocalizedMessage( 1061652 ); // The garlic in the potion would surely kill you.
			}
			else if ( from.Poisoned )
			{
                if (from.BeginAction(typeof(BasePotion)))
                {
                    DoCure(from);

                    BasePotion.PlayDrinkEffect(from);

                    from.FixedParticles(0x373A, 10, 15, 5012, EffectLayer.Waist);
                    from.PlaySound(0x1E0);

                    if (!Engines.ConPVP.DuelContext.IsFreeConsume(from))
                        this.Consume();
                    Timer.DelayCall(TimeSpan.FromSeconds(Delay), new TimerStateCallback(ReleaseLock), from);
                }
                else
                {
                    from.NonlocalOverheadMessage(MessageType.Regular, 0x22, true, "You must wait before using another mana potion.");
                    //from.LocalOverheadMessage(MessageType.Regular, 0x22, 500235); // You must wait 10 seconds before using another healing potion.
                }             
			}
			else
			{
				from.SendLocalizedMessage( 1042000 ); // You are not poisoned.
			}
		}

        private static void ReleaseLock(object state)
        {
            ((Mobile)state).EndAction(typeof(BasePotion));
        }
    }
}