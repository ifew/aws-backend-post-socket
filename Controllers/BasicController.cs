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
    public class BasicController : ControllerBase
    {        

        public IConfiguration _configuration { get; }
        public BasicController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public async Task<string> Get()
        {
            var endpointUri = _configuration["AWS:urlAPIGw"];

            var signer = new AWS4RequestSigner(_configuration["AWS:Key"], _configuration["AWS:Secret"]);
            var request = new HttpRequestMessage {
                Method = HttpMethod.Get,
                RequestUri = new Uri(endpointUri)
            };

            request = await signer.Sign(request, _configuration["AWS:Service"], _configuration["AWS:Region"]);

            var client = new HttpClient();
            var response = await client.SendAsync(request);

            var responseStr = await response.Content.ReadAsStringAsync();

            return responseStr;
            
        }
    }
}
