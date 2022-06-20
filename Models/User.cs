using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAppAPI_FM.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string surname { get; set; }
        public int phone { get; set; }
        public ICollection<Order> orders { get; set; }
        public User()
        {
            orders = new List<Order>();
        }
        
    }
}
