using CoreLogin_Domain.Entities;
using CoreLogin_Domain.Repositories;
using CoreLogin_Infrastructure.Data;
using CoreLogin_Infrastructure.Helpers;
using Microsoft.AspNetCore.Mvc;
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
    public async Task<ActionResult> AddUserAsync(User user)
    {

      var userExists = await GetUserByEmailAsync(user.Email);
      if (userExists != null)
      {
        return new BadRequestObjectResult("User Already Exists");
      }

      user.Uid = Guid.NewGuid();
      user.Password = PasswordHelper.CriptographPassword(user.Password);

      await _dbContext.Users.AddAsync(user);
      await _dbContext.SaveChangesAsync();

      return new OkResult();
    }

    /// <summary>
    /// Get user by email
    /// </summary>
    /// <param name="email">Email</param>
    /// <returns></returns>
    public async Task<User> GetUserByEmailAsync(string email)
    {
      return await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == email);
    }

    /// <summary>
    /// Get User By Uid (Guid)
    /// </summary>
    /// <param name="Uid">Uid</param>
    /// <returns></returns>
    public async Task<User> GetUserByIdAsync(Guid Uid)
    {
      return await _dbContext.Users.FirstOrDefaultAsync(u => u.Uid == Uid);
    }

    /// <summary>
    /// Get User by Email and Password
    /// </summary>
    /// <param name="email">Email</param>
    /// <param name="password">Password</param>
    /// <returns></returns>
    public async Task<User> GetUserByUserEmailAndPasswordAsync(string email, string password)
    {
      var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == email);
      if (user == null || !PasswordHelper.VerifyPassword(password, user.Password))
      {
        return null;
      }

      user.Password = string.Empty;
    
      return user;
    }

    public async Task<User> GetUserByUserNameAsync(string userName)
    {
      return await _dbContext.Users.FirstOrDefaultAsync(u => u.UserName == userName);
    }
  }
}
