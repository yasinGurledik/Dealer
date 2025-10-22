using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dealer.Application.DTOs;
public class UserDto
{
	public int Id { get; set; }
	public string UserName { get; set; }
	public string Email { get; set; }
	public bool IsActive { get; set; }
}

public class UserLoginDto
{
	public string UserName { get; set; } = null!;
	public string Password { get; set; } = null!;
}

public class UserRegisterDto
{
	public string UserName { get; set; } = null!;
	public string Email { get; set; } = null!;
	public string Password { get; set; } = null!;
}

