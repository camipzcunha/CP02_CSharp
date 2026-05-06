using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjetoBanco.API.Models;

[Table("TB_CLIENTE")]
public class PessoaJuridica : Cliente
{
    [Required]
    [MaxLength(18)]
    public string Cnpj { get; set; } = string.Empty;

    [Required]
    [MaxLength(200)]
    public string RazaoSocial { get; set; } = string.Empty;
}