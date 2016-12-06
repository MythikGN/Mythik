using System;
using System.Collections.Generic;
using Server.Items;
using Server.Engines.Craft;
using System.Linq;

namespace Server.Mobiles
{
	public class SBGlassblower : SBInfo
	{
		private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo();
		private IShopSellInfo m_SellInfo = new InternalSellInfo();

		public SBGlassblower()
		{
		}

		public override IShopSellInfo SellInfo { get { return m_SellInfo; } }
		public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } }

		public class InternalBuyInfo : List<GenericBuyInfo>
		{
			public InternalBuyInfo()
			{
                Add(new GenericBuyInfo("Refresh", typeof(RefreshPotion), 15, 10, 0xF0B, 0));
                Add(new GenericBuyInfo("Agility", typeof(AgilityPotion), 15, 10, 0xF08, 0));
                Add(new GenericBuyInfo("Nightsight", typeof(NightSightPotion), 15, 10, 0xF06, 0));
                Add(new GenericBuyInfo("Lesser Heal", typeof(LesserHealPotion), 15, 10, 0xF0C, 0));
                Add(new GenericBuyInfo("Strength", typeof(StrengthPotion), 15, 10, 0xF09, 0));
                Add(new GenericBuyInfo("Lesser Poison", typeof(LesserPoisonPotion), 15, 10, 0xF0A, 0));
                Add(new GenericBuyInfo("Lesser Cure", typeof(LesserCurePotion), 15, 10, 0xF07, 0));
                Add(new GenericBuyInfo("Lesser Explosion", typeof(LesserExplosionPotion), 21, 10, 0xF0D, 0));

				Add( new GenericBuyInfo( typeof( MortarPestle ), 8, 10, 0xE9B, 0 ) );

				Add( new GenericBuyInfo( typeof( BlackPearl ), 5, 20, 0xF7A, 0 ) );
				Add( new GenericBuyInfo( typeof( Bloodmoss ), 5, 20, 0xF7B, 0 ) );
				Add( new GenericBuyInfo( typeof( Garlic ), 3, 20, 0xF84, 0 ) );
				Add( new GenericBuyInfo( typeof( Ginseng ), 3, 20, 0xF85, 0 ) );
				Add( new GenericBuyInfo( typeof( MandrakeRoot ), 3, 20, 0xF86, 0 ) );
				Add( new GenericBuyInfo( typeof( Nightshade ), 3, 20, 0xF88, 0 ) );
				Add( new GenericBuyInfo( typeof( SpidersSilk ), 3, 20, 0xF8D, 0 ) );
				Add( new GenericBuyInfo( typeof( SulfurousAsh ), 3, 20, 0xF8C, 0 ) );

				Add( new GenericBuyInfo( typeof( Bottle ), 5, 100, 0xF0E, 0 ) ); 

				Add( new GenericBuyInfo( typeof( HeatingStand ), 2, 10, 0x1849, 0 ) ); 

				//Add( new GenericBuyInfo( "Crafting Glass With Glassblowing", typeof( GlassblowingBook ), 10637, 30, 0xFF4, 0 ) );
				//Add( new GenericBuyInfo( "Finding Glass-Quality Sand", typeof( SandMiningBook ), 10637, 30, 0xFF4, 0 ) );
				//Add( new GenericBuyInfo( "1044608", typeof( Blowpipe ), 21, 100, 0xE8A, 0x3B9 ) );
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
				Add( typeof( Bottle ), 3 );
				Add( typeof( MortarPestle ), 4 );

                /*Add( typeof( NightSightPotion ), 7 );
				Add( typeof( AgilityPotion ), 7 );
				Add( typeof( StrengthPotion ), 7 );
				Add( typeof( RefreshPotion ), 7 );
				Add( typeof( LesserCurePotion ), 7 );
				Add( typeof( LesserHealPotion ), 7 );
				Add( typeof( LesserPoisonPotion ), 7 );
				Add( typeof( LesserExplosionPotion ), 10 );*/
                AddPotions();
                Add( typeof( GlassblowingBook ), 5000 );
				Add( typeof( SandMiningBook ), 5000 );
				Add( typeof( Blowpipe ), 10 );
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