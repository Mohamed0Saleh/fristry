using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace MVCtoMOvie.Models
{
    public class GenreViewModel
    {

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public byte Id { get; set; }

        [DisplayName("name OF Genre")]
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

    }
}
