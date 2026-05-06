using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjetoBanco.API.Models;

[Table("TB_CONTRATACAO")]
public class Contratacao
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    public int ClienteId { get; set; }
    public Cliente? Cliente { get; set; }

    [Required]
    public int ProdutoId { get; set; }
    public Produto? Produto { get; set; }

    public DateTime DataSolicitacao { get; set; } = DateTime.UtcNow;

    [MaxLength(20)]
    public string Status { get; set; } = "PENDENTE"; // PENDENTE, APROVADO, RECUSADO

    [MaxLength(500)]
    public string? Observacao { get; set; }
}