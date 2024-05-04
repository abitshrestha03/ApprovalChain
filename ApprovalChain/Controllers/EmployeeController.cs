using ApprovalChain.Data;
using ApprovalChain.Models;
using ApprovalChain.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApprovalChain.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IHttpContextAccessor _sessionContxt;
        private readonly IEmployeeRepo _employeeRepo;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public EmployeeController(IEmployeeRepo employeeRepo, IWebHostEnvironment webHostEnvironment, IHttpContextAccessor sessionContxt)
        {
            _employeeRepo = employeeRepo;
            _webHostEnvironment = webHostEnvironment;
            _sessionContxt = sessionContxt;
        }
        public IActionResult Index()
        {
            List<Employee> employees = _employeeRepo.GetEmployees();
            List<ArcDocument> arcDocuments = _employeeRepo.GetDocument();
            ViewBag.EmployeeList = employees.Distinct().ToList();
            return View(arcDocuments);
        }
        public IActionResult InitiateFile(ArcDocument arcDocument, IFormFile? ArcDocumentName, List<Employee> Recommenders, Employee Approvailer)
        {
            ICollection<int> ids = new List<int>();
            foreach (var employeeId in Recommenders)
            {
                ids.Add(employeeId.EmployeeId);
            }
            ids.Add(Approvailer.EmployeeId);
            arcDocument.EmployeeId = (_sessionContxt.HttpContext.Session.GetInt32("EmployeeId") ?? default(int));
            arcDocument.EmployeeIds = ids;
            arcDocument.ChangeHandler = ids.FirstOrDefault();

            string wwwRootPath = _webHostEnvironment.WebRootPath;

            if (ArcDocumentName != null)
            {
                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(ArcDocumentName.FileName);
                string productPath = Path.Combine(wwwRootPath, "Image", "Upload");
                string filePath = Path.Combine(productPath, fileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    ArcDocumentName.CopyTo(fileStream);
                }

                arcDocument.ArcDocumentName = @"/Image/Upload/" + fileName;
            }
            if (arcDocument.ArcDocumentId!=0)
            {
                _employeeRepo.UpdateDocument(arcDocument);
            }
            else
            {
                _employeeRepo.FileProcessStart(arcDocument);
            }
            return RedirectToAction("Index");
        }
        public IActionResult Approval()
        {
            List<ArcDocument> arcDocument = _employeeRepo.GetDocument();
            return View(arcDocument);
        }
        [HttpPost]
        public IActionResult Approve(ArcDocument document)
        {
            _employeeRepo.changeRecommender(document);
            return RedirectToAction();
        }
        [HttpPost]
        public IActionResult RejectDocument(string rejectionReason, ArcDocument arcDocument)
        {
            _employeeRepo.RejectDocument(arcDocument, rejectionReason);
            return View();
        }
    }
}
