using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjetoBanco.API.Data;
using ProjetoBanco.API.DTOs;
using ProjetoBanco.API.Models;

namespace ProjetoBanco.API.Controllers;

[ApiController]
[Route("api/clientes")]
public class ClientesController : ControllerBase
{
    private readonly AppDbContext _context;

    public ClientesController(AppDbContext context)
    {
        _context = context;
    }

    [HttpPost("pf")]
    public async Task<IActionResult> CriarPF([FromBody] PessoaFisicaDto dto)
    {
        // Valida agência
        var agencia = await _context.Agencias.FindAsync(dto.AgenciaId);
        if (agencia == null)
            return BadRequest(new { erro = "Agência não encontrada." });

        // Valida CPF duplicado
        var cpfExiste = await _context.PessoasFisicas
        .Where(p => p.Cpf == dto.Cpf)
        .CountAsync() > 0;

        if (cpfExiste)
            return Conflict(new { erro = "CPF já cadastrado." });

        var pf = new PessoaFisica
        {
            Nome = dto.Nome,
            Email = dto.Email,
            Cpf = dto.Cpf,
            DataNascimento = dto.DataNascimento,
            AgenciaId = dto.AgenciaId,
            TipoCliente = "PF"
        };

        _context.PessoasFisicas.Add(pf);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(BuscarPorId), new { id = pf.Id }, pf);
    }

    [HttpPost("pj")]
    public async Task<IActionResult> CriarPJ([FromBody] PessoaJuridicaDto dto)
    {
        // Valida agência
        var agencia = await _context.Agencias.FindAsync(dto.AgenciaId);
        if (agencia == null)
            return BadRequest(new { erro = "Agência não encontrada." });

        // Valida CNPJ duplicado
        var cnpjExiste = await _context.PessoasJuridicas
        .Where(p => p.Cnpj == dto.Cnpj)
        .CountAsync() > 0;
        if (cnpjExiste)
            return Conflict(new { erro = "CNPJ já cadastrado." });

        var pj = new PessoaJuridica
        {
            Nome = dto.Nome,
            Email = dto.Email,
            Cnpj = dto.Cnpj,
            RazaoSocial = dto.RazaoSocial,
            AgenciaId = dto.AgenciaId,
            TipoCliente = "PJ"
        };

        _context.PessoasJuridicas.Add(pj);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(BuscarPorId), new { id = pj.Id }, pj);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> BuscarPorId(int id)
    {
        var cliente = await _context.Clientes
            .Include(c => c.Agencia)
            .FirstOrDefaultAsync(c => c.Id == id);

        if (cliente == null) return NotFound();

        return Ok(cliente);
    }
}