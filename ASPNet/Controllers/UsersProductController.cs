using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ASPNet.Controllers
{
    [Route("api/UsersProduct")]
    [ApiController]
    public class UsersProductController : Controller
    {
        [Route("GetUsersProduct")]
        [HttpGet]
        public async Task<List<UsersProduct>> GetUsersProduct([FromQuery] int? id)
        {
            if (id is null)
            {
                using (ApiDbContext DB = new ApiDbContext())
                {
                    return await DB.UsersProducts.Include("Product").Include("User").ToListAsync();
                }
            }
            using (ApiDbContext DB = new ApiDbContext())
            {
                UsersProduct? findingUsersProduct;
                try
                {
                    findingUsersProduct = await DB.UsersProducts.Include("Product").Include("User").
                    Where(a => a.RecordId == id).FirstAsync();
                }
                catch (Exception ex)
                {
                    return new List<UsersProduct>();
                }
                return new List<UsersProduct> { findingUsersProduct };
            }
        }

        [Route("PostUsersProduct")]
        [HttpPost]
        public async Task<IActionResult> PostUsersProduct([FromBody] UsersProduct addingUsersProduct)
        {
            if (addingUsersProduct is null)
            {
                return BadRequest();
            }
            addingUsersProduct.RecordId = default(int);
            using (ApiDbContext DB = new ApiDbContext())
            {
                try
                {
                    await DB.UsersProducts.AddAsync(addingUsersProduct);
                    await DB.SaveChangesAsync();
                    return Ok();
                }
                catch (Exception ex)
                {
                    return new StatusCodeResult(StatusCodes.Status418ImATeapot); //Я чайник))
                }
            }
        }

        [Route("PutUsersProduct")]
        [HttpPut]
        public async Task<IActionResult> PutUsersProduct(int id, [FromBody] UsersProduct usersProduct)
        {
            UsersProduct? changingUsersProduct;
            using (ApiDbContext DB = new ApiDbContext())
            {
                changingUsersProduct = await DB.UsersProducts.FindAsync(id);
                if (changingUsersProduct is null)
                {
                    return BadRequest();
                }
                changingUsersProduct.ProductId = usersProduct.ProductId;
                changingUsersProduct.UserId = usersProduct.UserId;
                try
                {
                    await DB.SaveChangesAsync();
                    return Ok();
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
        }

        [Route("DeleteUsersProduct")]
        [HttpDelete]
        public async Task<IActionResult> DeleteUsersProduct(int id)
        {
            using (ApiDbContext DB = new ApiDbContext())
            {
                UsersProduct? usersProduct = await DB.UsersProducts.FindAsync(id);
                if (usersProduct is null)
                {
                    return BadRequest();
                }
                DB.UsersProducts.Remove(usersProduct);
                try
                {
                    await DB.SaveChangesAsync();
                    return Ok();
                }
                catch
                {
                    return BadRequest();
                }
            }
        }
    }
}
