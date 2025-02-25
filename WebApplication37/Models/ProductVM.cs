using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApplication37.Models
{
    public class ProductVM
    {
        public int Id { get; set; }


        public string Name { get; set; }


        public string Description { get; set; }

        public decimal Price { get; set; }

        public IFormFile image { get; set; }
        public int CategoryId { get; set; }


    }
}
