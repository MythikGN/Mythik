using System;
using Server;
using Server.Items;
using Server.Factions;
using Server.Targeting;
using Scripts.Mythik.Items.Craftables.Tinkering.GemArmor;
using Scripts.Mythik.Items.Craftables.Tinkering;
using Scripts.Mythik.Items;

namespace Server.Engines.Craft
{
	public class DefTinkering : CraftSystem
	{
		public override SkillName MainSkill
		{
			get	{ return SkillName.Tinkering; }
		}

		public override int GumpTitleNumber
		{
			get { return 1044007; } // <CENTER>TINKERING MENU</CENTER>
		}

		private static CraftSystem m_CraftSystem;

		public static CraftSystem CraftSystem
		{
			get
			{
				if ( m_CraftSystem == null )
					m_CraftSystem = new DefTinkering();

				return m_CraftSystem;
			}
		}

		private DefTinkering() : base( 1, 1, 1.25 )// base( 1, 1, 3.0 )
		{
		}

		public override double GetChanceAtMin( CraftItem item )
		{
			if ( item.NameNumber == 1044258 || item.NameNumber == 1046445 ) // potion keg and faction trap removal kit
				return 0.5; // 50%

			return 0.0; // 0%
		}

		public override int CanCraft( Mobile from, BaseTool tool, Type itemType )
		{
			if( tool == null || tool.Deleted || tool.UsesRemaining < 0 )
				return 1044038; // You have worn out your tool!
			else if ( !BaseTool.CheckAccessible( tool, from ) )
				return 1044263; // The tool must be on your person to use.
			else if ( itemType != null && ( itemType.IsSubclassOf( typeof( BaseFactionTrapDeed ) ) || itemType == typeof( FactionTrapRemovalKit ) ) && Faction.Find( from ) == null )
				return 1044573; // You have to be in a faction to do that.

			return 0;
		}

		private static Type[] m_TinkerColorables = new Type[]
			{
				typeof( ForkLeft ), typeof( ForkRight ),
				typeof( SpoonLeft ), typeof( SpoonRight ),
				typeof( KnifeLeft ), typeof( KnifeRight ),
				typeof( Plate ),
				typeof( Goblet ), typeof( PewterMug ),
				typeof( KeyRing ),
				typeof( Candelabra ), typeof( Scales ),
				typeof( Key ), typeof( Globe ),
				typeof( Spyglass ), typeof( Lantern ),
				typeof( HeatingStand )
			};

		public override bool RetainsColorFrom( CraftItem item, Type type )
		{
			if ( !type.IsSubclassOf( typeof( BaseIngot ) ) )
				return false;

			type = item.ItemType;

			bool contains = false;

			for ( int i = 0; !contains && i < m_TinkerColorables.Length; ++i )
				contains = ( m_TinkerColorables[i] == type );

			return contains;
		}

		public override void PlayCraftEffect( Mobile from )
		{
			// no sound
			//from.PlaySound( 0x241 );
		}

		public override int PlayEndingEffect( Mobile from, bool failed, bool lostMaterial, bool toolBroken, int quality, bool makersMark, CraftItem item )
		{
			if ( toolBroken )
				from.SendLocalizedMessage( 1044038 ); // You have worn out your tool

			if ( failed )
			{
				if ( lostMaterial )
					return 1044043; // You failed to create the item, and some of your materials are lost.
				else
					return 1044157; // You failed to create the item, but no materials were lost.
			}
			else
			{
				if ( quality == 0 )
					return 502785; // You were barely able to make this item.  It's quality is below average.
				else if ( makersMark && quality == 2 )
					return 1044156; // You create an exceptional quality item and affix your maker's mark.
				else if ( quality == 2 )
					return 1044155; // You create an exceptional quality item.
				else				
					return 1044154; // You create the item.
			}
		}

		public override bool ConsumeOnFailure( Mobile from, Type resourceType, CraftItem craftItem )
		{
			if ( resourceType == typeof( Silver ) )
				return false;

			return base.ConsumeOnFailure( from, resourceType, craftItem );
		}

		public void AddJewelrySet( GemType gemType, Type itemType )
		{
			int offset = (int)gemType - 1;

			int index = AddCraft( typeof( GoldRing ), 1044049, 1044176 + offset, 40.0, 90.0, typeof( IronIngot ), 1044036, 2, 1044037 );
			AddRes( index, itemType, 1044231 + offset, 1, 1044240 );

			index = AddCraft( typeof( SilverBeadNecklace ), 1044049, 1044185 + offset, 40.0, 90.0, typeof( IronIngot ), 1044036, 2, 1044037 );
			AddRes( index, itemType, 1044231 + offset, 1, 1044240 );

			index = AddCraft( typeof( GoldNecklace ), 1044049, 1044194 + offset, 40.0, 90.0, typeof( IronIngot ), 1044036, 2, 1044037 );
			AddRes( index, itemType, 1044231 + offset, 1, 1044240 );

			index = AddCraft( typeof( GoldEarrings ), 1044049, 1044203 + offset, 40.0, 90.0, typeof( IronIngot ), 1044036, 2, 1044037 );
			AddRes( index, itemType, 1044231 + offset, 1, 1044240 );

			index = AddCraft( typeof( GoldBeadNecklace ), 1044049, 1044212 + offset, 40.0, 90.0, typeof( IronIngot ), 1044036, 2, 1044037 );
			AddRes( index, itemType, 1044231 + offset, 1, 1044240 );

			index = AddCraft( typeof( GoldBracelet ), 1044049, 1044221 + offset, 40.0, 90.0, typeof( IronIngot ), 1044036, 2, 1044037 );
			AddRes( index, itemType, 1044231 + offset, 1, 1044240 );
		}

		public override void InitCraftList()
		{
			int index = -1;

			#region Wooden Items
			AddCraft( typeof( JointingPlane ), 1044042, 1024144, 0.0, 50.0, typeof( Log ), 1044041, 4, 1044351 );
			AddCraft( typeof( MouldingPlane ), 1044042, 1024140, 0.0, 50.0, typeof( Log ), 1044041, 4, 1044351 );
			AddCraft( typeof( SmoothingPlane ), 1044042, 1024146, 0.0, 50.0, typeof( Log ), 1044041, 4, 1044351 );
			AddCraft( typeof( ClockFrame ), 1044042, 1024173, 0.0, 50.0, typeof( Log ), 1044041, 6, 1044351 );
			AddCraft( typeof( Axle ), 1044042, 1024187, -25.0, 25.0, typeof( Log ), 1044041, 2, 1044351 );
			AddCraft( typeof( RollingPin ), 1044042, 1024163, 0.0, 50.0, typeof( Log ), 1044041, 5, 1044351 );

			/*if( Core.SE )
			{
				index = AddCraft( typeof( Nunchaku ), 1044042, 1030158, 70.0, 120.0, typeof( IronIngot ), 1044036, 3, 1044037 );
				AddRes( index, typeof( Log ), 1044041, 8, 1044351 );
				SetNeededExpansion( index, Expansion.SE );
			}*/
			#endregion

			#region Tools
			AddCraft( typeof( Scissors ), 1044046, 1023998, 5.0, 55.0, typeof( IronIngot ), 1044036, 2, 1044037 );
			AddCraft( typeof( MortarPestle ), 1044046, 1023739, 20.0, 70.0, typeof( IronIngot ), 1044036, 3, 1044037 );
			AddCraft( typeof( Scorp ), 1044046, 1024327, 30.0, 80.0, typeof( IronIngot ), 1044036, 2, 1044037 );
			AddCraft( typeof( TinkerTools ), 1044046, 1044164, 10.0, 60.0, typeof( IronIngot ), 1044036, 2, 1044037 );
			AddCraft( typeof( Hatchet ), 1044046, 1023907, 30.0, 80.0, typeof( IronIngot ), 1044036, 4, 1044037 );
			AddCraft( typeof( DrawKnife ), 1044046, 1024324, 30.0, 80.0, typeof( IronIngot ), 1044036, 2, 1044037 );
			AddCraft( typeof( SewingKit ), 1044046, 1023997, 10.0, 70.0, typeof( IronIngot ), 1044036, 2, 1044037 );
			AddCraft( typeof( Saw ), 1044046, 1024148, 30.0, 80.0, typeof( IronIngot ), 1044036, 4, 1044037 );
			AddCraft( typeof( DovetailSaw ), 1044046, 1024136, 30.0, 80.0, typeof( IronIngot ), 1044036, 4, 1044037 );
			AddCraft( typeof( Froe ), 1044046, 1024325, 30.0, 80.0, typeof( IronIngot ), 1044036, 2, 1044037 );
			AddCraft( typeof( Shovel ), 1044046, 1023898, 40.0, 90.0, typeof( IronIngot ), 1044036, 4, 1044037 );
			AddCraft( typeof( Hammer ), 1044046, 1024138, 30.0, 80.0, typeof( IronIngot ), 1044036, 1, 1044037 );
			AddCraft( typeof( Tongs ), 1044046, 1024028, 35.0, 85.0, typeof( IronIngot ), 1044036, 1, 1044037 );
			AddCraft( typeof( SmithHammer ), 1044046, 1025091, 40.0, 90.0, typeof( IronIngot ), 1044036, 4, 1044037 );
			AddCraft( typeof( SledgeHammer ), 1044046, 1024021, 40.0, 90.0, typeof( IronIngot ), 1044036, 4, 1044037 );
			AddCraft( typeof( Inshave ), 1044046, 1024326, 30.0, 80.0, typeof( IronIngot ), 1044036, 2, 1044037 );
			AddCraft( typeof( Pickaxe ), 1044046, 1023718, 40.0, 90.0, typeof( IronIngot ), 1044036, 4, 1044037 );
			AddCraft( typeof( Lockpick ), 1044046, 1025371, 45.0, 95.0, typeof( IronIngot ), 1044036, 1, 1044037 );
			AddCraft( typeof( Skillet ), 1044046, 1044567, 30.0, 80.0, typeof( IronIngot ), 1044036, 4, 1044037 );
			AddCraft( typeof( FlourSifter ), 1044046, 1024158, 50.0, 100.0, typeof( IronIngot ), 1044036, 3, 1044037 );
			AddCraft( typeof( FletcherTools ), 1044046, 1044166, 35.0, 85.0, typeof( IronIngot ), 1044036, 3, 1044037 );
			AddCraft( typeof( MapmakersPen ), 1044046, 1044167, 25.0, 75.0, typeof( IronIngot ), 1044036, 1, 1044037 );
			AddCraft( typeof( ScribesPen ), 1044046, 1044168, 25.0, 75.0, typeof( IronIngot ), 1044036, 1, 1044037 );
			#endregion

			#region Parts
			AddCraft( typeof( Gears ), 1044047, 1024179, 5.0, 55.0, typeof( IronIngot ), 1044036, 2, 1044037 );
			AddCraft( typeof( ClockParts ), 1044047, 1024175, 25.0, 75.0, typeof( IronIngot ), 1044036, 1, 1044037 );
			AddCraft( typeof( BarrelTap ), 1044047, 1024100, 35.0, 85.0, typeof( IronIngot ), 1044036, 2, 1044037 );
			AddCraft( typeof( Springs ), 1044047, 1024189, 5.0, 55.0, typeof( IronIngot ), 1044036, 2, 1044037 );
			AddCraft( typeof( SextantParts ), 1044047, 1024185, 30.0, 80.0, typeof( IronIngot ), 1044036, 4, 1044037 );
			AddCraft( typeof( BarrelHoops ), 1044047, 1024321, -15.0, 35.0, typeof( IronIngot ), 1044036, 5, 1044037 );
			AddCraft( typeof( Hinge ), 1044047, 1024181, 5.0, 55.0, typeof( IronIngot ), 1044036, 2, 1044037 );
			AddCraft( typeof( BolaBall ), 1044047, 1023699, 45.0, 95.0, typeof( IronIngot ), 1044036, 10, 1044037 );

			/*if ( Core.ML )
			{
				index = AddCraft( typeof( JeweledFiligree ), 1044047, 1072894, 70.0, 110.0, typeof( IronIngot ), 1044036, 2, 1044037 );
				AddRes( index, typeof( StarSapphire ), 1044231, 1, 1044253 );
				AddRes( index, typeof( Ruby ), 1044234, 1, 1044253 );
				SetNeededExpansion( index, Expansion.ML );
			}*/
			#endregion

			#region Utensils
			AddCraft( typeof( ButcherKnife ), 1044048, 1025110, 25.0, 75.0, typeof( IronIngot ), 1044036, 2, 1044037 );
			AddCraft( typeof( SpoonLeft ), 1044048, 1044158, 0.0, 50.0, typeof( IronIngot ), 1044036, 1, 1044037 );
			AddCraft( typeof( SpoonRight ), 1044048, 1044159, 0.0, 50.0, typeof( IronIngot ), 1044036, 1, 1044037 );
			AddCraft( typeof( Plate ), 1044048, 1022519, 0.0, 50.0, typeof( IronIngot ), 1044036, 2, 1044037 );
			AddCraft( typeof( ForkLeft ), 1044048, 1044160, 0.0, 50.0, typeof( IronIngot ), 1044036, 1, 1044037 );
			AddCraft( typeof( ForkRight ), 1044048, 1044161, 0.0, 50.0, typeof( IronIngot ), 1044036, 1, 1044037 );
			AddCraft( typeof( Cleaver ), 1044048, 1023778, 20.0, 70.0, typeof( IronIngot ), 1044036, 3, 1044037 );
			AddCraft( typeof( KnifeLeft ), 1044048, 1044162, 0.0, 50.0, typeof( IronIngot ), 1044036, 1, 1044037 );
			AddCraft( typeof( KnifeRight ), 1044048, 1044163, 0.0, 50.0, typeof( IronIngot ), 1044036, 1, 1044037 );
			AddCraft( typeof( Goblet ), 1044048, 1022458, 10.0, 60.0, typeof( IronIngot ), 1044036, 2, 1044037 );
			AddCraft( typeof( PewterMug ), 1044048, 1024097, 10.0, 60.0, typeof( IronIngot ), 1044036, 2, 1044037 );
			AddCraft( typeof( SkinningKnife ), 1044048, 1023781, 25.0, 75.0, typeof( IronIngot ), 1044036, 2, 1044037 );
			#endregion

			#region Misc
			AddCraft( typeof( KeyRing ), 1044050, 1024113, 10.0, 60.0, typeof( IronIngot ), 1044036, 2, 1044037 );
			AddCraft( typeof( Candelabra ), 1044050, 1022599, 55.0, 105.0, typeof( IronIngot ), 1044036, 4, 1044037 );
			AddCraft( typeof( Scales ), 1044050, 1026225, 60.0, 110.0, typeof( IronIngot ), 1044036, 4, 1044037 );
			AddCraft( typeof( Key ), 1044050, 1024112, 20.0, 70.0, typeof( IronIngot ), 1044036, 3, 1044037 );
			AddCraft( typeof( Globe ), 1044050, 1024167, 55.0, 105.0, typeof( IronIngot ), 1044036, 4, 1044037 );
			AddCraft( typeof( Spyglass ), 1044050, 1025365, 60.0, 110.0, typeof( IronIngot ), 1044036, 4, 1044037 );
			AddCraft( typeof( Lantern ), 1044050, 1022597, 30.0, 80.0, typeof( IronIngot ), 1044036, 2, 1044037 );
			AddCraft( typeof( HeatingStand ), 1044050, 1026217, 60.0, 110.0, typeof( IronIngot ), 1044036, 4, 1044037 );

			//if ( Core.SE )
			//{
				index = AddCraft( typeof( ShojiLantern ), 1044050, 1029404, 65.0, 115.0, typeof( IronIngot ), 1044036, 10, 1044037 );
				AddRes( index, typeof( Log ), 1044041, 5, 1044351 );
				//SetNeededExpansion( index, Expansion.SE );

				index = AddCraft( typeof( PaperLantern ), 1044050, 1029406, 65.0, 115.0, typeof( IronIngot ), 1044036, 10, 1044037 );
				AddRes( index, typeof( Log ), 1044041, 5, 1044351 );
				//SetNeededExpansion( index, Expansion.SE );

				index = AddCraft( typeof( RoundPaperLantern ), 1044050, 1029418, 65.0, 115.0, typeof( IronIngot ), 1044036, 10, 1044037 );
				AddRes( index, typeof( Log ), 1044041, 5, 1044351 );
				//SetNeededExpansion( index, Expansion.SE );

				index = AddCraft( typeof( WindChimes ), 1044050, 1030290, 80.0, 130.0, typeof( IronIngot ), 1044036, 15, 1044037 );
				//SetNeededExpansion( index, Expansion.SE );

				index = AddCraft( typeof( FancyWindChimes ), 1044050, 1030291, 80.0, 130.0, typeof( IronIngot ), 1044036, 15, 1044037 );
				//SetNeededExpansion( index, Expansion.SE );
			//}
			#endregion

			#region Jewelry
			AddJewelrySet( GemType.StarSapphire, typeof( StarSapphire ) );
			AddJewelrySet( GemType.Emerald, typeof( Emerald ) );
			AddJewelrySet( GemType.Sapphire, typeof( Sapphire ) );
			AddJewelrySet( GemType.Ruby, typeof( Ruby ) );
			AddJewelrySet( GemType.Citrine, typeof( Citrine ) );
			AddJewelrySet( GemType.Amethyst, typeof( Amethyst ) );
			AddJewelrySet( GemType.Tourmaline, typeof( Tourmaline ) );
			AddJewelrySet( GemType.Amber, typeof( Amber ) );
			AddJewelrySet( GemType.Diamond, typeof( Diamond ) );
			#endregion

			#region Multi-Component Items
			index = AddCraft( typeof( AxleGears ), 1044051, 1024177, 0.0, 0.0, typeof( Axle ), 1044169, 1, 1044253 );
			AddRes( index, typeof( Gears ), 1044254, 1, 1044253 );

			index = AddCraft( typeof( ClockParts ), 1044051, 1024175, 0.0, 0.0, typeof( AxleGears ), 1044170, 1, 1044253 );
			AddRes( index, typeof( Springs ), 1044171, 1, 1044253 );

			index = AddCraft( typeof( SextantParts ), 1044051, 1024185, 0.0, 0.0, typeof( AxleGears ), 1044170, 1, 1044253 );
			AddRes( index, typeof( Hinge ), 1044172, 1, 1044253 );

			index = AddCraft( typeof( ClockRight ), 1044051, 1044257, 0.0, 0.0, typeof( ClockFrame ), 1044174, 1, 1044253 );
			AddRes( index, typeof( ClockParts ), 1044173, 1, 1044253 );

			index = AddCraft( typeof( ClockLeft ), 1044051, 1044256, 0.0, 0.0, typeof( ClockFrame ), 1044174, 1, 1044253 );
			AddRes( index, typeof( ClockParts ), 1044173, 1, 1044253 );

			AddCraft( typeof( Sextant ), 1044051, 1024183, 0.0, 0.0, typeof( SextantParts ), 1044175, 1, 1044253 );

			index = AddCraft( typeof( Bola ), 1044051, 1046441, 60.0, 80.0, typeof( BolaBall ), 1046440, 4, 1042613 );
			AddRes( index, typeof( Leather ), 1044462, 3, 1044463 );

			index = AddCraft( typeof( PotionKeg ), 1044051, 1044258, 75.0, 100.0, typeof( Keg ), 1044255, 1, 1044253 );
			AddRes( index, typeof( Bottle ), 1044250, 10, 1044253 );
			AddRes( index, typeof( BarrelLid ), 1044251, 1, 1044253 );
			AddRes( index, typeof( BarrelTap ), 1044252, 1, 1044253 );
			
			
			#endregion

			#region Traps
			// Dart Trap
			index = AddCraft( typeof( DartTrapCraft ), 1044052, 1024396, 30.0, 80.0, typeof( IronIngot ), 1044036, 1, 1044037 );
			AddRes( index, typeof( Bolt ), 1044570, 1, 1044253 );

			// Poison Trap
			index = AddCraft( typeof( PoisonTrapCraft ), 1044052, 1044593, 30.0, 80.0, typeof( IronIngot ), 1044036, 1, 1044037 );
			AddRes( index, typeof( BasePoisonPotion ), 1044571, 1, 1044253 );

			// Explosion Trap
			index = AddCraft( typeof( ExplosionTrapCraft ), 1044052, 1044597, 55.0, 105.0, typeof( IronIngot ), 1044036, 1, 1044037 );
			AddRes( index, typeof( BaseExplosionPotion ), 1044569, 1, 1044253 );

			// Faction Gas Trap
			index = AddCraft( typeof( FactionGasTrapDeed ), 1044052, 1044598, 65.0, 115.0, typeof( Silver ), 1044572, Core.AOS ? 250 : 1000, 1044253 );
			AddRes( index, typeof( IronIngot ), 1044036, 10, 1044037 );
			AddRes( index, typeof( BasePoisonPotion ), 1044571, 1, 1044253 );

			// Faction explosion Trap
			index = AddCraft( typeof( FactionExplosionTrapDeed ), 1044052, 1044599, 65.0, 115.0, typeof( Silver ), 1044572, Core.AOS ? 250 : 1000, 1044253 );
			AddRes( index, typeof( IronIngot ), 1044036, 10, 1044037 );
			AddRes( index, typeof( BaseExplosionPotion ), 1044569, 1, 1044253 );

			// Faction Saw Trap
			index = AddCraft( typeof( FactionSawTrapDeed ), 1044052, 1044600, 65.0, 115.0, typeof( Silver ), 1044572, Core.AOS ? 250 : 1000, 1044253 );
			AddRes( index, typeof( IronIngot ), 1044036, 10, 1044037 );
			AddRes( index, typeof( Gears ), 1044254, 1, 1044253 );

			// Faction Spike Trap			
			index = AddCraft( typeof( FactionSpikeTrapDeed ), 1044052, 1044601, 65.0, 115.0, typeof( Silver ), 1044572, Core.AOS ? 250 : 1000, 1044253 );
			AddRes( index, typeof( IronIngot ), 1044036, 10, 1044037 );
			AddRes( index, typeof( Springs ), 1044171, 1, 1044253 );

			// Faction trap removal kit
			index = AddCraft( typeof( FactionTrapRemovalKit ), 1044052, 1046445, 90.0, 115.0, typeof( Silver ), 1044572, 500, 1044253 );
			AddRes( index, typeof( IronIngot ), 1044036, 10, 1044037 );
            #endregion


            #region GemArmor

            #region Tourmaline
            index = AddCraft(typeof(TourmalinePlateHelm), "Gem Armor", "Tourmaline Plate Helm", 101.0, 150.0, typeof(Tourmaline), 1044237, 240);
            AddSkill(index, SkillName.Blacksmith, 101.0, 150.0);
            AddRes(index, typeof(IronIngot), 1044036, 30);
            SetItemHue(index, ItemSets.TourmalineSet.Hue);
            index = AddCraft(typeof(TourmalinePlateGorgot), "Gem Armor", "Tourmaline Plate Gorgot", 101.0, 150.0, typeof(Tourmaline), 1044237, 160);
            AddSkill(index, SkillName.Blacksmith, 101.0, 150.0);
            AddRes(index, typeof(IronIngot), 1044036, 25);
            SetItemHue(index, ItemSets.TourmalineSet.Hue);
            index = AddCraft(typeof(TourmalinePlateChest), "Gem Armor", "Tourmaline Plate Chest", 105.0, 150.0, typeof(Tourmaline), 1044237, 400);
            AddSkill(index, SkillName.Blacksmith, 105.0, 150.0);
            AddRes(index, typeof(IronIngot), 1044036, 50);
            SetItemHue(index, ItemSets.TourmalineSet.Hue);
            index = AddCraft(typeof(TourmalinePlateArms), "Gem Armor", "Tourmaline Plate Arms", 101.0, 150.0, typeof(Tourmaline), 1044237, 288);
            AddSkill(index, SkillName.Blacksmith, 101.0, 150.0);
            AddRes(index, typeof(IronIngot), 1044036, 40);
            SetItemHue(index, ItemSets.TourmalineSet.Hue);
            index = AddCraft(typeof(TourmalinePlateGloves), "Gem Armor", "Tourmaline Plate Gloves", 101.0, 150.0, typeof(Tourmaline), 1044237, 192);
            AddSkill(index, SkillName.Blacksmith, 101.0, 150.0);
            AddRes(index, typeof(IronIngot), 1044036, 30);
            SetItemHue(index, ItemSets.TourmalineSet.Hue);
            index = AddCraft(typeof(TourmalinePlateLegs), "Gem Armor", "Tourmaline Plate Legs", 103.0, 150.0, typeof(Tourmaline), 1044237, 320);
            AddSkill(index, SkillName.Blacksmith, 103.0, 150.0);
            AddRes(index, typeof(IronIngot), 1044036, 30);
            SetItemHue(index, ItemSets.TourmalineSet.Hue);
            index = AddCraft(typeof(TourmalineHeaterShield), "Gem Armor", "Tourmaline Heater Shield", 104.0, 150.0, typeof(Tourmaline), 1044237, 288);
            AddSkill(index, SkillName.Blacksmith, 104.0, 150.0);
            AddRes(index, typeof(IronIngot), 1044036, 40);
            SetItemHue(index, ItemSets.TourmalineSet.Hue);
            #endregion

            #region Amber
            index = AddCraft(typeof(AmberPlateHelm), "Gem Armor", "Amber Plate Helm", 106.0, 150.0, typeof(Amber), 1044238, 240);
            AddSkill(index, SkillName.Blacksmith, 106.0, 150.0);
            AddRes(index, typeof(IronIngot), 1044036, 30);
            SetItemHue(index, ItemSets.AmberSet.Hue);
            index = AddCraft(typeof(AmberPlateGorgot), "Gem Armor", "Amber Plate Gorgot", 106.0, 150.0, typeof(Amber), 1044238, 160);
            AddSkill(index, SkillName.Blacksmith, 106.0, 150.0);
            AddRes(index, typeof(IronIngot), 1044036, 25);
            SetItemHue(index, ItemSets.AmberSet.Hue);
            index = AddCraft(typeof(AmberPlateChest), "Gem Armor", "Amber Plate Chest", 110.0, 150.0, typeof(Amber), 1044238, 400);
            AddSkill(index, SkillName.Blacksmith, 110.0, 150.0);
            AddRes(index, typeof(IronIngot), 1044036, 50);
            SetItemHue(index, ItemSets.AmberSet.Hue);
            index = AddCraft(typeof(AmberPlateArms), "Gem Armor", "Amber Plate Arms", 108.0, 150.0, typeof(Amber), 1044238, 288);
            AddSkill(index, SkillName.Blacksmith, 108.0, 150.0);
            AddRes(index, typeof(IronIngot), 1044036, 40);
            SetItemHue(index, ItemSets.AmberSet.Hue);
            index = AddCraft(typeof(AmberPlateGloves), "Gem Armor", "Amber Plate Gloves", 106.0, 150.0, typeof(Amber), 1044238, 192);
            AddSkill(index, SkillName.Blacksmith, 106.0, 150.0);
            AddRes(index, typeof(IronIngot), 1044036, 30);
            SetItemHue(index, ItemSets.AmberSet.Hue);
            index = AddCraft(typeof(AmberPlateLegs), "Gem Armor", "Amber Plate Legs", 108.0, 150.0, typeof(Amber), 1044238, 320);
            AddSkill(index, SkillName.Blacksmith, 108.0, 150.0);
            AddRes(index, typeof(IronIngot), 1044036, 30);
            SetItemHue(index, ItemSets.AmberSet.Hue);
            index = AddCraft(typeof(AmberHeaterShield), "Gem Armor", "Amber Heater Shield", 110.0, 150.0, typeof(Amber), 1044238, 288);
            AddSkill(index, SkillName.Blacksmith, 110.0, 150.0);
            AddRes(index, typeof(IronIngot), 1044036, 40);
            SetItemHue(index, ItemSets.AmberSet.Hue);
            #endregion
            #region Amethyst
            index = AddCraft(typeof(AmethystPlateHelm), "Gem Armor", "Amethyst Plate Helm", 110.0, 140.0, typeof(Amethyst), 1044236, 240);
            AddSkill(index, SkillName.Blacksmith, 110.0, 140.0);
            AddRes(index, typeof(IronIngot), 1044036, 30);
            SetItemHue(index, ItemSets.AmethystSet.Hue);
            index = AddCraft(typeof(AmethystPlateGorgot), "Gem Armor", "Amethyst Plate Gorgot", 110.0, 140.0, typeof(Amethyst), 1044236, 160);
            AddSkill(index, SkillName.Blacksmith, 110.0, 140.0);
            AddRes(index, typeof(IronIngot), 1044036, 25);
            SetItemHue(index, ItemSets.AmethystSet.Hue);
            index = AddCraft(typeof(AmethystPlateChest), "Gem Armor", "Amethyst Plate Chest", 110.0, 140.0, typeof(Amethyst), 1044236, 400);
            AddSkill(index, SkillName.Blacksmith, 110.0, 140.0);
            AddRes(index, typeof(IronIngot), 1044036, 50);
            SetItemHue(index, ItemSets.AmethystSet.Hue);
            index = AddCraft(typeof(AmethystPlateArms), "Gem Armor", "Amethyst Plate Arms", 110.0, 140.0, typeof(Amethyst), 1044236, 288);
            AddSkill(index, SkillName.Blacksmith, 110.0, 140.0);
            AddRes(index, typeof(IronIngot), 1044036, 40);
            SetItemHue(index, ItemSets.AmethystSet.Hue);
            index = AddCraft(typeof(AmethystPlateGloves), "Gem Armor", "Amethyst Plate Gloves", 110.0, 140.0, typeof(Amethyst), 1044236, 192);
            AddSkill(index, SkillName.Blacksmith, 110.0, 140.0);
            AddRes(index, typeof(IronIngot), 1044036, 30);
            SetItemHue(index, ItemSets.AmethystSet.Hue);
            index = AddCraft(typeof(AmethystPlateLegs), "Gem Armor", "Amethyst Plate Legs", 110.0, 140.0, typeof(Amethyst), 1044236, 320);
            AddSkill(index, SkillName.Blacksmith, 110.0, 140.0);
            AddRes(index, typeof(IronIngot), 1044036, 30);
            SetItemHue(index, ItemSets.AmethystSet.Hue);
            index = AddCraft(typeof(AmethystHeaterShield), "Gem Armor", "Amethyst Heater Shield", 110.0, 140.0, typeof(Amethyst), 1044236, 288);
            AddSkill(index, SkillName.Blacksmith, 110.0, 140.0);
            AddRes(index, typeof(IronIngot), 1044036, 40);
            SetItemHue(index, ItemSets.AmethystSet.Hue);
            #endregion

            #region Citrine
            index = AddCraft(typeof(CitrinePlateHelm), "Gem Armor", "Citrine Plate Helm", 112.0, 180.0, typeof(IronIngot), 1044036, 30);
            AddSkill(index, SkillName.Blacksmith, 122.0, 160.0);
            AddRes(index, typeof(Citrine), 1044235, 240);
            SetItemHue(index, ItemSets.CitrineSet.Hue);
            index = AddCraft(typeof(CitrinePlateGorgot), "Gem Armor", "Citrine Plate Gorgot", 112.0, 180.0, typeof(IronIngot), 1044036, 25);
            AddSkill(index, SkillName.Blacksmith, 122.0, 150.0);
            AddRes(index, typeof(Citrine), 1044235, 160);
            SetItemHue(index, ItemSets.CitrineSet.Hue);
            index = AddCraft(typeof(CitrinePlateChest), "Gem Armor", "Citrine Plate Chest", 115.0, 180.0, typeof(IronIngot), 1044036, 50);
            AddSkill(index, SkillName.Blacksmith, 122.0, 180.0);
            AddRes(index, typeof(Citrine), 1044235, 400);
            SetItemHue(index, ItemSets.CitrineSet.Hue);
            index = AddCraft(typeof(CitrinePlateArms), "Gem Armor", "Citrine Plate Arms", 112.0, 180.0, typeof(IronIngot), 1044036, 40);
            AddSkill(index, SkillName.Blacksmith, 122.0, 160.0);
            AddRes(index, typeof(Citrine), 1044235, 288);
            SetItemHue(index, ItemSets.CitrineSet.Hue);
            index = AddCraft(typeof(CitrinePlateGloves), "Gem Armor", "Citrine Plate Gloves", 112.0, 180.0, typeof(IronIngot), 1044036, 30);
            AddSkill(index, SkillName.Blacksmith, 122.0, 160.0);
            AddRes(index, typeof(Citrine), 1044235, 192);
            SetItemHue(index, ItemSets.CitrineSet.Hue);
            index = AddCraft(typeof(CitrinePlateLegs), "Gem Armor", "Citrine Plate Legs", 112.0, 180.0, typeof(IronIngot), 1044036, 40);
            AddSkill(index, SkillName.Blacksmith, 122.0, 170.0);
            AddRes(index, typeof(Citrine), 1044235, 320);
            SetItemHue(index, ItemSets.CitrineSet.Hue);
            index = AddCraft(typeof(CitrineHeaterShield), "Gem Armor", "Citrine Heater Shield", 115.0, 180.0, typeof(IronIngot), 1044036, 50);
            AddSkill(index, SkillName.Blacksmith, 122.0, 180.0);
            AddRes(index, typeof(Citrine), 1044235, 288);
            SetItemHue(index, ItemSets.CitrineSet.Hue);
            #endregion

            #region Emerald
            index = AddCraft(typeof(EmeraldPlateHelm), "Gem Armor", "Emerald Plate Helm", 114.0, 180.0, typeof(Emerald), 1044232, 240);
            AddSkill(index, SkillName.Blacksmith, 118.0, 180.0);
            AddRes(index, typeof(IronIngot), 1044036, 30);
            SetItemHue(index, ItemSets.EmeraldSet.Hue);
            index = AddCraft(typeof(EmeraldPlateGorgot), "Gem Armor", "Emerald Plate Gorgot", 114.0, 180.0, typeof(Emerald), 1044232, 160);
            AddSkill(index, SkillName.Blacksmith, 118.0, 180.0);
            AddRes(index, typeof(IronIngot), 1044036, 25);
            SetItemHue(index, ItemSets.EmeraldSet.Hue);
            index = AddCraft(typeof(EmeraldPlateChest), "Gem Armor", "Emerald Plate Chest", 118.0, 180.0, typeof(Emerald), 1044232, 400);
            AddSkill(index, SkillName.Blacksmith, 118.0, 180.0);
            AddRes(index, typeof(IronIngot), 1044036, 50);
            SetItemHue(index, ItemSets.EmeraldSet.Hue);
            index = AddCraft(typeof(EmeraldPlateArms), "Gem Armor", "Emerald Plate Arms", 116.0, 180.0, typeof(Emerald), 1044232, 288);
            AddSkill(index, SkillName.Blacksmith, 118.0, 180.0);
            AddRes(index, typeof(IronIngot), 1044036, 40);
            SetItemHue(index, ItemSets.EmeraldSet.Hue);
            index = AddCraft(typeof(EmeraldPlateGloves), "Gem Armor", "Emerald Plate Gloves", 116.0, 180.0, typeof(Emerald), 1044232, 192);
            AddSkill(index, SkillName.Blacksmith, 118.0, 180.0);
            AddRes(index, typeof(IronIngot), 1044036, 30);
            SetItemHue(index, ItemSets.EmeraldSet.Hue);
            index = AddCraft(typeof(EmeraldPlateLegs), "Gem Armor", "Emerald Plate Legs", 118.0, 180.0, typeof(Emerald), 1044232, 320);
            AddSkill(index, SkillName.Blacksmith, 118.0, 180.0);
            AddRes(index, typeof(IronIngot), 1044036, 30);
            SetItemHue(index, ItemSets.EmeraldSet.Hue);
            index = AddCraft(typeof(EmeraldHeaterShield), "Gem Armor", "Emerald Heater Shield", 118.0, 180.0, typeof(Emerald), 1044232, 288);
            AddSkill(index, SkillName.Blacksmith, 118.0, 180.0);
            AddRes(index, typeof(IronIngot), 1044036, 40);
            SetItemHue(index, ItemSets.EmeraldSet.Hue);
            #endregion

            #region Ruby
            index = AddCraft(typeof(RubyPlateHelm), "Gem Armor", "Ruby Plate Helm", 122.0, 180.0, typeof(IronIngot), 1044036, 30);
            AddSkill(index, SkillName.Blacksmith, 122.0, 160.0);
            AddRes(index, typeof(Ruby), 1044234, 240);
            SetItemHue(index, ItemSets.RubySet.Hue);
            index = AddCraft(typeof(RubyPlateGorgot), "Gem Armor", "Ruby Plate Gorgot", 122.0, 180.0, typeof(IronIngot), 1044036, 25);
            AddSkill(index, SkillName.Blacksmith, 122.0, 150.0);
            AddRes(index, typeof(Ruby), 1044234, 160);
            SetItemHue(index, ItemSets.RubySet.Hue);
            index = AddCraft(typeof(RubyPlateChest), "Gem Armor", "Ruby Plate Chest", 122.0, 180.0, typeof(IronIngot), 1044036, 50);
            AddSkill(index, SkillName.Blacksmith, 122.0, 180.0);
            AddRes(index, typeof(Ruby), 1044234, 400);
            SetItemHue(index, ItemSets.RubySet.Hue);
            index = AddCraft(typeof(RubyPlateArms), "Gem Armor", "Ruby Plate Arms", 122.0, 180.0, typeof(IronIngot), 1044036, 40);
            AddSkill(index, SkillName.Blacksmith, 122.0, 160.0);
            AddRes(index, typeof(Ruby), 1044234, 288);
            SetItemHue(index, ItemSets.RubySet.Hue);
            index = AddCraft(typeof(RubyPlateGloves), "Gem Armor", "Ruby Plate Gloves", 122.0, 180.0, typeof(IronIngot), 1044036, 30);
            AddSkill(index, SkillName.Blacksmith, 122.0, 160.0);
            AddRes(index, typeof(Ruby), 1044234, 192);
            SetItemHue(index, ItemSets.RubySet.Hue);
            index = AddCraft(typeof(RubyPlateLegs), "Gem Armor", "Ruby Plate Legs", 122.0, 180.0, typeof(IronIngot), 1044036, 40);
            AddSkill(index, SkillName.Blacksmith, 122.0, 170.0);
            AddRes(index, typeof(Ruby), 1044234, 320);
            SetItemHue(index, ItemSets.RubySet.Hue);
            index = AddCraft(typeof(RubyHeaterShield), "Gem Armor", "Ruby Heater Shield", 122.0, 180.0, typeof(IronIngot), 1044036, 50);
            AddSkill(index, SkillName.Blacksmith, 122.0, 180.0);
            AddRes(index, typeof(Ruby), 1044234, 288);
            SetItemHue(index, ItemSets.RubySet.Hue);
            #endregion

            #region Sapphire
            index = AddCraft(typeof(SapphirePlateHelm), "Gem Armor", "Sapphire Plate Helm", 116.0, 140.0, typeof(Sapphire), 1044233, 240);
            AddSkill(index, SkillName.Blacksmith, 110.0, 140.0);
            AddRes(index, typeof(IronIngot), 1044036, 30);
            SetItemHue(index, ItemSets.SapphireSet.Hue);
            index = AddCraft(typeof(SapphirePlateGorgot), "Gem Armor", "Sapphire Plate Gorgot", 116.0, 140.0, typeof(Sapphire), 1044233, 160);
            AddSkill(index, SkillName.Blacksmith, 110.0, 140.0);
            AddRes(index, typeof(IronIngot), 1044036, 25);
            SetItemHue(index, ItemSets.SapphireSet.Hue);
            index = AddCraft(typeof(SapphirePlateChest), "Gem Armor", "Sapphire Plate Chest", 118.0, 140.0, typeof(Sapphire), 1044233, 400);
            AddSkill(index, SkillName.Blacksmith, 110.0, 140.0);
            AddRes(index, typeof(IronIngot), 1044036, 50);
            SetItemHue(index, ItemSets.SapphireSet.Hue);
            index = AddCraft(typeof(SapphirePlateArms), "Gem Armor", "Sapphire Plate Arms", 118.0, 140.0, typeof(Sapphire), 1044233, 288);
            AddSkill(index, SkillName.Blacksmith, 110.0, 140.0);
            AddRes(index, typeof(IronIngot), 1044036, 40);
            SetItemHue(index, ItemSets.SapphireSet.Hue);
            index = AddCraft(typeof(SapphirePlateGloves), "Gem Armor", "Sapphire Plate Gloves", 116.0, 140.0, typeof(Sapphire), 1044233, 192);
            AddSkill(index, SkillName.Blacksmith, 110.0, 140.0);
            AddRes(index, typeof(IronIngot), 1044036, 30);
            SetItemHue(index, ItemSets.SapphireSet.Hue);
            index = AddCraft(typeof(SapphirePlateLegs), "Gem Armor", "Sapphire Plate Legs", 116.0, 140.0, typeof(Sapphire), 1044233, 320);
            AddSkill(index, SkillName.Blacksmith, 110.0, 140.0);
            AddRes(index, typeof(IronIngot), 1044036, 30);
            SetItemHue(index, ItemSets.SapphireSet.Hue);
            index = AddCraft(typeof(SapphireHeaterShield), "Gem Armor", "Sapphire Heater Shield", 118.0, 140.0, typeof(Sapphire), 1044233, 288);
            AddSkill(index, SkillName.Blacksmith, 110.0, 140.0);
            AddRes(index, typeof(IronIngot), 1044036, 40);
            SetItemHue(index, ItemSets.SapphireSet.Hue);
            #endregion


            #region StarSapphire
            index = AddCraft(typeof(StarSapphirePlateHelm), "Gem Armor", "StarSapphire Plate Helm", 125.0, 180.0, typeof(StarSapphire), 1044231, 240);
            AddSkill(index, SkillName.Blacksmith, 125.0, 180.0);
            AddRes(index, typeof(IronIngot), 1044036, 30);
            SetItemHue(index, ItemSets.StarSapphireSet.Hue);
            index = AddCraft(typeof(StarSapphirePlateGorgot), "Gem Armor", "StarSapphire Plate Gorgot", 125.0, 180.0, typeof(StarSapphire), 1044231, 160);
            AddSkill(index, SkillName.Blacksmith, 125.0, 180.0);
            AddRes(index, typeof(IronIngot), 1044036, 25);
            SetItemHue(index, ItemSets.StarSapphireSet.Hue);
            index = AddCraft(typeof(StarSapphirePlateChest), "Gem Armor", "StarSapphire Plate Chest", 125.0, 180.0, typeof(StarSapphire), 1044231, 400);
            AddSkill(index, SkillName.Blacksmith, 125.0, 180.0);
            AddRes(index, typeof(IronIngot), 1044036, 50);
            SetItemHue(index, ItemSets.StarSapphireSet.Hue);
            index = AddCraft(typeof(StarSapphirePlateArms), "Gem Armor", "StarSapphire Plate Arms", 125.0, 180.0, typeof(StarSapphire), 1044231, 288);
            AddSkill(index, SkillName.Blacksmith, 125.0, 180.0);
            AddRes(index, typeof(IronIngot), 1044036, 40);
            SetItemHue(index, ItemSets.StarSapphireSet.Hue);
            index = AddCraft(typeof(StarSapphirePlateGloves), "Gem Armor", "StarSapphire Plate Gloves", 125.0, 180.0, typeof(StarSapphire), 1044231, 192);
            AddSkill(index, SkillName.Blacksmith, 125.0, 180.0);
            AddRes(index, typeof(IronIngot), 1044036, 30);
            SetItemHue(index, ItemSets.StarSapphireSet.Hue);
            index = AddCraft(typeof(StarSapphirePlateLegs), "Gem Armor", "StarSapphire Plate Legs", 125.0, 180.0, typeof(StarSapphire), 1044231, 320);
            AddSkill(index, SkillName.Blacksmith, 125.0, 180.0);
            AddRes(index, typeof(IronIngot), 1044036, 30);
            SetItemHue(index, ItemSets.StarSapphireSet.Hue);
            index = AddCraft(typeof(StarSapphireHeaterShield), "Gem Armor", "StarSapphire Heater Shield", 125.0, 180.0, typeof(StarSapphire), 1044231, 288);
            AddSkill(index, SkillName.Blacksmith, 125.0, 180.0);
            AddRes(index, typeof(IronIngot), 1044036, 40);
            SetItemHue(index, ItemSets.StarSapphireSet.Hue);
            #endregion
            #region Diamond
            index = AddCraft(typeof(DiamondPlateHelm), "Gem Armor", "Diamond Plate Helm", 125.0, 180.0, typeof(Diamond), 1044239, 240);
            AddSkill(index, SkillName.Blacksmith, 125.0, 180.0);
            AddRes(index, typeof(IronIngot), 1044036, 30);
            SetItemHue(index, ItemSets.DiamondSet.Hue);
            index = AddCraft(typeof(DiamondPlateGorgot), "Gem Armor", "Diamond Plate Gorgot", 125.0, 180.0, typeof(Diamond), 1044239, 160);
            AddSkill(index, SkillName.Blacksmith, 125.0, 180.0);
            AddRes(index, typeof(IronIngot), 1044036, 25);
            SetItemHue(index, ItemSets.DiamondSet.Hue);
            index = AddCraft(typeof(DiamondPlateChest), "Gem Armor", "Diamond Plate Chest", 125.0, 180.0, typeof(Diamond), 1044239, 400);
            AddSkill(index, SkillName.Blacksmith, 125.0, 180.0);
            AddRes(index, typeof(IronIngot), 1044036, 50);
            SetItemHue(index, ItemSets.DiamondSet.Hue);
            index = AddCraft(typeof(DiamondPlateArms), "Gem Armor", "Diamond Plate Arms", 125.0, 180.0, typeof(Diamond), 1044239, 288);
            AddSkill(index, SkillName.Blacksmith, 125.0, 180.0);
            AddRes(index, typeof(IronIngot), 1044036, 40);
            SetItemHue(index, ItemSets.DiamondSet.Hue);
            index = AddCraft(typeof(DiamondPlateGloves), "Gem Armor", "Diamond Plate Gloves", 125.0, 180.0, typeof(Diamond), 1044239, 192);
            AddSkill(index, SkillName.Blacksmith, 125.0, 180.0);
            AddRes(index, typeof(IronIngot), 1044036, 30);
            SetItemHue(index, ItemSets.DiamondSet.Hue);
            index = AddCraft(typeof(DiamondPlateLegs), "Gem Armor", "Diamond Plate Legs", 125.0, 180.0, typeof(Diamond), 1044239, 320);
            AddSkill(index, SkillName.Blacksmith, 125.0, 180.0);
            AddRes(index, typeof(IronIngot), 1044036, 30);
            SetItemHue(index, ItemSets.DiamondSet.Hue);
            index = AddCraft(typeof(DiamondHeaterShield), "Gem Armor", "Diamond Heater Shield", 125.0, 180.0, typeof(Diamond), 1044239, 288);
            AddSkill(index, SkillName.Blacksmith, 125.0, 180.0);
            AddRes(index, typeof(IronIngot), 1044036, 40);
            SetItemHue(index, ItemSets.DiamondSet.Hue);
            #endregion

            #endregion

            index = AddCraft(typeof(FusionCube), 1044050, "Fusion Cube", 80.0, 110.0, typeof(IronIngot), 1044036, 500);
            AddSkill(index, SkillName.Magery, 75.0, 100.0);
            AddRes(index, typeof(GoldIngot), 1027146, 500);

            index = AddCraft(typeof(EmptySkinDyeBottle), 1044050, "Skin Dye Bottle", 80.0, 110.0, typeof(IronIngot), 1044036, 100);

            // Set the overridable material
            SetSubRes( typeof( IronIngot ), 1044022 );

            // Add every material you want the player to be able to choose from
            // This will override the overridable material
            AddSubRes(typeof(IronIngot), 1044022, 00.0, 1044036, 1044267);
            AddSubRes(typeof(DullCopperIngot), 1044023, 55.0, 1044036, 1044268);
            AddSubRes(typeof(ShadowIronIngot), 1044024, 60.0, 1044036, 1044268);
            AddSubRes(typeof(CopperIngot), 1044025, 65.0, 1044036, 1044268);
            AddSubRes(typeof(BronzeIngot), 1044026, 70.0, 1044036, 1044268);
            AddSubRes(typeof(GoldIngot), 1044027, 75.0, 1044036, 1044268);
            AddSubRes(typeof(AgapiteIngot), 1044028, 80.0, 1044036, 1044268);
            AddSubRes(typeof(VeriteIngot), 1044029, 85.0, 1044036, 1044268);
            AddSubRes(typeof(ValoriteIngot), 1044030, 90.0, 1044036, 1044268);


            AddSubRes(typeof(RoseIngot), "ROSE", 80.0, 1044036, 1044267); // equiv to agapite?
            AddSubRes(typeof(BloodRockIngot), "BLOODROCK", 95.0, 1044036, 1044268);
            AddSubRes(typeof(BlackRockIngot), "BLACKROCK", 99.9, 1044036, 1044268);
            AddSubRes(typeof(PlatniumIngot), "PLATNIUM", 104.9, 1044036, 1044268);
            AddSubRes(typeof(CarbonIngot), "CARBON", 109.9, 1044036, 1044268);
            AddSubRes(typeof(KevlarIngot), "KEVLAR", 114.9, 1044036, 1044268);
            AddSubRes(typeof(LiquidIngot), "LIQUID", 119.9, 1044036, 1044268);
            AddSubRes(typeof(DeltaIngot), "DELTA", 124.9, 1044036, 1044268);
            AddSubRes(typeof(RagnarokIngot), "RAGNAROK", 129.9, 1044036, 1044268);

            MarkOption = true;
			Repair = true;
            CanEnhance = false;// Core.AOS;
		}
	}

    

	public abstract class TrapCraft : CustomCraft
	{
		private LockableContainer m_Container;

		public LockableContainer Container{ get{ return m_Container; } }

		public abstract TrapType TrapType{ get; }

		public TrapCraft( Mobile from, CraftItem craftItem, CraftSystem craftSystem, Type typeRes, BaseTool tool, int quality ) : base( from, craftItem, craftSystem, typeRes, tool, quality )
		{
		}

		private int Verify( LockableContainer container )
		{
			if ( container == null || container.KeyValue == 0 )
				return 1005638; // You can only trap lockable chests.
			if ( From.Map != container.Map || !From.InRange( container.GetWorldLocation(), 2 ) )
				return 500446; // That is too far away.
			if ( !container.Movable )
				return 502944; // You cannot trap this item because it is locked down.
			if ( !container.IsAccessibleTo( From ) )
				return 502946; // That belongs to someone else.
			if ( container.Locked )
				return 502943; // You can only trap an unlocked object.
			if ( container.TrapType != TrapType.None )
				return 502945; // You can only place one trap on an object at a time.

			return 0;
		}

		private bool Acquire( object target, out int message )
		{
			LockableContainer container = target as LockableContainer;

			message = Verify( container );

			if ( message > 0 )
			{
				return false;
			}
			else
			{
				m_Container = container;
				return true;
			}
		}

		public override void EndCraftAction()
		{
			From.SendLocalizedMessage( 502921 ); // What would you like to set a trap on?
			From.Target = new ContainerTarget( this );
		}

		private class ContainerTarget : Target
		{
			private TrapCraft m_TrapCraft;

			public ContainerTarget( TrapCraft trapCraft ) : base( -1, false, TargetFlags.None )
			{
				m_TrapCraft = trapCraft;
			}

			protected override void OnTarget( Mobile from, object targeted )
			{
				int message;

				if ( m_TrapCraft.Acquire( targeted, out message ) )
					m_TrapCraft.CraftItem.CompleteCraft( m_TrapCraft.Quality, false, m_TrapCraft.From, m_TrapCraft.CraftSystem, m_TrapCraft.TypeRes, m_TrapCraft.Tool, m_TrapCraft );
				else
					Failure( message );
			}

			protected override void OnTargetCancel( Mobile from, TargetCancelType cancelType )
			{
				if ( cancelType == TargetCancelType.Canceled )
					Failure( 0 );
			}

			private void Failure( int message )
			{
				Mobile from = m_TrapCraft.From;
				BaseTool tool = m_TrapCraft.Tool;

				if ( tool != null && !tool.Deleted && tool.UsesRemaining > 0 )
					from.SendGump( new CraftGump( from, m_TrapCraft.CraftSystem, tool, message ) );
				else if ( message > 0 )
					from.SendLocalizedMessage( message );
			}
		}

		public override Item CompleteCraft( out int message )
		{
			message = Verify( this.Container );

			if ( message == 0 )
			{
				int trapLevel = (int)(From.Skills.Tinkering.Value / 10);

				Container.TrapType = this.TrapType;
				Container.TrapPower = trapLevel * 9;
				Container.TrapLevel = trapLevel;
				Container.TrapOnLockpick = true;

				message = 1005639; // Trap is disabled until you lock the chest.
			}

			return null;
		}
	}

	[CraftItemID( 0x1BFC )]
	public class DartTrapCraft : TrapCraft
	{
		public override TrapType TrapType{ get{ return TrapType.DartTrap; } }

		public DartTrapCraft( Mobile from, CraftItem craftItem, CraftSystem craftSystem, Type typeRes, BaseTool tool, int quality ) : base( from, craftItem, craftSystem, typeRes, tool, quality )
		{
		}
	}

	[CraftItemID( 0x113E )]
	public class PoisonTrapCraft : TrapCraft
	{
		public override TrapType TrapType{ get{ return TrapType.PoisonTrap; } }

		public PoisonTrapCraft( Mobile from, CraftItem craftItem, CraftSystem craftSystem, Type typeRes, BaseTool tool, int quality ) : base( from, craftItem, craftSystem, typeRes, tool, quality )
		{
		}
	}

	[CraftItemID( 0x370C )]
	public class ExplosionTrapCraft : TrapCraft
	{
		public override TrapType TrapType{ get{ return TrapType.ExplosionTrap; } }

		public ExplosionTrapCraft( Mobile from, CraftItem craftItem, CraftSystem craftSystem, Type typeRes, BaseTool tool, int quality ) : base( from, craftItem, craftSystem, typeRes, tool, quality )
		{
		}
	}
}