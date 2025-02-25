using Microsoft.EntityFrameworkCore;
using WebApplication37.Models;

namespace WebApplication37.Data
{
    public class Mycontext:DbContext
    {
        public Mycontext(DbContextOptions<Mycontext> options)
       : base(options)
        {
        }

        public DbSet<Users> user { get; set; }
        public DbSet<Product> product{ get;set; }
        public DbSet<categies> Cate { get; set; }
        public DbSet<AddTocart>  addtocard{ get; set; }


    }
}
