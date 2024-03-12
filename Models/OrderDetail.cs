using System;
using System.Collections.Generic;

#nullable disable

namespace MVCG4.Models
{
    public partial class OrderDetail
    {
        public int OId { get; set; }
        public int ProId { get; set; }
        public int Quantity { get; set; }
    }
}
