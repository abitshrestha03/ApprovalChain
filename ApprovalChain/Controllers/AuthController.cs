using ApprovalChain.Models;
using ApprovalChain.Repository;
using Microsoft.AspNetCore.Mvc;

namespace ApprovalChain.Controllers
{
    public class AuthController:Controller
    {
        private readonly IHttpContextAccessor _sessionContxt;
        private readonly IAuthRepo _authRepo;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public AuthController(IAuthRepo authRepo, IHttpContextAccessor sessionContxt)
        {
            _authRepo = authRepo;
            _sessionContxt = sessionContxt;
        }
        public IActionResult Signup()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Signup(Employee employee)
        {
            if (ModelState.IsValid)
            {
                if (employee == null)
                {
                    return RedirectToAction("Signup");
                }
                _authRepo.CreateEmployee(employee);
                return RedirectToAction("Login");
            }
            return RedirectToAction("Signup");
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(Employee employee)
        {
            {
                if (employee == null)
                {
                    return RedirectToAction("Signup");
                }
                var result=_authRepo.CheckEmployee(employee.EmployeeEmail,employee.EmployeePassword);
                if (result){
                    return RedirectToAction("Index", "Employee");
                }
                return RedirectToAction("Login");
            }
        }
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}
