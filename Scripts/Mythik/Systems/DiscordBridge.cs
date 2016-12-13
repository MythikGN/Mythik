

namespace Scripts.Mythik.Systems
{
   /* public class DiscordBridge
    {
        private static readonly string TOKEN = "MjU3NzE3MDE3Nzg2NzEyMDY0.Cy-xQg.lAADQiE7LBb6tK5oa89oNx34h84";
        private static Channel _channel;
        private static DiscordClient _client;
        
        public static void Initialize()
        {
            _client = new DiscordClient();

            _client.MessageReceived += async (s, e) =>
            {
                if (e.Message.Text.Contains("!chan"))
                    _channel = e.Channel;
                if (!e.Message.IsAuthor)
                    ChatSystem.SendChatMessage(e.Message.User.Nickname,e.Message.Text);
            };
            ChatSystem.ChatMessageSent += async (s, e) => {
                await _channel.SendMessage((string)s);
            };

            _client.ExecuteAndWait(async () => {
                await _client.Connect(TOKEN, TokenType.Bot);
            });
        }
    }*/
}