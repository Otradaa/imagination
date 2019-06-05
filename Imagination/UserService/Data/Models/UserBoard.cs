using StorageService.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace UserService.Data.Models
{
    public class UserBoard
    {
        public int Id { get; set; }
        [Required]
        public int UserId { get; set; }
        [Required]
        public string BoardName { get; set; }
        public string Description { get; set; }
    }

    public class FullBoard
    {
        public UserBoard board { get; set; }
        public IEnumerable<BoardImage> boardimages { get; set; }
        public IEnumerable<Image> images { get; set; }
    }
}
