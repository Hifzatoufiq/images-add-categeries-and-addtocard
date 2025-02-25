using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApplication37.Models
{
   
        public class Product
        {
            public int Id { get; set; }

          
            public string Name { get; set; }


            public string Description { get; set; }

            public decimal Price { get; set; }

            // If you want to include an image, add this property
            public string ImagePath { get; set; }
        public categies category { get; set; }
        public int CategoryId { get; set; }
 
    }
    }
