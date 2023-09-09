using BloggWebSite.Models.Domain;

namespace BloggWebSite.Repositories
{
    public interface IBlogPostLikeRepository
    {
        Task<int> GetTotalLikes(Guid blogPostId);

        Task<BlogPostLike> AddLikeForBlog(BlogPostLike blogPostLike);
    }
}