using System;
using System.Collections.Generic;
using Server.Mobiles;
using Scripts.Mythik;

namespace Server.Events.DoomSystem
{
    public class SecretRoomRegion : BaseDoomSystemRegion
	{
        public WandererOfTheVoid Wanderer
        {
            get
            {
                List<Mobile> list = GetMobiles();

                foreach (Mobile m in list)
                    if (m is WandererOfTheVoid)
                        return (WandererOfTheVoid)m;

                return null;
            }
        }

		public SecretRoomRegion() : base( "Secret Room", Map.Felucca, 80, new Rectangle2D[] { MythikStaticValues.UpdateDoomBounds( new Rectangle2D( 465, 92, 9, 9 )) }, 4  )
		{
		}   
     
        public override void OnEnter(Mobile m)
        {
            if (DoomSystem.CanActivate(m) && !Active)
                Activate();
        }

        public override void Deactivate()
        {
            if (Wanderer != null)
                Wanderer.Delete();

            base.Deactivate();
        }
	}
}