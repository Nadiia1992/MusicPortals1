using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MusicPortal.Models;


namespace AspNetCore.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SingerController(MusicContext context) : ControllerBase
{

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Singers>>> GetSingers()
    {
        return await context.Singers.ToListAsync();
    }


    [HttpGet("{id}")]
    public async Task<ActionResult<Singers>> GetSinger(int id)
    {
        var singer = await context.Singers.FindAsync(id);

        if (singer is null)
        {
            return NotFound();
        }

        return singer;
    }


    [HttpPut("{id}")]
    public async Task<IActionResult> PutSinger(int id, Singers singers)
    {
        if (id != singers.Id)
        {
            return BadRequest();
        }

        context.Entry(singers).State = EntityState.Modified;

        try
        {
            await context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!SingersExists(id))
            {
                return NotFound();
            }
            throw;
        }

        return NoContent();
    }


    [HttpPost]
    public async Task<ActionResult<Singers>> PostSinger(Singers singers)
    {
        context.Singers.Add(singers);
        await context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetSinger), new { id = singers.Id }, singers);
    }


    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteSinger(int id)
    {
        var singers = await context.Singers.FindAsync(id);
        if (singers is null)
        {
            return NotFound();
        }

        context.Singers.Remove(singers);
        await context.SaveChangesAsync();

        return NoContent();
    }

    private bool SingersExists(int id)
    {
        return context.Singers.Any(e => e.Id == id);
    }
}
