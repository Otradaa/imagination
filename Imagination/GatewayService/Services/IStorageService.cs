using StorageService.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GatewayService.Services
{
    public interface IStorageService
    {
        Task<IEnumerable<Image>> GetImageFiles(IEnumerable<int> ids);
        Task<Image> AddImage(string path, string tags);
        Task<IEnumerable<Image>> GetImagesByTag(string tag);

    }
}
