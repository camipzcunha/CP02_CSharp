using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjetoBanco.API.Data;
using ProjetoBanco.API.DTOs;
using ProjetoBanco.API.Models;

namespace ProjetoBanco.API.Controllers;

[ApiController]
[Route("api/agencias")]
public class AgenciasController : ControllerBase
{
    private readonly AppDbContext _context;

    public AgenciasController(AppDbContext context)
    {
        _context = context;
    }

    [HttpPost]
    public async Task<IActionResult> Criar([FromBody] AgenciaDto dto)
    {
        var agencia = new Agencia
        {
            Nome = dto.Nome,
            Numero = dto.Numero,
            Endereco = dto.Endereco
        };

        _context.Agencias.Add(agencia);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(BuscarPorId), new { id = agencia.Id }, agencia);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> BuscarPorId(int id)
    {
        var agencia = await _context.Agencias.FindAsync(id);
        if (agencia == null) return NotFound();
        return Ok(agencia);
    }
}