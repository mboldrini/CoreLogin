using CoreLogin_Domain.Entities;
using CoreLogin_Domain.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace CoreLogin_Application.Services
{
  public class UserService
  {
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
      _userRepository = userRepository;
    }

    public async Task<ActionResult> AddUserAsync(User user)
    {
      return await _userRepository.AddUserAsync(user);
    }

    public async Task<User> GetUserByIdAsync(Guid id)
    {
      var user = await _userRepository.GetUserByIdAsync(id);
      user.Password = string.Empty;

      return user;
    }

    public async Task<User> GetUserByEmailAsync(string email)
    {
      var user = await _userRepository.GetUserByEmailAsync(email);
      user.Password = string.Empty;

      return user;
    }

    public async Task<User> GetUserByUserNameAsync(string userName)
    {
      var user = await _userRepository.GetUserByUserNameAsync(userName);
      user.Password = string.Empty;

      return user;
    }

    public async Task<User> GetUserByUserEmailAndPasswordAsync(string email, string password)
    {
      var user = await _userRepository.GetUserByUserEmailAndPasswordAsync(email, password);
      user.Password = string.Empty;

      return user;
    }

  }
}
