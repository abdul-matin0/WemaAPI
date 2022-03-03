using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using WemaAPI.BankService.Helper;
using static WemaAPI.BankService.Helper.BankData;

namespace WemaAPI.BankService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BankController : ControllerBase
    {
        APISettings _apiSettings;

        public BankController(IOptions<APISettings> apiSettings)
        {
            _apiSettings = apiSettings.Value;
        }


        // GET: api/<BankController>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {

            // send get request to GetBanks endpoint
            HttpClient client = new HttpClient();

            client.BaseAddress = new Uri("https://wema-alatdev-apimgt.azure-api.net/alat-test/api/Shared/");
            client.DefaultRequestHeaders
                  .Accept
                  .Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, "GetAllBanks");
            request.Headers.Add("Ocp-Apim-Subscription-Key", $"{_apiSettings.PublicKey}");

            HttpResponseMessage response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                // process json
                string jsonresult = await response.Content.ReadAsStringAsync();
                Rootobject result = JsonConvert.DeserializeObject<Rootobject>(jsonresult);

                return Ok(result);
            }


            return StatusCode(StatusCodes.Status400BadRequest);

        }

    }
}
