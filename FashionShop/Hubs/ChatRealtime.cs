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

            var response = await _httpClient.PostAsync("http://127.0.0.1:8000/chat", content);
            var responseBody = await response.Content.ReadAsStringAsync();

            dynamic result = JsonConvert.DeserializeObject(responseBody);
            // Kiểm tra phần response của biến result có null không, nếu không trả về giá trị response theo data. (data['response'])
            // Biến result của mô hình Nino là dạng dữ liệu json, không phải dạng số lượng trong list hoặc dict nên không thể count, chỉ có thể trả ra giá trị yêu cầu
            // VD: result của input:'hi' là output: {'response':'Xin chào'} -> cần lấy ra giá trị của key response.
            return result.response != null ? result.response.ToString() : "Xin lỗi, tôi không hiểu!";
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