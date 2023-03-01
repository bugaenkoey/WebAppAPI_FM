using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
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


        // POST api/<ValuesController>
        [HttpPost]
        /*  public void Post([FromBody] string json)*/
        //    public IEnumerable<string> Post(string json)
        public string Post(string json)
        // public ResponseData Post(string json)
        {
           
            RequestData requestData = JsonSerializer.Deserialize<RequestData>(json);

            // ModelName modelName = ParceModelName(requestData);


            using FavoriteMasterContext db = new FavoriteMasterContext();
            IEntityType entityType = db.Model.FindEntityType(typeof(IEntity));

            DbSet<IEntity> vr = (DbSet<IEntity>)db.Model.FindEntityType($"Orders");

            string response = String.Empty;
            switch (ParseModelName(requestData))
            {
                case ModelName.Service:
                    response = ModelService(requestData);
                    break;
                case ModelName.User:
                    response = ModelUser(requestData);
                    break;
                case ModelName.Order:
                    response = ModelOrder(requestData);
                    break;
                default:
                    response = "responseData Error";
                    break;
            }
            return response;
        }
        private ModelName ParseModelName(RequestData requestData)
        {
            ModelName modelName;
            try
            {
                modelName = (ModelName)Enum.Parse(typeof(ModelName), requestData.modelName);
            }
            catch (Exception)
            {
                modelName = ModelName.Error;
                throw new Exception("Enum.IsDefined(typeof(ModelName) = true");
            }

            return modelName;
        }

        private CalledMethod ParseCalledMethod(RequestData requestData)
        {
            CalledMethod method;
            try
            {
                method = (CalledMethod)Enum.Parse(typeof(CalledMethod), requestData.calledMethod);
            }
            catch (Exception)
            {
                //method = CalledMethod.Error;
                Console.WriteLine("ERROR CalledMethod = Not found");
                //return "ERROR CalledMethod = Not found";
                method = CalledMethod.Error;

            }
            return method;
        }
        private string ModelOrder(RequestData requestData)
        {

            //  CalledMethod method = ParseCalledMethod(requestData);

            string response = String.Empty;

            Order order = JsonSerializer.Deserialize<Order>(requestData.methodProperties.ToString());

            using FavoriteMasterContext db = new FavoriteMasterContext();
            var table = db.Orders;

            {

                switch (ParseCalledMethod(requestData))
                {
                    case CalledMethod.Get:
                      //  response = JsonSerializer.Serialize(table.ToList());
                        response = Get(requestData);
                        break;

                    case CalledMethod.GetId:
                        response = JsonSerializer.Serialize(table.Find(order.Id));
                        break;

                    case CalledMethod.Add:
                        table.Add(order);
                        db.SaveChanges();
                        response = JsonSerializer.Serialize(table.Find(order.Id));
                        break;

                    case CalledMethod.Del:
                        Order orderR = table.Find(order.Id);

                        if (orderR != null)
                        {
                            table.Remove(orderR);
                            db.SaveChanges();
                        }

                        if (table.Find(order.Id) == null)
                        {
                            response = $"{order.Id} Deleted";
                        }
                        break;

                    case CalledMethod.Edit:

                        Order orderE = table.Find(order.Id);
                        if (orderE != null)
                        {
                            orderE.Service = order.Service;
                            orderE.ServiceId = order.ServiceId;
                            orderE.User = order.User;
                            orderE.UserId = order.UserId;
                            orderE.Done = order.Done;
                            orderE.DateTime = order.DateTime;
                            orderE.Note = order.Note;
                            orderE.Comment = order.Comment;

                            table.Update(order);
                            db.SaveChanges();

                            response = JsonSerializer.Serialize(table.Find(order.Id));
                        }
                        break;

                    case CalledMethod.GetBetweenDates:
                        //     response = "TO DO GetBetweenDates ...";

                           response = GetBetweenDates(requestData);

                        break;

                    case CalledMethod.Error:
                        response = "Error Method...";
                        break;

                    default:
                        response = "Bed request...";
                        break;
                }

            }

            return response;
        }

        private string Get(RequestData requestData)
        {

            Order order = JsonSerializer.Deserialize<Order>(requestData.methodProperties.ToString());

            using FavoriteMasterContext db = new FavoriteMasterContext();



            var table = db.Orders;

            return JsonSerializer.Serialize(table.ToList());
        }


            private string GetBetweenDates(RequestData requestData)
        {
            DateTime[] between = JsonSerializer.Deserialize<DateTime[]>(requestData.methodProperties.ToString());
            
            using FavoriteMasterContext db = new FavoriteMasterContext();

            List<Order> orders =  db.Orders
                       .Where(p => p.DateTime >= between[0] && p.DateTime <= between[1])
                       .ToList();

            return JsonSerializer.Serialize(orders);
        }

        private string ModelUser(RequestData requestData)
        {


            string response = String.Empty;

            User user = JsonSerializer.Deserialize<User>(requestData.methodProperties.ToString());

            using FavoriteMasterContext db = new FavoriteMasterContext();
            var table = db.Users;

            {

                switch (ParseCalledMethod(requestData))
                {
                    case CalledMethod.Get:
                        response = JsonSerializer.Serialize(table.ToList());
                        break;

                    case CalledMethod.GetId:
                        response = JsonSerializer.Serialize(table.Find(user.Id));
                        break;

                    case CalledMethod.Add:
                        table.Add(user);
                        db.SaveChanges();
                        response = JsonSerializer.Serialize(table.Find(user.Id));
                        break;

                    case CalledMethod.Del:
                        User userR = table.Find(user.Id);

                        if (userR != null)
                        {
                            table.Remove(userR);
                            db.SaveChanges();
                        }

                        if (table.Find(user.Id) == null)
                        {
                            response = $"{user.Id} Deleted";
                        }
                        break;

                    case CalledMethod.Edit:
                        User userE = table.Find(user.Id);
                        if (userE != null)
                        {
                            userE.Name = user.Name;
                            userE.surname = user.surname;
                            userE.phone = user.phone;
                            userE.orders = user.orders;

                            table.Update(user);
                            db.SaveChanges();

                            response = JsonSerializer.Serialize(table.Find(user.Id));
                        }
                        break;

                    //case CalledMethod.GetBetweenDates:

                    //    response = "TO DO GetBetweenDates ...";
                    //    break;

                    case CalledMethod.Error:
                        response = "Error Method...";
                        break;

                    default:
                        response = "Bed request...";
                        break;
                }

            }

            return response;
        }

        private string ModelService(RequestData requestData)
        {


            string response = String.Empty;

            Service service = JsonSerializer.Deserialize<Service>(requestData.methodProperties.ToString());

            using FavoriteMasterContext db = new FavoriteMasterContext();
            var table = db.Services;

            {

                switch (ParseCalledMethod(requestData))
                {
                    case CalledMethod.Get:
                        response = JsonSerializer.Serialize(table.ToList());
                        break;

                    case CalledMethod.GetId:
                        response = JsonSerializer.Serialize(table.Find(service.Id));
                        break;

                    case CalledMethod.Add:
                        table.Add(service);
                        db.SaveChanges();
                        response = JsonSerializer.Serialize(table.Find(service.Id));
                        break;

                    case CalledMethod.Del:
                        Service serviceR = table.Find(service.Id);

                        if (serviceR != null)
                        {
                            table.Remove(serviceR);
                            db.SaveChanges();
                        }

                        if (table.Find(service.Id) == null)
                        {
                            response = $"{service.Id} Deleted";
                        }
                        break;

                    case CalledMethod.Edit:
                        Service serviceE = table.Find(service.Id);
                        if (serviceE != null)
                        {

                            serviceE.Name = service.Name;
                            serviceE.Description = service.Description;
                            serviceE.Price = service.Price;
                            serviceE.Duration = service.Duration;


                            table.Update(serviceE);
                            db.SaveChanges();

                            response = JsonSerializer.Serialize(table.Find(service.Id));
                        }
                        break;

                    //case CalledMethod.GetBetweenDates:

                    //    response = "TO DO GetBetweenDates ...";
                    //    break;

                    case CalledMethod.Error:
                        response = "Error Method...";
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
