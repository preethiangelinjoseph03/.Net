namespace Questionnaire.Model.DbSet
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Logs
    {
        [Required]
        public int Id { get; set; } // Represents the primary key or unique identifier        
        public string? Message { get; set; }  // Error or log message       
        public string? MessageTemplate { get; set; } // Template for log formatting       
        public string? Level { get; set; } // Severity level (e.g., Info, Error, Debug)
        [Required]
        [Column(TypeName = "datetime")]
        public DateTime TimeStamp { get; set; } // The time the log occurred       
        public string? Exception { get; set; }  // Exception details, if applicable       
        public string? Properties { get; set; } // Additional details or metadata
    }
}