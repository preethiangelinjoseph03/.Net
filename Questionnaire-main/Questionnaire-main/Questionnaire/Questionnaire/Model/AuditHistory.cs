namespace Questionnaire.Model
{
    public class AuditHistory
    {
        public int Id { get; set; } 
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public String Status { get; set; }

    }
}
