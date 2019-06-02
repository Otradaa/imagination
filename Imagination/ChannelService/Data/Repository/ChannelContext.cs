using ChannelService.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChannelService.Data.Repository
{
    public class ChannelContext : DbContext
    {
        public DbSet<Channel> Channels { get; set; }
        public DbSet<Subscription> Subscriptions { get; set; }
        public DbSet<ChannelImage> ChannelImages { get; set; }

        public ChannelContext(DbContextOptions<ChannelContext> options)
            : base(options)
        {
            Database.EnsureCreated();
            if (!Channels.Any())
            {
                Channels.Add(new Channel { Name = "Great New Channel", UserId = 1, Date = DateTime.Now, Description = "Great and New" });
                Channels.Add(new Channel { Name = "Not great Channel", UserId = 1, Date = DateTime.Now, Description = "not great at all" });

                SaveChanges();
            }

            if (!Subscriptions.Any())
            {
                Subscriptions.Add(new Subscription { UserId = 2, ChannelId = 1 });
                SaveChanges();
            }

            if (!ChannelImages.Any())
            {
                ChannelImages.Add(new ChannelImage { ChannelId = 1, Description = "Hey guys, it's a great new channel", Date = DateTime.Now, ImageId = 1 });
                SaveChanges();
            }
        }
    }
}
