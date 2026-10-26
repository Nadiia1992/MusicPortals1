using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MusicPortal.Models;


namespace AspNetCore.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GenreController(MusicContext context) : ControllerBase
{

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Genres>>> GetGenres()
    {
        return await context.Genres.ToListAsync();
    }


    [HttpGet("{id}")]
    public async Task<ActionResult<Genres>> GetGenre(int id)
    {
        var genre = await context.Genres.FindAsync(id);

        if (genre is null)
        {
            return NotFound();
        }

        return genre;
    }


    [HttpPut("{id}")]
    public async Task<IActionResult> PutGenre(int id, Genres genre)
    {
        if (id != genre.Id)
        {
            return BadRequest();
        }

        context.Entry(genre).State = EntityState.Modified;

        try
        {
            await context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!GenreExists(id))
            {
                return NotFound();
            }
            throw;
        }

        return NoContent();
    }


    [HttpPost]
    public async Task<ActionResult<Song>> PostGenre(Genres genre)
    {
        context.Genres.Add(genre);
        await context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetGenre), new { id = genre.Id }, genre);
    }


    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteGenre(int id)
    {
        var genre = await context.Genres.FindAsync(id);
        if (genre is null)
        {
            return NotFound();
        }

        context.Genres.Remove(genre);
        await context.SaveChangesAsync();

        return NoContent();
    }

    private bool GenreExists(int id)
    {
        return context.Genres.Any(e => e.Id == id);
    }
}
