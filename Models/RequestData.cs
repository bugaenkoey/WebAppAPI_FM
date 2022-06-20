using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAppAPI_FM
{
    public class RequestData
    {
        public string str { get; set; }
        public ModelName modelName { get; set; }
        public CalledMethod calledMethod { get; set; }
        public object methodProperties { get; set; }

        public RequestData(string str) 
        {
            this.str = str;
        }

        public RequestData()
        {
        }
    }
}
