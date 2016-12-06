using System;
using System.Collections.Generic;
using Server.Items;
using Server.Engines.Craft;
using System.Linq;

namespace Server.Mobiles
{
	public class SBMage : SBInfo
	{
		private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo();
		private IShopSellInfo m_SellInfo = new InternalSellInfo();

		public SBMage()
		{
		}

		public override IShopSellInfo SellInfo { get { return m_SellInfo; } }
		public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } }

		public class InternalBuyInfo : List<GenericBuyInfo>
		{
			public InternalBuyInfo()
			{
				Add( new GenericBuyInfo( typeof( Spellbook ), 18, 10, 0xEFA, 0 ) );
				
				//if ( Core.AOS )
				//	Add( new GenericBuyInfo( typeof( NecromancerSpellbook ), 115, 10, 0x2253, 0 ) );
				
				Add( new GenericBuyInfo( typeof( ScribesPen ), 8, 10, 0xFBF, 0 ) );

				Add( new GenericBuyInfo( typeof( BlankScroll ), 5, 40, 0x0E34, 0 ) );

				Add( new GenericBuyInfo( "1041072", typeof( MagicWizardsHat ), 11, 10, 0x1718, Utility.RandomDyedHue() ) );

				Add( new GenericBuyInfo( typeof( RecallRune ), 15, 10, 0x1F14, 0 ) );

                Add(new GenericBuyInfo("Refresh", typeof(RefreshPotion), 15, 10, 0xF0B, 0));
                Add(new GenericBuyInfo("Agility", typeof(AgilityPotion), 15, 10, 0xF08, 0));
                Add(new GenericBuyInfo("Nightsight", typeof(NightSightPotion), 15, 10, 0xF06, 0));
                Add(new GenericBuyInfo("Lesser Heal", typeof(LesserHealPotion), 15, 10, 0xF0C, 0));
                Add(new GenericBuyInfo("Strength", typeof(StrengthPotion), 15, 10, 0xF09, 0));
                Add(new GenericBuyInfo("Lesser Poison", typeof(LesserPoisonPotion), 15, 10, 0xF0A, 0));
                Add(new GenericBuyInfo("Lesser Cure", typeof(LesserCurePotion), 15, 10, 0xF07, 0));
                Add(new GenericBuyInfo("Lesser Explosion", typeof(LesserExplosionPotion), 21, 10, 0xF0D, 0));

                Add( new GenericBuyInfo( typeof( BlackPearl ), 5, 80, 0xF7A, 0 ) ); // This group was all 20 count.
				Add( new GenericBuyInfo( typeof( Bloodmoss ), 5, 80, 0xF7B, 0 ) );
				Add( new GenericBuyInfo( typeof( Garlic ), 3, 80, 0xF84, 0 ) );
				Add( new GenericBuyInfo( typeof( Ginseng ), 3, 80, 0xF85, 0 ) );
				Add( new GenericBuyInfo( typeof( MandrakeRoot ), 3, 80, 0xF86, 0 ) );
				Add( new GenericBuyInfo( typeof( Nightshade ), 3, 80, 0xF88, 0 ) );
				Add( new GenericBuyInfo( typeof( SpidersSilk ), 3, 80, 0xF8D, 0 ) );
				Add( new GenericBuyInfo( typeof( SulfurousAsh ), 3, 80, 0xF8C, 0 ) );

				if ( Core.AOS )
				{
					Add( new GenericBuyInfo( typeof( BatWing ), 3, 80, 0xF78, 0 ) );  // This group was all 999 count.
					Add( new GenericBuyInfo( typeof( DaemonBlood ), 6, 80, 0xF7D, 0 ) );
                    Add( new GenericBuyInfo( typeof( PigIron ), 5, 80, 0xF8A, 0 ) );
                    Add( new GenericBuyInfo( typeof( NoxCrystal ), 6, 80, 0xF8E, 0 ) );
                    Add( new GenericBuyInfo( typeof( GraveDust ), 3, 80, 0xF8F, 0 ) );
                }

				Type[] types = Loot.RegularScrollTypes;

				int circles = 3;

				for ( int i = 0; i < circles*8 && i < types.Length; ++i )
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
				Add( typeof( WizardsHat ), 15 );
				Add( typeof( BlackPearl ), 3 ); 
				Add( typeof( Bloodmoss ),4 ); 
				Add( typeof( MandrakeRoot ), 2 ); 
				Add( typeof( Garlic ), 2 ); 
				Add( typeof( Ginseng ), 2 ); 
				Add( typeof( Nightshade ), 2 ); 
				Add( typeof( SpidersSilk ), 2 ); 
				Add( typeof( SulfurousAsh ), 2 ); 

				if ( Core.AOS )
				{
					Add( typeof( BatWing ), 1 );
					Add( typeof( DaemonBlood ), 3 );
					Add( typeof( PigIron ), 2 );
					Add( typeof( NoxCrystal ), 3 );
					Add( typeof( GraveDust ), 1 );
				}

				Add( typeof( RecallRune ), 13 );
				Add( typeof( Spellbook ), 25 );
                Add(typeof(BlankScroll), 3);
                Type[] types = Loot.RegularScrollTypes;
                //For this to work, all resources required for the item must already be listed under BuyInfo we use the prices vendors sell, not the price they pay
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

                if ( Core.SE )
				{				
					Add( typeof( ExorcismScroll ), 3 );
					Add( typeof( AnimateDeadScroll ), 8 );
					Add( typeof( BloodOathScroll ), 8 );
					Add( typeof( CorpseSkinScroll ), 8 );
					Add( typeof( CurseWeaponScroll ), 8 );
					Add( typeof( EvilOmenScroll ), 8 );
					Add( typeof( PainSpikeScroll ), 8 );
					Add( typeof( SummonFamiliarScroll ), 8 );
					Add( typeof( HorrificBeastScroll ), 8 );
					Add( typeof( MindRotScroll ), 10 );
					Add( typeof( PoisonStrikeScroll ), 10 );
					Add( typeof( WraithFormScroll ), 15 );
					Add( typeof( LichFormScroll ), 16 );
					Add( typeof( StrangleScroll ), 16 );
					Add( typeof( WitherScroll ), 16 );
					Add( typeof( VampiricEmbraceScroll ), 20 );
					Add( typeof( VengefulSpiritScroll ), 20 );
			}

		}
	}
	}
}