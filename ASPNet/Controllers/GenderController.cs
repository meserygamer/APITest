using Microsoft.AspNetCore.Mvc;

namespace ASPNet.Controllers
{
    [Route("api/Gender")]
    [ApiController]
    public class GenderController : Controller
    {
        [Route("GetGenders")]
        [HttpGet]
        public List<Gender> GetGenders([FromQuery]int? id)
        {
            if (id is null)
            {
                using (ApiDbContext DB = new ApiDbContext())
                {
                    return DB.Genders.ToList();
                }
            }
            try
            {
                using (ApiDbContext DB = new ApiDbContext())
                {
                    return new List<Gender>() {DB.Genders.Find(id)};
                }
            }
            catch (Exception ex)
            {
                return new List<Gender>();
            }
        }

        [Route("PostGenders/{genderName}")]
        [HttpPost]
        public IActionResult PostGenders(String genderName)
        {
            if(genderName == null)
            {
                return BadRequest("Название добавляемого гендера не было введенно");
            }
            Gender addingGender = new Gender();
            addingGender.GenderName = genderName;
            using (ApiDbContext DB = new ApiDbContext())
            {
                DB.Genders.Add(addingGender);
                try
                {
                    DB.SaveChanges();
                    return Ok();
                }
                catch (Exception ex)
                {
                    return BadRequest($"{ex.Message}");
                }
            }
        }

        [Route("PostGenders")]
        [HttpPut]
        public async Task<IActionResult> PutGenders([FromQuery]Gender gender)
        {
            if (gender == null)
            {
                return BadRequest("Изменяемый гендер, не предоставлен");
            }
            using (ApiDbContext DB = new ApiDbContext())
            {
                Gender? changingGender = await DB.Genders.FindAsync(gender.GenderId);
                if(changingGender is null)
                {
                    return BadRequest("Изменяемый гендер не был найден!");
                }
                changingGender.GenderName = gender.GenderName;
                try
                {
                    await DB.SaveChangesAsync();
                    return Ok();
                }
                catch (Exception ex)
                {
                    return BadRequest($"{ex.Message}");
                }
            }
        }

        [Route("DeleteGenders")]
        [HttpDelete]
        public async Task<IActionResult> DeleteGenders([FromQuery]int id)
        {
            using (ApiDbContext DB = new ApiDbContext())
            {
                Gender? gender = await DB.Genders.FindAsync(id);
                if(gender is null)
                {
                    return BadRequest();
                }
                DB.Genders.Remove(gender);
                try
                {
                    await DB.SaveChangesAsync();
                    return Ok();
                }
                catch(Exception ex)
                {
                    return BadRequest($"{ex.Message}");
                }
            }
        }
    }
}
