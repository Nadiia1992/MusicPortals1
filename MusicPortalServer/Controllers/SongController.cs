using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MusicPortal.Models;


namespace AspNetCore.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SongController(MusicContext context) : ControllerBase
{

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Song>>> GetSongs()
    {
        return await context.Songs.ToListAsync();
    }


    [HttpGet("{id}")]
    public async Task<ActionResult<Song>> GetSong(int id)
    {
        var song = await context.Songs.FindAsync(id);

        if (song is null)
        {
            return NotFound();
        }

        return song;
    }


    [HttpPut("{id}")]
    public async Task<IActionResult> PutSong(int id, Song song)
    {
        if (id != song.Id)
        {
            return BadRequest();
        }

        context.Entry(song).State = EntityState.Modified;

        try
        {
            await context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!SongExists(id))
            {
                return NotFound();
            }
            throw;
        }

        return NoContent();
    }


    [HttpPost]
    public async Task<ActionResult<Song>> PostSong(Song song)
    {
        context.Songs.Add(song);
        await context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetSong), new { id = song.Id }, song);
    }


    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteSong(int id)
    {
        var song = await context.Songs.FindAsync(id);
        if (song is null)
        {
            return NotFound();
        }

        context.Songs.Remove(song);
        await context.SaveChangesAsync();

        return NoContent();
    }

    private bool SongExists(int id)
    {
        return context.Songs.Any(e => e.Id == id);
    }
}
