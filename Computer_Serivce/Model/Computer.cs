using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Computer_Serivce.Model
{
    public class Computer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public required string Brand { get; set; }

        [Required]
        public required string Model { get; set; }

        public string? YearMade { get; set; }

        [Required]
        public required string SerialNumber { get; set; }

        public virtual ICollection<Repair>? Repairs { get; set; }
    }
}