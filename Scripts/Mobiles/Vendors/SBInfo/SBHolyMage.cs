using System;
using System.Collections.Generic;
using Server.Items;
using Server.Engines.Craft;
using System.Linq;

namespace Server.Mobiles
{
	public class SBHolyMage : SBInfo
	{
		private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo();
		private IShopSellInfo m_SellInfo = new InternalSellInfo();

		public SBHolyMage()
		{
		}

		public override IShopSellInfo SellInfo { get { return m_SellInfo; } }
		public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } }

		public class InternalBuyInfo : List<GenericBuyInfo>
		{
			public InternalBuyInfo()
			{
				Add( new GenericBuyInfo( typeof( Spellbook ), 18, 10, 0xEFA, 0 ) );
				Add( new GenericBuyInfo( typeof( ScribesPen ), 8, 10, 0xFBF, 0 ) );
				Add( new GenericBuyInfo( typeof( BlankScroll ), 5, 20, 0x0E34, 0 ) );

				Add( new GenericBuyInfo( "1041072", typeof( MagicWizardsHat ), 11, 10, 0x1718, Utility.RandomDyedHue() ) );

				Add( new GenericBuyInfo( typeof( RecallRune ), 15, 10, 0x1f14, 0 ) );

                Add(new GenericBuyInfo("Refresh", typeof(RefreshPotion), 15, 10, 0xF0B, 0));
                Add(new GenericBuyInfo("Agility", typeof(AgilityPotion), 15, 10, 0xF08, 0));
                Add(new GenericBuyInfo("Nightsight", typeof(NightSightPotion), 15, 10, 0xF06, 0));
                Add(new GenericBuyInfo("Lesser Heal", typeof(LesserHealPotion), 15, 10, 0xF0C, 0));
                Add(new GenericBuyInfo("Strength", typeof(StrengthPotion), 15, 10, 0xF09, 0));
                Add(new GenericBuyInfo("Lesser Cure", typeof(LesserCurePotion), 15, 10, 0xF07, 0));

				Add( new GenericBuyInfo( typeof( BlackPearl ), 5, 80, 0xF7A, 0 ) ); //This set was 20 amount.
				Add( new GenericBuyInfo( typeof( Bloodmoss ), 5, 80, 0xF7B, 0 ) );
				Add( new GenericBuyInfo( typeof( Garlic ), 3, 80, 0xF84, 0 ) );
				Add( new GenericBuyInfo( typeof( Ginseng ), 3, 80, 0xF85, 0 ) );
				Add( new GenericBuyInfo( typeof( MandrakeRoot ), 3, 80, 0xF86, 0 ) );
				Add( new GenericBuyInfo( typeof( Nightshade ), 3, 80, 0xF88, 0 ) );
				Add( new GenericBuyInfo( typeof( SpidersSilk ), 3, 80, 0xF8D, 0 ) );
				Add( new GenericBuyInfo( typeof( SulfurousAsh ), 3, 80, 0xF8C, 0 ) );

				Type[] types = Loot.RegularScrollTypes;

				for ( int i = 0; i < types.Length && i < 8; ++i )
				{
					int itemID = 0x1F2E + i;

					if ( i == 6 )
						itemID = 0x1F2D;
					else if ( i > 6 )
						--itemID;

					Add( new GenericBuyInfo( types[i], 12 + ((i / 8) * 10), 20, itemID, 0 ) );
				}
			}
		}

		public class InternalSellInfo : GenericSellInfo
		{
			public InternalSellInfo()
			{
				Add( typeof( BlackPearl ), 3 ); 
				Add( typeof( Bloodmoss ), 3 ); 
				Add( typeof( MandrakeRoot ), 2 ); 
				Add( typeof( Garlic ), 2 ); 
				Add( typeof( Ginseng ), 2 ); 
				Add( typeof( Nightshade ), 2 ); 
				Add( typeof( SpidersSilk ), 2 ); 
				Add( typeof( SulfurousAsh ), 2 ); 
				Add( typeof( RecallRune ), 8 );
				Add( typeof( Spellbook ), 9 );
				Add( typeof( BlankScroll ), 3 );

                /*Add( typeof( NightSightPotion ), 7 );
				Add( typeof( AgilityPotion ), 7 );
				Add( typeof( StrengthPotion ), 7 );
				Add( typeof( RefreshPotion ), 7 );
				Add( typeof( LesserCurePotion ), 7 );
				Add( typeof( LesserHealPotion ), 7 );*/
                AddPotions();


                Type[] types = Loot.RegularScrollTypes;
                var buyInfo = new InternalBuyInfo();
                for (int i = 0; i < types.Length; ++i)
                {
                    var cItem = Engines.Craft.DefInscription.CraftSystem.CraftItems.SearchFor(types[i]);
                    var price = 0;
                    foreach (CraftRes res in cItem.Resources)
                    {
                        var info = buyInfo.FirstOrDefault(x => x.Type == res.ItemType);
                        if (info != null)
                            price += info.Price;
                    }
                    //Modify the price based on difficulty to craft, for scribe this ranges from  25-125 for mage scrolls
                    // gives us a price mod of 1.25-6.25
                    //so lvl 1-2-3 will lose cash then you make more and more higher you go
                    price += (int)cItem.Skills.GetAt(0).MaxSkill / 20;
                    price += 3; // Add 3 to the base price so now profit will be 4-10 depending on circle.

                    Add(types[i], price);
                    //Add(types[i], ((i / 8) + 2) * 2);

                }

            }
            private void AddPotions()
            {
                var buyInfo = new InternalBuyInfo();
                foreach (CraftItem item in DefAlchemy.CraftSystem.CraftItems)
                {
                    var price = 0;
                    foreach (CraftRes res in item.Resources)
                    {
                        var info = buyInfo.FirstOrDefault(x => x.Type == res.ItemType);
                        if (info != null)
                            price += info.Price;
                    }
                    //Modify the price based on difficulty to craft, for scribe this ranges from  25-125 for mage scrolls
                    // gives us a price mod of 1.25-6.25
                    //so lvl 1-2-3 will lose cash then you make more and more higher you go
                    price += (int)item.Skills.GetAt(0).MaxSkill / 20;
                    price += 3; // Add 3 to the base price so now profit will be 4-10 depending on circle.
                    if (price != 0)
                        Add(item.ItemType, price);
                }


            }
        }
	}
}