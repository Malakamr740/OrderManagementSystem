﻿namespace OrderCore.DTO.Request
{
    public class ProductDTO
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public string Status { get; set; } = string.Empty;
        public double Amount { get; set; }
        public bool IsDeleted { get; set; }
    }
}
