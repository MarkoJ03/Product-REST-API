using DataAccessLayer.Model;
using System.Text.Json.Serialization;

namespace BusinessLogicLayer.DTOs
{
    public class ProductDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public float Price { get; set; }
        public string Description { get; set; } = string.Empty;

        [JsonIgnore]
        public ICollection<UserProduct> UserProducts { get; set; } = new List<UserProduct>();




        public ProductDTO(int id, string name, float price, string description)
        {
            Id = id;
            Name = name;
            Price = price;
            Description = description;
        }
        
        public ProductDTO() { }
    }
}

