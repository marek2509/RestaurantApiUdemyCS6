using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;

namespace RestaurantApiUdemyCS6.Controllers
{
    [Route("file")]
    [Authorize]

    public class FileController : Controller
    {
        public ActionResult GetFile([FromQuery]string fileName)
        {
            var rootPath = Directory.GetCurrentDirectory();
            var filePath = $"{rootPath}/PrivateFiles/{fileName}";

           var fileExists = System.IO.File.Exists(filePath);

            if (!fileExists)
            {
                return NotFound();
            }
            var contentProvider = new FileExtensionContentTypeProvider();
            contentProvider.TryGetContentType(filePath, out var contentType);

           //ładowanie do pamięci
           var fileContents = System.IO.File.ReadAllBytes(filePath);

            return File(fileContents, contentType, fileName);
        }
    }
}
