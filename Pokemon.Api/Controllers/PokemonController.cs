using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Pokemon.Api.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class PokemonController : ControllerBase
    {
        private readonly string _baseUrl;
        public PokemonController(IConfiguration configuration)
        {
            _baseUrl = configuration["BaseUrl"];
        }
        
        //GET:api/pokemon/Listar
        [HttpGet]
        public async Task<IActionResult> Listar()
        {
            var http = new HttpClient();
          var url =   string.Concat(_baseUrl + "pokemon?limit=1200&offset=0");
           var uri =  new Uri(url);
            using (var resposonse = await http.GetAsync(uri))
            {
                if (resposonse.IsSuccessStatusCode)
                {
                    var json = await resposonse.Content.ReadAsStringAsync();

                 var response =   JsonConvert.DeserializeObject<Response>(json);

                    return Ok(response.results);
                }
                
            }
            return BadRequest();
        }
        [HttpPost]
        public async Task<IActionResult> GetPokemonById([FromBody]  Model model)
        {
            var http = new HttpClient();
            var uri = new Uri(model.Url);
            using (var resposonse = await http.GetAsync(uri))
            {
                if (resposonse.IsSuccessStatusCode)
                {
                    var json = await resposonse.Content.ReadAsStringAsync();
                    return Ok(json);
                }

            }
            return BadRequest();
        }

        //public async Task<IActionResult> GetPokemonUrl()
        //{
        //    var ht
        //}
    }
    public class Response
    {
        public int count { get; set; }
        public dynamic next { get; set; }
        public string previous { get; set; }
        public List<Response> results  { get; set; }

        public string Url { get; set; }
        public string Name { get; set; }

    }
    public class Model
    {
        public string Name { get; set; }
        public string Url { get; set; }
    }
}
