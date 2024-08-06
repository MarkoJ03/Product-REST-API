using System.Text.Json.Serialization;

namespace DataAccessLayer.Model
{
    public class Product
    {

        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public float Price { get; set; }
        public string Description { get; set; } = string.Empty;

        [JsonIgnore]
        public ICollection<UserProduct> UserProducts { get; set; }


        public Product(int id, string name, float price, string description)
        {
            Id = id;
            Name = name;
            Price = price;
            Description = description;
        }

        public Product() { }
    }

}
