using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjetoBanco.API.Models;

[Table("TB_PRODUTO")]
public class Emprestimo : Produto
{
    [Required]
    public decimal ValorMaximo { get; set; }

    [Required]
    public decimal TaxaJurosAnual { get; set; }

    [Required]
    public int PrazoMaximoMeses { get; set; }
}