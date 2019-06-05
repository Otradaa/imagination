using StorageService.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserService.Data.Models;

namespace GatewayService.Models
{
    public class BoardWithImages
    {
        public UserBoard board { get; set; }
        public IEnumerable<BoardImage> boardimages { get; set; }
        public IEnumerable<Image> images { get; set; }
    }
}
