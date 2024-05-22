using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Computer_Serivce.Model
{
    public class Repair
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public required int YearOfService { get; set; }

        [Required]
        public required string ServiceType { get; set; }
        public string? Description { get; set; }

        public int ComputerId { get; set; }
        public Computer Computer { get; set; }

        public Repair()
        {
            
        }
    }
}
