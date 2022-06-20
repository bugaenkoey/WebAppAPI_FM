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
    [ApiController]
    [Route("api/[controller]")]
    public class ValuesController : ControllerBase
    {
        // GET: api/<ValuesController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<ValuesController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<ValuesController>
        [HttpPost]
        /*  public void Post([FromBody] string json)*/
        //   public string Post(string json)
        //    public IEnumerable<string> Post(string json)
        public string Post(string json)
        {
            /*
             RequestData requestData = new("Evgen");
             requestData.modelName = ModelName.Service;
            requestData.calledMethod = CalledMethod.GET;
         requestData.methodProperties = null;

             string json2 = JsonSerializer.Serialize(requestData);
 */
            //    string rd = JsonSerializer.Serialize(requestData);
            //  rd = "{\"apiKey\":\"[ВАШ КЛЮЧ]\",\"modelName\":\"Common\",\"calledMethod\":\"getPalletsList\",\"methodProperties\":{}}";
            //var str = JsonSerializer.Deserialize<RequestData>(json);

            //     return json4;
            /*   
               CalledMethod calledMethod = JsonSerializer.Deserialize<RequestData>(json).calledMethod;
               var properties = JsonSerializer.Deserialize<RequestData>(json).methodProperties;
                       ResponseData responseData = new ResponseData();
   */
            /*     RequestData requestData = new RequestData();
                 requestData.modelName = ModelName.Service;
                 requestData.calledMethod = CalledMethod.GET;
                 requestData.methodProperties =   "it is StringTest!";
                 string strRqDt = JsonSerializer.Serialize(requestData);*/
            //      json = strRqDt;

            RequestData requestData1 = JsonSerializer.Deserialize<RequestData>(json);
            //        switch (JsonSerializer.Deserialize<RequestData>(json).modelName)
            switch (requestData1.modelName)
            {
                case ModelName.Service:
                    /*     responseData.success = true;
                         responseData.data = (object[])ServiceModel(calledMethod, properties);
                         return JsonSerializer.Serialize(responseData);*/
                  return  ServiceModel(requestData1);
                    break;
                case ModelName.User:

                    break;
                case ModelName.Order:
                    break;
                default:
                    // return "responseData";
                    break;
            }
            // return "responseData";
            return "";

        }

        private string ServiceModel(RequestData requestData)
        {
            string response = String.Empty;
            {
                using FavoriteMasterContext db = new FavoriteMasterContext();
                switch (requestData.calledMethod)
                {
                    case CalledMethod.GET:
                        response = JsonSerializer.Serialize(db.Services.ToList());
                        break;
                    case CalledMethod.POST:



                    default:
                        response = "Bed request...";
                        break;

                }
            }

            return response;
        }

        private IEnumerable<Service> GetService()
        {
            using FavoriteMasterContext db = new FavoriteMasterContext();
            return db.Services.ToList();
        }



        // PUT api/<ValuesController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
