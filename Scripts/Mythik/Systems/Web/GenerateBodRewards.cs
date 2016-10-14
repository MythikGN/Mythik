using Newtonsoft.Json;
using Server;
using Server.Engines.BulkOrders;
using Server.Engines.Craft;
using Server.Items;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scripts.Mythik.Systems.Web
{
    class GenerateBodRewards
    {
        public static void Initialize()
        {
           // GenerateBods(SmithRewardCalculator.Instance,"smithbods.json");
           // GenerateBods(TailorRewardCalculator.Instance, "tailorbods.json");
        }


        private static void GenerateBods(RewardCalculator calc,string file)
        {
            var stw = new StreamWriter("web/" + file);
            using (JsonWriter writer = new JsonTextWriter(stw))
            {
                writer.Formatting = Formatting.Indented;
                //writer.WritePropertyName("items");
                writer.WriteStartArray();
                if(calc is SmithRewardCalculator)
                {
                    SmallBOD sbod = new SmallSmithBOD();

                    sbod.Type = typeof(PlateArms);

                    GenerateItemRange(sbod, calc, writer);
                    sbod.Type = typeof(Dagger);
                    GenerateItemRange(sbod, calc, writer);

                    GenerateLBod(writer, calc, LargeBulkEntry.LargeRing);
                    GenerateLBod(writer, calc, LargeBulkEntry.LargeChain);
                    GenerateLBod(writer, calc, LargeBulkEntry.LargePlate);
                }
                else
                {
                    var sbod = new SmallTailorBOD();
                    sbod.Type = typeof(TricorneHat);
                    GenerateItemRange(sbod, calc, writer);
                    sbod.Type = typeof(LeatherArms);
                    GenerateItemRange(sbod, calc, writer);

                    GenerateLBod(writer, calc, LargeBulkEntry.LargeRing);
                    GenerateLBod(writer, calc, LargeBulkEntry.LargeChain);
                    GenerateLBod(writer, calc, LargeBulkEntry.LargePlate);
                }

            }


        }

        private static void GenerateLBod(JsonWriter writer, RewardCalculator calc, SmallBulkEntry[] bods)
        {
            throw new NotImplementedException();
        }

        private static void GenerateItemRange(SmallBOD bod, RewardCalculator calc, JsonWriter writer)
        {
            for (int qty = 10; qty <= 20; qty += 5)
            {
                for (int res = 0; res <= (int)BulkMaterialType.Valorite; res++)
                {
                    bod.AmountMax = qty;
                    bod.Material = (BulkMaterialType)res;

                    bod.RequireExceptional = false;
                    GenerateItem(bod, calc, writer);

                    bod.RequireExceptional = true;
                    GenerateItem(bod, calc, writer);
                    //GenerateItem(calc, qty, res, false, writer);
                }
            }
        }

        private static void GenerateItem(SmallBOD bod, RewardCalculator calc, JsonWriter writer)
        {
            var items = bod.ComputeRewards(true);
            writer.WriteStartObject();

            writer.WritePropertyName("points");
            writer.WriteValue(calc.ComputePoints(bod).ToString());
            writer.WritePropertyName("material");
            if(bod.Material == BulkMaterialType.None)
                writer.WriteValue("Iron");
            else
                writer.WriteValue(bod.Material.ToString());
            writer.WritePropertyName("materialindex");
            writer.WriteValue((int)bod.Material + "");
            writer.WritePropertyName("qty");
            writer.WriteValue(bod.AmountMax.ToString());

            writer.WritePropertyName("bodtype");
            if(bod.Type.IsSubclassOf(typeof(BaseArmor)))
                writer.WriteValue("Armor");
            if (bod.Type.IsSubclassOf(typeof(BaseWeapon)))
                writer.WriteValue("Weapon");
            writer.WritePropertyName("excep");
            writer.WriteValue(bod.RequireExceptional);
            writer.WritePropertyName("rewards");
            writer.WriteStartArray();
            foreach(var r in items)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("Name");
                if(r is SpecialScroll)
                {
                    var pscroll = r as SpecialScroll;
                    var name = string.Format("a power scroll of {0} ({1} Skill)", pscroll.GetName(), pscroll.Value);
                    writer.WriteValue(name);
                }
                else
                {
                    if (r.LabelNumber == 0)
                        writer.WriteValue(r.Name);
                    else
                        writer.WriteValue(CliLoc.LocToString(r.LabelNumber));
                }
                    

                writer.WritePropertyName("type");
                writer.WriteValue(CraftItem.ItemIDOf(r.GetType()));

                writer.WriteEndObject();
            }
            writer.WriteEndArray();

            writer.WriteEndObject();
           // var points = calc.ComputePoints(qty, v, (BulkMaterialType)res, 1, null);
          //  var reward = calc.LookupRewards(points);
            
           // var gold = calc.ComputeGold(qty, v, (BulkMaterialType)res, 1, null);

        }
    }
}
