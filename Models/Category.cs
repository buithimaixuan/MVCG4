using System;
using System.Collections.Generic;

#nullable disable

namespace MVCG4.Models
{
    public partial class Category
    {
        public int CatId { get; set; }
        public string CatName { get; set; }
        public string TypeCategories { get; set; }
        public string CatDescription { get; set; }
    }
}
