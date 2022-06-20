using WebAppAPI_FM.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAppAPI_FM.Controllers
{
    // [Route("api/[controller]")]
    [ApiController]
    [Route("user")]
    public class UserController : ControllerBase
    {
        // GET: api/<UserController1>
        [HttpGet]
        //public string[] Get()
        public IEnumerable<User> Get()
        {

            using FavoriteMasterContext db = new FavoriteMasterContext();
            return db.Users.ToList();
        }

        // GET api/<UserController1>/5
        [HttpGet("{id}")]
        public User Get(int id)
        {
            using FavoriteMasterContext db = new FavoriteMasterContext();
            return db.Users.Find(id);
        }

        [HttpGet("{phone}")]
        public User Get(string phone)
        {
            using FavoriteMasterContext db = new FavoriteMasterContext();
            return db.Users.Find(phone);
        }

        //[HttpGet("{phone}")]
        //public int Get(int phone)
        //{
        //    using FavoriteMasterContext db = new FavoriteMasterContext();
        //    int id = db.Users.Find(phone).Id;
        //    return id;
        //}

        /*using (var context = new BloggingContext())
{
    // Query for all blogs with names starting with B
    var blogs = from b in context.Blogs
                   where b.Name.StartsWith("B")
                   select b;

    // Query for the Blog named ADO.NET Blog
    var blog = context.Blogs
                    .Where(b => b.Name == "ADO.NET Blog")
                    .FirstOrDefault();
}
         */

        // POST api/<UserController1>
        [HttpPost]
        public User Post([FromBody] User user)
        {
            using FavoriteMasterContext db = new FavoriteMasterContext();
            db.Users.Add(user);
            db.SaveChanges();

            return user;
        }
        //public int Post([FromBody] User user)
        //{
        //    using FavoriteMasterContext db = new FavoriteMasterContext();
        //    db.Users.Add(user);
        //    db.SaveChanges();

        //    var newUser = user.Id;
        //    return newUser;
        //}


        // PUT api/<UserController1>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {


        }


        [HttpPut]
        public void Put([FromBody] User user)
        {
            using FavoriteMasterContext db = new FavoriteMasterContext();
            // получаем первый объект
            User userR = db.Users.Find(user.Id);
            if (userR != null)
            {
                userR.Name = user.Name;
                userR.surname = user.surname;
                userR.phone = user.phone;


                //обновляем объект
                db.Users.Update(userR);
                db.SaveChanges();
            }
        }

        // DELETE api/<UserController1>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            using FavoriteMasterContext db = new FavoriteMasterContext();

            User user = db.Users.Find(id);
            // получаем первый объект
            // User user = db.Users.FirstOrDefault();
            if (user != null)
            {
                //удаляем объект
                db.Users.Remove(user);
                db.SaveChanges();
            }

        }
    }
}
