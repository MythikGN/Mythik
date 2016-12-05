using Scripts.Mythik.Mobiles;
using Server.Gumps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Server.Network;
using Server.SkillHandlers;

namespace Server.Mythik
{
    class StatusGump : Gump
    {
        public StatusGump(MythikPlayerMobile from) :base(from.GetGumpLoc(typeof(StatusGump)).Item1, from.GetGumpLoc(typeof(StatusGump)).Item2)
        {
            from.CloseGump(typeof(StatusGump));
            this.Closable = true;
            this.Disposable = true;
            this.Dragable = true;
            this.Resizable = false;
            this.AddPage(0);
            this.AddButton(388, 150, 9026, 9027, 1, GumpButtonType.Reply, 0);
            this.AddImage(0, 0, 10860);
            this.AddLabel(88, 73, 0, from.Str.ToString());
            this.AddLabel(88, 101, 0, from.Dex.ToString());
            this.AddLabel(88, 131, 0, from.Int.ToString());
            this.AddLabel(147, 73, 0, from.Hits.ToString());
            this.AddLabel(147, 101, 0, from.Stam.ToString());
            this.AddLabel(147, 131, 0, from.Mana.ToString());
            this.AddLabel(220, 72, 0, from.StatCap.ToString());
            this.AddLabel(220, 100, 0, from.Luck.ToString());
            this.AddLabel(220, 130, 0, (from.TotalWeight + Mobile.BodyWeight).ToString());
            IWeapon weapon = from.Weapon;

            int min = 0, max = 0;

            if (weapon != null)
                weapon.GetStatusDamage(from, out min, out max);

            this.AddLabel(287, 72, 0, min +"-"+max);
            this.AddLabel(287, 100, 0, from.TotalGold.ToString());
            this.AddLabel(287, 130, 0, from.Followers +"/"+from.FollowersMax);
            this.AddLabel(356, 70, 0, from.PhysicalResistance.ToString());
            this.AddLabel(356, 87, 0, from.FireResistance.ToString());
            this.AddLabel(356, 102, 0, from.ColdResistance.ToString());
            this.AddLabel(356, 116, 0, from.PoisonResistance.ToString());
            this.AddLabel(356, 132, 0, from.EnergyResistance.ToString());
            this.AddHtml(58, 50, 321, 15, "<CENTER>" +from.Name, (bool)false, (bool)false);

        }
        public override void OnResponse(NetState sender, RelayInfo info)
        {
            base.OnResponse(sender, info);
            if (info.ButtonID == 1)
                sender.Mobile.SendGump(new SetLocationGump(sender, typeof(StatusGump)));
        }
    }
}
