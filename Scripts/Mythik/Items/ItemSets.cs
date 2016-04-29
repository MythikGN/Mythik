using Scripts.Mythik.Items.Craftables.Tinkering.GemArmor;
using Server;
using Server.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scripts.Mythik.Items
{
    public static class ItemSets
    {
        public static BaseGearSet AmberSet = new AmberArmorSet();
        public static BaseGearSet RubySet = new RubyArmorSet();
        public static BaseGearSet SapphireSet = new AmberArmorSet();
        public static BaseGearSet CitrineSet = new AmberArmorSet();
    }

    public class RubyArmorSet : BaseGearSet
    {
        //set bonus, number items for set bonus, hue, bonus for single item
        public RubyArmorSet() : base(new DefaultSkillMod(SkillName.Cartography, true, 10.0), 6,
             0x804, new DefaultSkillMod(Server.SkillName.Cartography, true, 2.0))
        {

        }
    }
    public class CitrineArmorSet : BaseGearSet
    {
        //set bonus, number items for set bonus, hue, bonus for single item
        public CitrineArmorSet() : base(new DefaultSkillMod(SkillName.Cartography, true, 10.0), 6,
             0x804, new DefaultSkillMod(Server.SkillName.Cartography, true, 2.0))
        {

        }
    }
    public class SapphireArmorSet : BaseGearSet
    {
        //set bonus, number items for set bonus, hue, bonus for single item
        public SapphireArmorSet() : base(new DefaultSkillMod(SkillName.Cartography, true, 10.0), 6,
             0x804, new DefaultSkillMod(Server.SkillName.Cartography, true, 2.0))
        {

        }
    }
    public class AmberArmorSet : BaseGearSet
    {
        //set bonus, number items for set bonus, hue, bonus for single item
        public AmberArmorSet() : base(new DefaultSkillMod(SkillName.Cartography, true, 10.0), 6,
             0x804, new DefaultSkillMod(Server.SkillName.Cartography, true, 2.0))
        {

        }
    }
}
