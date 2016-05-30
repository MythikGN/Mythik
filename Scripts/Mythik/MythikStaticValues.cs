using Server;
using Server.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scripts.Mythik
{
    class MythikStaticValues
    {
        public static Point3D NeutralZone = new Point3D(10, 10, 0);

        public static int[] RareClothHues = new int[] { 0x79, 0x792, 0x793, 0x794, 0x795, 0x796, 0x797, 0x798, 0x799, 0x79a, 0x79b, 0x79c, 0x79d, 0x79e, 0x7a0, 0x7a, 0x7a2, 0x7a3, 0x7a4, 0x7a5, 0x7a6, 0x7a7, 0x7a8, 0x7a9, 0x7aa, 0x7ab, 0x7ac, 0x7ad, 0x7ae, 0x7af, 0x7b0, 0x7b, 0x7b2, 0x7b3, 0x7b4, 0x7b5, 0x7b6, 0x7b7, 0x7b8, 0x7b9, 0x7ba, 0x7bb, 0x7bc, 0x7bd, 0x7be, 0x7bf, 0x7c0, 0x7c, 0x7c2, 0x7c3, 0x7c4, 0x7c5, 0x7c6, 0x7c7, 0x7c8, 0x7c9, 0x7ca, 0x7cb, 0x7cc, 0x7cd, 0x7ce, 0x7e3, 0x7e4, 0x7e5, 0x7e6, 0x7e7, 0x7e8, 0x7e9, 0x7ea, 0x7eb, 0x7ec, 0x7ed, 0x7ee, 0x7ef, 0x7f0, 0x7f, 0x7f2, 0x7f3, 0x7f4, 0x7f5, 0x7f6, 0x7f7, 0x7f8, 0x7f9, 0x7fa, 0x7fb, 0x7fc, 0x7fd, 0x7fe, 0x7ff, 0x800, 0x80, 0x802, 0x803, 0x804, 0x805, 0x806, 0x807, 0x808, 0x809, 0x80a, 0x80b, 0x80c, 0x80d, 0x80e, 0x80f, 0x810, 0x81, 0x812, 0x813, 0x814, 0x815, 0x816, 0x817, 0x818, 0x819, 0x81a, 0x81b, 0x81c, 0x81d, 0x81e, 0x81f, 0x820, 0x82, 0x822, 0x823, 0x824, 0x825, 0x826 ,0x827 ,0x828 ,0x829 ,0x82a ,0x82b ,0x82c ,0x82d ,0x82e ,0x82f ,0x830 ,0x83,0x832 ,0x833 ,0x834 ,0xb9d ,0xb9e ,0xb9f ,0xba0 ,0xba,0xba2 ,0xba3 ,0xba4 ,0xba5 ,0xba6 ,0xba7 ,0xba8 ,0x480 ,0x48,0x482 ,0x484 ,0x485 ,0x486 ,0x487 ,0x488 ,0x489 ,0x48a ,0x48b ,0x48c ,0x48d ,0x48e ,0x48f ,0x490 ,0x49,0x492 ,0x493 ,0x775 ,0x776 ,0x777 ,0x778 ,0x779 ,0x77a ,0x77b ,0x77c ,0x77d ,0x77e ,0x77f ,0x780 ,0x78,0x782 ,0x783 ,0x784 ,0x785 ,0x786 ,0x787 ,0x789 ,0x78a ,0x78b ,0x78c ,0x78d ,0x78e ,0x78f ,0x790 ,0x806 ,0xb9b ,0x82e ,0x865 ,0x80f ,0x7c6 ,0x7c5 ,0x7b8 ,0x81,0x786 ,0x77b ,0x7cc ,0xba,0xba,0xb9b ,0xb9e ,0x84d ,0x7be ,0x833 ,0x7b8 ,0x804 ,0x78c ,0x783 ,0x7b2 ,0x777 ,0x77e ,0xba8 ,0xba4 ,0xbb7 ,};
    }

    internal static class SphereUtils
    {
        
        public static string LocToString(int index)
        {

            //scan through the cliloc's for the correct entry
            return CliLoc.LocToString(index);
           
        }

        //the special case, where there are arguments to insert in the string
        public static string LocToString(int index, string args)
        {
            if (string.IsNullOrEmpty(args))
            {
                return LocToString(index);
            }

            string basestring = LocToString(index);

            //parse the string for any argument identifiers
            while (basestring != null && basestring.IndexOf("~") > -1)
            {
                //this determines the string that needs replacing
                string replacestring = FindReplace("~", basestring);

                int argsdivider = args.IndexOf("\t");

                //here's the string that will replace it
                string argstring = argsdivider == -1 ? args : args.Substring(0, argsdivider);

                //rethreaded
                if (argstring.IndexOf("#") == 0)
                {
                    int recurseloc = Convert.ToInt32(argstring.Substring(1, argstring.Length - 1));

                    argstring = LocToString(recurseloc);
                }

                basestring = basestring.Replace(replacestring, argstring);


                if (argsdivider > -1)
                    args = args.Substring(argsdivider + 1, args.Length - (argsdivider) - 1);
            }

            return basestring;

        }

        //determine the maximum string length in the collection of strings.  Useful when determining gump size, for example
        public static int GetMaxLength(List<string> strings)
        {
            if (strings == null)
            {
                return 0;
            }

            int maxlength = 0;

            foreach (string str in strings)
            {
                maxlength = Math.Max(maxlength, str.Length);
            }

            return maxlength;
        }

        //useful find and replace method when inserting arguments in a localized string
        private static string FindReplace(string key, string fromstring)
        {
            string replacestring = "";

            if (fromstring.IndexOf(key) > -1)
            {
                replacestring += key;

                //chop off up to and including the first key
                fromstring = fromstring.Substring(fromstring.IndexOf(key) + 1, fromstring.Length - fromstring.IndexOf(key) - 1);

                //grab up to the second key
                replacestring += fromstring.Substring(0, fromstring.IndexOf(key) + 1);
            }

            return replacestring;
        }



        public static string ComputeName(Item i)
        {
            if (i is BaseWeapon)
                return ComputeName((i as BaseWeapon));
            if (i is BaseArmor)
                return ComputeName((i as BaseArmor));
            if (i is BaseClothing)
                return ComputeName((i as BaseClothing));
            if (i is BaseInstrument)
                return ComputeName((i as BaseInstrument));
            if (i is SpellScroll)
                return ComputeName((i as SpellScroll));
            if (i is RepairDeed)
                return ComputeName((i as RepairDeed));
            if (i is BaseIngot)
                return ComputeName(i as BaseIngot);
            if (i is BaseIngot)
                return ComputeName(i as BaseOre);
            return GenericComputeName(i);
        }
        public static string ComputeName(BaseIngot ba)
        {
            if (!string.IsNullOrEmpty(ba.Name))
                return ba.Name;
            string name = "ingot";
            if (ba.Amount > 1)
                name = name + "s";
            var resource = string.Empty;
            if (ba.Resource != CraftResource.None)
            {
                resource = CraftResources.GetName(ba.Resource);
                name = string.Format("{0} {1} {2}",ba.Amount, resource, name.ToLower());
            }

            return name;
        }
        public static string ComputeName(BaseOre ba)
        {
            if (!string.IsNullOrEmpty(ba.Name))
                return ba.Name;
            string name = "ore";
            if (ba.Amount > 1)
                name = name + "s";
            var resource = string.Empty;
            if (ba.Resource != CraftResource.None)
            {
                resource = CraftResources.GetName(ba.Resource);
                name = string.Format("{0} {1} {2}", ba.Amount, resource, name.ToLower());
            }
               
            return name;
        }
        public static string ComputeName(BaseArmor ba)
        {
            if (!string.IsNullOrEmpty(ba.Name))
                return ba.Name;
            string name;
            if (ba.Name == null)
                name = LocToString(ba.LabelNumber);
            else
                name = ba.Name;
            if (ba.Amount > 1)
                name = name + "s";
            var resource = string.Empty;
            if (ba.Resource != CraftResource.None && ba.Resource != CraftResource.Iron)
                resource = CraftResources.GetName(ba.Resource);
            if ((ba.ProtectionLevel != ArmorProtectionLevel.Regular))// && ba.Resource == CraftResource.Iron )
            //If the armor is magical
            {
                if (ba.Quality == ArmorQuality.Exceptional)
                    name = string.Format("{0} {1} of {2}", "Exceptional", name.ToLower(), LocToString((1038005 + (int)ba.ProtectionLevel)).ToLower());
                else
                    name = string.Format("{0} of {1}", name, LocToString((1038005 + (int)ba.ProtectionLevel)).ToLower());
            }
            else if (ba.Resource == CraftResource.None && ba.ProtectionLevel == ArmorProtectionLevel.Regular)
            //If the armor is not magical and not crafted
            {
                if (ba.Quality == ArmorQuality.Exceptional)
                    name = string.Format("{0} {1}", "Exceptional", name);
            }
            else if (ba.Resource != CraftResource.None)
            {
                //If it's crafted by a player
                if (ba.Crafter != null)
                {
                    if (ba.Quality == ArmorQuality.Exceptional)
                    {
                        if (ba.Resource != CraftResource.Iron)
                            name = string.Format("{0} {1} {2} crafted by {3}", "Exceptional", resource.ToLower(), name.ToLower(), ba.Crafter.Name);
                        else
                            name = string.Format("{0} {1} crafted by {2}", "Exceptional", name.ToLower(), ba.Crafter.Name);
                    }
                    else if (ba.Resource != CraftResource.Iron)
                        name = string.Format("{0} {1}", resource, name.ToLower());
                    else
                        name = string.Format("{0}", name);
                }
                else
                    if (ba.Quality == ArmorQuality.Exceptional)
                    if (!string.IsNullOrEmpty(resource))
                        name = string.Format("{0} {1} {2}", "Exceptional", resource.ToLower(), name.ToLower());
                    else
                        name = string.Format("{0} {1}", "Exceptional", name.ToLower());
                else
                        if (!string.IsNullOrEmpty(resource))
                    name = string.Format("{0} {1}", resource, name.ToLower());
                else
                    name = string.Format(name);
            }

            if (ba.Amount > 1)
                name = ba.Amount + " " + name;

            return name;
        }

        public static string ComputeCustomWeaponName(BaseWeapon bw)
        {
            string name = bw.Name;

            if (bw.Crafter == null)
            {
                if (bw.Quality == WeaponQuality.Exceptional)
                    name = "Exceptional " + bw.Name.ToLower();
            }
            else
            {
                if (bw.Quality == WeaponQuality.Exceptional)
                    name = string.Format("Exceptional {0} crafted by {1} ", bw.Name.ToLower(), bw.Crafter.Name);
                else
                    name = string.Format("{0} crafted by {1}", bw.Name, bw.Crafter.Name);
            }

            return name;
        }

        public static string ComputeName(BaseWeapon bw)
        {
            if (!string.IsNullOrEmpty(bw.Name))
                return bw.Name;

            string name;

            if (bw.Name == null)
                name = LocToString(bw.LabelNumber);
            else
                name = bw.Name;

            if (bw.Amount > 1)
                name = name + "s";

            var resource = string.Empty;

            if (bw.Slayer != SlayerName.None)
            {
                SlayerEntry entry = SlayerGroup.GetEntryByName(bw.Slayer);
                if (entry != null)
                {
                    string slayername = LocToString(entry.Title);
                    name = slayername + " " + name.ToLower();
                }
            }

            if (bw.Resource != CraftResource.None && bw.Resource != CraftResource.Iron)
                resource = CraftResources.GetName(bw.Resource);

            if ((bw.DamageLevel != WeaponDamageLevel.Regular || bw.AccuracyLevel != WeaponAccuracyLevel.Regular) && bw.Resource == CraftResource.Iron)
            {
                //If the weapon is accurate or magical
                if (bw.DamageLevel != WeaponDamageLevel.Regular && bw.AccuracyLevel != WeaponAccuracyLevel.Regular)
                    name = string.Format("{0} {1} of {2}", ComputeAccuracyLevel(bw), name.ToLower(), LocToString((1038015 + (int)bw.DamageLevel)).ToLower());
                else if (bw.AccuracyLevel != WeaponAccuracyLevel.Regular)
                    name = string.Format("{0} {1}", ComputeAccuracyLevel(bw), name.ToLower());
                else
                    name = string.Format("{0} of {1}", name, LocToString((1038015 + (int)bw.DamageLevel)).ToLower());

                if (bw.Quality == WeaponQuality.Exceptional)
                    name = "Exceptional " + name.ToLower();
            }
            else if (bw.Resource != CraftResource.None)
            {
                //If it's crafted by a player
                if (bw.Crafter != null)
                    if (bw.Quality == WeaponQuality.Exceptional)
                        if (bw.Resource != CraftResource.Iron)
                            name = string.Format("{0} {1} {2} crafted by {3}", "Exceptional", resource.ToLower(), name.ToLower(), bw.Crafter.Name);
                        else
                            name = string.Format("{0} {1} crafted by {2}", "Exceptional", name.ToLower(), bw.Crafter.Name);
                    else if (bw.Resource != CraftResource.Iron)
                        if (!string.IsNullOrEmpty(resource))
                            name = string.Format("{0} {1} crafted by {2}", resource, name.ToLower(), bw.Crafter.Name);
                        else
                            name = string.Format("{0} crafted by {1}", name, bw.Crafter.Name);
                    else
                        name = string.Format("{0} crafted by {1}", name, bw.Crafter.Name);
                else if (bw.Resource != CraftResource.Iron)
                    if (bw.Quality == WeaponQuality.Exceptional)
                        if (!string.IsNullOrEmpty(resource))
                            name = string.Format("{0} {1} {2}", "Exceptional", resource.ToLower(), name.ToLower());
                        else
                            name = string.Format("{0}, {1}", "Exceptional", name.ToLower());
                    else if (!string.IsNullOrEmpty(resource))
                        name = string.Format("{0} {1}", resource, name.ToLower());
                    else
                        name = string.Format(name);
                else if (bw.Resource == CraftResource.Iron)
                    if (bw.Quality == WeaponQuality.Exceptional)
                        name = string.Format("{0} {1}", "Exceptional", name.ToLower());
            }
            if (bw.Amount > 1)
                name = bw.Amount + " " + name;

            return name;
        }

        public static string ComputeName(BaseClothing bc)
        {
            if (!string.IsNullOrEmpty(bc.Name))
                return bc.Name;

            string name;

            if (bc.Name == null)
                name = LocToString(bc.LabelNumber);
            else
                name = bc.Name;

            if (bc.Amount > 1)
                name = name + "s";

            var resource = string.Empty;

            if (bc.Resource != CraftResource.None && bc.Resource != CraftResource.Iron)
                resource = CraftResources.GetName(bc.Resource);

            if (bc.Crafter != null)
                if (bc.Quality == ClothingQuality.Exceptional)
                    if (bc.Resource != CraftResource.None)
                        name = string.Format("{0} {1} {2} crafted by {3}", "Exceptional", resource.ToLower(), name.ToLower(), bc.Crafter.Name);
                    else
                        name = string.Format("{0} {1} crafted by {2}", "Exceptional", name.ToLower(), bc.Crafter.Name);
                else if (bc.Resource != CraftResource.None)
                    if (!string.IsNullOrEmpty(resource))
                        name = string.Format("{0} {1} crafted by {2}", resource, name.ToLower(), bc.Crafter.Name);
                    else
                        name = string.Format("{0} crafted by {1}", name, bc.Crafter.Name);
                else
                    name = string.Format("{0} crafted by {1}", name.ToLower(), bc.Crafter.Name);
            else if (bc.Resource != CraftResource.None)
                if (bc.Quality == ClothingQuality.Exceptional)
                    if (!string.IsNullOrEmpty(resource))
                        name = string.Format("{0} {1} {2}", "Exceptional", resource.ToLower(), name.ToLower());
                    else
                        name = string.Format(" {0} {1}", "Exceptional", name.ToLower());
                else
                    if (!string.IsNullOrEmpty(resource))
                    name = string.Format("{0} {1}", resource, name.ToLower());
                else
                    name = string.Format(name);
            else if (bc.Resource == CraftResource.None)
                if (bc.Quality == ClothingQuality.Exceptional)
                    name = string.Format("{0} {1}", "Exceptional", name.ToLower());

            if (bc.Amount > 1)
                name = bc.Amount + " " + name;

            return name;
        }

        public static string ComputeName(BaseInstrument bi)
        {
            string name;

            if (bi.Name == null)
                name = LocToString(bi.LabelNumber);
            else
                name = bi.Name;

            if (bi.Crafter != null)
                name = string.Format("{0} crafted by {1}", name, bi.Crafter.Name);

            return name;
        }

        public static string ComputeName(SpellScroll ss)
        {
            string name = string.IsNullOrEmpty(ss.Name) ? LocToString(ss.LabelNumber) : ss.Name;

            return (name + " scroll");
        }

        public static string ComputeName(RepairDeed rd)
        {
            string name = string.Format("Repair service contract from {0} {1} crafted by {2}",
                                        LocToString(RepairDeed.GetSkillTitle(rd.SkillLevel)).ToLower(),
                                        rd.RepairSkill.ToString().ToLower(),
                                        rd.Crafter != null ? rd.Crafter.Name : "unknown");

            return name;
        }

        public static string GenericComputeName(Item i)
        {
            string name;

            if (i.Name == null)
                name = LocToString(i.LabelNumber);
            else
                name = i.Name;

            return name;
        }
        public static string ComputeAccuracyLevel(BaseWeapon bw)
        {
            string level;
            switch (bw.AccuracyLevel)
            {
                case WeaponAccuracyLevel.Accurate:
                    level = "Accurate";
                    break;

                case WeaponAccuracyLevel.Surpassingly:
                    level = "Surpassingly accurate";
                    break;

                case WeaponAccuracyLevel.Eminently:
                    level = "Eminently accurate";
                    break;

                case WeaponAccuracyLevel.Exceedingly:
                    level = "Exceedingly accurate";
                    break;

                case WeaponAccuracyLevel.Supremely:
                    level = "Supremely accurate";
                    break;

                default:
                    level = "";
                    break;
            }
            return level;
        }
    }
}
