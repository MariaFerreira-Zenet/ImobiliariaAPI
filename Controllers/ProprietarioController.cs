using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ImobiliariaApi.Data;
using ImobiliariaApi.Models;
using ImobiliariaApi.DTOs;

[Route("api/[controller]")]
[ApiController]
public class ProprietarioController : ControllerBase
{
    private readonly ImobiliariaContext _context;

    public ProprietarioController(ImobiliariaContext context)
    {
        _context = context;
    }

    // GET: api/Proprietario
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProprietarioDTO>>> GetProprietarios()
    {
        var proprietarios = await _context.Proprietarios
            .Include(p => p.Imoveis)
            .Select(p => new ProprietarioDTO
            {
                Id = p.Id,
                Nome = p.Nome,
                Imoveis = p.Imoveis.Select(i => new ImovelDTO
                {
                    ImovelId = i.Id,
                    Endereco = i.Endereco
                }).ToList()
            })
            .ToListAsync();

        return proprietarios;
    }


    // GET: api/Proprietario/5
    [HttpGet("{id}")]
    public async Task<ActionResult<ProprietarioDTO>> GetProprietario(int id)
    {
        var proprietario = await _context.Proprietarios
            .Where(p => p.Id == id)
            .Include(p => p.Imoveis)
            .Select(p => new ProprietarioDTO
            {
                Id = p.Id,
                Nome = p.Nome,
                Imoveis = p.Imoveis.Select(i => new ImovelDTO
                {
                    ImovelId = i.Id,
                    Endereco = i.Endereco
                }).ToList()
            })
            .FirstOrDefaultAsync();

        if (proprietario == null)
        {
            return NotFound();
        }

        return proprietario;
    }

    [HttpPost]
    public async Task<ActionResult<Proprietario>> PostProprietario(CreateProprietarioDTO createProprietarioDTO)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var proprietario = new Proprietario
        {
            Nome = createProprietarioDTO.Nome,
            CPF = createProprietarioDTO.CPF,
            Telefone = createProprietarioDTO.Telefone
        };

        _context.Proprietarios.Add(proprietario);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetProprietario), new { id = proprietario.Id }, proprietario);
    }


    // PUT: api/Proprietario/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutProprietario(int id, UpdateProprietarioDTO updateProprietarioDTO)
    {
        var proprietario = await _context.Proprietarios.FindAsync(id);
        if (proprietario == null)
        {
            return NotFound();
        }

        if (!string.IsNullOrWhiteSpace(updateProprietarioDTO.Nome))
        {
            proprietario.Nome = updateProprietarioDTO.Nome;
        }

        if (!string.IsNullOrWhiteSpace(updateProprietarioDTO.Telefone))
        {
            proprietario.Telefone = updateProprietarioDTO.Telefone;
        }

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Proprietarios.Any(e => e.Id == id))
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

    // DELETE: api/Proprietario/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProprietario(int id)
    {
        var proprietario = await _context.Proprietarios.FindAsync(id);
        if (proprietario == null)
        {
            return NotFound();
        }

        _context.Proprietarios.Remove(proprietario);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool ProprietarioExists(int id)
    {
        return _context.Proprietarios.Any(e => e.Id == id);
    }

    [HttpPost("{proprietarioId}/corretores/{corretorId}")]
    public async Task<IActionResult> AddCorretorToProprietario(int proprietarioId, int corretorId)
    {
        // Verificar se ambos existem
        var proprietarioExists = await _context.Proprietarios.AnyAsync(p => p.Id == proprietarioId);
        var corretorExists = await _context.Corretores.AnyAsync(c => c.Id == corretorId);

        if (!proprietarioExists || !corretorExists)
        {
            return NotFound();
        }

        // Criar a nova associação
        var association = new ProprietarioCorretor
        {
            ProprietarioId = proprietarioId,
            CorretorId = corretorId
        };

        // Adicionar a associação e salvar
        _context.Add(association);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{proprietarioId}/corretores/{corretorId}")]
    public async Task<IActionResult> RemoveCorretorFromProprietario(int proprietarioId, int corretorId)
    {
        var association = await _context.ProprietarioCorretor
            .FirstOrDefaultAsync(pc => pc.ProprietarioId == proprietarioId && pc.CorretorId == corretorId);

        if (association == null)
        {
            return NotFound();
        }

        _context.ProprietarioCorretor.Remove(association);
        await _context.SaveChangesAsync();

        return NoContent();
    }


}
