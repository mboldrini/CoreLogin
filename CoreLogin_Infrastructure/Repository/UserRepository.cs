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
    public async Task<ActionResult<User>> GetUserByEmailAsync(string email)
    {
      var userExists = await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == email);
      if(userExists == null)
      {
        return new NotFoundResult();
      }

      return new OkObjectResult(userExists);
    }

    /// <summary>
    /// Get User By Uid (Guid)
    /// </summary>
    /// <param name="Uid">Uid</param>
    /// <returns></returns>
    public async Task<ActionResult<User>> GetUserByIdAsync(Guid Uid)
    {
      var userExists = await _dbContext.Users.FirstOrDefaultAsync(u => u.Uid == Uid);
      if (userExists == null)
      {
        return new NotFoundResult();
      }

      return new OkObjectResult(userExists);
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

    public async Task<ActionResult<User>> GetUserByUserNameAsync(string userName)
    {
      var userExists = await _dbContext.Users.FirstOrDefaultAsync(u => u.UserName == userName);
      if (userExists == null)
      {
        return new NotFoundResult();
      }

      return new OkObjectResult(userExists);
    }
  }
}
