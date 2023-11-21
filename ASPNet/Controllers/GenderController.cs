using Microsoft.AspNetCore.Mvc;

namespace ASPNet.Controllers
{
    [Route("api/Gender")]
    [ApiController]
    public class GenderController : Controller
    {
        [Route("GetGenders")]
        [HttpGet]
        public IActionResult GetGenders(int? id = null)
        {
            if (id is null)
            {
                using (ApiDbContext DB = new ApiDbContext())
                {
                    return Ok(DB.Genders.ToList());
                }
            }
            try
            {
                using (ApiDbContext DB = new ApiDbContext())
                {
                    return Ok(DB.Genders.Find(id));
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
