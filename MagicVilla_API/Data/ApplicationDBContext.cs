using MagicVilla_API.Models;
using Microsoft.EntityFrameworkCore;

namespace MagicVilla_API.Data
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {
            
        }

        public DbSet<Villa> Villas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //seed data
            modelBuilder.Entity<Villa>().HasData(
                new Villa()
                {
                    Id = 1,
                    Name = "Aarya inn",
                    Details = "Some dummy meanighless text",
                    ImageUrl = "https://images.unsplash.com/photo-1679678690998-88c8711cbe5f?w=600&auto=format&fit=crop&q=60&ixlib=rb-4.0.3&ixid=M3wxMjA3fDF8MHxzZWFyY2h8MXx8aG90ZWx8ZW58MHx8MHx8fDA%3D",
                    Occupancy = 5,
                    Rate = 2500,
                    Sqft = 200,
                    Amenity = "wifi",
                    CreatedDate= DateTime.Now
                },
                new Villa()
                                {
                                    Id = 2,
                                    Name = "People hotel",
                                    Details = "Some dummy meanighless text",
                                    ImageUrl = "https://plus.unsplash.com/premium_photo-1678240508014-d1ab7345bfe6?w=600&auto=format&fit=crop&q=60&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8NXx8aG90ZWx8ZW58MHx8MHx8fDA%3D",
                                    Occupancy = 15,
                                    Rate = 500,
                                    Sqft = 200,
                                    Amenity = "parking",
                                    CreatedDate= DateTime.Now
                                },
                new Villa()
                                                {
                                                    Id = 3,
                                                    Name = "Mriott",
                                                    Details = "Some dummy meanighless text",
                                                    ImageUrl = "https://plus.unsplash.com/premium_photo-1675745329954-9639d3b74bbf?w=600&auto=format&fit=crop&q=60&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8Mnx8aG90ZWx8ZW58MHx8MHx8fDA%3D",
                                                    Occupancy = 55,
                                                    Rate = 8500,
                                                    Sqft = 9870,
                                                    Amenity = "all",
                                                    CreatedDate = DateTime.Now
                                                }
                );

        }
    }
}
