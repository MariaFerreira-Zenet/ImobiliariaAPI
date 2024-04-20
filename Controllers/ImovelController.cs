using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ImobiliariaApi.Data;
using ImobiliariaApi.Models;
using ImobiliariaApi.DTOs;

[Route("api/[controller]")]
[ApiController]
public class ImovelController : ControllerBase
{
    private readonly ImobiliariaContext _context;

    public ImovelController(ImobiliariaContext context)
    {
        _context = context;
    }

    // GET: api/Imovel
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ImovelDTO>>> GetImoveis()
    {
        var imoveis = await _context.Imoveis
            .Include(i => i.Proprietario)
            .ThenInclude(p => p.Corretores)
            .Include(i => i.Inquilino)
            .Select(i => new ImovelDTO
            {
                ImovelId = i.Id,
                Endereco = i.Endereco,
                ProprietarioNome = i.Proprietario.Nome,
                InquilinoNome = i.Inquilino != null ? i.Inquilino.Nome : null,
                CorretorNome = i.Proprietario.Corretores.FirstOrDefault() != null
                               ? i.Proprietario.Corretores.Select(pc => pc.Corretor.Nome).FirstOrDefault()
                               : null
            })
            .ToListAsync();

        return Ok(imoveis);
    }

    // GET: api/Imovel/5

    [HttpGet("{id}")]
    public async Task<ActionResult<ImovelDTO>> GetImovel(int id)
    {
        var imovelDTO = await _context.Imoveis
            .Where(i => i.Id == id)
            .Include(i => i.Proprietario)
                .ThenInclude(p => p.Corretores)
                    .ThenInclude(pc => pc.Corretor)
            .Include(i => i.Inquilino)
            .Select(i => new ImovelDTO
            {
                ImovelId = i.Id,
                Endereco = i.Endereco,
                ProprietarioNome = i.Proprietario.Nome,
                InquilinoNome = i.Inquilino != null ? i.Inquilino.Nome : null,
                CorretorNome = i.Proprietario.Corretores
                    .Where(pc => pc.Corretor != null)
                    .Select(pc => pc.Corretor.Nome)
                    .FirstOrDefault() 
            })
            .FirstOrDefaultAsync();

        if (imovelDTO == null)
        {
            return NotFound();
        }

        return imovelDTO;
    }

    // POST: api/Imovel
    [HttpPost]
    public async Task<ActionResult<Imovel>> PostImovel(CreateImovelDTO createImovelDTO)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var imovel = new Imovel
        {
            Endereco = createImovelDTO.Endereco,
            ProprietarioId = createImovelDTO.ProprietarioId,
            InquilinoId = createImovelDTO.InquilinoId 
        };

        _context.Imoveis.Add(imovel);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetImovel), new { id = imovel.Id }, imovel);
    }


    // PUT: api/Imovel/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutImovel(int id, UpdateImovelDTO updateImovelDTO)
    {
        var imovel = await _context.Imoveis.FindAsync(id);
        if (imovel == null)
        {
            return NotFound();
        }

        imovel.Endereco = updateImovelDTO.Endereco;
        imovel.ProprietarioId = updateImovelDTO.ProprietarioId;
        imovel.InquilinoId = updateImovelDTO.InquilinoId;

        _context.Entry(imovel).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Imoveis.Any(e => e.Id == id))
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


    // DELETE: api/Imovel/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteImovel(int id)
    {
        var imovel = await _context.Imoveis.FindAsync(id);
        if (imovel == null)
        {
            return NotFound();
        }

        _context.Imoveis.Remove(imovel);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    // POST: api/Imovel/5/rent/3
    [HttpPost("{imovelId}/rent/{inquilinoId}")]
    public async Task<IActionResult> RentImovel(int imovelId, int inquilinoId)
    {
        var imovel = await _context.Imoveis.FindAsync(imovelId);
        var inquilino = await _context.Inquilinos.FindAsync(inquilinoId);

        if (imovel == null || inquilino == null)
        {
            return NotFound();
        }

        imovel.InquilinoId = inquilinoId;
        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpGet("alugados")]
    public async Task<ActionResult<IEnumerable<ImovelAlugadoDTO>>> GetImoveisAlugados()
    {
        var imoveisAlugados = await _context.Imoveis
            .Where(i => i.InquilinoId.HasValue)
            .Select(i => new ImovelAlugadoDTO
            {
                ImovelId = i.Id,
                Endereco = i.Endereco
            })
            .ToListAsync();

        return Ok(imoveisAlugados);
    }
}
