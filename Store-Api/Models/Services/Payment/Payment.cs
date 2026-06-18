using Newtonsoft.Json;
using StoreApi.Models.Classes.Payment;

namespace StoreApi.Models.Services.Payment
{
    public class Payment
    {
        public static async Task<PaymentVerify> PaymentVerify(string trackId)
        {
            using var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Post, "https://gateway.zibal.ir/v1/verify");
            request.Content = new StringContent(
            "{'merchant': 'zibal',  'trackId':" + trackId,
            System.Text.Encoding.UTF8, "application/json");
            var response = await client.SendAsync(request);
            Console.Write(response.Content.ReadAsStringAsync());
            PaymentVerify? res =  JsonConvert.DeserializeObject<PaymentVerify>(await response.Content.ReadAsStringAsync());
            return res!;
        }
    }
}
