using System;
using System.Collections.Generic;
using System.IO;
using Ultima;
namespace Server.Mythik
{
    public class BodyConverter
    {
        private static Dictionary<int, Tuple<int, int>> m_BodyTable = new Dictionary<int, Tuple<int, int>>();
        static BodyConverter()
        {
            Initialize();
        }

        /// <summary>
        ///     Fills bodyconv.def Tables
        /// </summary>
        public static void Initialize()
        {
            string path = Path.Combine(Core.BaseDirectory,"Data/body.def");
            if (path == null)
                return;
            using (var ip = new StreamReader(path))
            {
                string line;

                while ((line = ip.ReadLine()) != null)
                {
                    if ((line = line.Trim()).Length == 0 || line.StartsWith("#"))
                    {
                        continue;
                    }

                    try
                    {
                        string[] split = line.Split(' ');

                        int original = System.Convert.ToInt32(split[0]);
                        int newBody = System.Convert.ToInt32(split[1].Trim(new char[] {'{','}' }));
                        int hue = System.Convert.ToInt32(split[2]);
                        m_BodyTable.Add(original, new Tuple<int, int>(newBody, hue));
                    }
                    catch { }
                }
            }

        }
        internal static Body ConvertBodyToBase(int v)
        {
            if (m_BodyTable.ContainsKey(v))
            {
                //Console.WriteLine("Converting: " + v + " to: " + m_BodyTable[v].Item1);
                return m_BodyTable[v].Item1;
            }
            return v;
        }

        internal static int GetBaseBodyHue(int v)
        {
            if (m_BodyTable.ContainsKey(v))
                return m_BodyTable[v].Item2;
            return 0;
        }


        internal static short ConvertBody203(Body body, Mobile mobile = null)
        {
            if (body.BodyID >= 302 && body.BodyID < 319)
                return (short)(body.BodyID -197);
            if (body.BodyID == 319) // maggots
                return 100;
            if (body.BodyID == 300) // aos ele
                return 98;
            if (body.BodyID == 301) // aos tree guy
                return 99;
            if (body.BodyID == 132)//kirin
                return 399;
            //if (body.BodyID == 122)//unicorn
            //    return 689;
            if (body.BodyID == 0x317)//beetle
                return 685;
            if (body.BodyID == 0x31A)//swamp
                return 397;
            if (body.BodyID == 0x31F)//arm swampie
                return 398;
            if (body.BodyID == 187)//ridge
                return 395;
            if (body.BodyID == 188)//sav ridge
                return 396;

            return (short)body.BodyID;

            switch (body.BodyID)
            {
                case 132: // kirin
                    return 11;
                case 0x7A:// unicorn
                    return 222;
                case 0x317: // beetle
                    return 224;
                case 0x31A://swampdrag
                    return 227;
                case 318://darkfather aka demonknight
                    return 19;
                case 243://hiryu
                    return 229;
                case 246: // bake
                    return 230;
                case 244: //runebeetle
                    return 20;
                case 46: //aw
                    return 23;
                case 173://spider champ meph
                    return 25;
                case 149://succubus
                    return 27;
                case 40://balron
                    return 32;
                case 308://bone daemon
                    return 34;
                case 303://devourer
                    return 37;
                case 311: //shadowknight
                    return 38;
                case 312://abyss horror
                    return 40;
                case 316: //wanderer of void
                    return 43;
                case 104://skele drag
                    return 46;
                case 315://flesh renderer
                    return 49;

                case 304: //flesh golem
                    return 62;
                case 305: //gorefiend
                    return 63;
                case 306: //  impaler
                    return 64;
                case 307: //gibberling
                    return 65;
                case 309: //patch skele
                    return 66;
                case 310: // wailing banshee
                    return 67;
                case 313: // darknight creeper
                    return 68;
                case 314://ravager
                    return 69;
                case 317: //vampbat
                    return 73;
                case 319://maggots
                    return 74;
                case 157://spider
                    return 77;
                case 11: //Spider
                    return 78;
                case 20://spider
                    return 79;
                case 28://spider
                    return 82;
                case 101://centaur
                    return 83;
                case 102://new demon?
                    return 9;//old demon
                case 43: //icedemon
                    return 84;
                case 62://wyrven
                    return 88;
                case 74://imp
                    return 46;
                case 26://shade
                    return 89;
                case 76://titan?
                    return 90;
                case 764://juka
                    return 91;
                case 765:
                    return 92;
                case 766:
                    return 93;
                case 767://???
                    return 94;
                case 769://??
                    return 96;
                case 770://meer lady
                    return 97;
                case 771://meer guy
                    return 98;
                case 772://meer boss
                    return 99;
                case 773: //meer archer
                    return 100;
                case 776://sml hrde dnm
                    return 101;
                case 752://golem
                    return 102;
                case 781://solen worker
                    return 105;
                case 782://warrior
                    return 106;
                case 783://queen
                    return 107;
                case 784:
                    return 108;
                case 67: // garg
                    return 109;
                case 753:
                    return 110;
                case 754:
                    return 111;
                case 755:
                    return 112;
                case 758:
                    return 113;
                default:
                    return (short)body.BodyID;

            }
        }
    }
}
