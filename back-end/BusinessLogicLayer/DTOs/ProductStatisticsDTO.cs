using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.DTOs
{
    public class ProductStatisticsDTO
    {
        public int TotalProducts { get; set; }
        public double AveragePrice { get; set; }
        public float MinPrice { get; set; }
        public float MaxPrice { get; set; }
        public int TotalAssignments { get; set; }
    }
}
