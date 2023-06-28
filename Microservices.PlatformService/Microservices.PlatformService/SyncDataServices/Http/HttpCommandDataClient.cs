using Microservices.PlatformService.Models.DTO;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Reflection.Metadata;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Microservices.PlatformService.SyncDataServices.Http
{
    public class HttpCommandDataClient : ICommandDataClient
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public HttpCommandDataClient(HttpClient httpCLient, IConfiguration configuration)
        {
            _httpClient = httpCLient;
            _configuration = configuration;
        }

        public async Task SendPlatformToCommand(PlatformReadDTO model)
        {
            /* var httpContent = new StringContent(
                 JsonSerializer.Serialize(model),
                 Encoding.UTF8,
                 "application/json"
                 );*/
            var httpContent = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
            var commandEndpoint = _configuration["CommandService"];
            try
            {
                var response = await _httpClient.PostAsync(commandEndpoint, httpContent);

                if (response.IsSuccessStatusCode)
                    Console.WriteLine("---> Sync Post to CommandService was OK!");
                else
                    Console.WriteLine("---> Sync Post to CommandService was NOT OK!");

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            
        }
    }
}
