using StorageService.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ChannelService.Data.Models
{
    public class Channel
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        [Required]
        public int UserId { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
    }

    public class ResponseChannel
    {
        public Channel channel { get; set; }
        public IEnumerable<ChannelImage> images { get; set; }
        public IEnumerable<Image> files { get; set; }
        public int subsCount { get; set; }
    }
}
