using System;
using System.Collections.Generic;

#nullable disable

namespace MVCG4.Models
{
    public partial class Cart
    {
        public Cart(){
        }
        public int? AccId { get; set; }
        public int ProId { get; set; }
        public int ProQuantity { get; set; }
        public decimal CartPrice { get; set; }
    }
}
