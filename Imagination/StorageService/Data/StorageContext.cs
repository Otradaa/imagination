using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using StorageService.Data.Models;

namespace StorageService.Models
{
    public class StorageContext : DbContext
    {
        public StorageContext (DbContextOptions<StorageContext> options)
            : base(options)
        {
            Database.EnsureCreated();
            if (!Images.Any())
            {
                Images.Add(new Image { Tags = "cake, cherry, happy", Path = "~/images/111.jpg" });
                Images.Add(new Image { Tags = "cake, happy birthday", Path = "~/images/111.jpg" });
                //Images.Add(new Image { });

                SaveChanges();
            }
        }

        public DbSet<StorageService.Data.Models.Image> Images { get; set; }
    }
}
