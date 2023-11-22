using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ASPNet.Controllers
{
    [Route("api/Product")]
    [ApiController]
    public class ProductController : Controller
    {
        [Route("GetProduct")]
        [HttpGet]
        public async Task<List<Product>> GetProduct([FromQuery] int? id)
        {
            if (id is null)
            {
                using (ApiDbContext DB = new ApiDbContext())
                {
                    return await DB.Products.ToListAsync();
                }
            }
            using (ApiDbContext DB = new ApiDbContext())
            {
                Product? findingProduct = await DB.Products.FindAsync(id);
                if (findingProduct == null)
                {
                    return new List<Product>();
                }
                return new List<Product> { findingProduct };
            }
        }

        [Route("PostProduct")]
        [HttpPost]
        public async Task<IActionResult> PostProduct([FromBody] Product addingProduct)
        {
            if (addingProduct is null)
            {
                return BadRequest();
            }
            addingProduct.ProductId = default(int);
            using (ApiDbContext DB = new ApiDbContext())
            {
                try
                {
                    await DB.Products.AddAsync(addingProduct);
                    await DB.SaveChangesAsync();
                    return Ok();
                }
                catch (Exception ex)
                {
                    return new StatusCodeResult(StatusCodes.Status418ImATeapot); //Я чайник))
                }
            }
        }

        [Route("PutProduct")]
        [HttpPut]
        public async Task<IActionResult> PutProduct(int id, [FromBody] Product product)
        {
            Product? changingProduct;
            using (ApiDbContext DB = new ApiDbContext())
            {
                changingProduct = await DB.Products.FindAsync(id);
                if (changingProduct is null)
                {
                    return BadRequest();
                }
                changingProduct.ProductName = product.ProductName;
                changingProduct.ProductDescription = product.ProductDescription;
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

        [Route("DeleteProduct")]
        [HttpDelete]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            using (ApiDbContext DB = new ApiDbContext())
            {
                Product? product = await DB.Products.FindAsync(id);
                if (product is null)
                {
                    return BadRequest();
                }
                DB.Products.Remove(product);
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
