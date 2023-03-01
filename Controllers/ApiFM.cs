using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using WebAppAPI_FM.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAppAPI_FM.Controllers
{
  //  [Route("api/[controller]")]
    [Route("[controller]")]
    [ApiController]
    public class ApiFM : ControllerBase
    {
        [HttpGet("reg")]
        public IEnumerable<string> Reg()
        {
            return new String[]
            {
                "reg1",
                "reg2"
            };
        }

        [HttpGet("auth")]
        public string Auth()
        {
            return "Auth";
        }

        // GET: api/<ApiFM>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<ApiFM>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<ApiFM>
        [HttpPost]
        public string Post([FromBody] string value)
        {
            return value;
        }

        // PUT api/<ApiFM>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ApiFM>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

        [HttpGet("get-between-dates")]

        public string GetBetweenDate(DateTime start, DateTime end)
        {
            int p1 = 0;
            int p2 = 3;
            //  start = new  DateTime("20.08.2019 00:00:00");
            //  DateTime[] between = JsonSerializer.Deserialize<DateTime[]>(requestData.methodProperties.ToString());

            using FavoriteMasterContext db = new FavoriteMasterContext();

            List<Order> orders = db.Orders
                       .Where(p => p.DateTime >= start && p.DateTime <= end)
                       .Skip(p1)
                       .Take(p2)
                       .OrderBy(p => p.DateTime)
                       .ToList();

            return JsonSerializer.Serialize(orders);
        }

        // POST api/<ApiFM>
        [HttpPost ("add-user")]
        public void AddUser([FromBody] string value)
        {
           
        }

        [HttpGet("get-count-dey")]
        public int GetCountDey()
        {
            return 3;
        }
    }
}
