﻿using CoreLogin_Application.Services;
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

    [HttpPost]
    [Route("create")]
    [AllowAnonymous]
    public async Task<IActionResult> AddUserAsync([FromBody] User user)
    {
      var newUser = await _userRepository.AddUserAsync(user);

      return newUser;
    }

    [HttpPost]
    [Route("login")]
    [AllowAnonymous]
    public async Task<IActionResult> GetUserByUserEmailAndPasswordAsync([FromBody] LoginInfosRequest loginInfos)
    {
      var user = await _userRepository.GetUserByUserEmailAndPasswordAsync(loginInfos.Email, loginInfos.Password);
      if (user == null)
      {
        return NotFound();
      }

      return Ok(TokenService.GenerateToken(user));
    }

  }
}