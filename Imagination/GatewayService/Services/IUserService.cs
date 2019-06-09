using GatewayService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserService.Data.Models;

namespace GatewayService.Services
{
    public interface IUserService
    {
        Task<IEnumerable<UserBoard>> GetUserBoardsList(int id);
        Task<User> GetUserProfile(int id);
        Task<UserBoard> AddUserBoard(int id, UserBoard board);
        Task<BoardWithImages> GetBoardWithImages(int id);
        Task<BoardImage> AddBoardImage(int bid, int img, string descr);
        Task<int> AddUser(User user);
        Task DeleteBoard(int id);
        Task DeleteImage(int id, int imageid);
    }
}
