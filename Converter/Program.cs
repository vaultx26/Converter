using System;
using System.Net.Http;
using System.Text.Json;


namespace Converter
{
    public class Response
    {
        public Dictionary<string, double> conversion_rates { get; set; }
    }
    class Convert
    {
        static HttpClient client = new HttpClient();
        static async Task<Response> ConvertAsync(string argument)
        {
            Response response = null;
            HttpResponseMessage responseMessage = await client.GetAsync("https://v6.exchangerate-api.com/v6/f067e2d7a4ffb66acce1629d/latest/" + argument);
            if(responseMessage.IsSuccessStatusCode)
            {
                String content = await responseMessage.Content.ReadAsStringAsync();
                response = JsonSerializer.Deserialize<Response>(content);
            }
            return response;
        }
        static async Task Main(string[] args)
        {
            if(args.Length != 2)
            {
                return;
            }
            Response response = await ConvertAsync(args[0]);
            String second = args[1];
            Console.WriteLine("Convert " + args[0] +  " to " + args[1] + " : " + response.conversion_rates[second]);

        }
    }
    
}
