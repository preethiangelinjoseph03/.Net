using System.Text.Json.Serialization;

namespace Questionnaire.Model.Entity
{
    public class LanguageDTO
    {       
        public int Id { get; set; }
        public string LanguageName { get; set; } = null!;
    }
}