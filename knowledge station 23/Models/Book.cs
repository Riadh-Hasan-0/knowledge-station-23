using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace knowledge_station_23.Models
{
    public class Book
    {
        public int Id { get; set; }
        public String Title { get; set; }
        public string Description { get; set; }
        public string AuthorId { get; set; }
        public string Path { get; set; }
        [NotMapped]
        [Display(Name = "Choose Image")]
        public IFormFile ImageFile { get; set; }


    }
}
