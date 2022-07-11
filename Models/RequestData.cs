using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAppAPI_FM.Models;

namespace WebAppAPI_FM
{
    public class RequestData
    {
        public string Str { get; set; }
        public ModelName modelName { get; set; }
        public CalledMethod calledMethod { get; set; }
        public object methodProperties { get; set; }
        

        public RequestData(string str) 
        {
            this.Str = str;
        }

        public RequestData()
        {
        }
    }
}
