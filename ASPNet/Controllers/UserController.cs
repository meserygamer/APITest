﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ASPNet.Controllers
{
    [Route("api/User")]
    [ApiController]
    public class UserController : Controller
    {
        [Route("GetUser")]
        [HttpGet]
        public async Task<List<User>> GetUser([FromQuery]int? id)
        {
            if(id is null)
            {
                using (ApiDbContext DB = new ApiDbContext())
                {
                    return await DB.Users.ToListAsync();
                }
            }
            using (ApiDbContext DB = new ApiDbContext())
            {
                User? findingUser = await DB.Users.FindAsync(id);
                if (findingUser == null) 
                { 
                    return new List<User>();
                }
                return new List<User> {  findingUser };
            }
        }

        [Route("PostUser")]
        [HttpPost]
        public async Task<IActionResult> PostUser([FromBody] User addingUser)
        {
            if (addingUser is null)
            {
                return BadRequest();
            }
            addingUser.UserId = default(int);
            addingUser.Gender = null!;
            addingUser.UsersProducts = null!;
            using (ApiDbContext DB = new ApiDbContext())
            {
                try
                {
                    await DB.Users.AddAsync(addingUser);
                    await DB.SaveChangesAsync();
                    return Ok();
                }
                catch (Exception ex)
                {
                    return new StatusCodeResult(StatusCodes.Status418ImATeapot); //Я чайник))
                }
            }
        }

        [Route("PutUser")]
        [HttpPut]
        public async Task<IActionResult> PutUser(int id, [FromBody] User user)
        {
            User? changingUser;
            using (ApiDbContext DB = new ApiDbContext())
            {
                changingUser = await DB.Users.FindAsync(id);
                if (changingUser is null)
                {
                    return BadRequest();
                }
                changingUser.Surname = user.Surname;
                changingUser.Name = user.Name;
                changingUser.Patronymic = user.Patronymic;
                changingUser.Birthdate = user.Birthdate;
                changingUser.GenderId = user.GenderId;
                try
                {
                    await DB.SaveChangesAsync();
                    return Ok();
                }
                catch(Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
        }

        [Route("DeleteUser")]
        [HttpDelete]
        public async Task<IActionResult> DeleteUser(int id)
        {
            using (ApiDbContext DB = new ApiDbContext())
            {
                User? user = await DB.Users.FindAsync(id);
                if(user is null)
                {
                    return BadRequest();
                }
                DB.Users.Remove(user);
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
