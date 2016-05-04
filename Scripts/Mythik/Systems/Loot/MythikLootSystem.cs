using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Server.Mobiles;
using Server;
using Server.Items;

namespace Scripts.Mythik.Systems.Loot
{
    public class MythikLootSystem
    {
        //Just add LootPacks here, standard loot/luck system takes care of the rest.

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
            
            //mob.AddLoot(LootPack.)



            if (mob.IsParagon)
            {

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
