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
    }
}
