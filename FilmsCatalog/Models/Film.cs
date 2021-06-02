using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FilmsCatalog.Models
{
    public class Film
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        public String CreatorId { get; set; }

        public User Creator { get; set; }


        [Required]
        public String Name { get; set; }

        [Required]
        public String Description { get; set; }

        [Required]
        [Column(TypeName = "date")]
        public DateTime YearsOfRealease { get; set; }

        [Required]
        public String Director { get; set; }

        [Required]
        public String PathToPoster { get; set; }

    }
}
