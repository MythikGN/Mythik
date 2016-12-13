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
   

    class VetsRabbitCloak : Cloak,IUniqueItem
    {
        /// <summary>
        /// Instead of adding in DefTailoring.cs can be added like this in the same class as the craftable.
        /// </summary>
        public static void Initalize()
        {
            int index = DefTailoring.CraftSystem.AddCraft(typeof(VetsRabbitCloak), "Rares", "Veterinarian's Cloak", 100.1, 125, typeof(Leather), "Leather", 15);
            DefTailoring.CraftSystem.AddSkill(index, SkillName.Veterinary, 70, 100);
            DefTailoring.CraftSystem.AddRes(index, typeof(RabbitFur), "Rabbit Fur", 50);
        }
        [Constructable]
        public VetsRabbitCloak() : base()
        {
            Hue = 0x7A1;//Ancient hue
            Name = "Veterinarian's Cloak";
            SkillBonuses.SetValues(0, Server.SkillName.Veterinary, 5.0);
        }
        [Constructable]
        public VetsRabbitCloak(Serial serial): base(serial)
        {

        }

        public RareLevel UniqueLevel
        {
            get
            {
                return RareLevel.Rare;
            }
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
        }
        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
        }
    }
}
