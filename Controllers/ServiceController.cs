using WebAppAPI_FM.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAppAPI_FM.Controllers
{
    [Route("Service")]
    [ApiController]
    public class ServiceController : ControllerBase
    {
        // GET: api/<ServiceController>
        [HttpGet]
        public IEnumerable<Service> Get()
        {

            using FavoriteMasterContext db = new FavoriteMasterContext();
            return db.Services.ToList();
        }

        // GET api/<ServiceController>/5
        [HttpGet("{id}")]
        public Service Get(int id)
        {
            using FavoriteMasterContext db = new FavoriteMasterContext();
            return db.Services.Find(id);
        }

        // POST api/<ServiceController>
        [HttpPost]
        public void Post([FromBody] Service service)
        {
            using FavoriteMasterContext db = new FavoriteMasterContext();
            db.Services.Add(service);
            db.SaveChanges();
        }

        // PUT api/<ServiceController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ServiceController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            using FavoriteMasterContext db = new FavoriteMasterContext();
            Service service = db.Services.Find(id);
            // получаем первый объект
            if (service != null)
            {
                //удаляем объект
                db.Services.Remove(service);
                db.SaveChanges();
            }
        }

        [HttpPut]
        public void Put([FromBody] Service service)
        {
            using FavoriteMasterContext db = new FavoriteMasterContext();
            // получаем первый объект
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
            }
        }
    }
}
