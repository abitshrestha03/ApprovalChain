using ApprovalChain.Data;
using ApprovalChain.Models;

namespace ApprovalChain.Repository
{
    public class AuthRepo : IAuthRepo
    {
        public readonly ApplicationDbContext _dbContext;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public AuthRepo(ApplicationDbContext dbContext, IHttpContextAccessor httpContextAccessor)
        {
            _dbContext = dbContext;
            _httpContextAccessor = httpContextAccessor;
        }
        public void CreateEmployee(Employee employee)
        {
            _dbContext.Employees.Add(employee);
            _dbContext.SaveChanges();
        }
        public bool CheckEmployee(string Email, string Password)
        {
            var emailFound = _dbContext.Employees.FirstOrDefault(e => e.EmployeeEmail == Email);
            if (emailFound != null)
            {
                if (emailFound.EmployeePassword == Password)
                {
                    _httpContextAccessor.HttpContext.Session.SetInt32("EmployeeId", emailFound.EmployeeId);
                    _httpContextAccessor.HttpContext.Session.SetString("EmployeeEmail", emailFound.EmployeeEmail);
                    return true;
                }
            }
            return false;
        }
    }
}
