using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace Dealer.API.Middleware
{
	public class JwtValidationMiddleware
	{
		private readonly RequestDelegate _next;
		private readonly IConfiguration _configuration;
		private readonly ILogger<JwtValidationMiddleware> _logger;

		public JwtValidationMiddleware(RequestDelegate next, IConfiguration configuration, ILogger<JwtValidationMiddleware> logger)
		{
			_next = next;
			_configuration = configuration;
			_logger = logger;
		}
		public async Task InvokeAsync(HttpContext context)
		{
			// Token kontrolü yapılmayacak path'ler
			var exemptPaths = new[] { "/api/users/login", "/api/users/register" };

			if (exemptPaths.Any(p => context.Request.Path.StartsWithSegments(p, StringComparison.OrdinalIgnoreCase)))
			{
				await _next(context);
				return;
			}
			var token = context.Request.Headers["x-auth-token"].FirstOrDefault()?.Split(" ").Last();

			if (!string.IsNullOrEmpty(token))
			{
				// Token yoksa 401 dön
				context.Response.StatusCode = StatusCodes.Status401Unauthorized;
				await context.Response.WriteAsync("Token gerekli");
				return;
			}

			try
			{
				var jwtSettings = _configuration.GetSection("JwtSettings");
				var tokenHandler = new JwtSecurityTokenHandler();
				var key = Encoding.UTF8.GetBytes(jwtSettings["SecretKey"]);

				tokenHandler.ValidateToken(token, new TokenValidationParameters
				{
					ValidateIssuerSigningKey = true,
					IssuerSigningKey = new SymmetricSecurityKey(key),
					ValidateIssuer = true,
					ValidIssuer = jwtSettings["Issuer"],
					ValidateAudience = true,
					ValidAudience = jwtSettings["Audience"],
					ValidateLifetime = true,
					ClockSkew = TimeSpan.Zero
				}, out SecurityToken validatedToken);


				var jwtToken = (JwtSecurityToken)validatedToken;
				var userId = jwtToken.Claims.First(x => x.Type == "id").Value;
				if (userId != null)
					context.Items["UserId"] = userId;
				var companyId = jwtToken.Claims.First(x => x.Type == "companyId").Value;
				if (companyId != null)
					context.Items["companyId"] = companyId;

				await _next(context);
			}
			catch (SecurityTokenExpiredException ex)
			{
				_logger.LogWarning(ex, "Token süresi dolmuş");
				context.Response.StatusCode = StatusCodes.Status401Unauthorized;
				await context.Response.WriteAsync("Token süresi dolmuş");
			}
			catch (SecurityTokenInvalidSignatureException ex)
			{
				_logger.LogWarning(ex, "Token imza doğrulaması başarısız");
				context.Response.StatusCode = StatusCodes.Status401Unauthorized;
				await context.Response.WriteAsync("Token imzası geçersiz");
			}
			catch (SecurityTokenInvalidAudienceException ex)
			{
				_logger.LogWarning(ex, "Token audience geçersiz");
				context.Response.StatusCode = StatusCodes.Status401Unauthorized;
				await context.Response.WriteAsync("Token audience geçersiz");
			}
			catch (SecurityTokenInvalidIssuerException ex)
			{
				_logger.LogWarning(ex, "Token issuer geçersiz");
				context.Response.StatusCode = StatusCodes.Status401Unauthorized;
				await context.Response.WriteAsync("Token issuer geçersiz");
			}
			catch (SecurityTokenException ex)
			{
				_logger.LogWarning(ex, "Token doğrulama hatası");
				context.Response.StatusCode = StatusCodes.Status401Unauthorized;
				await context.Response.WriteAsync("Token doğrulama hatası");
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Beklenmeyen hata (token doğrulama)");
				context.Response.StatusCode = StatusCodes.Status500InternalServerError;
				await context.Response.WriteAsync("Sunucu hatası");
			}



		}
	}
}