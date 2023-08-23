using BloggWebSite.Data;
using BloggWebSite.Models.Domain;
using BloggWebSite.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BloggWebSite.Controllers
{
    public class AdminTagsController : Controller
    {
        private readonly BloggieDbContext bloggieDbContext;
        //private readonly BloggieDbContext _bloggieDbContext;

        public AdminTagsController(BloggieDbContext bloggieDbContext)
        {
            this.bloggieDbContext = bloggieDbContext;
            //_bloggieDbContext = bloggieDbContext;
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        /*
        [HttpPost]           //=> this appraoch is reading from incoming request
        [ActionName("Add")]
        public IActionResult SubmitTag()
        {
            var name = Request.Form["name"];
            var DisplayName = Request.Form["DisplayName"];

            return View("Add");
        }
        */

        //ModelBinding

        [HttpPost]
        [ActionName("Add")]
        public async Task<IActionResult> Add(AddTagRequest addTagRequest)
        {
            //we are mapping AddRequest to tag domainModel
            var tag = new Tag
            {
                Name = addTagRequest.Name,
                DisplayName = addTagRequest.DisplayName
            };

            await bloggieDbContext.Tags.AddAsync(tag);
            bloggieDbContext.SaveChanges();
            return RedirectToAction("List");
        }

        [HttpGet]
        [ActionName("List")]
        public async Task<IActionResult> List()
        {
            //use dbcontext to read tags
            var tags = await bloggieDbContext.Tags.ToListAsync();

            return View(tags);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            //var tag = bloggieDbContext.Tags.Find(id);   //=> 1st method
            var tag = await bloggieDbContext.Tags.FirstOrDefaultAsync(x => x.Id == id);  //=> 2nd method

            if (tag != null)
            {
                var editTagRequest = new EditTagRequest()
                {
                    Id = tag.Id,
                    Name = tag.Name,
                    DisplayName = tag.DisplayName
                };

                return View(editTagRequest);
            }

            return View(null);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditTagRequest editTagRequest)
        {
            var tag = new Tag()
            {
                Id = editTagRequest.Id,
                Name = editTagRequest.Name,
                DisplayName = editTagRequest.DisplayName
            };

            var existingTag = await bloggieDbContext.Tags.FindAsync(tag.Id);

            if (existingTag != null)
            {
                existingTag.Name = tag.Name;
                existingTag.DisplayName = tag.DisplayName;
                //saving cahnges
                await bloggieDbContext.SaveChangesAsync();

                //show success Notifications
                //return RedirectToAction("Edit", new { id = editTagRequest.Id });

                return RedirectToAction("List");
            }

            //show failed Notifications
            return RedirectToAction("Edit", new { id = editTagRequest.Id });
        }

        [HttpPost]
        public async Task<IActionResult> Delete(EditTagRequest editTagRequest)
        {
            var tag = await bloggieDbContext.Tags.FindAsync(editTagRequest.Id);

            if (tag != null)
            {
                bloggieDbContext.Tags.Remove(tag);
                await bloggieDbContext.SaveChangesAsync();

                //Show a success Notification
                return RedirectToAction("List");
            }

            //show an error Notification
            return RedirectToAction("Edit", new { id = editTagRequest.Id });
        }
    }
}