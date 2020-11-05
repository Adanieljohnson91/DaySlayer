using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using DaySlayer.Models;
using Newtonsoft.Json;
using System.Text;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DaySlayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NodeController : ControllerBase
    {

        public string URL = "http://localhost:8080";
        static readonly HttpClient client = new HttpClient();
        // GET api/node/5
        [HttpPost("{id}")]
        public async Task<IActionResult> Get(int id, History history)
        {
            string json = JsonConvert.SerializeObject(history);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync($"{URL}/{id}", httpContent);
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();
            return Content(responseBody, "application/json");
        }
    }
}
