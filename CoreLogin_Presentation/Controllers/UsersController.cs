using CoreLogin_Application.Services;
using CoreLogin_Domain.Entities;
using CoreLogin_Domain.Repositories;
using CoreLogin_Domain.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CoreLogin_Presentation.Controllers
{

  [Route("v1/users")]
  public class UsersController : ControllerBase
  {

    private readonly IUserRepository _userRepository;

    public UsersController(IUserRepository userRepository)
    {
      _userRepository = userRepository;
    }


    /// <summary>
    /// Endpoint to Create a new User
    /// </summary>
    /// <param name="user">User infos (UserObject)</param>
    /// <returns>User</returns>
    [HttpPost]
    [Route("create")]
    [AllowAnonymous]
    public async Task<ActionResult<User>> AddUserAsync([FromBody] User user)
    {
      var newUser = await _userRepository.AddUserAsync(user);
      if(newUser == null)
      {
        return BadRequest("User already exists");
      }

      return new OkObjectResult(newUser);
    }


    /// <summary>
    /// Endpoint to get a User by Email and password
    /// </summary>
    /// <param name="loginInfos">Email and password, via body</param>
    /// <returns>User Token</returns>
    [HttpPost]
    [Route("login")]
    [AllowAnonymous]
    public async Task<IActionResult> GetUserByUserEmailAndPasswordAsync([FromBody] LoginInfosRequest loginInfos)
    {
      var user = await _userRepository.GetUserByUserEmailAndPasswordAsync(loginInfos.Email, loginInfos.Password);
      if (user == null)
      {
        return new NotFoundObjectResult("Email or password incorrect");
      }

      return Ok(TokenService.GenerateToken(user));
    }

  }
}
