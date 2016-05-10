using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Server.Mobiles;
using Server;
using Server.Items;
using Scripts.Mythik.Items.Uniques;

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
                new LootPackItem( typeof( LumberjackCap ), 1 ),
                new LootPackItem( typeof( DamagedMiningHat ), 1 )
            };

        public static readonly LootPack Level1 = new LootPack(new LootPackEntry[]
            {
                new LootPackEntry(  true, Gold,                     100.00, "2d20+50" ),
                new LootPackEntry( false, PotionItems,              5,      2 ),
                new LootPackEntry( false, LowScrollItems,           5,      1 ),
                new LootPackEntry( false, LevelOneUniques,          0.13,   1 ) // 0.13 is equiv to 1 in 769
            });

        public static void GenerateLoot(BaseCreature mob)
        {
            //No chance of loot
            var diff = BaseInstrument.GetBaseDifficulty(mob);
            int lvl = 0;
            if (diff < 35)
                lvl = 1;
            if (diff < 60)
                lvl = 2;
            if (diff < 85)
                lvl = 3;
            if (diff < 105)
                lvl = 4;
            if (diff >= 105)
                lvl = 5;

            mob.AddLoot(Level1);



            if (mob.IsParagon)
            {
                mob.AddLoot(Level1);
            }
            if(mob.Region.IsPartOf("Doom"))
            {

            }
        }

        /// <summary>
        /// List of items rewarded using ToT style system
        /// </summary>
        private static Type[] m_LesserArtifacts = new Type[] {
            typeof(LesserPigmentsOfTokuno),typeof( MetalPigmentsOfTokuno )

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
