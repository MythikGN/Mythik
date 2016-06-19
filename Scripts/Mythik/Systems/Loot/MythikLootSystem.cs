using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Server.Mobiles;
using Server;
using Server.Items;
using Scripts.Mythik.Items.Uniques;
using Scripts.Mythik.Items.Rares;
using Scripts.Mythik.Items.Rares.Recipes;
using Scripts.Mythik.Items.Rares.Equipment;
using Scripts.Mythik.Items.Craftables.Tinkering.GemArmor;

namespace Scripts.Mythik.Systems.Loot
{
    public class MythikLootSystem
    {
        private static readonly LootPackItem[] Gold = new LootPackItem[]
        {
                new LootPackItem( typeof( Gold ), 1 )
        };
        private static readonly LootPackItem[] LowScrollItems = new LootPackItem[]
             {
                new LootPackItem( typeof( ClumsyScroll ), 1 )
             };

        private static readonly LootPackItem[] MedScrollItems = new LootPackItem[]
            {
                new LootPackItem( typeof( ArchCureScroll ), 1 )
            };

        private static readonly LootPackItem[] HighScrollItems = new LootPackItem[]
            {
                new LootPackItem( typeof( SummonAirElementalScroll ), 1 )
            };

        private static readonly LootPackItem[] PotionItems = new LootPackItem[]
            {
                new LootPackItem( typeof( AgilityPotion ), 1 ),
                new LootPackItem( typeof( StrengthPotion ), 1 ),
                new LootPackItem( typeof( RefreshPotion ), 1 ),
                new LootPackItem( typeof( LesserCurePotion ), 1 ),
                new LootPackItem( typeof( LesserHealPotion ), 1 ),
                new LootPackItem( typeof( LesserPoisonPotion ), 1 )
            };
        public static readonly LootPackItem[] OldMagicItems = new LootPackItem[]
            {
                new LootPackItem( typeof( BaseJewel ), 1 ),
                new LootPackItem( typeof( BaseArmor ), 4 ),
                new LootPackItem( typeof( BaseWeapon ), 3 ),
                new LootPackItem( typeof( BaseRanged ), 1 ),
                new LootPackItem( typeof( BaseShield ), 1 )
            };
        public static readonly LootPackItem[] BlueChromeSet = new LootPackItem[]
            {
                new LootPackItem( typeof( BlueChromePlateChest ), 1 ),
                new LootPackItem( typeof( BlueChromeHeaterShield ), 1 ),
                new LootPackItem( typeof( BlueChromePlateArms ), 1 ),
                new LootPackItem( typeof( BlueChromePlateGloves ), 1 ),
                new LootPackItem( typeof( BlueChromePlateHelm ), 1 ),
                new LootPackItem( typeof( BlueChromePlateLegs ), 1 ),
                new LootPackItem( typeof( BlueChromePlateGorgot ), 1 )
            };


        public static readonly LootPackItem[] LevelOneUniques = new LootPackItem[]
            {
                new LootPackItem( typeof( AlchemySandals ),1 ),
                new LootPackItem( typeof( BlacksmithyArms ), 1 ),
                new LootPackItem( typeof( CarpentryBoots ), 1 ),
                new LootPackItem( typeof( TamingSkirt ), 1 ),
                new LootPackItem( typeof( TailorSandals ), 1 ),
                new LootPackItem( typeof( TinkeringCap ), 1 ),
                new LootPackItem( typeof( InscribeBoots ), 1 ),
                new LootPackItem( typeof( LumberjackCap ), 1 ),
                new LootPackItem( typeof( DamagedMiningHat ), 1 )
            };
        public static readonly LootPackItem[] LevelTwoUniques = new LootPackItem[]
    {
                new LootPackItem( typeof( AlchemyBandena ),1 ),
                new LootPackItem( typeof( BlacksmithShoes ), 1 ),
                new LootPackItem( typeof( CarpentryCap ), 1 ),
                new LootPackItem( typeof( TamingCloak ), 1 ),
                new LootPackItem( typeof( TailorGloves ), 1 ),
                new LootPackItem( typeof( TinkeringShirt ), 1 ),
                new LootPackItem( typeof( InscribeHat ), 1 ),
                new LootPackItem( typeof( LumberjackGloves ), 1 ),
                new LootPackItem( typeof( Miningboots ), 1 )
    };

        public static readonly LootPackItem[] LevelThreeUniques = new LootPackItem[]
{
                new LootPackItem( typeof( AlchemyApron ),1 ),
                new LootPackItem( typeof( BlacksmithBandana ), 1 ),
                new LootPackItem( typeof( CarpentryApron ), 1 ),
                new LootPackItem( typeof( TamingBoots ), 1 ),
                new LootPackItem( typeof( TailorHat ), 1 ),
                new LootPackItem( typeof( TinkeringPants ), 1 ),
                new LootPackItem( typeof( InscribeApron ), 1 ),
                new LootPackItem( typeof( LumberjackShirt ), 1 )
};

        public static readonly LootPackItem[] LevelFourniques = new LootPackItem[]
{
                new LootPackItem( typeof( AlchemyGloves ),1 ),
                new LootPackItem( typeof( BlacksmithApron ), 1 ),
                new LootPackItem( typeof( CarpentryGloves ), 1 ),
                new LootPackItem( typeof( TamingRobe ), 1 ),
                new LootPackItem( typeof( TailoringDoublet ), 1 ),
                new LootPackItem( typeof( TinkeringGloves ), 1 ),
                new LootPackItem( typeof( InscribeShirt ), 1 ),
                new LootPackItem( typeof( LumberjackPants ), 1 ),
                new LootPackItem( typeof( Miningboots ), 1 )
};

        public static readonly LootPackItem[] LevelFiveUniques = new LootPackItem[]
{
                new LootPackItem( typeof( AlchemyRobe ),1 ),
                new LootPackItem( typeof( BlacksmithRobe ), 1 ),
                new LootPackItem( typeof( CarpentryShirt ), 1 ),
                new LootPackItem( typeof( TamingCrook ), 1 ),
                new LootPackItem( typeof( TailorSkirt ), 1 ),
                new LootPackItem( typeof( TinkeringApron ), 1 ),
                new LootPackItem( typeof( InscribeShirt ), 1 ),
                new LootPackItem( typeof( LumberjackApron ), 1 ),
                new LootPackItem( typeof( MiningRobe ), 1 )
};

        public static readonly LootPackItem[] SkillJewels = new LootPackItem[]
{
                new LootPackItem( typeof( SkillRing ),1 ),
                new LootPackItem( typeof( SkillNecklace ), 1 ),
                new LootPackItem( typeof( SkillEarrings ), 1 ),
                new LootPackItem( typeof( SkillBracelet ), 1 ),
};
        /// <summary>
        /// Standard loot packs for level 1-5 monsters, handles dropping gold, pots, scrolls, gems and addons. 
        /// These loot packs drop direct on monster corpse, Killers luck effects drop rates.
        /// </summary>
        public static readonly LootPack Level1 = new LootPack(new LootPackEntry[]
            {
                new LootPackEntry(  false, Gold,                     100.00, "2d120+25" ),
                new LootPackEntry( false, PotionItems,              45.0,      2 ),
                new LootPackEntry( false, LowScrollItems,           35.0,      1 ),
                new LootPackEntry(false, new LootPackItem[] { new LootPackItem( typeof(Amber),1) },60.0,"1d3"),
                new LootPackEntry( false, OldMagicItems,  2.0, 1, 1, 00, 60 ),
                new LootPackEntry( false, BlueChromeSet,          0.25,   1 ),
                new LootPackEntry( false, SkillJewels,          0.20,   1 ),
                new LootPackEntry( false, LevelOneUniques,          0.13,   1 ) // 0.13 is equiv to 1 in 769

            });

        public static readonly LootPack Level2 = new LootPack(new LootPackEntry[]
    {
                new LootPackEntry(  false, Gold,                     100.00, "2d180+50" ),
                new LootPackEntry( false, PotionItems,              55.0,      2 ),
                new LootPackEntry( false, LowScrollItems,           45.0,      2 ),
                new LootPackEntry(false, new LootPackItem[] { new LootPackItem( typeof(Citrine),1),new LootPackItem( typeof(Amethyst),1) },60.0,"1d3"),
                new LootPackEntry( false, OldMagicItems,  6.0, 1, 1, 30, 70 ),
                new LootPackEntry( false, SkillJewels,          0.22,   1 ),
                new LootPackEntry( false, LevelTwoUniques,          0.14,   1 ) // 0.13 is equiv to 1 in 769
    });

        public static readonly LootPack Level3 = new LootPack(new LootPackEntry[]
{
                new LootPackEntry(  false, Gold,                     100.00, "2d280+90" ),
                new LootPackEntry( false, PotionItems,              55.0,      2 ),
                new LootPackEntry( false, MedScrollItems,           45.0,      1 ),
                new LootPackEntry(false, new LootPackItem[] { new LootPackItem( typeof(Tourmaline),1),new LootPackItem( typeof(Sapphire),1) },60.0,"1d3"),
                new LootPackEntry( false, OldMagicItems,  5.0, 1, 1, 30, 80 ),
                new LootPackEntry( false, SkillJewels,          0.26,   1 ),
                new LootPackEntry( false, LevelThreeUniques,          0.15,   1 ) // 0.13 is equiv to 1 in 769
});

        public static readonly LootPack Level4 = new LootPack(new LootPackEntry[]
{
                new LootPackEntry(  false, Gold,                     100.00, "2d400+150" ),
                new LootPackEntry( false, PotionItems,              60.0,      2 ),
                new LootPackEntry(false, new LootPackItem[] { new LootPackItem( typeof(Ruby),1),new LootPackItem( typeof(Emerald),1) },50.0,"1d4"),

                new LootPackEntry( false, MedScrollItems,           40.0,      2 ),
                new LootPackEntry( false, OldMagicItems,  4.0, 1, 1, 20, 100 ),
                new LootPackEntry( false, SkillJewels,          0.30,   1 ),
                new LootPackEntry( false, LevelFourniques,          0.16,   1 ) // 0.13 is equiv to 1 in 769
});

        public static readonly LootPack Level5 = new LootPack(new LootPackEntry[]
{
                new LootPackEntry(  false, Gold,                     100.00, "2d500+250" ),
                new LootPackEntry( false, PotionItems,              70.00,      "2d1+1" ),
                new LootPackEntry(false, new LootPackItem[] { new LootPackItem( typeof(StarSapphire),1),new LootPackItem( typeof(Diamond),1) },60.0,"1d5"),
                new LootPackEntry( false, HighScrollItems,           50.00,      "2d2" ),
                new LootPackEntry( false, OldMagicItems,  6.0, 1, 1, 50, 100 ),
                new LootPackEntry( false, SkillJewels,          0.34,   1 ), 
                new LootPackEntry( false, LevelFiveUniques,          0.17,   1 ) // 0.13 is equiv to 1 in 769
});

        public static void GenerateLoot(BaseCreature mob)
        {

           switch(mob.GetMonsterLevel() + (mob.IsParagon == true ? 1 : 0)) // bump the loot level + 1 if paragon
            {
                case 1:
                    mob.AddLoot(Level1);
                    break;
                case 2:
                    mob.AddLoot(Level2);
                    break;
                case 3:
                    mob.AddLoot(Level3);
                    break;
                case 4:
                    mob.AddLoot(Level4);
                    break;
                case 5:
                    mob.AddLoot(Level5);
                    break;
                default:
                    break;
            }
            if(mob.Region.IsPartOf("Doom"))
            {

            }
        }

        // these are dropped via paragon system as well
        // probably via tmaps? TODO
        /// <summary>
        /// List of items rewarded using ToT style system dropped direct to backpack, 
        /// and an increasing chance to get one every mob you kill, till you get one.
        /// Killers luck also effects drop rate.
        /// </summary>
        public static Type[] LesserArtifacts = new Type[] {
            typeof(RareClothDyeTub),typeof( RareLeatherDyeTub ), typeof(RuneBookChargeDeed),
            typeof(AnimateDeadScrollRecipe),typeof(BloodOathScrollRecipe),typeof(SummonFamiliarScrollRecipe),
            typeof(EnemyOfOneScrollRecipe), typeof(HolyWarFork), typeof(SkillBracelet), typeof(SkillRing), typeof(SkillNecklace), typeof(SkillEarrings)

        };
        /// <summary>
        /// Called Once Per mob death, to handle ToT style direct to backpack 
        /// system, where your chance to get one increases each kill
        /// </summary>
        /// <param name="baseCreature"></param>
        /// <param name="m_Mobile"></param>
        internal static void HandleKill(BaseCreature victim, Mobile killer)
        {
            PlayerMobile pm = killer as PlayerMobile;
            BaseCreature bc = victim as BaseCreature;
            if (bc.Controlled || bc.Owners.Count > 0 || bc.Fame <= 0)
                return;

            pm.ToTTotalMonsterFame += (int)(bc.Fame * (1 + Math.Sqrt(pm.Luck) / 100));

            //This is the Exponentional regression with only 2 datapoints.
            //A log. func would also work, but it didn't make as much sense.
            //This function isn't OSI exact beign that I don't know OSI's func they used ;p
            int x = pm.ToTTotalMonsterFame;

            //const double A = 8.63316841 * Math.Pow( 10, -4 );
            const double A = 0.000863316841;
            //const double B = 4.25531915 * Math.Pow( 10, -6 );
            const double B = 0.00000425531915;

            double chance = A * Math.Pow(10, B * x);

            if (chance > Utility.RandomDouble())
            {
                Item i = null;

                try
                {
                    i = Activator.CreateInstance(LesserArtifacts[Utility.Random(LesserArtifacts.Length)]) as Item;
                }
                catch
                { }

                if (i != null)
                {
                    pm.SendLocalizedMessage(1062317); // For your valor in combating the fallen beast, a special artifact has been bestowed on you.

                    if (!pm.PlaceInBackpack(i))
                    {
                        if (pm.BankBox != null && pm.BankBox.TryDropItem(killer, i, false))
                            pm.SendLocalizedMessage(1079730); // The item has been placed into your bank box.
                        else
                        {
                            pm.SendLocalizedMessage(1072523); // You find an artifact, but your backpack and bank are too full to hold it.
                            i.MoveToWorld(pm.Location, pm.Map);
                        }
                    }

                    pm.ToTTotalMonsterFame = 0;
                }
            }

        }
    }
}
