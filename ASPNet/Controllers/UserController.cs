using Microsoft.AspNetCore.Mvc;

namespace ASPNet.Controllers
{
    [Route("api/User")]
    [ApiController]
    public class UserController : Controller
    {
        [Route("GetUser")]
        [HttpGet]
        public IActionResult GetUser()
        {
            using (ApiDbContext DB = new ApiDbContext())
            {
                return Ok(DB.Users.ToList());
            }
        }
    }
}
