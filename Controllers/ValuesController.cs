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
     // public ResponseData Post(string json)
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

            string response = String.Empty;
            RequestData requestData1 = JsonSerializer.Deserialize<RequestData>(json);
            //        switch (JsonSerializer.Deserialize<RequestData>(json).modelName)
            switch (requestData1.modelName)
            {
                case ModelName.Service:
                    /*     responseData.success = true;
                         responseData.data = (object[])ServiceModel(calledMethod, properties);
                         return JsonSerializer.Serialize(responseData);*/

                    response= ServiceModel(requestData1);

                    break;
                case ModelName.User:
                    response= UserModel(requestData1);
                    break;
                case ModelName.Order:
                    response = OrderModel(requestData1);
                    break;
                default:
                    response= "responseData Default";
                    break;
            }
            // return "responseData";
            return response;

        }

        private string OrderModel(RequestData requestData)
        {
            string response = String.Empty;
            {
                using FavoriteMasterContext db = new FavoriteMasterContext();
                switch (requestData.calledMethod)
                {
                    case CalledMethod.Get:
                        response = JsonSerializer.Serialize(db.Orders.ToList());
                        break;
                    case CalledMethod.Add:
                        response = "TO DO ";
                        break;

                    default:
                        response = "Bed request...";
                        break;
                }
            }
            return response;
        }

        private string UserModel(RequestData requestData)
        {
            string response = String.Empty;
            {
                using FavoriteMasterContext db = new FavoriteMasterContext();
                switch (requestData.calledMethod)
                {
                    case CalledMethod.Get:
                        response = JsonSerializer.Serialize(db.Users.ToList());
                        break;
                    case CalledMethod.Add:
                        response = "TO DO ";
                        break;

                    default:
                        response = "Bed request...";
                        break;
                }
            }
            return response;
        }

        private string ServiceModel(RequestData requestData)
        {
            string response = String.Empty;
        //  string obj=  requestData.methodProperties;
            Service service = JsonSerializer.Deserialize<Service>(requestData.methodProperties.ToString());
            {
                using FavoriteMasterContext db = new FavoriteMasterContext();
               
                switch (requestData.calledMethod)
                {
                    case CalledMethod.Get:

                        response = JsonSerializer.Serialize(db.Services.ToList());
                        break;
                    case CalledMethod.GetId:
                        response = JsonSerializer.Serialize(db.Services.Find(service.Id));
                        break;
                    case CalledMethod.Add:
                        db.Services.Add(service);
                        db.SaveChanges();
                        response = JsonSerializer.Serialize(db.Services.ToList());
                        break;
                    case CalledMethod.Del:
                        Service serviceid = db.Services.Find(service.Id);
                        // получаем первый объект
                        if (serviceid != null)
                        {
                            //удаляем объект
                            db.Services.Remove(serviceid);
                            db.SaveChanges();
                        }
                        if (db.Services.Find(service.Id)==null)
                        {
                            response = $"{service.Id} Deleted";
                        }
                        break;
                    case CalledMethod.Edit:

                        Service serviceR = db.Services.Find(service.Id);
                        if (serviceR != null)
                        {
                            serviceR.Name = service.Name;
                            serviceR.Description = service.Description;
                            serviceR.Price = service.Price;
                            serviceR.Duration = service.Duration;

                            //обновляем объект
                            db.Services.Update(serviceR);
                            db.SaveChanges();
                       // response = $"{service.Id} Update";

                            response = JsonSerializer.Serialize(db.Services.Find(service.Id));
                        }
                        break;
                    case CalledMethod.GetBetweenDates:

                        response = "TO DO";
                        break;
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
