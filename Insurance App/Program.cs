using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PojisteniApp.Data;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using PojisteniApp.Models;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
        builder.Services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(connectionString));
        builder.Services.AddDatabaseDeveloperPageExceptionFilter();

        builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>();
        builder.Services.AddControllersWithViews();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseMigrationsEndPoint();
        }
        else
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
        app.MapRazorPages();

        /*
        using (var scope = app.Services.CreateScope())
        {
            //var name = new InsuracePersonData();
            //name.Email = "email";

            RoleManager<IdentityRole> spravceRoli = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            UserManager<IdentityUser> spravceUzivatelu = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();

            spravceRoli.CreateAsync(new IdentityRole("user")).Wait();
            
            IdentityUser uzivatel = spravceUzivatelu.FindByEmailAsync("user@mail.com").Result;

            spravceUzivatelu.AddToRoleAsync(uzivatel, "user").Wait();
            
        }
        */

        app.Run();
    }
}