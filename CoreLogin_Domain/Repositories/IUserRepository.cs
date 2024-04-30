using CoreLogin_Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace CoreLogin_Domain.Repositories
{
  public interface IUserRepository
  {
    Task<User> AddUserAsync(User user);
    Task<User> GetUserByEmailAsync(string email);
    Task<User> GetUserByUserEmailAndPasswordAsync(string email, string password);
  }
}
