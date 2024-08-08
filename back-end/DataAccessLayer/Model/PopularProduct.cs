using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Model
{
    public class PopularProduct
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int AssignmentsCount { get; set; }
    }
}
