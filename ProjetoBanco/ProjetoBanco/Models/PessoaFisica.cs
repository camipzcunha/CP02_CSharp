using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjetoBanco.API.Models;

[Table("TB_CLIENTE")]
public class PessoaFisica : Cliente
{
    [Required]
    [MaxLength(14)]
    public string Cpf { get; set; } = string.Empty;

    [Required]
    public DateTime DataNascimento { get; set; }
}