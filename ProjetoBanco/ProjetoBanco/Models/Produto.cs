using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjetoBanco.API.Models;

[Table("TB_PRODUTO")]
public abstract class Produto
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    [MaxLength(100)]
    public string Nome { get; set; } = string.Empty;

    [Required]
    [MaxLength(500)]
    public string Descricao { get; set; } = string.Empty;

    public string TipoProduto { get; set; } = string.Empty; // discriminator
}