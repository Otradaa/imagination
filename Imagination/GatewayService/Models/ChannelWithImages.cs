using ChannelService.Data.Models;
using StorageService.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GatewayService.Models
{
    public class ChannelWithImages
    {
        public Channel channel { get; set; }
        public IEnumerable<ChannelImage> images { get; set; }
        public IEnumerable<Image> files { get; set; }
        public int subsCount { get; set; }
    }
}
