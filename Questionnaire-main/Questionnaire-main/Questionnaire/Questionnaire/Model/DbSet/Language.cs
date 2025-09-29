using System.ComponentModel.DataAnnotations;

namespace Questionnaire.Model.DbSet
{
    public class Language
    {
        public int Id { get; set; }
        [Required]
        public string LanguageName { get; set; } = null!;
    }
}