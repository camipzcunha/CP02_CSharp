using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjetoBanco.API.Models;

[Table("TB_AGENCIA")]
public class Agencia
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    [MaxLength(100)]
    public string Nome { get; set; } = string.Empty;

    [Required]
    [MaxLength(10)]
    public string Numero { get; set; } = string.Empty;

    [Required]
    [MaxLength(200)]
    public string Endereco { get; set; } = string.Empty;

    public ICollection<Cliente> Clientes { get; set; } = new List<Cliente>();
}