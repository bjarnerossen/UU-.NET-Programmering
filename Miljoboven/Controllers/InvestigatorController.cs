using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Miljoboven.Models;

namespace Miljoboven.Controllers
{
    [Authorize(Roles = "Investigator")]
    public class InvestigatorController : Controller
    {
        private readonly IMiljobovenRepository _repository;
        private IHttpContextAccessor _contextAcc;

        public InvestigatorController(IMiljobovenRepository repository, IHttpContextAccessor cont)
        {
            _repository = repository;
            _contextAcc = cont;
        }

        public ViewResult StartInvestigator()
        {
            var employeeId = _contextAcc.HttpContext.User.Identity.Name;
            Employee investigator = _repository.GetEmployeeDetails(employeeId);
            ViewBag.Username = investigator.EmployeeName;
            return View(_repository);
        }

        public ViewResult CrimeInvestigator(int id)
        {
            var employeeId = _contextAcc.HttpContext.User.Identity.Name;

            Employee investigator = null;
            foreach (Employee employee in _repository.Employees)
            {
                if (employeeId == employee.EmployeeId)
                {
                    investigator = employee;
                }
            }

            ViewBag.UserName = investigator.EmployeeName;
            ViewBag.ID = id;
            return View(_repository);
        }

        private async Task FileManager(int id, IFormFile file, bool isSample)
        {
            if (file.Length > 0)
            {
                // Generate unique file name
                var fileName = Guid.NewGuid() + "_" + file.FileName;

                // Determine the directory based on whether it's a sample or an image
                var directory = isSample ? "uploaded-samples" : "uploaded-pictures";
                var path = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot/uploaded-files/{directory}");

                // Ensure the directory exists
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);

                // Combine the path and file name
                var fullPath = Path.Combine(path, fileName);

                try
                {
                    // Save the file directly to the appropriate directory
                    await using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }

                    // Save the file details to the appropriate table in the database
                    if (isSample)
                        _repository.SaveSample(id, fileName);
                    else
                        _repository.SavePicture(id, fileName);
                }
                catch (Exception ex)
                {
                    // Handle or log error if file saving fails
                    throw new Exception("File upload failed", ex);
                }
            }
        }

        public async Task<IActionResult> SaveInvestigatorChanges(int id, string events, string information, string status,
            IFormFile uploadImage, IFormFile uploadSample)
        {
            // Validate that there is at least one non-empty field before calling the repository
            if (!string.IsNullOrEmpty(events) || !string.IsNullOrEmpty(information) || !string.IsNullOrEmpty(status))
            {
                _repository.InvestigatorEdit(id, events, information, status);
            }

            // Handle sample upload
            if (uploadSample != null)
            {
                await FileManager(id, uploadSample, true);
            }

            // Handle image upload
            if (uploadImage != null)
            {
                await FileManager(id, uploadImage, false);
            }

            // Redirect to the CrimeInvestigator action with the given id
            return RedirectToAction("CrimeInvestigator", new { id });
        }
    }
}
