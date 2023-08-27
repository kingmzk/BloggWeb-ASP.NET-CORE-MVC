using BloggWebSite.Models.Domain;

namespace BloggWebSite.Models.ViewModels
{
    public class HomeViewModel
    {
        public IEnumerable<BlogPost> BlogPosts { get; set; }

        public IEnumerable<Tag> Tags { get; set; }
    }
}