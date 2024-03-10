using Microsoft.EntityFrameworkCore;
using onlineExamInestractour.Models;
using onlineExamInestractour.Repository;

namespace onlineExamInestractour
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddTransient<IInstructorRepository,InstructorRepo>();
            builder.Services.AddDbContext<ExamDbContext>(a =>
            {
                a.UseSqlServer(builder.Configuration.GetConnectionString("con1"));
            });
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
