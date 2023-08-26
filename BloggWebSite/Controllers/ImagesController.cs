using BloggWebSite.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace BloggWebSite.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly IImageRepository imageRepository;

        public ImagesController(IImageRepository imageRepository)
        {
            this.imageRepository = imageRepository;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return Ok("This is the images controller");
        }

        [HttpPost]
        public async Task<IActionResult> UploadAsync(IFormFile file)      //Iformfile is used to post the image
        {
            var imageURL = await imageRepository.UploadAsync(file);

            if(imageURL == null)
            {
                return Problem("Image could not be uploaded!", null,(int)HttpStatusCode.InternalServerError);
            }

            return  new JsonResult(new {link = imageURL});
        } 



    }
}
