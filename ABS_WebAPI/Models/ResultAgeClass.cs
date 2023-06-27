using System.Data;
using System.Reflection;

namespace ABS_WebAPI.Models
{
    public class ResultAgeClass
    {

        public int regionCode { get; set; }

        public string regionName { get; set; }

        public List<PopCategory> pop { get; set; }

        

    }
}
