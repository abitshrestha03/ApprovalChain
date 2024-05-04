using ApprovalChain.Data;
using ApprovalChain.Models;
using Microsoft.EntityFrameworkCore;

namespace ApprovalChain.Repository
{
    public class EmployeeRepo : IEmployeeRepo
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public EmployeeRepo(ApplicationDbContext dbContext, IHttpContextAccessor httpContextAccessor)
        {
            _dbContext = dbContext;
            _httpContextAccessor = httpContextAccessor;
        }
        public List<Employee> GetEmployees()
        {
            List<Employee> employees = _dbContext.Employees.ToList();
            return employees;
        }
        public void FileProcessStart(ArcDocument document)
        {
            _dbContext.ArcDocuments.Add(document);
            _dbContext.SaveChanges();
        }
        public void UpdateDocument(ArcDocument updateDocument)
        {
            _dbContext.ArcDocuments.Update(updateDocument);
            _dbContext.SaveChanges();
        }
        public List<ArcDocument> GetDocument()
        {
            List<ArcDocument> document = _dbContext.ArcDocuments.Include(d => d.Initiator).ToList();
            return document;
        }
        public ArcDocument GetSingleDocument(int id)
        {
            var singleDocument = _dbContext.ArcDocuments.Find(id);
            return singleDocument;
        }
        public void changeRecommender(ArcDocument document)
        {
            var currentId = _httpContextAccessor.HttpContext?.Session.GetInt32("EmployeeId");
            var currentDocument = _dbContext.ArcDocuments.Find(document.ArcDocumentId);
            var Ids = currentDocument?.EmployeeIds.ToArray();
            if (Ids.Contains(currentId.Value))
            {
                int currentIndex = Array.IndexOf(Ids, currentId.Value);
                if (currentIndex < Ids.Length - 1)
                {
                    currentDocument.ChangeHandler = Ids[currentIndex + 1];
                    _dbContext.ArcDocuments.Update(currentDocument);
                    _dbContext.SaveChanges();
                }
                else if (currentIndex == Ids.Length - 1)
                {
                    currentDocument.ChangeHandler = Ids[0];
                    currentDocument.Status = ArcDocument.DocumentStatus.Approved;
                    currentDocument.Remarks = "Approved";
                    _dbContext.ArcDocuments.Update(currentDocument);
                    _dbContext.SaveChanges();
                }
            }
        }
        public void RejectDocument(ArcDocument document, string rejectionReason)
        {
            var rejectionDocument = _dbContext.ArcDocuments.Find(document.ArcDocumentId);
            if (rejectionDocument != null)
            {
                rejectionDocument.Remarks = rejectionReason;
                rejectionDocument.Status = ArcDocument.DocumentStatus.Rejected;
                rejectionDocument.ChangeHandler = null;
                _dbContext.ArcDocuments.Update(rejectionDocument);
                _dbContext.SaveChanges();
            }
        }
    }
}
