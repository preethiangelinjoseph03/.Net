using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Questionnaire.Model.DbSet;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace Questionnaire.Data
{
    public class QuestionnaireDBContext : IdentityDbContext<ApplicationUser, IdentityRole, string>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public QuestionnaireDBContext(DbContextOptions<QuestionnaireDBContext> options, IHttpContextAccessor httpContextAccessor)
            : base(options)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public DbSet<Language> Languages { get; set; }
        public DbSet<Question> Question { get; set; }
        public DbSet<Logs> Logs { get; set; }
        public DbSet<CandidateAnswers> CandidateAnswers { get; set; }
        public DbSet<OptionsAndAnswer> OptionsAndAnswer { get; set; }
        public DbSet<AnswerEvaluation> AnswerEvaluation { get; set; }

        public override int SaveChanges()
        {
            UpdateAuditFields();
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            UpdateAuditFields();
            return base.SaveChangesAsync(cancellationToken);
        }

        private void UpdateAuditFields()
        {
            var entries = ChangeTracker
                .Entries()
                .Where(e => e.Entity is Question &&
                            (e.State == EntityState.Added || e.State == EntityState.Modified));

            var currentUser = GetCurrentUsername();

            foreach (var entry in entries)
            {
                var entity = (Question)entry.Entity;

                if (entry.State == EntityState.Added)
                {
                    entity.CreatedOn = DateTime.UtcNow;
                    entity.CreatedBy = currentUser;
                    entity.ModifiedOn = DateTime.UtcNow;   // Optionally set ModifiedOn on create too
                    entity.ModifiedBy = currentUser;
                }
                else if (entry.State == EntityState.Modified)
                {
                    entity.ModifiedOn = DateTime.UtcNow;
                    entity.ModifiedBy = currentUser;
                }
            }
        }

        private string GetCurrentUsername()
        {
            var user = _httpContextAccessor.HttpContext?.User;
            if (user == null) return "System";

            var username = user.FindFirstValue(ClaimTypes.Name);
            return string.IsNullOrEmpty(username) ? "System" : username;
        }
    }
}
