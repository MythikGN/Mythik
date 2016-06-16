using Newtonsoft.Json;
using Server;
using Server.Commands;
using Server.Engines.Craft;
using Server.Items;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Scripts.Mythik.Web
{
    public class GenerateCraftTables
    {
        public static void Initialize()
        {
            CommandSystem.Register("gencrafttables", AccessLevel.Player, new CommandEventHandler(GenTables));
            GenerateTable(DefAlchemy.CraftSystem);
            GenerateTable(DefBlacksmithy.CraftSystem);
            GenerateTable(DefBowFletching.CraftSystem);
            GenerateTable(DefCarpentry.CraftSystem);
            GenerateTable(DefCartography.CraftSystem);
            GenerateTable(DefCooking.CraftSystem);
            GenerateTable(DefTinkering.CraftSystem);
            GenerateTable(DefInscription.CraftSystem);
            GenerateTable(DefTailoring.CraftSystem);
        }

        private static void GenTables(CommandEventArgs e)
        {
            GenerateTable(DefAlchemy.CraftSystem);
            GenerateTable(DefBlacksmithy.CraftSystem);
            GenerateTable(DefBowFletching.CraftSystem);
            GenerateTable(DefCarpentry.CraftSystem);
            GenerateTable(DefCartography.CraftSystem);
            GenerateTable(DefCooking.CraftSystem);
            GenerateTable(DefTinkering.CraftSystem);
            GenerateTable(DefInscription.CraftSystem);
            GenerateTable(DefTailoring.CraftSystem);
        }

        private static void GenerateTable(CraftSystem system)
        {
            var stw = new StreamWriter("web/" + system.MainSkill.ToString() + ".json");
            using (JsonWriter writer = new JsonTextWriter(stw))
            {
                writer.Formatting = Formatting.Indented;
                //writer.WritePropertyName("items");
                writer.WriteStartArray();
                foreach (CraftItem item in system.CraftItems.GetList())
                {
                    writer.WriteStartObject();
                    writer.WritePropertyName("Name");
                    if (item.NameNumber == 0)
                        writer.WriteValue(item.NameString);
                    else
                        writer.WriteValue(CliLoc.LocToString(item.NameNumber));

                    writer.WritePropertyName("hue");
                    writer.WriteValue(item.ItemHue);
                    writer.WritePropertyName("type");
                    writer.WriteValue(CraftItem.ItemIDOf(item.ItemType));
                    writer.WritePropertyName("skills");
                    writer.WriteStartArray();
                    foreach(CraftSkill skill in item.Skills)
                    {
                        writer.WriteStartObject();
                        writer.WritePropertyName("skill");
                        writer.WriteValue(skill.SkillToMake.ToString());
                        writer.WritePropertyName("minskill");
                        writer.WriteValue(skill.MinSkill);
                        writer.WritePropertyName("maxskill");
                        writer.WriteValue(skill.MaxSkill);
                        writer.WriteEndObject();
                    }
                    writer.WriteEndArray();
                    writer.WritePropertyName("resources");
                    writer.WriteStartArray();
                    foreach (CraftRes res in item.Resources)
                    {
                        writer.WriteStartObject();
                        writer.WritePropertyName("res");
                        if (res.NameNumber != 0)
                            writer.WriteValue(CliLoc.LocToString(res.NameNumber));
                        else if (res.NameString?.Length > 2)
                            writer.WriteValue(res.NameString);
                        else
                        {

                            if (CraftResources.GetFromType(res.ItemType) != CraftResource.None)
                            {
                                var resource = CraftResources.GetName(CraftResources.GetFromType(res.ItemType));
                                writer.WriteValue(resource);
                            }

                        }
                        writer.WritePropertyName("qty");
                        writer.WriteValue(res.Amount);
                        writer.WriteEndObject();
                    }
                    writer.WriteEndArray();

                    writer.WriteEndObject();
                }
                writer.WriteEndArray(); 
            }
               
        }
    }
}
