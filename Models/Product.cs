using System;
using System.Collections.Generic;

#nullable disable

namespace MVCG4.Models
{
    public partial class Product
    {
        public int ProId { get; set; }
        public int CatId { get; set; }
        public string ProName { get; set; }
        public string ProImage { get; set; }
        public string Origin { get; set; }
        public string Brand { get; set; }
        public int ProQuantity { get; set; }
        public decimal ProPrice { get; set; }
        public decimal Discount { get; set; }
        public string ProDescription { get; set; }
        public DateTime CreateDate { get; set; }
        public int IsDelete { get; set; }
    }
}
