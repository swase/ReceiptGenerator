using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestaurantData
{
    public class Customer
    {
        [Key]
        public int CustomerID { get; set; }
        public virtual Order Order { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}