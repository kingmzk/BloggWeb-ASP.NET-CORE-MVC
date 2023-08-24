using BloggWebSite.Data;
using BloggWebSite.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace BloggWebSite.Repositories
{
    public class TagRepository : ITagRepository
    {
        private readonly BloggieDbContext bloggieDbContext;
        //private readonly BloggieDbContext _bloggieDbContext;

        public TagRepository(BloggieDbContext bloggieDbContext)
        {
            this.bloggieDbContext = bloggieDbContext;
            //_bloggieDbContext = bloggieDbContext;
        }

        public async Task<Tag?> AddAsync(Tag tag)
        {
            await bloggieDbContext.Tags.AddAsync(tag);
            bloggieDbContext.SaveChanges();
            return tag;
        }

        public async Task<Tag?> DeleteAsync(Guid id)
        {
            var existingTag = await bloggieDbContext.Tags.FindAsync(id);

            if (existingTag != null)
            {
                bloggieDbContext.Tags.Remove(existingTag);
                await bloggieDbContext.SaveChangesAsync();
                return existingTag;
            }
            else
            {
                return null;
            }
        }

        public async Task<IEnumerable<Tag>> GetAllAsync()
        {
            return await bloggieDbContext.Tags.ToListAsync();
        }

        public Task<Tag?> GetAsync(Guid id)
        {
            //var tag = bloggieDbContext.Tags.Find(id);   //=> 1st method
            //var tag = await bloggieDbContext.Tags.FirstOrDefaultAsync(x => x.Id == id);  //=> 2nd method
            return bloggieDbContext.Tags.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Tag?> UpdateAsync(Tag tag)
        {
            var existingTag = await bloggieDbContext.Tags.FindAsync(tag.Id);

            if (existingTag != null)
            {
                existingTag.Name = tag.Name;
                existingTag.DisplayName = tag.DisplayName;

                await bloggieDbContext.SaveChangesAsync();

                return existingTag;
            }

            return null;
        }
    }
}