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

        /// <summary>
        /// Standard loot packs for level 1-5 monsters, handles dropping gold, pots, scrolls, gems and addons. 
        /// These loot packs drop direct on monster corpse, Killers luck effects drop rates.
        /// </summary>
        public static readonly LootPack Level1 = new LootPack(new LootPackEntry[]
            {
                new LootPackEntry(  true, Gold,                     100.00, "2d20+50" ),
                new LootPackEntry( false, PotionItems,              5,      2 ),
                new LootPackEntry( false, LowScrollItems,           5,      1 ),
                new LootPackEntry(false, new LootPackItem[] { new LootPackItem( typeof(Amber),1) },60.0,"2d1+3"),
                new LootPackEntry( false, LevelOneUniques,          0.13,   1 ) // 0.13 is equiv to 1 in 769
            });

        public static readonly LootPack Level2 = new LootPack(new LootPackEntry[]
    {
                new LootPackEntry(  true, Gold,                     100.00, "2d80+50" ),
                new LootPackEntry( false, PotionItems,              5,      2 ),
                new LootPackEntry( false, LowScrollItems,           5,      2 ),
                new LootPackEntry(false, new LootPackItem[] { new LootPackItem( typeof(Citrine),1),new LootPackItem( typeof(Amethyst),1) },60.0,"2d1+3"),
                new LootPackEntry( false, LevelOneUniques,          0.13,   1 ) // 0.13 is equiv to 1 in 769
    });

        public static readonly LootPack Level3 = new LootPack(new LootPackEntry[]
{
                new LootPackEntry(  true, Gold,                     100.00, "2d180+90" ),
                new LootPackEntry( false, PotionItems,              5,      2 ),
                new LootPackEntry( false, MedScrollItems,           5,      1 ),
                new LootPackEntry(false, new LootPackItem[] { new LootPackItem( typeof(Tourmaline),1),new LootPackItem( typeof(Sapphire),1) },60.0,"2d1+3"),
                new LootPackEntry( false, LevelOneUniques,          0.13,   1 ) // 0.13 is equiv to 1 in 769
});

        public static readonly LootPack Level4 = new LootPack(new LootPackEntry[]
{
                new LootPackEntry(  true, Gold,                     100.00, "2d300+150" ),
                new LootPackEntry( false, PotionItems,              5,      2 ),
                new LootPackEntry( false, MedScrollItems,           5,      2 ),
                new LootPackEntry(false, new LootPackItem[] { new LootPackItem( typeof(Ruby),1),new LootPackItem( typeof(Emerald),1) },60.0,"2d1+3"),
                new LootPackEntry( false, LevelOneUniques,          0.13,   1 ) // 0.13 is equiv to 1 in 769
});

        public static readonly LootPack Level5 = new LootPack(new LootPackEntry[]
{
                new LootPackEntry(  true, Gold,                     100.00, "2d500+250" ),
                new LootPackEntry( false, PotionItems,              5,      2 ),
                new LootPackEntry( false, HighScrollItems,           10,      2 ),
                new LootPackEntry(false, new LootPackItem[] { new LootPackItem( typeof(StarSapphire),1),new LootPackItem( typeof(Diamond),1) },60.0,"2d1+3"),
                new LootPackEntry( false, LevelOneUniques,          0.13,   1 ) // 0.13 is equiv to 1 in 769
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

        /// <summary>
        /// List of items rewarded using ToT style system dropped direct to backpack, 
        /// and an increasing chance to get one every mob you kill, till you get one.
        /// Killers luck also effects drop rate.
        /// </summary>
        private static Type[] m_LesserArtifacts = new Type[] {
            typeof(RareClothDyeTub),typeof( RareLeatherDyeTub ), typeof(RuneBookChargeDeed),
            typeof(AnimateDeadScrollRecipe),typeof(BloodOathScrollRecipe),typeof(SummonFamiliarScrollRecipe),
            typeof(EnemyOfOneScrollRecipe)

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
                    i = Activator.CreateInstance(m_LesserArtifacts[Utility.Random(m_LesserArtifacts.Length)]) as Item;
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
