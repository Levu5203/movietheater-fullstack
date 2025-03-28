using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;

namespace MovieTheater.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        [HttpGet("protected")]
        public IActionResult ProtectedEndpoint()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var userEmail = User.FindFirstValue(ClaimTypes.Email);

            return Ok(new
            {
                Message = "You are authorized!",
                UserId = userId,
                UserEmail = userEmail,
                Claims = User.Claims.Select(c => new { c.Type, c.Value })
            });
        }
    }
}
