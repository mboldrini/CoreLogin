using CoreLogin_Domain.Entities;
using CoreLogin_Domain.Repositories;
using CoreLogin_Infrastructure.Data;
using CoreLogin_Infrastructure.Helpers;
using Microsoft.EntityFrameworkCore;
namespace CoreLogin_Infrastructure.Repository
{
  public class UserRepository : IUserRepository
  {

    public readonly DataContext _dbContext;

    public UserRepository(DataContext dbContext)
    {
      _dbContext = dbContext;
    }

    /// <summary>
    /// Create a new user
    /// </summary>
    /// <param name="user">User object</param>
    /// <returns></returns>
    public async Task<User> AddUserAsync(User user)
    {

      var userExists = await GetUserByEmailAsync(user.Email);
      if (userExists != null)
      {
        return null;
      }

      user.Uid = Guid.NewGuid();
      user.Password = PasswordHelper.CriptographPassword(user.Password);

      await _dbContext.Users.AddAsync(user);
      await _dbContext.SaveChangesAsync();

      user.Password = string.Empty;

      return user;
    }

    /// <summary>
    /// Get user by email
    /// </summary>
    /// <param name="email">Email</param>
    /// <returns></returns>
    public async Task<User> GetUserByEmailAsync(string email)
    {
      var userExists = await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == email);
      if(userExists == null)
      {
        return null;
      }

      return userExists;
    }

    /// <summary>
    /// Get User by Email and Password - used to get the JWT Token (don't show the user infos
    /// </summary>
    /// <param name="email">Email</param>
    /// <param name="password">Password</param>
    /// <returns>User</returns>
    public async Task<User> GetUserByUserEmailAndPasswordAsync(string email, string password)
    {
      var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == email);
      if (user == null || !PasswordHelper.VerifyPassword(password, user.Password))
      {
        return null;
      }

      return user;
    }

  }
}
