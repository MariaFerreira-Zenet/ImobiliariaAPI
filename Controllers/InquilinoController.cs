using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ImobiliariaApi.Data;
using ImobiliariaApi.Models;
using ImobiliariaApi.DTOs;

[Route("api/[controller]")]
[ApiController]
public class InquilinoController : ControllerBase
{
    private readonly ImobiliariaContext _context;

    public InquilinoController(ImobiliariaContext context)
    {
        _context = context;
    }

    // GET: api/Inquilino
    [HttpGet]
    public async Task<ActionResult<IEnumerable<InquilinoDTO>>> GetInquilinos()
    {
        return await _context.Inquilinos
            .Select(inquilino => new InquilinoDTO
            {
                Id = inquilino.Id,
                Nome = inquilino.Nome,
                CPF = inquilino.CPF,
                Telefone = inquilino.Telefone,
                ImoveisAlugados = inquilino.ImoveisAlugados.Select(imovel => new ImovelAlugadoDTO
                {
                    ImovelId = imovel.Id,
                    Endereco = imovel.Endereco
                }).ToList()
            })
            .ToListAsync();
    }

    // GET: api/Inquilino/5
    [HttpGet("{id}")]
    public async Task<ActionResult<InquilinoDTO>> GetInquilino(int id)
    {
        var inquilino = await _context.Inquilinos
            .Where(i => i.Id == id)
            .Include(i => i.ImoveisAlugados)
            .Select(i => new InquilinoDTO
            {
                Id = i.Id,
                Nome = i.Nome,
                CPF = i.CPF,
                Telefone = i.Telefone,
                ImoveisAlugados = i.ImoveisAlugados.Select(imovel => new ImovelAlugadoDTO
                {
                    ImovelId = imovel.Id,
                    Endereco = imovel.Endereco
                }).ToList()
            })
            .FirstOrDefaultAsync();

        if (inquilino == null)
        {
            return NotFound();
        }

        return inquilino;
    }


    // POST: api/Inquilino
    [HttpPost]
    public async Task<ActionResult<InquilinoDTO>> PostInquilino(CreateInquilinoDTO createInquilinoDTO)
    {
        var inquilino = new Inquilino
        {
            Nome = createInquilinoDTO.Nome,
            CPF = createInquilinoDTO.CPF,
            Telefone = createInquilinoDTO.Telefone
        };

        _context.Inquilinos.Add(inquilino);
        await _context.SaveChangesAsync();

        var inquilinoDTO = new InquilinoDTO
        {
            Id = inquilino.Id,
            Nome = inquilino.Nome,
            CPF = inquilino.CPF,
            Telefone = inquilino.Telefone,
            ImoveisAlugados = new List<ImovelAlugadoDTO>()
        };

        return CreatedAtAction(nameof(GetInquilino), new { id = inquilino.Id }, inquilinoDTO);
    }



    // PUT: api/Inquilino/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutInquilino(int id, UpdateInquilinoDTO updateInquilinoDTO)
    {
        var inquilino = await _context.Inquilinos.FindAsync(id);
        if (inquilino == null)
        {
            return NotFound();
        }

        inquilino.Nome = updateInquilinoDTO.Nome;
        inquilino.CPF = updateInquilinoDTO.CPF;
        inquilino.Telefone = updateInquilinoDTO.Telefone;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Inquilinos.Any(e => e.Id == id))
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


    // POST: api/Inquilino/alugar
    [HttpPost("alugar")]
    public async Task<ActionResult> AlugarImovel(AlugarImovelDTO alugarImovelDTO)
    {

        var inquilino = await _context.Inquilinos.FindAsync(alugarImovelDTO.InquilinoId);
        var imovel = await _context.Imoveis.FindAsync(alugarImovelDTO.ImovelId);

        if (inquilino == null || imovel == null)
        {
            return NotFound();
        }

        if (imovel.InquilinoId.HasValue)
        {
            return BadRequest("O imóvel já está alugado.");
        }

        imovel.InquilinoId = inquilino.Id;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (Exception)
        {
            return StatusCode(500, "Um erro ocorreu ao alugar o imóvel.");
        }

        return Ok(new { message = "Imóvel alugado com sucesso." });
    }

    // DELETE: api/Inquilino/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteInquilino(int id)
    {
        var inquilino = await _context.Inquilinos.FindAsync(id);
        if (inquilino == null)
        {
            return NotFound();
        }

        _context.Inquilinos.Remove(inquilino);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
