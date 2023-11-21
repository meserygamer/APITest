using Microsoft.AspNetCore.Mvc;

namespace ASPNet.Controllers
{
    [Route("api/Gender")]
    [ApiController]
    public class GenderController : Controller
    {
        [Route("GetGenders/{id?}")]
        [HttpGet]
        public IActionResult GetGenders(string? id)
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
                    return Ok(DB.Genders.Find(Convert.ToInt32(id)));
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
