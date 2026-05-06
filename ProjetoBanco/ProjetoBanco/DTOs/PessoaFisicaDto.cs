namespace ProjetoBanco.API.DTOs;

public class PessoaFisicaDto
{
    public string Nome { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Cpf { get; set; } = string.Empty;
    public DateTime DataNascimento { get; set; }
    public int AgenciaId { get; set; }
}