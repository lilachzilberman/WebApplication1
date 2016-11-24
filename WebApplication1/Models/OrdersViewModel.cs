namespace WebApplication1.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class OrdersViewModel
    {
        public string Email { get; set; }

        public int OrderId { get; set; }

        public DateTime OrderDate { get; set; }
        
        public string ProductName { get; set; }

        public int Quantity { get; set; }
    }
}
