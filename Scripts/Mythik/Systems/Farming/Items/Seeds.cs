using Mythik.Systems.Farming;
using Server;
using Server.Items;
using Server.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scripts.Mythik.Systems.Farming.Items
{

    public class FarmSeed : Item, ICommodity
    {
        public PlantType PlantType { get; private set; }
        public TextDefinition Description
        {
            get
            {
                return this.Name;
            }
        }

        public bool IsDeedable
        {
            get
            {
                return false;
            }
        }

        [Constructable]
        public FarmSeed() : this(PlantType.BlackPearl)
        {
        }
        [Constructable]
        public FarmSeed(Serial serial) : base(serial)
        {
        }

        [Constructable]
        public FarmSeed(PlantType plantType) : base(0xDCF)
        {
            //TODO seeds either are like
            // ginseng seeds, garlic seeds etc.
            //or seeds are just seeds and select what you want to plant from a craft gump
            this.PlantType = plantType;
            this.Name = PlantType.ToString() + " seeds";
            Weight = 0.1;
            Stackable = true;
        }
        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write((int)1);
            writer.Write((int)PlantType);
        }
        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            var version = reader.ReadInt();
            PlantType = (PlantType)reader.ReadInt();
        }




        public override void OnDoubleClick(Mobile from)
        {
            from.BeginTarget(3, false, Server.Targeting.TargetFlags.None, OnTarget);
        }

        private void OnTarget(Mobile from, object targeted)
        {
            var dirt = targeted as FarmableDirt;
            if (dirt == null)
            {
                from.SendMessage("You can only plant this in tilled farm soil.");
                return;
            }

            Consume(1);
            //TODO seed skill level
            if (!from.CheckSkill(SkillName.Begging, 0.75))
            {
                from.SendMessage("You fumble and lose the seed.");
                return;
            }
            var region = Region.Find(dirt.Location, Map);
            if (region == null)
                return;
            var houseRegion = region as HouseRegion;
            if (houseRegion == null || !(houseRegion.House is IFarm))
                return;


            var plant = new FarmPlant(from, PlantType);
            plant.MoveToWorld(dirt.Location, dirt.Map);
            (houseRegion.House as IFarm).RemoveFarmitem(dirt);
            dirt.Consume();
            if ((houseRegion.House as IFarm).AddFarmItem(plant))
            {
                from.SendMessage("You plant the seed.");
            }
            
        }
    }
}
   
