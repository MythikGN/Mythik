using Server.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Server;
using Server.Gumps;
using Server.Network;
using Server.Mobiles;
using Server.Engines.Craft;

namespace Scripts.Mythik.Items.Craftables.Tinkering
{
    public class FusionCube : BaseContainer
    {
        private static List<FusionCubeRecipe> m_Recipes = new List<FusionCubeRecipe>() {
            new FusionCubeRecipe(typeof(IronIngot),"Iron Ingots", 1, typeof(Log),"Logs", 2,true),
            new FusionCubeRecipe(typeof(Log),"Logs", 1, typeof(IronIngot),"Iron Ingots", 2,true),
            new FusionCubeRecipe(typeof(GreaterHealPotion),"Greater Heal Potion", 1, typeof(HealPotion),"Heal Potion",3,true),
        };
        class FusionCubeRecipe
        {
            public int m_createdAmount;
            public Type m_createdItem;
            public List<CraftRes> m_Resources = new List<CraftRes>();
            public bool m_allowMultipleCraftResults;
            public TextDefinition m_createdName;

            public FusionCubeRecipe(Type createdItem, TextDefinition createdName, int amount, Type resource, TextDefinition resName, int resAmount, bool allowMultipleCraftResults)
            {
                this.m_createdItem = createdItem;
                this.m_createdAmount = amount;
                this.m_createdName = createdName;
                this.m_Resources.Add(new CraftRes(resource, resName, resAmount,""));
                this.m_allowMultipleCraftResults = allowMultipleCraftResults;
            }
            /// <summary>
            /// No stackables requiring 2+ source items.
            /// </summary>
            /// <param name="createdItem"></param>
            /// <param name="amount"></param>
            /// <param name="resourceA"></param>
            /// <param name="amountA"></param>
            /// <param name="resourceB"></param>
            /// <param name="amountB"></param>
            /*public FusionCubeRecipe(Type createdItem, int amount, Type resourceA, int amountA, Type resourceB, int amountB)
            {
                this.m_createdItem = createdItem;
                this.m_createdAmount = amount;
                this.m_Resources.Add(new CraftRes(resourceA, amountA));
                this.m_Resources.Add(new CraftRes(resourceB, amountB));

            }*/
        }

        [Constructable]
        public FusionCube() : base(0x09B0)
        {
            this.Hue = GiftBoxHues.RandomNeonBoxHue;
        }
        public FusionCube(Serial serial) : base(serial)
        {

        }
        public override void OnDoubleClick(Mobile from)
        {
            if (from.InRange(GetWorldLocation(), 2))
            {
                from.Frozen = true;
                from.SendGump(new FusionCubeGump(from,this));
            }
            else
            {
                from.SendLocalizedMessage(500446); // That is too far away.
            }
        }
        private bool Fuse()
        {
            foreach(var recipe in m_Recipes)
            {
                bool canMake = false;
                foreach (var res in recipe.m_Resources)
                {
                    var resToFuse = FindItemByType(res.ItemType);
                    if (resToFuse?.Amount > res.Amount)
                    {
                        canMake = true;
                    }
                    else
                    {
                        canMake = false;
                    }
                    if (!canMake)
                        break;
                }
                if (!canMake)
                    continue;
                int resultCnt = recipe.m_createdAmount;
                if(recipe.m_allowMultipleCraftResults)
                {
                    var res = FindItemByType(recipe.m_Resources[0].ItemType);
                    resultCnt = res.Amount / recipe.m_Resources[0].Amount;
                }
                var result = (Item)Activator.CreateInstance(recipe.m_createdItem);
                if (resultCnt > 1)
                    result.Amount = resultCnt;
                this.Items.Clear();
                
                AddItem((Item)result);
                result.X = 50;
                result.Y = 50;
                return true;
            }
            return false;
        }
        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
        }
        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
        }
        private class FusionCubeGump : Gump
        {
            private FusionCube m_cube;

            public FusionCubeGump(Mobile from,FusionCube cube) : base(25,25)
            {
                this.m_cube = cube;
                this.Closable = true;
                this.Disposable = true;
                this.Dragable = true;
                this.Resizable = false;
                this.AddPage(0);
                this.AddBackground(26, 31, 200, 115, 9200);
                this.AddAlphaRegion(23, 28, 195, 109);

                this.AddLabel(84, 38, 0, @"Fusion Cube");
                this.AddButton(42, 62, 9800, 248, 1, GumpButtonType.Reply, 0);
                this.AddLabel(47, 116, 0, @"Open");
                this.AddButton(93, 55, 5571, 5572, 2, GumpButtonType.Reply, 0);
                this.AddLabel(109, 118, 0, @"Fuse");
                this.AddButton(160, 55, 219, 248, 3, GumpButtonType.Reply, 0);
                this.AddLabel(172, 120, 0, @"Help");

            }
            public override void OnResponse(NetState sender, RelayInfo info)
            {
                switch(info.ButtonID)
                {
                    case 1:
                        m_cube.Open(sender.Mobile);
                        break;
                    case 2:
                        if (m_cube.Fuse())
                            m_cube.Open(sender.Mobile);
                        break;
                    case 3:
                        sender.Mobile.SendGump(new FusionCubeHelpGump());
                        break;
                }
                base.OnResponse(sender, info);
            }
        }
        private class FusionCubeHelpGump : Gump
        {
            public FusionCubeHelpGump() : base(25,25)
            {

                this.Closable = true;
                this.Disposable = true;
                this.Dragable = true;
                this.Resizable = false;
                this.AddPage(0);
                this.AddBackground(25, 25, 364, 436, 9300);
                this.AddLabel(143, 32, 0, @"Fusion Recipes");
                this.AddImage(200, 60, 2701);
                this.AddLabel(90, 60, 0, @"Input");
                this.AddLabel(290, 60, 0, @"Output");
                this.AddButton(350, 30, 1151, 1150, 1, GumpButtonType.Reply, 0);
                int y = 90;
                foreach(var rec in FusionCube.m_Recipes)
                {
                    AddLabel(210, y, 0, rec.m_createdAmount + " " + rec.m_createdName.String);
                    AddLabel(40, y, 0, rec.m_Resources[0].Amount + " " + rec.m_Resources[0].NameString);
                    y += 20;
                }
                


            }
            public override void OnResponse(NetState sender, RelayInfo info)
            {
                base.OnResponse(sender, info);
            }
        }

        
    }
}
