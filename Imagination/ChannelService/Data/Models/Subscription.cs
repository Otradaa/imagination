using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ChannelService.Data.Models
{
    public class Subscription
    {
        public int Id { get; set; }
        [Required]
        public int ChannelId { get; set; }
        [Required]
        public int UserId { get; set; }
    }

    public class SubsResponse
    {
        public int Id { get; set; }
        [Required]
        public string ChannelName { get; set; }
        [Required]
        public int UserId { get; set; }
    }
}
