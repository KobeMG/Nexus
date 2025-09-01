using System;
using System.Collections.Generic;
namespace Proyecto_Nexus.Models
{
    public class ShoppingCart
    {
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public ICollection<CartItem> Items { get; set; }
    }
}
