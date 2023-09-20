using BloggWebSite.Data;
using BloggWebSite.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<BloggieDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("BloggieDbConnectionString")));

//// Define a method to configure DbContextOptions
//public void ConfigureBloggieDbContext(DbContextOptionsBuilder optionsBuilder)
//{
//    optionsBuilder.UseSqlServer(builder.Configuration.GetConnectionString("BloggieDbConnectionString"));
//}

//// Add DbContext to services using the configured options
//builder.Services.AddDbContext<BloggieDbContext>(ConfigureBloggieDbContext);

builder.Services.AddDbContext<AuthDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("BloggieAuthDbConnectionString")));

builder.Services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<AuthDbContext>();

builder.Services.Configure<IdentityOptions>(options =>
{
    //Default Settings
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequireUppercase = true;
    options.Password.RequiredLength = 6;
    options.Password.RequiredUniqueChars = 1;
});

builder.Services.AddScoped<ITagRepository, TagRepository>();

builder.Services.AddScoped<IBlogPostRepository, BlogPostRepository>();

builder.Services.AddScoped<IImageRepository, CloudinaryImageRepository>();

builder.Services.AddScoped<IBlogPostLikeRepository, BlogPostLikeRepository>();

builder.Services.AddScoped<IBlogPostCommentRepository, BlogPostCommentRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();//add this for auth

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();