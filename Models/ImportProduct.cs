using System;
using System.Collections.Generic;

#nullable disable

namespace MVCG4.Models
{
    public partial class ImportProduct
    {
        public int ImpId { get; set; }
        public int SupId { get; set; }
        public int ProId { get; set; }
        public int Quantity { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
