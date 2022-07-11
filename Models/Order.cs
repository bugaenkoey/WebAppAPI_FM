using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAppAPI_FM.Models
{
    public class Order : IEntity
    {
        public int Id { get; set; }
        public Service Service { get; set; }
        public int? ServiceId { get; set; }
        public int? UserId { get; set; }
        public User User { get; set; }
        public bool Done { get; set; } = false;
        public DateTime? DateTime { get; set; }
        public string? Note { get; set; }
        public string? Comment { get; set; }
       // public int Id { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
}
