using ApprovalChain.Models;

namespace ApprovalChain.Repository
{
    public interface IEmployeeRepo
    {
        public List<Employee> GetEmployees();
        public void FileProcessStart(ArcDocument document);
        public List<ArcDocument> GetDocument();
        public void changeRecommender(ArcDocument document);
        public void RejectDocument(ArcDocument document,string rejectionReason);
        public void UpdateDocument(ArcDocument document);
        public ArcDocument GetSingleDocument(int id);
    }
}