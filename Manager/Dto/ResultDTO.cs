using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manager.Dto
{
    public class ResultDTO<T> where T : class
    {
        public int Page { get; set; }
        public List<T> results { get; set; }
        public T Data { get; set; }
        public int total_pages { get; set; }
        public int total_results { get; set; }
        public bool IsStatus { get; set; }
        public DatesDto dates { get; set; }
    }
}
