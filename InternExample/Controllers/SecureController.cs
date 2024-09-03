using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class SecureController : ControllerBase
{
    [HttpGet("data")]
    public IActionResult GetData()
    {
        return Ok("ข้อมูลนี้ได้รับการป้องกันแล้ว");
    }
}
