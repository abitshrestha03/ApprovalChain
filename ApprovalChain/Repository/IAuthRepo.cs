using ApprovalChain.Models;

namespace ApprovalChain.Repository
{
    public interface IAuthRepo
    {
        void CreateEmployee(Employee employee);
        bool CheckEmployee(string Email,string Password);
    }
}
