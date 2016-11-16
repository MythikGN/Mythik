using Server.Items;
using Server.Mobiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scripts.Mythik.Systems
{
    public class MonsterLevels
    {
        public static int GetMonsterLevel(BaseCreature mob)
        {
            var diff = BaseInstrument.GetBaseDifficulty(mob);
            if (diff < 35)
                return 1;
            if (diff < 60)
                return 2;
            if (diff < 85)
                return 3;
            if (diff < 105)
                return 4;
            return 5;
        }
        public static string GetMonsterDifficultyLevelText(BaseCreature mob)
        {
            switch (GetMonsterLevel(mob))
            {
                case 1: return "Easy";
                case 2: return "Moderate";
                case 3: return "Intermediate";
                case 4: return "Difficult";
                case 5: return "Challenging";
                default: return "";
            }
        }
    }
}
