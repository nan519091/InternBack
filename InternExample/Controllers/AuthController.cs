using InternExample.Entity;
using InternExample.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IConfiguration _configuration;
    private readonly UserRepository _userRepository;

    public AuthController(IConfiguration configuration, UserRepository userRepository)
    {
        _configuration = configuration;
        _userRepository = userRepository;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] User loginUser)
    {
        // ตรวจสอบว่าข้อมูลของผู้ใช้ไม่เป็นค่าว่าง
        if (loginUser == null || string.IsNullOrEmpty(loginUser.Username) || string.IsNullOrEmpty(loginUser.Password))
        {
            return BadRequest("Invalid user data");
        }

        // ค้นหาผู้ใช้จาก username
        var user = await _userRepository.GetUserByUsernameAsync(loginUser.Username);
        if (user != null)
        {
            // Verify รหัสผ่านที่รับมาโดยเปรียบเทียบกับ hash ที่เก็บในฐานข้อมูล
            var isPasswordValid = PasswordHelper.VerifyPassword(loginUser.Password, user.Password);

            if (isPasswordValid)
            {
                // สร้าง JWT token ถ้ารหัสผ่านถูกต้อง
                var token = GenerateJwtToken(user.Username);
                return Ok(new { Token = token });
            }
            else
            {
                return BadRequest("Invalid login");
            }
        }
        return Unauthorized();
    }

    private string GenerateJwtToken(string username)
    {
        // สร้างค่า key สำหรับ JWT
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        // ตั้งค่าข้อมูลของ token
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Name, username)
            }),
            Expires = DateTime.UtcNow.AddHours(1),
            Issuer = _configuration["Jwt:Issuer"],
            Audience = _configuration["Jwt:Audience"],
            SigningCredentials = credentials
        };

        // สร้าง token
        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}
