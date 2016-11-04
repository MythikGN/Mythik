using Server;
using Server.Commands;
using Server.Gumps;
using Server.Mobiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Server.Network;

namespace Scripts.Mythik.Systems
{
    public class CreatureGuide
    {
        public static List<CreatureInfo> Animals = new List<CreatureInfo>();
        public static List<CreatureInfo> Monsters = new List<CreatureInfo>();
        public static List<CreatureInfo> Mounts = new List<CreatureInfo>();
        public static void Initialize()
        {
            CommandSystem.Register("cguide", AccessLevel.Player, new CommandEventHandler(ShowGuide));
            PopulateCreatures();
        }

        private static void PopulateCreatures()
        {
            foreach (var assembly in ScriptCompiler.Assemblies)
            {
                foreach (var type in assembly.GetTypes())
                {
                    // Skip types that don't have parameterless constructor
                    var constructor = type.GetConstructor(Type.EmptyTypes);
                    if (constructor == null) continue;

                    try
                    {

                        if (type.IsSubclassOf(typeof(BaseCreature)))
                        {
                            BaseCreature mob = (BaseCreature)Activator.CreateInstance(type);
                            if (mob != null)
                            {
                                if(mob is IMount)
                                {
                                    AddCreature(mob, Mounts);
                                }
                                else if(mob.AI == AIType.AI_Animal)
                                {
                                    AddCreature(mob, Animals);
                                }
                                else
                                {
                                    AddCreature(mob, Monsters);
                                }
                                mob.Delete();
                            }
                        }
                    }
                    catch { }
                }               // END foreach (var type
            }
        }

        private static void AddCreature(BaseCreature mob, List<CreatureInfo> list)
        {
            if (mob.Body == 0x190 || mob.Body == 0x191 || string.IsNullOrWhiteSpace(mob.Name))
                return;
            list.Add(new CreatureInfo() {Name = mob.Name, Difficulty = MonsterLevels.GetMonsterDifficultyLevelText(mob), Hits = mob.Hits, GraphicID = ShrinkTable.Lookup(mob) });
        }
        [Usage("cguide")]
        [Description("Shows the Create Guide")]
        public static void ShowGuide(CommandEventArgs e)
        {
            var from = e.Mobile;
            from.SendGump(new CreatureListGump(CreatureListGump.Buttons.None));
        }

    }

    public class CreatureListGump : Gump
    {
        public enum Buttons
        {
            None,Animals,Mobs,Mounts
        }
        public CreatureListGump(Buttons page) : base(25,25)
        {
            this.Closable = true;
            this.Disposable = true;
            this.Dragable = true;
            this.Resizable = false;
            this.AddPage(0);
            this.AddBackground(0, 0, 530, 448, 5054);
            this.AddImageTiled(10, 10, 510, 22, 2624);
            this.AddImageTiled(10, 36, 510, 25, 2624);
            this.AddLabel(193, 10, 0, @"Mythik Hunters Guide");
            this.AddLabel(84, 38, 0, @"Animals");
            this.AddButton(46, 38, 4007, 4008, (int)Buttons.Animals, GumpButtonType.Reply, 0);
            this.AddButton(216, 38, 4007, 4008, (int)Buttons.Mobs, GumpButtonType.Reply, 0);
            this.AddLabel(253, 38, 0, @"Monsters");
            this.AddButton(382, 38, 4007, 4008, (int)Buttons.Mounts, GumpButtonType.Reply, 0);
            this.AddLabel(419, 38, 0, @"Mounts");
            this.AddImageTiled(19, 66, 155, 369, 2624);
            this.AddImageTiled(187, 66, 155, 369, 2624);
            this.AddImageTiled(356, 65, 155, 369, 2624);
            if (page == Buttons.None)
                return;
            switch(page)
            {
                case Buttons.Animals:
                    BuildList(CreatureGuide.Animals,100);
                    break;
                case Buttons.Mobs:
                    BuildList(CreatureGuide.Monsters,200);
                    break;
                case Buttons.Mounts:
                    BuildList(CreatureGuide.Mounts,300);
                    break;

            }
        }
        public override void OnResponse(NetState sender, RelayInfo info)
        {
            var bt = info.ButtonID;
            if (bt == 0 || bt > 400)
                return;
            switch(bt)
            {
                case 1:
                    sender.Mobile.SendGump(new CreatureListGump(CreatureListGump.Buttons.Animals));
                    return;
                case 2:
                    sender.Mobile.SendGump(new CreatureListGump(CreatureListGump.Buttons.Mobs));
                    return;
                case 3:
                    sender.Mobile.SendGump(new CreatureListGump(CreatureListGump.Buttons.Mounts));
                    return;
            }
            if (bt >= 300)
                sender.Mobile.SendGump(new CreatureGuideDetails(CreatureGuide.Mounts[bt - 300],Buttons.Mounts));
            else if(bt >= 200)
                sender.Mobile.SendGump(new CreatureGuideDetails(CreatureGuide.Monsters[bt - 200],Buttons.Mobs));
            else
                sender.Mobile.SendGump(new CreatureGuideDetails(CreatureGuide.Monsters[bt - 100],Buttons.Animals));
        }

        private void BuildList(List<CreatureInfo> mobiles,int startIndex)
        {
            int perPage = 39;

            int x = 31;
            int y = 76;
            int cnt = 0;
            int index = 0;
            foreach(var mob in mobiles)
            {
                int i = index % perPage;
                if(i == 0)
                {
                    x = 31;
                    y = 76;
                    if (index > 0)
                        this.AddButton(490, 420, 4005, 4006 , 4, GumpButtonType.Page, (index / perPage) + 1);
                    AddPage((index / perPage) + 1);
                    if (index > 0)
                        this.AddButton(25, 420, 4014, 4015, 4, GumpButtonType.Page, (index / perPage));
                }
                this.AddButton(x, y, 4007, 4008, startIndex + index++, GumpButtonType.Reply, 0);
                this.AddLabel( x +38, y, 0, mob.Name);
                y += 25;
                cnt++;
                if (cnt >= (perPage / 3))
                {
                    cnt = 0;
                    y = 76;
                    x += 168;
                }

            }
        }
    }

    internal class CreatureGuideDetails : Gump
    {
        private CreatureListGump.Buttons m_category;

        public CreatureGuideDetails(CreatureInfo creatureInfo, CreatureListGump.Buttons cat) : base(25,25)
        {
            m_category = cat;
            this.Closable = true;
            this.Disposable = true;
            this.Dragable = true;
            this.Resizable = false;
            this.AddPage(0);
            this.AddBackground(0, 0, 530, 448, 5054);
            this.AddImageTiled(10, 10, 510, 22, 2624);
            this.AddImageTiled(10, 36, 510, 25, 2624);
            this.AddLabel(193, 10, 0, @"Mythik Hunters Guide");
            this.AddLabel(84, 38, 0, @"Animals");
            this.AddButton(46, 38, 4007, 4008, 1, GumpButtonType.Reply, 0);
            this.AddButton(216, 38, 4007, 4008, 2, GumpButtonType.Reply, 0);
            this.AddLabel(253, 38, 0, @"Monsters");
            this.AddButton(382, 38, 4007, 4008, 3, GumpButtonType.Reply, 0);
            this.AddLabel(419, 38, 0, @"Mounts");
            this.AddImageTiled(19, 66, 490, 369, 2624);
            this.AddButton(29, 404, 4014, 4015, 4, GumpButtonType.Reply, 0);
            this.AddHtml(224, 78, 150, 30, @"<big>" + creatureInfo.Name.ToUpperInvariant() +"</big", false, false);
            //this.AddLabel(234, 78, 0, creatureInfo.Name);
            this.AddBackground(31, 74, 100, 100, 83);
            this.AddItem(65, 103, creatureInfo.GraphicID);
            this.AddLabel(151, 116, 0, @"Difficulty: " + creatureInfo.Difficulty);
            this.AddLabel(345, 116, 0, @"Health : " + creatureInfo.Hits);

        }
        public override void OnResponse(NetState sender, RelayInfo info)
        {
            base.OnResponse(sender, info);
            var bt = info.ButtonID;
            if (bt == 4)
                bt = (int)m_category;
            var from = sender.Mobile;
            switch(bt)
            {
                case 0:
                case 4:
                    return;
                case 1:
                    from.SendGump(new CreatureListGump(CreatureListGump.Buttons.Animals));
                    break;
                case 2:
                    from.SendGump(new CreatureListGump(CreatureListGump.Buttons.Mobs));
                    break;
                case 3:
                    from.SendGump(new CreatureListGump(CreatureListGump.Buttons.Mounts));
                    break;
            }
        }
    }

    public class CreatureInfo
    {
        public string Name;
        public string Difficulty;

        public int Hits { get; internal set; }
        public int GraphicID { get; internal set; }
    }
}
