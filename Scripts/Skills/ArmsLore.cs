using System;
using Server.Items;
using Server.Mobiles;
using Server.Network;
using Server.Targeting;
using Server.Gumps;
using Scripts.Mythik.Mobiles;

namespace Server.SkillHandlers
{
    public class EquipInfoGump : Gump
    {
        public EquipInfoGump(Scripts.Mythik.Mobiles.MythikPlayerMobile from) :this(from,null)
        {

        }
        public EquipInfoGump(Scripts.Mythik.Mobiles.MythikPlayerMobile from, Item item) : base(from.GetGumpLoc(typeof(EquipInfoGump)).Item1, from.GetGumpLoc(typeof(EquipInfoGump)).Item2)
        {
            from.CloseGump(typeof(EquipInfoGump));
            this.Closable = true;
            this.Disposable = true;
            this.Dragable = true;
            this.Resizable = false;
            this.AddPage(0);
            var height = 200;
            if(item != null)
                height = CliLoc.GetPropertiesList(item).Count * 23;
            height += 5;
            this.AddBackground(2, 8, 196, 46 + height, 9270);
            this.AddBackground(9, 17, 182, 29 + height, 9270);
            this.AddAlphaRegion(0, 7, 191, 50+height);
            this.AddButton(156, 20, 4011, 4012, 1, GumpButtonType.Reply, 0);
            if (item == null)
                return;
            var text = @"<CENTER><BASEFONT COLOR=GREEN>";
            var textHeader = @"<CENTER><BASEFONT COLOR=WHITE>";
            int y = 20;
            //if (string.IsNullOrWhiteSpace(item.Name))
            //    AddHtmlLocalized(98, y += 17, 100, 15, item.LabelNumber, false, false);
            // else
            //    AddHtml(98, y += 17, 100, 15, item.Name, false, false);
            int cnt = 0;
            foreach (var prop in CliLoc.GetPropertiesList(item))
            {
                if (cnt == 0 && prop?.Length > 1)
                    AddHtml(18, y += 23, 160, 17, textHeader + char.ToUpper(prop[0]) + prop.Substring(1), false, false);
                else
                    AddHtml(18, y += 23, 160, 17, text + prop, false, false);
                cnt++;
                /*if (prop.Item2 == null)
                    AddHtmlLocalized(98, y += 17, 100, 15, prop.Item1, false, false);
                else
                    AddHtmlLocalized(98, y += 17, 100, 15, prop.Item1, prop.Item2, 0, false, false);*/

            }
            //this.AddHtml(90, 87, 119, 231, text, (bool)false, (bool)false);

        }

        public override void OnResponse(NetState sender, RelayInfo info)
        {
            base.OnResponse(sender, info);
            if (info.ButtonID == 1)
                sender.Mobile.SendGump(new SetLocationGump(sender, typeof(EquipInfoGump)));
        }
    }

    internal class SetLocationGump : Gump
    {
        private Type type;

        public SetLocationGump(NetState sender, Type type) : base(200,200)
        {
            
            this.type = type;
            sender.Mobile.CloseGump(typeof(SetLocationGump));
            this.Closable = true;
            this.Disposable = true;
            this.Dragable = true;
            this.Resizable = false;
            this.AddPage(0);
            this.AddBackground(82, 78, 347, 327, 9270);
            this.AddLabel(170, 93, 43, @"Gump Positioning System");
            var from = sender.Mobile as Scripts.Mythik.Mobiles.MythikPlayerMobile;
            
            this.AddLabel(170, 113, 43, @"Current Position: " + from.GetGumpLoc(type).Item1 + " / " + from.GetGumpLoc(type).Item2);
            this.AddButton(354, 370, 247, 248, (int)1, GumpButtonType.Reply, 0); //okay
            this.AddImage(237, 222, 113);
            this.AddButton(245, 200, 5208, 248, (int)10, GumpButtonType.Reply, 0);
            this.AddButton(245, 175, 5208, 248, (int)11, GumpButtonType.Reply, 0);
            this.AddButton(245, 150, 5208, 248, (int)12, GumpButtonType.Reply, 0);
            this.AddLabel(270, 200, 0, @"-1");
            this.AddLabel(270, 175, 0, @"-10");
            this.AddLabel(270, 150, 0, @"-20");
            this.AddButton(245, 265, 5209, 248, (int)20, GumpButtonType.Reply, 0);
            this.AddLabel(270, 265, 0, @"+1");
            this.AddButton(245, 290, 5209, 248, (int)21, GumpButtonType.Reply, 0);
            this.AddLabel(270, 290, 0, @"+10");
            this.AddButton(245, 315, 5209, 248, (int)22, GumpButtonType.Reply, 0);
            this.AddLabel(270, 315, 0, @"+20");

            this.AddButton(285, 225, 5224, 248, (int)40, GumpButtonType.Reply, 0);
            this.AddLabel(285, 245, 0, @"+1");
            this.AddButton(315, 225, 5224, 248, (int)41, GumpButtonType.Reply, 0);
            this.AddLabel(315, 245, 0, @"+10");
            this.AddButton(345, 225, 5224, 248, (int)42, GumpButtonType.Reply, 0);
            this.AddLabel(345, 245, 0, @"+20");

            this.AddButton(205, 225, 5223, 248, (int)30, GumpButtonType.Reply, 0);
            this.AddLabel(205, 245, 0, @"-1");
            this.AddButton(175, 225, 5223, 248, (int)31, GumpButtonType.Reply, 0);
            this.AddLabel(175, 245, 0, @"-10");
            this.AddButton(145, 225, 5223, 248, (int)32, GumpButtonType.Reply, 0);
            this.AddLabel(145, 245, 0, @"-20");


        }
        public override void OnResponse(NetState sender, RelayInfo info)
        {
            var from = sender.Mobile as Scripts.Mythik.Mobiles.MythikPlayerMobile;
            
            //base.OnResponse(sender, info);
            switch(info.ButtonID)
            {
                case 0:
                case 1:
                    return;
                case 10:
                    from.SetGumpLoc(type, 0, -1);
                    break;
                case 11:
                    from.SetGumpLoc(type, 0, -10);
                    break;
                case 12:
                    from.SetGumpLoc(type, 0, -20);
                    break;
                case 20:
                    from.SetGumpLoc(type, 0, 1);
                    break;
                case 21:
                    from.SetGumpLoc(type, 0, 10);
                    break;
                case 22:
                    from.SetGumpLoc(type, 0, 20);
                    break;
                case 30:
                    from.SetGumpLoc(type, -1, 0);
                    break;
                case 31:
                    from.SetGumpLoc(type, -10, 0);
                    break;
                case 32:
                    from.SetGumpLoc(type, -20, 0);
                    break;
                case 40:
                    from.SetGumpLoc(type, 1, 0);
                    break;
                case 41:
                    from.SetGumpLoc(type, 10, 0);
                    break;
                case 42:
                    from.SetGumpLoc(type, 20, 0);
                    break;
            }
            from.SendGump(new SetLocationGump(sender, type));
            var gump = (Gump)Activator.CreateInstance(type, new object[] { from });
            from.SendGump(gump);
        }
    }

    public class ArmsLore
	{
		public static void Initialize()
		{
			SkillInfo.Table[(int)SkillName.ArmsLore].Callback = new SkillUseCallback( OnUse );
		}

		public static TimeSpan OnUse(Mobile m)
		{
			m.Target = new InternalTarget();

			m.SendLocalizedMessage( 500349 ); // What item do you wish to get information about?

			return TimeSpan.FromSeconds( 1.0 );
		}

		[PlayerVendorTarget]
		private class InternalTarget : Target
		{
			public InternalTarget() : base( 2, false, TargetFlags.None )
			{
				AllowNonlocal = true;
			}

			protected override void OnTarget( Mobile from, object targeted )
			{
                if(targeted is Item && from is MythikPlayerMobile)
                {
                    var item = (Item)targeted;
                    from.CloseGump(typeof(EquipInfoGump));
                    from.SendGump(new EquipInfoGump(from as MythikPlayerMobile, item));
                }
				if ( targeted is BaseWeapon )
				{
					if ( from.CheckTargetSkill( SkillName.ArmsLore, targeted, 0, 100 ) )
					{
						BaseWeapon weap = (BaseWeapon)targeted;

						if ( weap.MaxHitPoints != 0 )
						{
							int hp = (int)((weap.HitPoints / (double)weap.MaxHitPoints) * 10);

							if ( hp < 0 )
								hp = 0;
							else if ( hp > 9 )
								hp = 9;

							from.SendLocalizedMessage( 1038285 + hp );
						}

						int damage = (weap.MaxDamage + weap.MinDamage) / 2;
						int hand = (weap.Layer == Layer.OneHanded ? 0 : 1);

						if ( damage < 3 )
							damage = 0;
						else
							damage = (int)Math.Ceiling( Math.Min( damage, 30 ) / 5.0 );
							/*
						else if ( damage < 6 )
							damage = 1;
						else if ( damage < 11 )
							damage = 2;
						else if ( damage < 16 )
							damage = 3;
						else if ( damage < 21 )
							damage = 4;
						else if ( damage < 26 )
							damage = 5;
						else
							damage = 6;
							 * */

						WeaponType type = weap.Type;

						if ( type == WeaponType.Ranged )
							from.SendLocalizedMessage( 1038224 + (damage * 9) );
						else if ( type == WeaponType.Piercing )
							from.SendLocalizedMessage( 1038218 + hand + (damage * 9) );
						else if ( type == WeaponType.Slashing )
							from.SendLocalizedMessage( 1038220 + hand + (damage * 9) );
						else if ( type == WeaponType.Bashing )
							from.SendLocalizedMessage( 1038222 + hand + (damage * 9) );
						else
							from.SendLocalizedMessage( 1038216 + hand + (damage * 9) );

						if ( weap.Poison != null && weap.PoisonCharges > 0 )
							from.SendLocalizedMessage( 1038284 ); // It appears to have poison smeared on it.
					}
					else
					{
						from.SendLocalizedMessage( 500353 ); // You are not certain...
					}
				}
				else if(targeted is BaseArmor)
				{
					if( from.CheckTargetSkill(SkillName.ArmsLore, targeted, 0, 100) )
					{
						BaseArmor arm = (BaseArmor)targeted;

						if ( arm.MaxHitPoints != 0 )
						{
							int hp = (int)((arm.HitPoints / (double)arm.MaxHitPoints) * 10);

							if ( hp < 0 )
								hp = 0;
							else if ( hp > 9 )
								hp = 9;

							from.SendLocalizedMessage( 1038285 + hp );
						}


						from.SendLocalizedMessage( 1038295 + (int)Math.Ceiling( Math.Min( arm.ArmorRating, 35 ) / 5.0 ) );
						/*
						if ( arm.ArmorRating < 1 )
							from.SendLocalizedMessage( 1038295 ); // This armor offers no defense against attackers.
						else if ( arm.ArmorRating < 6 )
							from.SendLocalizedMessage( 1038296 ); // This armor provides almost no protection.
						else if ( arm.ArmorRating < 11 )
							from.SendLocalizedMessage( 1038297 ); // This armor provides very little protection.
						else if ( arm.ArmorRating < 16 )
							from.SendLocalizedMessage( 1038298 ); // This armor offers some protection against blows.
						else if ( arm.ArmorRating < 21 )
							from.SendLocalizedMessage( 1038299 ); // This armor serves as sturdy protection.
						else if ( arm.ArmorRating < 26 )
							from.SendLocalizedMessage( 1038300 ); // This armor is a superior defense against attack.
						else if ( arm.ArmorRating < 31 )
							from.SendLocalizedMessage( 1038301 ); // This armor offers excellent protection.
						else
							from.SendLocalizedMessage( 1038302 ); // This armor is superbly crafted to provide maximum protection.
						 * */
					}
					else
					{
						from.SendLocalizedMessage( 500353 ); // You are not certain...
					}
				}
				else if ( targeted is SwampDragon && ((SwampDragon)targeted).HasBarding )
				{
					SwampDragon pet = (SwampDragon)targeted;

					if ( from.CheckTargetSkill( SkillName.ArmsLore, targeted, 0, 100 ) )
					{
						int perc = (4 * pet.BardingHP) / pet.BardingMaxHP;

						if ( perc < 0 )
							perc = 0;
						else if ( perc > 4 )
							perc = 4;

						pet.PrivateOverheadMessage( MessageType.Regular, 0x3B2, 1053021 - perc, from.NetState );
					}
					else
					{
						from.SendLocalizedMessage( 500353 ); // You are not certain...
					}
				}
				else
				{
					from.SendLocalizedMessage( 500352 ); // This is neither weapon nor armor.
				}
			}
		}
	}
}