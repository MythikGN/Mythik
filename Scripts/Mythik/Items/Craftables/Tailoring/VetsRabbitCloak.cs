using Server;
using Server.Engines.Craft;
using Server.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scripts.Mythik.Items.Craftables.Tailoring
{
    class RabbitFur : BaseLeather, ICommodity
    {
        public TextDefinition Description
        {
            get
            {
                return "rabbit fur";
            }
        }

        public bool IsDeedable
        {
            get
            {
                return true;
            }
        }
        public RabbitFur(int num) : base(CraftResource.RegularLeather,num)
        {
            Hue = 0x7A1;//Ancient hue
            Name = "rabbit fur";
        }
    }

    class VetsRabbitCloak : Cloak,IUniqueItem
    {
        public static void Initalize()
        {
            int index = DefTailoring.CraftSystem.AddCraft(typeof(VetsRabbitCloak), "Rares", "Veterinarian's Cloak", 100.1, 125, typeof(Leather), "Leather", 15);
            DefTailoring.CraftSystem.AddSkill(index, SkillName.Veterinary, 70, 100);
            DefTailoring.CraftSystem.AddRes(index, typeof(RabbitFur), "Rabbit Fur", 50);
        }
        public VetsRabbitCloak() : base()
        {
            Hue = 0x7A1;//Ancient hue
            Name = "Veterinarian's Cloak";
            SkillBonuses.SetValues(0, Server.SkillName.Veterinary, 5.0);
        }

        public RareLevel UniqueLevel
        {
            get
            {
                return RareLevel.Rare;
            }
        }
    }
}
