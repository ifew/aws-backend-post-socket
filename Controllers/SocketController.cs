using System;
using System.Net.Http;
using System.Threading.Tasks;
using Aws4RequestSigner;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace aws_backend_post_websocket.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SocketController : ControllerBase
    {        

        public IConfiguration _configuration { get; }
        public SocketController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost]
        public async Task<string> Post(InputModel input)
        {
            var endpointUri = _configuration["AWS:urlSocketAPI"] + input.connection_id;

            var signer = new AWS4RequestSigner(_configuration["AWS:Key"], _configuration["AWS:Secret"]);
            var request = new HttpRequestMessage {
                Method = HttpMethod.Post,
                RequestUri = new Uri(endpointUri),
                Content = new StringContent(input.message)
            };

            request = await signer.Sign(request, _configuration["AWS:Service"], _configuration["AWS:Region"]);

            var client = new HttpClient();
            var response = await client.SendAsync(request);

            var responseStr = await response.Content.ReadAsStringAsync();

            return responseStr;
            
        }
    }

    public class InputModel
    {
        public string connection_id { get; set;}
        public string message { get; set;}
    }
}
