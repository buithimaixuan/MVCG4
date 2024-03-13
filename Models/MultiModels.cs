using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCG4.Models
{
    public class MultiModels
    {
        public IEnumerable<Order> orderList { get; set; }
        public IEnumerable<Cart> cartList { get; set; }
        public IEnumerable<OrderDetail> orderDtList { get; set; }
        public IEnumerable<Product> productList { get; set; }
        public IEnumerable<Category> categoryList { get; set; }
        public OrderDetail getOdt { get; set; }
        public Product getProduct { get; set; }
        public Category getCategory { get; set; }
        public Order getOrder { get; set; }
        public Account getAccount { get; set; }
    }
}