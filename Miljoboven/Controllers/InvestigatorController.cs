using Microsoft.AspNetCore.Mvc;
using Miljoboven.Models;

namespace Miljoboven.Controllers
{
    public class InvestigatorController : Controller
    {
        private readonly IMiljobovenRepository _repository;

        public InvestigatorController(IMiljobovenRepository repository)
        {
            _repository = repository;
        }

        public ViewResult StartInvestigator()
        {
            return View(_repository);
        }

        public ViewResult CrimeInvestigator(int id)
        {
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
