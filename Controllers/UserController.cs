using API.Data;
using API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : Controller
{
    private readonly CrudDbContext _dbContext;
    public UserController(CrudDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpPost("authenticate")]
    public async Task<IActionResult> Authenticate ([FromBody] User userObj)
    {
        if (userObj == null)
        {
            return BadRequest();
        }

        var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.Email == userObj.Email && x.Password == userObj.Password);

        if (user == null)
        {
            return NotFound(new
            {
                Message = "User Not Found"
            });
        }

        return Ok(new
        {
            Message = "Login Successfully"
        });    

    }

    [HttpPost("register")]
    public async Task<IActionResult> RegisterUser([FromBody] User userObj)
    {
        if (userObj == null) 
            return BadRequest();

        await _dbContext.Users.AddAsync(userObj);
        await _dbContext.SaveChangesAsync();

        return Ok(new
        {
            Message = "Reguster Successfully"
        });
    }
}
