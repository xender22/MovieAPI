using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace MovieAPI.Data.Models
{
    public class Metadata
    {
        [JsonIgnore] public int Id { get; set; }
        public int MovieId { get; set; }
        public string Title { get; set; }
        [Required]
        public string Language { get; set; }
        public string Duration { get; set; }
        public int ReleaseYear { get; set; }

    }
}
