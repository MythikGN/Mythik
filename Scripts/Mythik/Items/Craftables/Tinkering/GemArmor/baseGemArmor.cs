using Server;
using Server.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scripts.Mythik.Items.Craftables.Tinkering.GemArmor
{
    public class BaseGemArmor : BaseArmor
    {
        
        public BaseGemArmor(Serial serial) : base(serial)
        {

        }
        public BaseGemArmor(int itemID) : base(itemID)
        {

        }
        public override ArmorMaterialType MaterialType
        {
            get
            {
                return ArmorMaterialType.Plate;
            }
        }

        public override void OnAdded(object parent)
        {
            base.OnAdded(parent);

            if (parent is Mobile)
                CheckPartAdded((Mobile)parent);
        }
        public override void OnRemoved(object parent)
        {
            base.OnRemoved(parent);
            if (parent != null && parent is Mobile)
                CheckPartRemoved((Mobile)parent);


        }

        public virtual void CheckPartRemoved(Mobile parent)
        {
            
        }

        internal int GetSetCount(Mobile from, Type type)
        {
            int TotalPieces = 0;

            //Check our Hand Armor
            if (from.HandArmor != null)
            {
                if (from.HandArmor.GetType().IsSubclassOf(type))
                {
                    TotalPieces++;
                }
            }

            //Check our Chest Armor
            if (from.ChestArmor != null)
            {
                if (from.ChestArmor.GetType().IsSubclassOf(type))
                {
                    TotalPieces++;
                }
            }

            //Check our Legs Armor
            if (from.LegsArmor != null)
            {
                if (from.LegsArmor.GetType().IsSubclassOf(type))
                {
                    TotalPieces++;
                }
            }

            //Check our Neck Armor
            if (from.NeckArmor != null)
            {
                if (from.NeckArmor.GetType().IsSubclassOf(type))
                {
                    TotalPieces++;
                }
            }

            //Check our Head Armor
            if (from.HeadArmor != null)
            {
                if (from.HeadArmor.GetType().IsSubclassOf(type))
                {
                    TotalPieces++;
                }
            }

            //Check our Shield Armor
            if (from.ShieldArmor != null)
            {
                if (from.ShieldArmor.GetType().IsSubclassOf(type))
                {
                    TotalPieces++;
                }
            }

            //Check our Shield Armor
            if (from.ArmsArmor != null)
            {
                if (from.ArmsArmor.GetType().IsSubclassOf(type))
                {
                    TotalPieces++;
                }
            }

            return TotalPieces;
        }

        public virtual void CheckPartAdded(Mobile parent)
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
