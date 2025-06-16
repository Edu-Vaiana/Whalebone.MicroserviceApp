using Microsoft.AspNetCore.Mvc;
using Whalebone.MicroserviceApp.Data;
using Whalebone.MicroserviceApp.Models;

namespace Whalebone.MicroserviceApp.Controllers;

[ApiController]
[Route("[controller]")]
public class PersonController : ControllerBase
{
    private readonly AppDbContext _context;

    public PersonController(AppDbContext context)
    {
        _context = context;
    }

    [HttpPost("save")]
    public async Task<IActionResult> Save([FromBody] Person person)
    {
        _context.People.Add(person);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetById), new { id = person.ExternalId }, person);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var person = await _context.People.FindAsync(id);
        if (person == null)
            return NotFound();
        return Ok(person);
    }
}
