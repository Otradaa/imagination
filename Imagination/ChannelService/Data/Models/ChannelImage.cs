using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ChannelService.Data.Models
{
    public class ChannelImage
    {
        public int Id { get; set; }
        [Required]
        public int ChannelId { get; set; }
        [Required]
        public int ImageId { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
        public string Description { get; set; }
    }
}
