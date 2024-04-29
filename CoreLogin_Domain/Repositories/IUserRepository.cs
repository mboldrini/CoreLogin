using CoreLogin_Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace CoreLogin_Domain.Repositories
{
  public interface IUserRepository
  {
    Task<ActionResult> AddUserAsync(User user);
    Task<ActionResult<User>> GetUserByIdAsync(Guid id);
    Task<ActionResult<User>> GetUserByEmailAsync(string email);
    Task<ActionResult<User>> GetUserByUserNameAsync(string userName);
    Task<User> GetUserByUserEmailAndPasswordAsync(string email, string password);
  }
}
