using Server;
using Server.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scripts.Mythik.Systems.Farming.Items
{

    public abstract class  AbstractFarmSeed : Item, ICommodity
    {
        
        public AbstractFarmSeed(int artID) : base(artID)
        {
            Stackable = true;
        }

        public AbstractFarmSeed(Serial serial) : base(serial)
        {
        }

        public abstract TextDefinition Description { get; }
        public abstract bool IsDeedable { get; }

        public override void OnDoubleClick(Mobile from)
        {
            from.BeginTarget(3, false, Server.Targeting.TargetFlags.None, OnTarget);
        }

        private void OnTarget(Mobile from, object targeted)
        {
            var dirt = targeted as FarmableDirt;
            if(dirt == null)
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
            var plant = new SmallPlant(PlantType.NightShade);
            plant.Plant(from);
            plant.MoveToWorld(dirt.Location, dirt.Map);
            dirt.Delete();

        }
    }
    public class CommonSeeds : AbstractFarmSeed
    {
        [Constructable]
        public CommonSeeds() : base(11)
        {

        }

        public CommonSeeds(Serial serial) : base(serial) { }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
        }
        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
        }

        public override TextDefinition Description
        {
            get
            {
                return "Common Seeds";
            }
        }

        public override bool IsDeedable
        {
            get
            {
                return true;
            }
        }
    }
    public class UnCommonSeeds : AbstractFarmSeed
    {
        [Constructable]
        public UnCommonSeeds() : base(11)
        {

        }

        public UnCommonSeeds(Serial serial) : base(serial) { }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
        }
        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
        }

        public override TextDefinition Description
        {
            get
            {
                return "Uncommon Seeds";
            }
        }

        public override bool IsDeedable
        {
            get
            {
                return true;
            }
        }
    }
    public class ExoticSeeds : AbstractFarmSeed
    {
        [Constructable]
        public ExoticSeeds() : base(11)
        {

        }

        public ExoticSeeds(Serial serial) : base(serial) { }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
        }
        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
        }

        public override TextDefinition Description
        {
            get
            {
                return "Exotic Seeds";
            }
        }

        public override bool IsDeedable
        {
            get
            {
                return true;
            }
        }
    }
}
