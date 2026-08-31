using System.ComponentModel.DataAnnotations;
namespace sampleapi.Models
{
    public class SampleModel
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Age { get; set; }
        public string Position { get; set; } = string.Empty;
        public string Department { get; set; } = string.Empty;
    }
}
