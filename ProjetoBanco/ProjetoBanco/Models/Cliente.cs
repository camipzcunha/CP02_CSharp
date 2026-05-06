using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjetoBanco.API.Models;

[Table("TB_CLIENTE")]
public abstract class Cliente
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    [MaxLength(100)]
    public string Nome { get; set; } = string.Empty;

    [Required]
    [MaxLength(100)]
    public string Email { get; set; } = string.Empty;

    [Required]
    public int AgenciaId { get; set; }

    public Agencia? Agencia { get; set; }

    public ICollection<Contratacao> Contratacoes { get; set; } = new List<Contratacao>();

    public string TipoCliente { get; set; } = string.Empty; // discriminator
}