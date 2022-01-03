using System.Collections.Generic;

namespace DbWorks.Models
{
    public class Manager
    {
        public int Id { get; set; }
        public string LastName { get; set; }
        public virtual ICollection<Order> Orders { get; set; }

        public Manager()
        {
            Orders = new HashSet<Order>();
        }
    }
}
