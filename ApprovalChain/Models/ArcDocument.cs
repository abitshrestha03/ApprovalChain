using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApprovalChain.Models
{
    public class ArcDocument
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ArcDocumentId { get; set; }
        public string ArcDocumentName { get; set; }
        public string ArcDocumentDescription { get; set; }
        public ICollection<int> EmployeeIds { get; set; }
        public int? ChangeHandler { get; set; }
        public enum DocumentStatus
        {
            Pending,
            Rejected,
            Approved
        }
        public DocumentStatus Status { get; set; }
        [ForeignKey("Initiator")]
        public int EmployeeId { get; set; }
        public Employee Initiator { get; set; }
        public string? Remarks { get; set; }
    }
}
