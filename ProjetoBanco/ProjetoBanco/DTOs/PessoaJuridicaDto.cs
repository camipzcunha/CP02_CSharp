namespace ProjetoBanco.API.DTOs;

public class PessoaJuridicaDto
{
    public string Nome { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Cnpj { get; set; } = string.Empty;
    public string RazaoSocial { get; set; } = string.Empty;
    public int AgenciaId { get; set; }
}