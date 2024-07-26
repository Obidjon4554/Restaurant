namespace ExamLibrary
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
       public List<ProductCategory> ProductCategory = new List<ProductCategory>();
        
    }
}
