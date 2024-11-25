namespace Core.Models
{
    public class Product
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public string? ImageUrl { get; set; }

        public Product() { }

        public Product(string id, string name, string description, double price)
        {
            Id = id;
            Name = name;
            Description = description;
            Price = price;
        }

        public Product(string name, string description, double price)
        {
            Name = name;
            Description = description;
            Price = price;
        }
    }
}
