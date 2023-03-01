using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAppAPI_FM.Models;

namespace WebAppAPI_FM
{
    public class RequestData
    {
        public string apiKey { get; set; }
      //  public ModelName modelName { get; set; }
        public string modelName { get; set; }

        public string calledMethod { get; set; }

   /*     public CalledMethod calledMethod //{ get; set; }
        {
            get { return calledMethod; }
            set
            {

                // if (Enum.IsDefined(typeof(CalledMethod), value)) {
                try
                {
                    calledMethod = (CalledMethod)Enum.Parse(typeof(CalledMethod), value.ToString());
                }
                catch (Exception)
                {
                    calledMethod = CalledMethod.Error;
                    Console.WriteLine(calledMethod);
                    throw new Exception("Enum.IsDefined(typeof(CalledMethod) = true");
                }
                Console.WriteLine(calledMethod);
            }
        }
        */
        public object methodProperties { get; set; }
        

        public RequestData(string apiKey) 
        {
            this.apiKey = apiKey;
        }

        public RequestData()
        {
        }

      //  Enum.IsDefined(typeof(PetType), value)

        //public RequestData(string calledMethod)
        //{
        //   // this.CalledMethod = (CalledMethod)Enum.Parse(typeof(CalledMethod), calledMethod);
        //}
    }
}
