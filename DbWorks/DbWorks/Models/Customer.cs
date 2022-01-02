using System.Collections.Generic;

namespace DbWorks.Models
{
    public partial class Customer
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; private set; }
        public virtual ICollection<Product> Products { get; set; }

        public Customer()
        {
            Products = new HashSet<Product>();
        }
    }
}