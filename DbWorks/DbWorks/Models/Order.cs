using System;
using System.Collections.Generic;

namespace DbWorks.Models
{
    public partial class Order
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public decimal Sum { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual Manager Manager { get; set; }
        public virtual ICollection<Product> Products { get; set; }

        public Order()
        {
            Products = new HashSet<Product>();
        }
    }
}