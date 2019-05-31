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
        public DbSet<Subscription> ChannelSubscribers { get; set; }
        public DbSet<ChannelImage> ChannelImages { get; set; }

        public ChannelContext(DbContextOptions<ChannelContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
