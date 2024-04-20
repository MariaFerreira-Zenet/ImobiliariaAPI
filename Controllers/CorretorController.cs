using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ImobiliariaApi.Data;
using ImobiliariaApi.Models;
using ImobiliariaApi.DTOs;

[Route("api/[controller]")]
[ApiController]
public class CorretorController : ControllerBase
{
    private readonly ImobiliariaContext _context;

    public CorretorController(ImobiliariaContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<CorretorDTO>>> GetCorretores()
    {
        var corretores = await _context.Corretores
            .Include(c => c.Proprietarios)
                .ThenInclude(pc => pc.Proprietario)
            .Select(c => new CorretorDTO
            {
                Id = c.Id,
                Nome = c.Nome,
                Proprietarios = c.Proprietarios
                    .Select(pc => new SimpleProprietarioDTO
                    {
                        Id = pc.Proprietario.Id,
                        Nome = pc.Proprietario.Nome
                    })
                    .ToList()
            })
            .ToListAsync();

        return corretores;
    }


    // GET: api/Corretor/5
    [HttpGet("{id}")]
    public async Task<ActionResult<CorretorDTO>> GetCorretor(int id)
    {
        var corretorDTO = await _context.Corretores
            .Where(c => c.Id == id)
            .Include(c => c.Proprietarios)
                .ThenInclude(pc => pc.Proprietario)
            .Select(c => new CorretorDTO
            {
                Id = c.Id,
                Nome = c.Nome,
                Proprietarios = c.Proprietarios.Select(pc => new SimpleProprietarioDTO
                {
                    Id = pc.Proprietario.Id,
                    Nome = pc.Proprietario.Nome
                }).ToList()
            })
            .FirstOrDefaultAsync();

        if (corretorDTO == null)
        {
            return NotFound();
        }

        return corretorDTO;
    }


    // POST: api/Corretor
    [HttpPost]
    // POST: api/Corretor
    [HttpPost]
    public async Task<ActionResult<Corretor>> CreateCorretor(CreateCorretorDTO createCorretorDTO)
    {
        var corretor = new Corretor
        {
            Nome = createCorretorDTO.Nome
        };

        _context.Corretores.Add(corretor);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetCorretor), new { id = corretor.Id }, corretor);
    }

    // PUT: api/Corretor/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutCorretor(int id, UpdateCorretorDTO updateCorretorDTO)
    {
        var corretor = await _context.Corretores.FindAsync(id);
        if (corretor == null)
        {
            return NotFound();
        }

        corretor.Nome = updateCorretorDTO.Nome;

        _context.Entry(corretor).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Corretores.Any(e => e.Id == id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return NoContent();
    }


    // DELETE: api/Corretor/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCorretor(int id)
    {
        var corretor = await _context.Corretores.FindAsync(id);
        if (corretor == null)
        {
            return NotFound();
        }

        _context.Corretores.Remove(corretor);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpPost("{corretorId}/proprietarios/{proprietarioId}")]
    public async Task<IActionResult> AddProprietarioToCorretor(int corretorId, int proprietarioId)
    {
        var corretor = await _context.Corretores.FindAsync(corretorId);
        var proprietario = await _context.Proprietarios.FindAsync(proprietarioId);

        if (corretor == null || proprietario == null)
        {
            return NotFound();
        }

        var associationExists = await _context.ProprietarioCorretor
            .AnyAsync(pc => pc.CorretorId == corretorId && pc.ProprietarioId == proprietarioId);

        if (associationExists)
        {
            return StatusCode(StatusCodes.Status409Conflict, new { message = "Associação já existe." });
        }

        var association = new ProprietarioCorretor
        {
            CorretorId = corretorId,
            ProprietarioId = proprietarioId
        };

        _context.ProprietarioCorretor.Add(association);
        try
        {
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        { 
            return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Ocorreu um erro ao salvar os dados." });
        }

        return NoContent();
    }


    [HttpDelete("{corretorId}/proprietarios/{proprietarioId}")]
    public async Task<IActionResult> RemoveProprietarioFromCorretor(int corretorId, int proprietarioId)
    {
        var association = await _context.ProprietarioCorretor
            .FirstOrDefaultAsync(pc => pc.CorretorId == corretorId && pc.ProprietarioId == proprietarioId);

        if (association == null)
        {
            return NotFound();
        }

        _context.ProprietarioCorretor.Remove(association);
        await _context.SaveChangesAsync();

        return NoContent();
    }

}
