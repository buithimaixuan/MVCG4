using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace MVCG4.Models
{
    public partial class Order
    {
        public int OId { get; set; }
        public int CusId { get; set; }
        public string Payment { get; set; }
        public string Address { get; set; }
        public string Status { get; set; }
        [DataType(DataType.Date)]
        public DateTime ODate { get; set; }
        public decimal TotalPrice { get; set; }
        public int IsDelete { get; set; }
    }
}
