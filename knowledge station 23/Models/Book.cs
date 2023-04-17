using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace knowledge_station_23.Models
{
    public class Book
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public String Title { get; set; }
        public string Description { get; set; }
        [Required]
        [Display(Name = "Select Author")]
        public string AuthorId { get; set; }
        public string Path { get; set; }
        [NotMapped]
        [Display(Name = "Choose Image")]
        public IFormFile ImagePath { get; set; }


    }
}
