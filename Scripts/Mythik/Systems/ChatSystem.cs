using Scripts.Mythik.Mobiles;
using Server;
using Server.Commands;
using Server.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scripts.Mythik.Systems
{
   public class ChatSystem
    {
        public static void Initialize()
        {
            CommandSystem.Register("chaton", AccessLevel.Player, new CommandEventHandler(EnableChat));
            CommandSystem.Register("chatoff", AccessLevel.Player, new CommandEventHandler(DisableChat));
            CommandSystem.Register("c", AccessLevel.Player, new CommandEventHandler(SendChatMessage));
            CommandSystem.Register("ch", AccessLevel.Player, new CommandEventHandler(SendChatMessage));
            CommandSystem.Register("chat", AccessLevel.Player, new CommandEventHandler(SendChatMessage));
            CommandSystem.Register("s", AccessLevel.Player, new CommandEventHandler(SendChatMessage));
            CommandSystem.Register("sh", AccessLevel.Player, new CommandEventHandler(SendChatMessage));
            CommandSystem.Register("shout", AccessLevel.Player, new CommandEventHandler(SendChatMessage));
        }
        [Usage("c")]
        [Aliases("ch","chat","s","sh","shout")]
        [Description("Send a chat message")]
        private static void SendChatMessage(CommandEventArgs e)
        {
            var msg = "[" + e.Mobile.Name + "]: " + e.ArgString;
            var hue = 0x7a1;
            if (e.Mobile.AccessLevel > AccessLevel.Player)
                hue = 0x44;
            if(!(e.Mobile as MythikPlayerMobile).ChatEnabled)
            {
                e.Mobile.SendAsciiMessage("Enable Chat with .chaton");
                return;
            }
            Packet p = new AsciiMessage(Serial.MinusOne, -1, MessageType.Regular, hue, 3, "System", msg);
            

            List<NetState> list = NetState.Instances;

            p.Acquire();

            for (int i = 0; i < list.Count; ++i)
            {
                if (list[i].Mobile != null && ((MythikPlayerMobile)list[i].Mobile).ChatEnabled)
                    list[i].Send(p);
            }

            p.Release();

            NetState.FlushAll();
        }


        [Usage("chatoff")]
        [Description("Disable Chat")]
        private static void DisableChat(CommandEventArgs e)
        {
            ((MythikPlayerMobile)e.Mobile).ChatEnabled = false;
        }
        [Usage("chaton")]
        [Description("Enable Chat")]
        private static void EnableChat(CommandEventArgs e)
        {
            ((MythikPlayerMobile)e.Mobile).ChatEnabled = true;
        }
    }
}
