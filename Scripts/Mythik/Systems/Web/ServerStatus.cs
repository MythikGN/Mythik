using Server.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Scripts.Mythik.Systems.Web
{
    class ServerStatus
    {
        [Serializable]
        class JSONMobile
        {
            public string Name;
            public string Guild;
            public string Title;
            public string Kills, Karma, Fame;
        }
        public static void Initialize()
        {
            var clientsListner = new HttpListener();
            clientsListner.Prefixes.Add("http://play.mythikuo.com:3000/");
            clientsListner.Start();
            Task.Run(() => { while (clientsListner.IsListening) { SendResponse(clientsListner.GetContext().Response); } });

            var statsListner = new HttpListener();
            statsListner.Prefixes.Add("http://play.mythikuo.com:3001/");
            statsListner.Start();
            Task.Run(() => { while (statsListner.IsListening) { SendStatsResponse(statsListner.GetContext().Response); } });

        }

        private static void SendStatsResponse(HttpListenerResponse response)
        {
            var data = "{\"Clients\":\"" + NetState.Instances.Count + "\"}";
            var buffer = System.Text.Encoding.UTF8.GetBytes(data);

            response.ContentLength64 = buffer.Length;
            response.OutputStream.Write(buffer, 0, buffer.Length);
            response.OutputStream.Flush();
            response.OutputStream.Close();
        }

        private static void SendResponse(HttpListenerResponse response)
        {
            var list = new List<JSONMobile>();
            foreach( var ns in NetState.Instances)
            {
                var m = ns.Mobile;
                if (m != null)
                    list.Add(new Web.ServerStatus.JSONMobile() { Name = m.Name, Guild = m.Guild?.Name ?? "", Title = m.Title ?? "", Kills = m.Kills.ToString(), Fame = m.Fame.ToString(), Karma = m.Karma.ToString() });
            }
            var data = Newtonsoft.Json.JsonConvert.SerializeObject(list);
            var buffer = System.Text.Encoding.UTF8.GetBytes(data);

            response.ContentLength64 = buffer.Length;
            response.OutputStream.Write(buffer, 0, buffer.Length);
            response.OutputStream.Flush();
            response.OutputStream.Close();

        }
    }
}
