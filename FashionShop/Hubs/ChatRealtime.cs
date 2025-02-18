using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace FashionShop.Hubs
{
    [HubName("chat")]
    public class ChatRealtime : Hub
    {
        private static readonly HttpClient _httpClient = new HttpClient();
        public async Task Message(string name, string message)
        {
            Clients.All.message(name, message);

            string aiResponse = await GetRasaResponse(message);
            Clients.All.message("Bot", aiResponse);
        }

        private async Task<string> GetRasaResponse(string userMessage)
        {
            var payload = new { sender = "user", message = userMessage };
            var jsonPayload = JsonConvert.SerializeObject(payload);
            var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("http://localhost:5005/webhooks/rest/webhook", content);
            var responseBody = await response.Content.ReadAsStringAsync();

            dynamic result = JsonConvert.DeserializeObject(responseBody);
            return result.Count > 0 ? result[0].text.ToString() : "Xin lỗi, tôi không hiểu!";
        }
        public void Hello()
        {
            Clients.All.hello();
        }

        public void Connect(string name)
        {
            Clients.Caller.connect(name);
        }

        //public void Message(string name, string message)
        //{
        //    Clients.All.message(name, message);
        //}
    }
}