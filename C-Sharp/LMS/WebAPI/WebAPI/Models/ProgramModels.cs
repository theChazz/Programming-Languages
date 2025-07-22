using System.ComponentModel.DataAnnotations;

namespace WebAPI.Models
{
    public class ProgramRequest
    {
        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        [Required]
        [StringLength(50)]
        public string Code { get; set; }

        [StringLength(50)]
        public string Level { get; set; }

        [StringLength(100)]
        public string Department { get; set; }

        [StringLength(50)]
        public string Status { get; set; }

        public string Description { get; set; }

        [StringLength(50)]
        public string Type { get; set; }

        public int DurationMonths { get; set; }
    }

    public class ProgramResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Level { get; set; }
        public string Department { get; set; }
        public string Status { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public int DurationMonths { get; set; }
        public DateTime CreatedAt { get; set; }
    }
} 