using Dealer.Application.DTOs;
using Dealer.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
	private readonly IUserService _userService;
	public UsersController(IUserService userService) => _userService = userService;

	[HttpPost("register")]
	public async Task<IActionResult> Register(UserRegisterDto dto)
	{
		var user = await _userService.RegisterAsync(dto);
		return Ok(user);
	}

	[HttpPost("login")]
	public async Task<IActionResult> Login(UserLoginDto dto)
	{
		var token = await _userService.LoginAsync(dto);
		if (token == null) return Unauthorized();
		return Ok(new { Token = token });
	}
}
