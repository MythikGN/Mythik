using System;
using Server.Network;

namespace Server
{
	public class CurrentExpansion
	{
		private static readonly Expansion Expansion = Expansion.ML;

		public static void Configure()
		{
			Core.Expansion = Expansion;


			Mobile.InsuranceEnabled = false;
			ObjectPropertyList.Enabled = true;
			Mobile.VisibleDamageType = true ? VisibleDamageType.Related : VisibleDamageType.None;
			Mobile.GuildClickMessage = false;
			Mobile.AsciiClickMessage = true;

			if (true)
			{
				AOS.DisableStatInfluences();

                //Disable prop lists for now to keep 203 working, renable for 4+ only.

				if ( ObjectPropertyList.Enabled )
					PacketHandlers.SingleClickProps = true; // single click for everything is overriden to check object property list
			}
		}
	}
}
