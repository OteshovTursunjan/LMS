using LMS.Context;
using LMS.Repository;
using LMS.Service;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

using LMS.Models;

namespace LMS
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);



            builder.Services.AddControllersWithViews();
            string connectionStrings = "Host=localhost;Port=5432; Database=Git_LMS; " +
            "Username=postgres; Password=123";

            builder.Services.AddScoped<IUserRepository, UserRepository>();

            builder.Services.AddDbContext<AppDbContext>(options => options.UseNpgsql(connectionStrings));


        
            builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            builder.Services.AddScoped<IGroupRepository, GroupRepository>();
            builder.Services.AddScoped<IContractRepository, ContractRepository>();
            builder.Services.AddScoped<ISubjectRepository, SubjectRepository>();
            builder.Services.AddScoped<IStudentRepository, StudentRepository>();
            builder.Services.AddScoped<IWorkTableRepository, WorkTableRepository>();
           
            builder.Services.AddScoped<IAppService, AppService>();
            

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

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
