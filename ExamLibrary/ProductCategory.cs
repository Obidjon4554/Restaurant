﻿namespace ExamLibrary
{
    public class ProductCategory
    {
        public int CategoryId { get; set; }
        public int ProductId { get; set; }
        public Category Category { get; set; }
        public Product Product { get; set; }

    }
}
