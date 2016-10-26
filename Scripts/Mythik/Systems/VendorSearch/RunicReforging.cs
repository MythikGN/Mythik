using Server;
using Server.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Engines.VendorSearhing
{
    public static class RunicReforging
    {
        private static Dictionary<int, ImbuingDefinition> m_Table;
        public static Dictionary<int, ImbuingDefinition> Table { get { return m_Table; } }

        public static void Initialize()
        {
            m_Table = new Dictionary<int, ImbuingDefinition>();

            m_Table[1] = new ImbuingDefinition(AosAttribute.DefendChance, 1075620, 110, typeof(RelicFragment), typeof(Tourmaline), typeof(WhitePearl), 15, 1, 1111947);
            m_Table[2] = new ImbuingDefinition(AosAttribute.AttackChance, 1075616, 130, typeof(EnchantEssence), typeof(Tourmaline), typeof(WhitePearl), 15, 1, 1111958);
            m_Table[3] = new ImbuingDefinition(AosAttribute.RegenHits, 1075627, 100, typeof(EnchantEssence), typeof(Tourmaline), typeof(WhitePearl), 2, 1, 1111994);
            m_Table[4] = new ImbuingDefinition(AosAttribute.RegenStam, 1079411, 100, typeof(EnchantEssence), typeof(Diamond), typeof(WhitePearl), 3, 1, 1112043);
            m_Table[5] = new ImbuingDefinition(AosAttribute.RegenMana, 1079410, 100, typeof(EnchantEssence), typeof(Sapphire), typeof(WhitePearl), 2, 1, 1112003);
            m_Table[6] = new ImbuingDefinition(AosAttribute.BonusStr, 1079767, 110, typeof(EnchantEssence), typeof(Diamond), typeof(FireRuby), 8, 1, 1112044);
            m_Table[7] = new ImbuingDefinition(AosAttribute.BonusDex, 1079732, 110, typeof(EnchantEssence), typeof(Ruby), typeof(BlueDiamond), 8, 1, 1111948);
            m_Table[8] = new ImbuingDefinition(AosAttribute.BonusInt, 1079756, 110, typeof(EnchantEssence), typeof(Tourmaline), typeof(Turquoise), 8, 1, 1111995);
            m_Table[9] = new ImbuingDefinition(AosAttribute.BonusHits, 1075630, 110, typeof(EnchantEssence), typeof(Ruby), typeof(LuminescentFungi), 5, 1, 1111993);
            m_Table[10] = new ImbuingDefinition(AosAttribute.BonusStam, 1075632, 110, typeof(EnchantEssence), typeof(Diamond), typeof(LuminescentFungi), 8, 1, 1112042);
            m_Table[11] = new ImbuingDefinition(AosAttribute.BonusMana, 1075631, 110, typeof(EnchantEssence), typeof(Sapphire), typeof(LuminescentFungi), 8, 1, 1112002);
            m_Table[12] = new ImbuingDefinition(AosAttribute.WeaponDamage, 1075619, 100, typeof(EnchantEssence), typeof(Citrine), typeof(WhitePearl), 50, 1, 1112005);
            m_Table[13] = new ImbuingDefinition(AosAttribute.WeaponSpeed, 1075629, 110, typeof(RelicFragment), typeof(Tourmaline), typeof(WhitePearl), 30, 5, 1112045);
            m_Table[14] = new ImbuingDefinition(AosAttribute.SpellDamage, 1075628, 100, typeof(EnchantEssence), typeof(Emerald), typeof(WhitePearl), 12, 1, 1112041);
            m_Table[15] = new ImbuingDefinition(AosAttribute.CastRecovery, 1075618, 120, typeof(RelicFragment), typeof(Amethyst), typeof(WhitePearl), 3, 1, 1111952);
            m_Table[16] = new ImbuingDefinition(AosAttribute.CastSpeed, 1075617, 140, typeof(RelicFragment), typeof(Ruby), typeof(WhitePearl), 1, 1, 1111951);
            m_Table[17] = new ImbuingDefinition(AosAttribute.LowerManaCost, 1075621, 110, typeof(RelicFragment), typeof(Tourmaline), typeof(WhitePearl), 8, 1, 1111996);
            m_Table[18] = new ImbuingDefinition(AosAttribute.LowerRegCost, 1075625, 100, typeof(MagicalResidue), typeof(Amber), typeof(WhitePearl), 20, 1, 1111997);
            m_Table[19] = new ImbuingDefinition(AosAttribute.ReflectPhysical, 1075626, 100, typeof(MagicalResidue), typeof(Citrine), typeof(WhitePearl), 15, 1, 1112006);
            m_Table[20] = new ImbuingDefinition(AosAttribute.EnhancePotions, 1075624, 100, typeof(EnchantEssence), typeof(Citrine), typeof(WhitePearl), 25, 5, 1111950);
            m_Table[21] = new ImbuingDefinition(AosAttribute.Luck, 1061153, 100, typeof(MagicalResidue), typeof(Citrine), typeof(WhitePearl), 100, 1, 1111999);
            m_Table[22] = new ImbuingDefinition(AosAttribute.SpellChanneling, 1079766, 100, typeof(MagicalResidue), typeof(Diamond), typeof(WhitePearl), 1, 0, 1112040);
            m_Table[23] = new ImbuingDefinition(AosAttribute.NightSight, 1015168, 50, typeof(MagicalResidue), typeof(Tourmaline), typeof(WhitePearl), 1, 0, 1112004);

            m_Table[24] = new ImbuingDefinition(AosWeaponAttribute.LowerStatReq, 1079757, 100, typeof(EnchantEssence), typeof(Amethyst), typeof(WhitePearl), 100, 10, 1111998);
            m_Table[25] = new ImbuingDefinition(AosWeaponAttribute.HitLeechHits, 1079698, 110, typeof(MagicalResidue), typeof(Ruby), typeof(WhitePearl), 50, 2, 1111964);
            m_Table[26] = new ImbuingDefinition(AosWeaponAttribute.HitLeechStam, 1079707, 100, typeof(MagicalResidue), typeof(Diamond), typeof(WhitePearl), 50, 2, 1111992);
            m_Table[27] = new ImbuingDefinition(AosWeaponAttribute.HitLeechMana, 1079701, 110, typeof(MagicalResidue), typeof(Sapphire), typeof(WhitePearl), 50, 2, 1111967);
            m_Table[28] = new ImbuingDefinition(AosWeaponAttribute.HitLowerAttack, 1079699, 110, typeof(EnchantEssence), typeof(Emerald), typeof(ParasiticPlant), 50, 2, 1111965);
            m_Table[29] = new ImbuingDefinition(AosWeaponAttribute.HitLowerDefend, 1079700, 130, typeof(EnchantEssence), typeof(Tourmaline), typeof(ParasiticPlant), 50, 2, 1111966);
            m_Table[30] = new ImbuingDefinition(AosWeaponAttribute.HitPhysicalArea, 1079696, 100, typeof(MagicalResidue), typeof(Diamond), typeof(WhitePearl), 50, 2, 1111956);
            m_Table[31] = new ImbuingDefinition(AosWeaponAttribute.HitFireArea, 1079695, 100, typeof(MagicalResidue), typeof(Ruby), typeof(WhitePearl), 50, 2, 1111955);
            m_Table[32] = new ImbuingDefinition(AosWeaponAttribute.HitColdArea, 1079693, 100, typeof(MagicalResidue), typeof(Sapphire), typeof(WhitePearl), 50, 2, 1111953);
            m_Table[33] = new ImbuingDefinition(AosWeaponAttribute.HitPoisonArea, 1079697, 100, typeof(MagicalResidue), typeof(Emerald), typeof(WhitePearl), 50, 2, 1111957);
            m_Table[34] = new ImbuingDefinition(AosWeaponAttribute.HitEnergyArea, 1079694, 100, typeof(MagicalResidue), typeof(Amethyst), typeof(WhitePearl), 50, 2, 1111954);
            m_Table[35] = new ImbuingDefinition(AosWeaponAttribute.HitMagicArrow, 1079706, 120, typeof(RelicFragment), typeof(Amber), typeof(WhitePearl), 50, 2, 1111963);
            m_Table[36] = new ImbuingDefinition(AosWeaponAttribute.HitHarm, 1079704, 110, typeof(EnchantEssence), typeof(Emerald), typeof(ParasiticPlant), 50, 2, 1111961);
            m_Table[37] = new ImbuingDefinition(AosWeaponAttribute.HitFireball, 1079703, 140, typeof(EnchantEssence), typeof(Ruby), typeof(FireRuby), 50, 2, 1111960);
            m_Table[38] = new ImbuingDefinition(AosWeaponAttribute.HitLightning, 1079705, 140, typeof(RelicFragment), typeof(Amethyst), typeof(WhitePearl), 50, 2, 1111962);
            m_Table[39] = new ImbuingDefinition(AosWeaponAttribute.HitDispel, 1079702, 100, typeof(MagicalResidue), typeof(Amber), typeof(WhitePearl), 50, 2, 1111959);
            m_Table[40] = new ImbuingDefinition(AosWeaponAttribute.UseBestSkill, 1079592, 150, typeof(EnchantEssence), typeof(Amber), typeof(WhitePearl), 1, 0, 1111946);
            m_Table[41] = new ImbuingDefinition(AosWeaponAttribute.MageWeapon, 1079759, 100, typeof(EnchantEssence), typeof(Emerald), typeof(WhitePearl), 10, 1, 1112001);
            m_Table[42] = new ImbuingDefinition(AosWeaponAttribute.DurabilityBonus, 1017323, 100, typeof(EnchantEssence), typeof(Diamond), typeof(WhitePearl), 100, 10, 1112949);

            m_Table[49] = new ImbuingDefinition(AosArmorAttribute.MageArmor, 1079758, 0, typeof(EnchantEssence), typeof(Diamond), typeof(WhitePearl), 1, 0, 1112000);

            m_Table[51] = new ImbuingDefinition(AosElementAttribute.Physical, 1061158, 100, typeof(MagicalResidue), typeof(Diamond), typeof(WhitePearl), 15, 1, 1112010);
            m_Table[52] = new ImbuingDefinition(AosElementAttribute.Fire, 1061159, 100, typeof(MagicalResidue), typeof(Ruby), typeof(WhitePearl), 15, 1, 1112009);
            m_Table[53] = new ImbuingDefinition(AosElementAttribute.Cold, 1061160, 100, typeof(MagicalResidue), typeof(Sapphire), typeof(WhitePearl), 15, 1, 1112007);
            m_Table[54] = new ImbuingDefinition(AosElementAttribute.Poison, 1061161, 100, typeof(MagicalResidue), typeof(Emerald), typeof(WhitePearl), 15, 1, 1112011);
            m_Table[55] = new ImbuingDefinition(AosElementAttribute.Energy, 1061162, 100, typeof(MagicalResidue), typeof(Amethyst), typeof(WhitePearl), 15, 1, 1112008);

            m_Table[60] = new ImbuingDefinition("WeaponVelocity", 1080416, 130, typeof(RelicFragment), typeof(Tourmaline), typeof(WhitePearl), 50, 2, 1112048);
            m_Table[61] = new ImbuingDefinition("BalancedWeapon", 1072792, 150, typeof(RelicFragment), typeof(Amber), typeof(WhitePearl), 1, 0, 1112047);
            m_Table[62] = new ImbuingDefinition("SearingWeapon", 1151183, 150, null, null, null, 1, 0, -1);

            m_Table[101] = new ImbuingDefinition(SlayerName.OrcSlaying, 1060470, 100, typeof(MagicalResidue), typeof(Emerald), typeof(WhitePearl), 1, 0, 1111977);
            m_Table[102] = new ImbuingDefinition(SlayerName.TrollSlaughter, 1060480, 100, typeof(MagicalResidue), typeof(Emerald), typeof(WhitePearl), 1, 0, 1111990);
            m_Table[103] = new ImbuingDefinition(SlayerName.OgreTrashing, 1060468, 100, typeof(MagicalResidue), typeof(Emerald), typeof(WhitePearl), 1, 0, 1111975);
            m_Table[104] = new ImbuingDefinition(SlayerName.DragonSlaying, 1060462, 100, typeof(MagicalResidue), typeof(Emerald), typeof(WhitePearl), 1, 0, 1111970);
            m_Table[105] = new ImbuingDefinition(SlayerName.Terathan, 1060478, 100, typeof(MagicalResidue), typeof(Emerald), typeof(WhitePearl), 1, 0, 1111989);
            m_Table[106] = new ImbuingDefinition(SlayerName.SnakesBane, 1060475, 100, typeof(MagicalResidue), typeof(Emerald), typeof(WhitePearl), 1, 0, 1111980);
            m_Table[107] = new ImbuingDefinition(SlayerName.LizardmanSlaughter, 1060467, 100, typeof(MagicalResidue), typeof(Emerald), typeof(WhitePearl), 1, 0, 1111974);
            //m_Table[108] = new ImbuingDefinition(SlayerName.DaemonDismissal,  	 100, 	typeof(MagicalResidue), typeof(Emerald),            typeof(WhitePearl), 1, 0, 1112984);  //check
            m_Table[108] = new ImbuingDefinition(SlayerName.GargoylesFoe, 1060466, 100, typeof(MagicalResidue), typeof(Emerald), typeof(WhitePearl), 1, 0, 1111973);
            //m_Table[110] = new ImbuingDefinition(SlayerName.BalronDamnation,   	 100, 	typeof(MagicalResidue), typeof(Emerald),            typeof(WhitePearl), 1, 0, 1112001);  //check
            m_Table[111] = new ImbuingDefinition(SlayerName.Ophidian, 1060469, 100, typeof(MagicalResidue), typeof(Emerald), typeof(WhitePearl), 1, 0, 1111976);
            m_Table[112] = new ImbuingDefinition(SlayerName.SpidersDeath, 1060477, 100, typeof(MagicalResidue), typeof(Emerald), typeof(WhitePearl), 1, 0, 1111982);
            m_Table[113] = new ImbuingDefinition(SlayerName.ScorpionsBane, 1060474, 100, typeof(MagicalResidue), typeof(Emerald), typeof(WhitePearl), 1, 0, 1111979);
            m_Table[114] = new ImbuingDefinition(SlayerName.FlameDousing, 1060465, 100, typeof(MagicalResidue), typeof(Emerald), typeof(WhitePearl), 1, 0, 1111972);
            m_Table[115] = new ImbuingDefinition(SlayerName.WaterDissipation, 1060481, 100, typeof(MagicalResidue), typeof(Emerald), typeof(WhitePearl), 1, 0, 1111991);
            m_Table[116] = new ImbuingDefinition(SlayerName.Vacuum, 1060457, 100, typeof(MagicalResidue), typeof(Emerald), typeof(WhitePearl), 1, 0, 1111968);
            m_Table[117] = new ImbuingDefinition(SlayerName.ElementalHealth, 1060471, 100, typeof(MagicalResidue), typeof(Emerald), typeof(WhitePearl), 1, 0, 1111978);
            m_Table[118] = new ImbuingDefinition(SlayerName.EarthShatter, 1060463, 100, typeof(MagicalResidue), typeof(Emerald), typeof(WhitePearl), 1, 0, 1111971);
            m_Table[119] = new ImbuingDefinition(SlayerName.BloodDrinking, 1060459, 100, typeof(MagicalResidue), typeof(Emerald), typeof(WhitePearl), 1, 0, 1111969);
            m_Table[120] = new ImbuingDefinition(SlayerName.SummerWind, 1060476, 100, typeof(MagicalResidue), typeof(Emerald), typeof(WhitePearl), 1, 0, 1111981);

            //Super Slayers
            m_Table[121] = new ImbuingDefinition(SlayerName.Silver, 1060479, 130, typeof(RelicFragment), typeof(Ruby), typeof(WhitePearl), 1, 0, 1111988);
            m_Table[122] = new ImbuingDefinition(SlayerName.Repond, 1060472, 130, typeof(RelicFragment), typeof(Ruby), typeof(WhitePearl), 1, 0, 1111986);
            m_Table[123] = new ImbuingDefinition(SlayerName.ReptilianDeath, 1060473, 130, typeof(RelicFragment), typeof(Ruby), typeof(WhitePearl), 1, 0, 1111987);
            m_Table[124] = new ImbuingDefinition(SlayerName.Exorcism, 1060460, 130, typeof(RelicFragment), typeof(Ruby), typeof(WhitePearl), 1, 0, 1111984);
            m_Table[125] = new ImbuingDefinition(SlayerName.ArachnidDoom, 1060458, 130, typeof(RelicFragment), typeof(Ruby), typeof(WhitePearl), 1, 0, 1111983);
            m_Table[126] = new ImbuingDefinition(SlayerName.ElementalBan, 1060464, 130, typeof(RelicFragment), typeof(Ruby), typeof(WhitePearl), 1, 0, 1111985);
            m_Table[127] = new ImbuingDefinition(SlayerName.Fey, 1070855, 130, typeof(RelicFragment), typeof(Ruby), typeof(WhitePearl), 1, 0, 1154652);

            // Talisman Slayers
            m_Table[135] = new ImbuingDefinition(TalismanSlayerName.Bear, 1072504, 130, null, null, null, 1, 0, 0);
            m_Table[136] = new ImbuingDefinition(TalismanSlayerName.Vermin, 1072505, 130, null, null, null, 1, 0, 0);
            m_Table[137] = new ImbuingDefinition(TalismanSlayerName.Bat, 1072506, 130, null, null, null, 1, 0, 0);
            m_Table[138] = new ImbuingDefinition(TalismanSlayerName.Mage, 1072507, 130, null, null, null, 1, 0, 0);
            m_Table[139] = new ImbuingDefinition(TalismanSlayerName.Beetle, 1072508, 130, null, null, null, 1, 0, 0);
            m_Table[140] = new ImbuingDefinition(TalismanSlayerName.Bird, 1072509, 130, null, null, null, 1, 0, 0);
            m_Table[141] = new ImbuingDefinition(TalismanSlayerName.Ice, 1072510, 130, null, null, null, 1, 0, 0);
            m_Table[142] = new ImbuingDefinition(TalismanSlayerName.Flame, 1072511, 130, null, null, null, 1, 0, 0);
            m_Table[143] = new ImbuingDefinition(TalismanSlayerName.Bovine, 1072512, 130, null, null, null, 1, 0, 0);
           // m_Table[144] = new ImbuingDefinition(TalismanSlayerName.Wolf, 1075462, 130, null, null, null, 1, 0, 0);
           // m_Table[145] = new ImbuingDefinition(TalismanSlayerName.Undead, 1079752, 130, null, null, null, 1, 0, 0);
           // m_Table[146] = new ImbuingDefinition(TalismanSlayerName.Goblin, 1095010, 130, null, null, null, 1, 0, 0);

            m_Table[151] = new ImbuingDefinition(SkillName.Fencing, 1044102, 140, typeof(EnchantEssence), typeof(StarSapphire), typeof(CrystallineBlackrock), 15, 1, 1112012);
            m_Table[152] = new ImbuingDefinition(SkillName.Macing, 1044101, 140, typeof(EnchantEssence), typeof(StarSapphire), typeof(CrystallineBlackrock), 15, 1, 1112013);
            m_Table[153] = new ImbuingDefinition(SkillName.Swords, 1044100, 140, typeof(EnchantEssence), typeof(StarSapphire), typeof(CrystallineBlackrock), 15, 1, 1112016);
            m_Table[154] = new ImbuingDefinition(SkillName.Musicianship, 1044089, 140, typeof(EnchantEssence), typeof(StarSapphire), typeof(CrystallineBlackrock), 15, 1, 1112015);
            m_Table[155] = new ImbuingDefinition(SkillName.Magery, 1044085, 140, typeof(EnchantEssence), typeof(StarSapphire), typeof(CrystallineBlackrock), 15, 1, 1112014);

            m_Table[156] = new ImbuingDefinition(SkillName.Wrestling, 1044103, 140, typeof(EnchantEssence), typeof(StarSapphire), typeof(CrystallineBlackrock), 15, 1, 1112021);
            m_Table[157] = new ImbuingDefinition(SkillName.AnimalTaming, 1044095, 140, typeof(EnchantEssence), typeof(StarSapphire), typeof(CrystallineBlackrock), 15, 1, 1112017);
            m_Table[158] = new ImbuingDefinition(SkillName.SpiritSpeak, 1044092, 140, typeof(EnchantEssence), typeof(StarSapphire), typeof(CrystallineBlackrock), 15, 1, 1112019);
            m_Table[159] = new ImbuingDefinition(SkillName.Tactics, 1044087, 140, typeof(EnchantEssence), typeof(StarSapphire), typeof(CrystallineBlackrock), 15, 1, 1112020);
            m_Table[160] = new ImbuingDefinition(SkillName.Provocation, 1044082, 140, typeof(EnchantEssence), typeof(StarSapphire), typeof(CrystallineBlackrock), 15, 1, 1112018);

            m_Table[161] = new ImbuingDefinition(SkillName.Focus, 1044110, 140, typeof(EnchantEssence), typeof(StarSapphire), typeof(CrystallineBlackrock), 15, 1, 1112024);
            m_Table[162] = new ImbuingDefinition(SkillName.Parry, 1044065, 140, typeof(EnchantEssence), typeof(StarSapphire), typeof(CrystallineBlackrock), 15, 1, 1112026);
            m_Table[163] = new ImbuingDefinition(SkillName.Stealth, 1044107, 140, typeof(EnchantEssence), typeof(StarSapphire), typeof(CrystallineBlackrock), 15, 1, 1112027);
            m_Table[164] = new ImbuingDefinition(SkillName.Meditation, 1044106, 140, typeof(EnchantEssence), typeof(StarSapphire), typeof(CrystallineBlackrock), 15, 1, 1112025);
            m_Table[165] = new ImbuingDefinition(SkillName.AnimalLore, 1044062, 140, typeof(EnchantEssence), typeof(StarSapphire), typeof(CrystallineBlackrock), 15, 1, 1112022);
            m_Table[166] = new ImbuingDefinition(SkillName.Discordance, 1044075, 140, typeof(EnchantEssence), typeof(StarSapphire), typeof(CrystallineBlackrock), 15, 1, 1112023);

            m_Table[167] = new ImbuingDefinition(SkillName.Mysticism, 1044115, 140, typeof(EnchantEssence), typeof(StarSapphire), typeof(CrystallineBlackrock), 15, 1, 1115213);
            m_Table[168] = new ImbuingDefinition(SkillName.Bushido, 1044112, 140, typeof(EnchantEssence), typeof(StarSapphire), typeof(CrystallineBlackrock), 15, 1, 1112029);
            m_Table[169] = new ImbuingDefinition(SkillName.Necromancy, 1044109, 140, typeof(EnchantEssence), typeof(StarSapphire), typeof(CrystallineBlackrock), 15, 1, 1112031);
            m_Table[170] = new ImbuingDefinition(SkillName.Veterinary, 1044099, 140, typeof(EnchantEssence), typeof(StarSapphire), typeof(CrystallineBlackrock), 15, 1, 1112033);
            m_Table[171] = new ImbuingDefinition(SkillName.Stealing, 1044093, 140, typeof(EnchantEssence), typeof(StarSapphire), typeof(CrystallineBlackrock), 15, 1, 1112032);
            m_Table[172] = new ImbuingDefinition(SkillName.EvalInt, 1044076, 140, typeof(EnchantEssence), typeof(StarSapphire), typeof(CrystallineBlackrock), 15, 1, 1112030);
            m_Table[173] = new ImbuingDefinition(SkillName.Anatomy, 1044061, 140, typeof(EnchantEssence), typeof(StarSapphire), typeof(CrystallineBlackrock), 15, 1, 1112028);

            m_Table[174] = new ImbuingDefinition(SkillName.Peacemaking, 1044069, 140, typeof(EnchantEssence), typeof(StarSapphire), typeof(CrystallineBlackrock), 15, 1, 1112038);
            m_Table[175] = new ImbuingDefinition(SkillName.Ninjitsu, 1044113, 140, typeof(EnchantEssence), typeof(StarSapphire), typeof(CrystallineBlackrock), 15, 1, 1112037);
            m_Table[176] = new ImbuingDefinition(SkillName.Chivalry, 1044111, 140, typeof(EnchantEssence), typeof(StarSapphire), typeof(CrystallineBlackrock), 15, 1, 1112035);
            m_Table[177] = new ImbuingDefinition(SkillName.Archery, 1044091, 140, typeof(EnchantEssence), typeof(StarSapphire), typeof(CrystallineBlackrock), 15, 1, 1112034);
            m_Table[178] = new ImbuingDefinition(SkillName.MagicResist, 1044086, 140, typeof(EnchantEssence), typeof(StarSapphire), typeof(CrystallineBlackrock), 15, 1, 1112039);
            m_Table[179] = new ImbuingDefinition(SkillName.Healing, 1044077, 140, typeof(EnchantEssence), typeof(StarSapphire), typeof(CrystallineBlackrock), 15, 1, 1112036);
            m_Table[180] = new ImbuingDefinition(SkillName.Throwing, 1044117, 140, typeof(EnchantEssence), typeof(StarSapphire), typeof(CrystallineBlackrock), 15, 1, 1115212);

            m_Table[181] = new ImbuingDefinition(SkillName.Lumberjacking, 1002100, 140, typeof(EnchantEssence), typeof(StarSapphire), typeof(CrystallineBlackrock), 15, 1, 1002101);
            m_Table[182] = new ImbuingDefinition(SkillName.Snooping, 1002138, 140, typeof(EnchantEssence), typeof(StarSapphire), typeof(CrystallineBlackrock), 15, 1, 1002139);
            m_Table[183] = new ImbuingDefinition(SkillName.Mining, 1002111, 140, typeof(EnchantEssence), typeof(StarSapphire), typeof(CrystallineBlackrock), 15, 1, 1002112);



            m_Table[233] = new ImbuingDefinition(AosWeaponAttribute.ResistPhysicalBonus, 1061158, 100, typeof(MagicalResidue), typeof(Diamond), typeof(WhitePearl), 15, 1, 1112010);
            m_Table[234] = new ImbuingDefinition(AosWeaponAttribute.ResistFireBonus, 1061159, 100, typeof(MagicalResidue), typeof(Ruby), typeof(WhitePearl), 15, 1, 1112009);
            m_Table[235] = new ImbuingDefinition(AosWeaponAttribute.ResistColdBonus, 1061160, 100, typeof(MagicalResidue), typeof(Sapphire), typeof(WhitePearl), 15, 1, 1112007);
            m_Table[236] = new ImbuingDefinition(AosWeaponAttribute.ResistPoisonBonus, 1061161, 100, typeof(MagicalResidue), typeof(Emerald), typeof(WhitePearl), 15, 1, 1112011);
            m_Table[237] = new ImbuingDefinition(AosWeaponAttribute.ResistEnergyBonus, 1061162, 100, typeof(MagicalResidue), typeof(Amethyst), typeof(WhitePearl), 15, 1, 1112008);



            m_Table[500] = new ImbuingDefinition(AosArmorAttribute.SelfRepair, 1079709, 100, null, null, null, 5, 1, 1079709);
            m_Table[501] = new ImbuingDefinition(AosWeaponAttribute.SelfRepair, 1079709, 100, null, null, null, 5, 1, 1079709);
            //243 already used above
        }

        public static int GetAttributeName(object o)
        {
            int mod = GetMod(o);

            if (Table.ContainsKey(mod))
            {
                return m_Table[mod].AttributeName;
            }

            return 0;
        }
        public static int GetMod(object attr)
        {
            int mod = -1;

            if (attr is AosAttribute)
                mod = GetModForAttribute((AosAttribute)attr);

            else if (attr is AosWeaponAttribute)
                mod = GetModForAttribute((AosWeaponAttribute)attr);

            else if (attr is SkillName)
                mod = GetModForAttribute((SkillName)attr);

            else if (attr is SlayerName)
                mod = GetModForAttribute((SlayerName)attr);


            else if (attr is AosArmorAttribute)
                mod = GetModForAttribute((AosArmorAttribute)attr);

            else if (attr is AosElementAttribute)
                mod = GetModForAttribute((AosElementAttribute)attr);

            else if (attr is TalismanSlayerName)
                mod = GetModForAttribute((TalismanSlayerName)attr);

            else if (attr is string)
                mod = GetModForAttribute((string)attr);

            return mod;
        }
        public static int GetModForAttribute(AosAttribute attr)
        {
            foreach (KeyValuePair<int, ImbuingDefinition> kvp in m_Table)
            {
                int mod = kvp.Key;
                ImbuingDefinition def = kvp.Value;

                if (def.Attribute is AosAttribute && (AosAttribute)def.Attribute == attr)
                    return mod;
            }

            return -1;
        }
        public static int GetModForAttribute(AosWeaponAttribute attr)
        {
            foreach (KeyValuePair<int, ImbuingDefinition> kvp in m_Table)
            {
                int mod = kvp.Key;
                ImbuingDefinition def = kvp.Value;

                if (def.Attribute is AosWeaponAttribute && (AosWeaponAttribute)def.Attribute == attr)
                    return mod;
            }

            return -1;
        }

      
        public static int GetModForAttribute(AosArmorAttribute attr)
        {
            if (attr == AosArmorAttribute.LowerStatReq)
                return GetModForAttribute(AosWeaponAttribute.LowerStatReq);

            if (attr == AosArmorAttribute.DurabilityBonus)
                return GetModForAttribute(AosWeaponAttribute.DurabilityBonus);

            foreach (KeyValuePair<int, ImbuingDefinition> kvp in m_Table)
            {
                int mod = kvp.Key;
                ImbuingDefinition def = kvp.Value;

                if (def.Attribute is AosArmorAttribute && (AosArmorAttribute)def.Attribute == attr)
                    return mod;
            }

            return -1;
        }

        public static int GetModForAttribute(SkillName attr)
        {
            foreach (KeyValuePair<int, ImbuingDefinition> kvp in m_Table)
            {
                int mod = kvp.Key;
                ImbuingDefinition def = kvp.Value;

                if (def.Attribute is SkillName && (SkillName)def.Attribute == attr)
                    return mod;
            }

            return -1;
        }

        public static int GetModForAttribute(SlayerName attr)
        {
            foreach (KeyValuePair<int, ImbuingDefinition> kvp in m_Table)
            {
                int mod = kvp.Key;
                ImbuingDefinition def = kvp.Value;

                if (def.Attribute is SlayerName && (SlayerName)def.Attribute == attr)
                    return mod;
            }

            return -1;
        }

        public static int GetModForAttribute(TalismanSlayerName attr)
        {
            foreach (KeyValuePair<int, ImbuingDefinition> kvp in m_Table)
            {
                int mod = kvp.Key;
                ImbuingDefinition def = kvp.Value;

                if (def.Attribute is TalismanSlayerName && (TalismanSlayerName)def.Attribute == attr)
                    return mod;
            }

            return -1;
        }

        public static int GetModForAttribute(AosElementAttribute type)
        {
            switch (type)
            {
                case AosElementAttribute.Physical: return 51;
                case AosElementAttribute.Fire: return 52;
                case AosElementAttribute.Cold: return 53;
                case AosElementAttribute.Poison: return 54;
                case AosElementAttribute.Energy: return 55;
            }

            return -1;
        }

        public static int GetModForAttribute(string str)
        {
            if (str == "BalancedWeapon")
                return 61;

            if (str == "WeaponVelocity")
                return 60;

            if (str == "SearingWeapon")
                return 62;

            return -1;
        }
        public static AosAttributes GetAosAttributes(Item item)
        {
            if (item is BaseWeapon)
                return ((BaseWeapon)item).Attributes;

            if (item is BaseArmor)
                return ((BaseArmor)item).Attributes;

            if (item is BaseClothing)
                return ((BaseClothing)item).Attributes;

            if (item is BaseJewel)
                return ((BaseJewel)item).Attributes;

            if (item is BaseTalisman)
                return ((BaseTalisman)item).Attributes;

            if (item is BaseQuiver)
                return ((BaseQuiver)item).Attributes;

            return null;
        }

        public static AosArmorAttributes GetAosArmorAttributes(Item item)
        {
            if (item is BaseArmor)
                return ((BaseArmor)item).ArmorAttributes;

            if (item is BaseClothing)
                return ((BaseClothing)item).ClothingAttributes;

            return null;
        }

        public static AosWeaponAttributes GetAosWeaponAttributes(Item item)
        {
            if (item is BaseWeapon)
                return ((BaseWeapon)item).WeaponAttributes;

         
            return null;
        }

        public static AosElementAttributes GetElementalAttributes(Item item)
        {
            if (item is BaseClothing)
                return ((BaseClothing)item).Resistances;

            else if (item is BaseJewel)
                return ((BaseJewel)item).Resistances;

            else if (item is BaseWeapon)
                return ((BaseWeapon)item).AosElementDamages;

        

            return null;
        }

     

        public static AosSkillBonuses GetAosSkillBonuses(Item item)
        {
            if (item is BaseJewel)
                return ((BaseJewel)item).SkillBonuses;

            else if (item is BaseWeapon)
                return ((BaseWeapon)item).SkillBonuses;

            else if (item is BaseArmor)
                return ((BaseArmor)item).SkillBonuses;

            else if (item is BaseTalisman)
                return ((BaseTalisman)item).SkillBonuses;

            else if (item is Spellbook)
                return ((Spellbook)item).SkillBonuses;

          

            else if (item is BaseClothing)
                return ((BaseClothing)item).SkillBonuses;

            return null;
        }

     
        private static int ScaleAttribute(object o)
        {
            if (o is AosAttribute)
            {
                AosAttribute attr = (AosAttribute)o;

                if (attr == AosAttribute.Luck)
                    return 10;

                if (attr == AosAttribute.WeaponSpeed)
                    return 5;
            }
            else if (o is AosArmorAttribute)
            {
                AosArmorAttribute attr = (AosArmorAttribute)o;

                if (attr == AosArmorAttribute.LowerStatReq)
                    return 10;

                if (attr == AosArmorAttribute.DurabilityBonus)
                    return 10;
            }
            else if (o is AosWeaponAttribute)
            {
                AosWeaponAttribute attr = (AosWeaponAttribute)o;

                if (attr == AosWeaponAttribute.LowerStatReq)
                    return 10;

                if (attr == AosWeaponAttribute.DurabilityBonus)
                    return 10;

            }
            else if (o is SkillName)
                return 5;

            return 1;
        }
    }

    internal class CrystallineBlackrock
    {
    }

    internal class MagicalResidue
    {
    }

    internal class EnchantEssence
    {
    }

    internal class RelicFragment
    {
    }

    public class ImbuingDefinition
    {
        private object m_Attribute;
        private int m_AttributeName;
        private int m_Weight;
        private Type m_PrimaryRes;
        private Type m_GemRes;
        private Type m_SpecialRes;
        private int m_PrimaryName;
        private int m_GemName;
        private int m_SpecialName;
        private int m_MaxIntensity;
        private int m_IncAmount;
        private int m_Description;

        public object Attribute { get { return m_Attribute; } }
        public int AttributeName { get { return m_AttributeName; } }
        public int Weight { get { return m_Weight; } }
        public Type PrimaryRes { get { return m_PrimaryRes; } }
        public Type GemRes { get { return m_GemRes; } }
        public Type SpecialRes { get { return m_SpecialRes; } }
        public int PrimaryName { get { return m_PrimaryName; } }
        public int GemName { get { return m_GemName; } }
        public int SpecialName { get { return m_SpecialName; } }
        public int MaxIntensity { get { return m_MaxIntensity; } }
        public int IncAmount { get { return m_IncAmount; } }
        public int Description { get { return m_Description; } }

        public ImbuingDefinition(object attribute, int attributeName, int weight, Type pRes, Type gRes, Type spRes, int mInt, int inc, int desc)
        {
            m_Attribute = attribute;
            m_AttributeName = attributeName;
            m_Weight = weight;
            m_PrimaryRes = pRes;
            m_GemRes = gRes;
            m_SpecialRes = spRes;

            m_PrimaryName = GetLocalization(pRes);
            m_GemName = GetLocalization(gRes);
            m_SpecialName = GetLocalization(spRes);

            m_MaxIntensity = mInt;
            m_IncAmount = inc;
            m_Description = desc;
        }

        public int GetLocalization(Type type)
        {
            if (type == null)
                return 0;

            if (type == typeof(Tourmaline)) return 1023864;
            if (type == typeof(Ruby)) return 1023859;
            if (type == typeof(Diamond)) return 1023878;
            if (type == typeof(Sapphire)) return 1023857;
            if (type == typeof(Citrine)) return 1023861;
            if (type == typeof(Emerald)) return 1023856;
            if (type == typeof(StarSapphire)) return 1023855;
            if (type == typeof(Amethyst)) return 1023862;

           // if (type == typeof(RelicFragment)) return 1031699;
           // if (type == typeof(EnchantEssence)) return 1031698;
           // if (type == typeof(MagicalResidue)) return 1031697;

            if (type == typeof(DarkSapphire)) return 1032690;
            if (type == typeof(Turquoise)) return 1032691;
            if (type == typeof(PerfectEmerald)) return 1032692;
            if (type == typeof(EcruCitrine)) return 1032693;
            if (type == typeof(WhitePearl)) return 1032694;
            if (type == typeof(FireRuby)) return 1032695;
            if (type == typeof(BlueDiamond)) return 1032696;
            if (type == typeof(BrilliantAmber)) return 1032697;

            if (type == typeof(ParasiticPlant)) return 1032688;
            if (type == typeof(LuminescentFungi)) return 1032689;

            if (LocBuffer == null)
                LocBuffer = new Dictionary<Type, int>();

            if (LocBuffer.ContainsKey(type))
                return LocBuffer[type];

            Item item = Loot.Construct(type);

            if (item != null)
            {
                LocBuffer[type] = item.LabelNumber;
                item.Delete();

                return LocBuffer[type]; ;
            }

            if (type != null)
                Console.WriteLine("Warning, missing name cliloc for type {0}.", type.Name);
            return -1;
        }

        public Dictionary<Type, int> LocBuffer { get; set; }
    }
}
