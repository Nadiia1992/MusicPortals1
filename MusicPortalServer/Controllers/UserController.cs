using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MusicPortal.Models;


namespace AspNetCore.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController(MusicContext context) : ControllerBase
{
   
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Users>>> GetUsers()
    {
        return await context.User.ToListAsync();
    }


    [HttpGet("{id}")]
    public async Task<ActionResult<Users>> GetUser(int id)
    {
        var user = await context.User.FindAsync(id);

        if (user is null)
        {
            return NotFound();
        }

        return user;
    }

  
    [HttpPut("{id}")]
    public async Task<IActionResult> PutUser(int id, Users user)
    {
        if (id != user.Id)
        {
            return BadRequest();
        }

        context.Entry(user).State = EntityState.Modified;

        try
        {
            await context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!UserExists(id))
            {
                return NotFound();
            }
            throw;
        }

        return NoContent();
    }

 
    [HttpPost]
    public async Task<ActionResult<Users>> PostUser(Users user)
    {
        context.User.Add(user);
        await context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetUser), new { id = user.Id }, user);
    }


    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUser(int id)
    {
        var user = await context.User.FindAsync(id);
        if (user is null)
        {
            return NotFound();
        }

        context.User.Remove(user);
        await context.SaveChangesAsync();

        return NoContent();
    }

    private bool UserExists(int id)
    {
        return context.User.Any(e => e.Id == id);
    }
}
