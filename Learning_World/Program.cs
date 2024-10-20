using Learning_World.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();


builder.Services.AddDbContext<ElearningPlatformContext>(
                options => options.UseSqlServer(builder.Configuration.GetConnectionString("connectionString")));
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    // Default route
    _ = endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");

    // Module route for rendering the full page
    _ = endpoints.MapControllerRoute(
        name: "module",
        pattern: "Learn/index/{id}/module/{moduleId}",
        defaults: new { controller = "Learn", action = "Index" });

    // Partial view route for AJAX requests (lesson content)
    _ = endpoints.MapControllerRoute(
        name: "modulePartial",
        pattern: "Learn/PartsPartialView/{moduleId}",
        defaults: new { controller = "Learn", action = "PartsPartialView" });

    _ = endpoints.MapControllerRoute(
        name: "lessonDisplayPartial",
        pattern: "Learn/lesson/{moduleId}/{lessonType}/{lessonId}",
        defaults: new { controller = "Learn", action = "LessonsPartialView" });

    //_ = endpoints.MapControllerRoute(
    //    name: "lessonDisplayPartial",
    //    pattern: "Learn/LessonDisplayPartialView/{moduleId}/{lessonType}/{lessonId}",
    //    defaults: new { controller = "Learn", action = "LessonDisplayPartialView" });
       
});

app.Run();
