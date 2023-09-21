using Microsoft.AspNetCore.Identity;

namespace BloggWebSite.Repositories
{
    public interface IUserRepository
    {
        Task<IEnumerable<IdentityUser>> GetAll();
    }
}