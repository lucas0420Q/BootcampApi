using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public string ProductType { get; set; }
        public ICollection<Request> Requests { get; set; } = new List<Request>();
    }
}
