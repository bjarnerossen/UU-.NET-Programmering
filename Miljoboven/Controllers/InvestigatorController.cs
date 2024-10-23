using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Miljoboven.Models;

namespace Miljoboven.Controllers
{
    [Authorize(Roles = "Investigator")]
    public class InvestigatorController : Controller
    {
        private readonly IMiljobovenRepository repository;
        private IHttpContextAccessor contextAcc;

        public InvestigatorController(IMiljobovenRepository repo, IHttpContextAccessor cont)
        {
            repository = repo;
            contextAcc = cont;
        }

        public ViewResult StartInvestigator(string status, string caseNumber)
        {
            ViewBag.UserName = contextAcc.HttpContext.User.Identity.Name;
            string user = contextAcc.HttpContext.User.Identity.Name;

            // Get the complete errand list
            var errandList = repository.GetErrandListInvestigator(user).ToList();

            // If a case number is provided, filter the list by the exact case number
            if (!string.IsNullOrWhiteSpace(caseNumber))
            {
                errandList = errandList.Where(e => e.RefNumber == caseNumber).ToList();
            }
            else
            {
                // Filter by status if it's not "Välj alla" or empty
                if (!string.IsNullOrWhiteSpace(status) && status != "Välj alla")
                {
                    errandList = errandList.Where(e => e.StatusName == status).ToList();
                }
            }

            // Check if the result list is empty
            if (!errandList.Any())
            {
                ViewBag.Message = $"Inga ärenden hittades för: \nStatus: {status}";
            }

            // Assign the filtered list to the view
            ViewBag.ErrandList = errandList;

            return View(repository);
        }

        public ViewResult CrimeInvestigator(int id)
        {
            ViewBag.ID = id;
            return View(repository);
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
                        repository.SaveSample(id, fileName);
                    else
                        repository.SavePicture(id, fileName);
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
            // Validate that there is at least one non-empty field before calling the repo
            if (!string.IsNullOrEmpty(events) || !string.IsNullOrEmpty(information) || !string.IsNullOrEmpty(status))
            {
                repository.InvestigatorEdit(id, events, information, status);
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
