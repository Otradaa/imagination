using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace UserService.Data.Models
{
    public class BoardImage
    {
        public int Id { get; set; }
        [Required]
        public int BoardId { get; set; }
        [Required]
        public int ImageId { get; set; }
    }
}
