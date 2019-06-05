using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StorageService.Data.Models
{
    public class Image
    {
        public int Id { get; set; }
        [Required]
        public string Tags { get; set; }
        [Required]
        public string Path { get; set; }
    }
}
