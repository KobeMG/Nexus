﻿namespace Proyecto_Nexus.ViewModels
{
    public class CartViewModel
    {
        public List<CartItemViewModel> CartItems { get; set; }
        public decimal GrandTotal { get; set; }
    }
}