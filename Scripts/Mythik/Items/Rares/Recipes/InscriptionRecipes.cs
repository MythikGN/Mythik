using Server;
using Server.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scripts.Mythik.Items.Rares.Recipes
{
    public class AnimateDeadScrollRecipe : RecipeScroll
    {
        [Constructable]
        public AnimateDeadScrollRecipe()
			: base((int)RecipeName.AnimateDeadScroll)
		{
            
        }

        public AnimateDeadScrollRecipe(Serial serial)
			: base( serial )
		{
        }
        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
        }
        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
        }
    }
    public class BloodOathScrollRecipe : RecipeScroll
    {
        [Constructable]
        public BloodOathScrollRecipe()
            : base((int)RecipeName.BloodOathScroll)
        {

        }

        public BloodOathScrollRecipe(Serial serial)
            : base(serial)
        {
        }
        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
        }
        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
        }
    }
    public class EnemyOfOneScrollRecipe : RecipeScroll
    {
        [Constructable]
        public EnemyOfOneScrollRecipe()
            : base((int)RecipeName.EnemyOfOneScroll)
        {

        }

        public EnemyOfOneScrollRecipe(Serial serial)
            : base(serial)
        {
        }
        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
        }
        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
        }
    }
    public class SummonFamiliarScrollRecipe : RecipeScroll
    {
        [Constructable]
        public SummonFamiliarScrollRecipe()
            : base((int)RecipeName.SummonFamiliarScroll)
        {

        }

        public SummonFamiliarScrollRecipe(Serial serial)
            : base(serial)
        {
        }
        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
        }
        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
        }
    }

    public class ExorcismScrollRecipe : RecipeScroll
    {
        [Constructable]
        public ExorcismScrollRecipe()
            : base((int)RecipeName.ExorcismScroll)
        {

        }

        public ExorcismScrollRecipe(Serial serial)
            : base(serial)
        {
        }
        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
        }
        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
        }
    }

}
