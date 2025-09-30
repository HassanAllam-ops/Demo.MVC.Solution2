using Demo.BusnissLogic.Profiles;
using Demo.BusnissLogic.Services.Classes;
using Demo.BusnissLogic.Services.Interfaces;
using Demo.DataAccess.Data.Contexts;
using Demo.DataAccess.Models.IdentityModels;
using Demo.DataAccess.Repositories.Departments;
using Demo.DataAccess.Repositories.Empolyees;
using Demo.DataAccess.Repositories.UOW;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Demo.PresentationLayer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            #region Add services to the container
            // Add services to the container.
            builder.Services.AddControllersWithViews(options =>
            {
                options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
            });


            builder.Services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
                options.UseLazyLoadingProxies();
            });

            builder.Services.AddScoped<IDepartmentRepositories, DepartmentRepositories>();
            builder.Services.AddScoped<IDepartmentServices, DepartmentServices>();
            builder.Services.AddScoped<IEmpolyeeRepository, EmpolyeeRepository>();

            builder.Services.AddAutoMapper(M => M.AddProfile(new MappingProfile()));

            builder.Services.AddScoped<IEmpolyeeServices, EmpolyeeServices>();
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddScoped<IAttachmentService, AttachmentService>();

            builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.Password.RequiredLength = 6;
                options.Password.RequireUppercase = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireDigit = true;
                options.Password.RequireNonAlphanumeric = true;

                options.User.RequireUniqueEmail = true;

                options.Lockout.AllowedForNewUsers = true;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromDays(2);
                options.Lockout.MaxFailedAccessAttempts = 5;
            }).AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();

            #endregion

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

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Account}/{action=LogIn}/{id?}");

            app.Run();
        }
    }
}
