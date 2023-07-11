using ApiGatewayService.Models;
using ApiGatewayService.Models;

namespace ApiGatewayService.ResponseModels
{
    public class ProductGetResponse
    {
       
       
        public int Id { get; set; }

       
        public Category Category { get; set; }

       
        public string Name { get; set; }

       
        public double Price { get; set; }

        public string Description { get; set; }

       
        public string Address { get; set; }

        public DateTime CreatedAt { get; set; }


        public IEnumerable<ProductImage> Images { get; set; }


        public ProductGetResponse(int id, Category category, double price, string description, string address,string name, IEnumerable<ProductImage> images)
        {
            Id = id;
            Category = category;
            Price = price;
            Description = description;
            Address = address;
            Images = images;
            Name = name;
        }
    }
}
