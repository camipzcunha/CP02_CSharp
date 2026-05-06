namespace ProjetoBanco.API.DTOs;

public class ContratacaoDto
{
    public int ClienteId { get; set; }
    public int ProdutoId { get; set; }
    public string? Observacao { get; set; }
}