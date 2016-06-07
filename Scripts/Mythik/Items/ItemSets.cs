using Scripts.Mythik.Items.Craftables.Tinkering.GemArmor;
using Server;
using Server.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//saphire    - mining / lj +1 per + 5 set 7 piece
//emerald    - arch/tact 1/1
//diaemerald - carp
//ruby       - taming/lore + 2
//citrine    - healing/anat
//amber      - carto
//tourmaline - fishing/cooking + 2 5 bonus  6 piece
//starsaph   - sword/tact/parry 1/1/1
//amethyst   - bowcraft 
//diamond    - mage/eval 1/1
//Turquoise  - 

// Scribe ? 
// Carp? 
// Tailor?
// Tink?
// Smith?
// Alch?

//sapphire    - mining / lj
//emerald    - Carp
//ruby       - taming/lore 
//citrine    - Tink / item id
//amber      - carto
//tourmaline - fishing/cooking
//starsaph   - Tailor
//amethyst   - bowcraft
//diamond    - Scribe / Mage - combine with carto?
//Turquoise  - Smith / arms lore
//Pearl      - Alch

namespace Scripts.Mythik.Items
{
    public static class ItemSets
    {
        public static BaseGearSet BlueChromeSet = new BlueChromeSet();

        public static BaseGearSet AmberSet = new AmberArmorSet();
        public static BaseGearSet RubySet = new RubyArmorSet();
        public static BaseGearSet SapphireSet = new AmberArmorSet();
        public static BaseGearSet CitrineSet = new AmberArmorSet();
        
        internal static BaseGearSet EmeraldSet = new EmeraldArmorSet();
        internal static BaseGearSet DiamondSet = new DiamondArmorSet();
        internal static BaseGearSet AmethystSet = new AmethystArmorSet();
        internal static BaseGearSet StarSapphireSet = new StarSapphireArmorSet();
        internal static BaseGearSet TourmalineSet = new TourmalineArmorSet();

        internal static BaseGearSet PearlSet = new PearlArmorSet();
        internal static BaseGearSet TurquoiseSet = new TurquoiseArmorSet();
    }
    public class TurquoiseArmorSet : BaseGearSet
    {
        //set bonus, number items for set bonus, hue, bonus for single item
        public TurquoiseArmorSet() : base(new DefaultSkillMod(SkillName.Blacksmith, true, 8.0),
            6, 0x804,
            new DefaultSkillMod(Server.SkillName.Blacksmith, true, 1.0))
        {

        }
    }
    public class PearlArmorSet : BaseGearSet
    {
        //set bonus, number items for set bonus, hue, bonus for single item
        public PearlArmorSet() : base(new DefaultSkillMod(SkillName.Alchemy, true, 8.0),
            6, 0x804,
            new DefaultSkillMod(Server.SkillName.Alchemy, true, 1.0))
        {

        }
    }
    public class EmeraldArmorSet : BaseGearSet
    {
        //set bonus, number items for set bonus, hue, bonus for single item
        public EmeraldArmorSet() : base(new DefaultSkillMod(SkillName.Carpentry, true, 8.0),
            6, 0x804, 
            new DefaultSkillMod(Server.SkillName.Carpentry, true, 1.0))
        {

        }
    }
    public class DiamondArmorSet : BaseGearSet
    {
        //set bonus, number items for set bonus, hue, bonus for single item
        public DiamondArmorSet() : base(new DefaultSkillMod(SkillName.Camping, true, 10.0),
            6, 0x809,
            new DefaultSkillMod(Server.SkillName.Camping, true, 2.0))
        {

        }
    }
    public class AmethystArmorSet : BaseGearSet
    {
        //set bonus, number items for set bonus, hue, bonus for single item
        public AmethystArmorSet() : base(new DefaultSkillMod(SkillName.Fletching, true, 8.0),
            6, 0x804,
            new DefaultSkillMod(Server.SkillName.Fletching, true, 1.0))
        {

        }
    }
    public class StarSapphireArmorSet : BaseGearSet
    {
        //set bonus, number items for set bonus, hue, bonus for single item
        public StarSapphireArmorSet() : base(new DefaultSkillMod(SkillName.Tailoring, true, 8.0),
            6, 0x805,
            new DefaultSkillMod(Server.SkillName.Tailoring, true, 1.0))
        {

        }
    }
    public class TourmalineArmorSet : BaseGearSet
    {
        //set bonus, number items for set bonus, hue, bonus for single item
        public TourmalineArmorSet() : base(new DefaultSkillMod(SkillName.Fishing, true, 8.0),
            6, 0x804,
            new DefaultSkillMod(Server.SkillName.Fishing, true, 1.0))
        {

        }
    }

    public class BlueChromeSet : BaseGearSet
    {
        //TODO corect hue
        public BlueChromeSet() : base(new DefaultSkillMod(SkillName.Parry, true, 10.0), 6,
             0x804, new DefaultSkillMod(Server.SkillName.Parry, true, 2.0))
        {

        }
    }
    public class RubyArmorSet : BaseGearSet
    {
        //set bonus, number items for set bonus, hue, bonus for single item
        public RubyArmorSet() : base(new DefaultSkillMod(SkillName.AnimalTaming, true, 6.0), 6,
             0x804, new DefaultSkillMod(Server.SkillName.AnimalTaming, true, 2.0))
        {

        }
    }
    public class CitrineArmorSet : BaseGearSet
    {
        //set bonus, number items for set bonus, hue, bonus for single item
        public CitrineArmorSet() : base(new DefaultSkillMod(SkillName.Tinkering, true, 8.0), 6,
             0x804, new DefaultSkillMod(Server.SkillName.Tinkering, true, 1.0))
        {

        }
    }
    public class SapphireArmorSet : BaseGearSet
    {
        //set bonus, number items for set bonus, hue, bonus for single item
        public SapphireArmorSet() : base(new DefaultSkillMod(SkillName.Mining, true, 8.0), 6,
             0x803, new DefaultSkillMod(Server.SkillName.Mining, true, 1.0))
        {

        }
    }
    public class AmberArmorSet : BaseGearSet
    {
        //set bonus, number items for set bonus, hue, bonus for single item
        public AmberArmorSet() : base(
            new SkillMod[] {
            new DefaultSkillMod(SkillName.Cartography, true, 8.0),
            new DefaultSkillMod(SkillName.Inscribe, true, 8.0)
            },
            6,
             0x804, 
             new SkillMod[] {
                 new DefaultSkillMod(SkillName.Cartography, true, 1.0),
                 new DefaultSkillMod(SkillName.Inscribe, true, 1.0)
             }
             )
        {

        }
    }
}
