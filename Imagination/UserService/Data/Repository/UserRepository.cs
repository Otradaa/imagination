using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserService.Data.Models;

namespace UserService.Data.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly UserContext _context;

        public UserRepository(UserContext context)
        {
            _context = context;
        }

        public IEnumerable<User> GetUsers()
        {
            return _context.Users;
        }

        public async Task<User> GetUser(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task Modify(User user)
        {
            _context.Entry(user).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public bool UserExists(int id)
        {
            return _context.Users.Any(e => e.Id == id);
        }

        public async Task AddUser(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveUser(User user)
        {
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }

        public IEnumerable<UserBoard> GetBoardsByUserId(int userId)
        {
            return _context.UserBoards.Where(b => b.UserId == userId);
        }

        public async Task AddBoard(UserBoard board)
        {
            _context.UserBoards.Add(board);
            await _context.SaveChangesAsync();
        }

        public async Task<UserBoard> GetBoard(int id)
        {
            return await _context.UserBoards.FindAsync(id);
        }

        public async Task RemoveBoard(UserBoard board)
        {
            _context.UserBoards.Remove(board);
            await _context.SaveChangesAsync();
        }

        public async Task AddImage(BoardImage image)
        {
            _context.BoardImages.Add(image);
            await _context.SaveChangesAsync();
        }

        public async Task<BoardImage> GetBoardImage(int id)
        {
            return await _context.BoardImages.FindAsync(id);
        }

        public async Task RemoveImage(BoardImage image)
        {
            _context.BoardImages.Remove(image);
            await _context.SaveChangesAsync();
        }

        public IEnumerable<BoardImage> GetImagesByBoardId(int boardId)
        {
            return _context.BoardImages.Where(b => b.BoardId == boardId);
        }
    }
}
