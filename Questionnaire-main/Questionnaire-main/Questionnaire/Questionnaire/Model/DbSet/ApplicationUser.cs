using Microsoft.AspNetCore.Identity;

namespace Questionnaire.Model.DbSet
{
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; } = string.Empty;
        
    }
    
}
