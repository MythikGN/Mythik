using Server;
using Server.Engines.Craft;
using Server.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scripts.Mythik.Items.Craftables.Fletching
{
    class RobeOfFire : BaseArmor, ICraftable
    {
        public override int InitMinHits
        {
            get
            {
                return 150;
            }
        }
        public override int InitMaxHits
        {
            get
            {
                return 150;
            }
        }

        public override ArmorMaterialType MaterialType
        {
            get
            {
                return ArmorMaterialType.Cloth;
            }
        }


        private readonly double m_EffectChance = 0.5; // 50%
        private readonly TimeSpan m_EffectDelay = TimeSpan.FromSeconds(30); // min 30 seconds between effects.


        private DateTime m_NextEffect;


        [Constructable]
        public RobeOfFire() :base(0x1F03)
        {
            Hue = 0x898;
            Name = "Robe of Fire";
            
        }

        public override int OnHit(BaseWeapon weapon, int damageTaken)
        {
            if(DateTime.UtcNow >= m_NextEffect)
            {
                if(Utility.RandomDouble() < m_EffectChance)
                {
                    var m = weapon.Parent as Mobile;
                    if(m != null )
                    {
                        m.Damage(20, Parent as Mobile);
                        m.FixedParticles(0x3709, 10, 30, 5052, EffectLayer.LeftFoot);
                        m.PlaySound(0x208);
                        m_NextEffect = DateTime.UtcNow;
                    }
                }
            }
            return base.OnHit(weapon, damageTaken);
        }

        public RobeOfFire(Serial serial): base ( serial )
		{
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write((int)0); // version 
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();
        }
    }
}
