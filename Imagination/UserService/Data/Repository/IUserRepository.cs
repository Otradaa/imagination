using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserService.Data.Models;

namespace UserService.Data.Repository
{
    public interface IUserRepository
    {
        IEnumerable<User> GetUsers();
        Task<User> GetUser(int id);
        Task Modify(User user);
        bool UserExists(int id);
        Task AddUser(User user);
        Task RemoveUser(User user);


        IEnumerable<UserBoard> GetBoardsByUserId(int userId);
        Task AddBoard(UserBoard board);
        Task<UserBoard> GetBoard(int id);
        Task RemoveBoard(UserBoard board);
        Task AddImage(BoardImage image);
        Task<BoardImage> GetBoardImage(int id);
        Task RemoveImage(BoardImage image);
        IEnumerable<BoardImage> GetImagesByBoardId(int boardId);
    }
}
