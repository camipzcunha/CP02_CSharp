using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjetoBanco.API.Data;
using ProjetoBanco.API.DTOs;
using ProjetoBanco.API.Models;

namespace ProjetoBanco.API.Controllers;

[ApiController]
[Route("api/contratacoes")]
public class ContratacoesController : ControllerBase
{
    private readonly AppDbContext _context;

    public ContratacoesController(AppDbContext context)
    {
        _context = context;
    }

    [HttpPost]
    public async Task<IActionResult> Solicitar([FromBody] ContratacaoDto dto)
    {
        var cliente = await _context.Clientes.FindAsync(dto.ClienteId);
        if (cliente == null)
            return NotFound(new { erro = "Cliente não encontrado." });

        var produto = await _context.Produtos.FindAsync(dto.ProdutoId);
        if (produto == null)
            return NotFound(new { erro = "Produto não encontrado." });

        var score = CalcularScore(cliente);

        var contratacao = new Contratacao
        {
            ClienteId = dto.ClienteId,
            ProdutoId = dto.ProdutoId,
            DataSolicitacao = DateTime.UtcNow
        };

        if (score >= 500)
        {
            contratacao.Status = "APROVADO";
            contratacao.Observacao = $"Aprovado. Score de crédito: {score}.";
        }
        else
        {
            contratacao.Status = "RECUSADO";
            contratacao.Observacao = $"Recusado. Score insuficiente: {score}. Mínimo: 500.";
        }

        _context.Contratacoes.Add(contratacao);
        await _context.SaveChangesAsync();

        return StatusCode(202, contratacao);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> BuscarPorId(int id)
    {
        var contratacao = await _context.Contratacoes
            .Include(c => c.Cliente)
            .Include(c => c.Produto)
            .FirstOrDefaultAsync(c => c.Id == id);

        if (contratacao == null) return NotFound();

        return Ok(contratacao);
    }

    private int CalcularScore(Cliente cliente)
    {
        int score = 300;

        if (cliente is PessoaFisica pf)
        {
            var idade = DateTime.Today.Year - pf.DataNascimento.Year;
            if (idade >= 25) score += 100;
            if (idade >= 40) score += 100;
        }

        if (cliente is PessoaJuridica)
        {
            score += 250;
        }

        var contratacoesAprovadas = _context.Contratacoes
            .Where(c => c.ClienteId == cliente.Id && c.Status == "APROVADO")
            .Count();

        score += contratacoesAprovadas * 50;

        return score;
    }
}