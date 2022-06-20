using WebAppAPI_FM.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAppAPI_FM.Controllers
{
    [ApiController]
    [Route("Order")]
    public class OrderController : ControllerBase
    {
        // GET: api/<OrderController>
        [HttpGet]
        public IEnumerable<Order> Get()
        {
            using FavoriteMasterContext db = new FavoriteMasterContext();
            return db.Orders.ToList();
        }

        // GET api/<OrderController>/5
        [HttpGet("{id}")]
        public Order Get(int id)
        {

            using FavoriteMasterContext db = new FavoriteMasterContext();
            return db.Orders.Find(id);
        }

        // POST api/<OrderController>
        [HttpPost]
        public void Post([FromBody] Order order)
        {
            using FavoriteMasterContext db = new FavoriteMasterContext();
            db.Orders.Add(order);
            db.SaveChanges();
        }

        // PUT api/<OrderController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<OrderController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            using FavoriteMasterContext db = new FavoriteMasterContext();
            
                Order order= db.Orders.Find(id);
                // получаем первый объект
                if (order != null)
                {
                    //удаляем объект
                    db.Orders.Remove(order);
                    db.SaveChanges();
                }
            
        }

        [HttpPut]
        public void Put([FromBody] Order order)
        {
            using FavoriteMasterContext db = new FavoriteMasterContext();
            // получаем первый объект
            Order orderR = db.Orders.Find(order.Id);
            if (orderR != null)
            {
                orderR.User = order.User;
                orderR.Service = order.Service;
                orderR.Note = order.Note;
                orderR.DateTime = order.DateTime;
                orderR.Done = order.Done;
                orderR.Comment = order.Comment;
               
                //обновляем объект
                db.Orders.Update(orderR);
                db.SaveChanges();
            }
        }
    }
}
