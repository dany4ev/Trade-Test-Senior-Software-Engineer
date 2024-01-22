using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

using Trade_Test.Data.EfModels;
using Trade_Test.Models;
using Trade_Test.Utilities.Extensions;

using Trade_Test_Web.Data.EfModels;
using Trade_Test_Web.Models.Enums;
using Trade_Test_Web.Utilities.Middlewares;

namespace Trade_Test_Web {
    public class Program {

        public static async Task Main(string[] args) {
            var builder = WebApplication.CreateBuilder(args);


            // Add services to the container.
            builder.Services.AddHttpContextAccessor();
            builder.Services.AddControllersWithViews();

            // NOTE: keeping these secrets here is not a good practice and these will be moved into some sort of web vault
            var TradeTestConnectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(TradeTestConnectionString));

            builder.Services.AddDbContext<TradeTestDbContext>(options => {
                options.UseSqlServer(TradeTestConnectionString);
            });

            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();


            // Note: Add DI registrations for all dependencies here
            builder.Services.RegisterDependencies();

            builder.Services.AddMvc();
            builder.Services.AddControllers().AddNewtonsoftJson();

            builder.Services.AddEndpointsApiExplorer();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment()) {
                app.UseMigrationsEndPoint();
                app.UseDeveloperExceptionPage();
            }
            else {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseMiddleware<GlobalExceptionHandlingMiddleware>();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.MapRazorPages();

            // NOTE: Check and create roles on application startup if not existing already
            using (var scope = app.Services.CreateScope()) {

                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                var roles = new string[] { nameof(RoleType.Admin), nameof(RoleType.Patron) };

                foreach (var role in roles) {

                    if (!await roleManager.RoleExistsAsync(role))
                        await roleManager.CreateAsync(new IdentityRole(role));
                }
            }

            using (var scope = app.Services.CreateScope()) {

                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
                string adminUserName = builder.Configuration.GetValue<string>("Admin:UserName");
                string adminEmail = builder.Configuration.GetValue<string>("Admin:Email");
                string adminPhoneNumber = builder.Configuration.GetValue<string>("Admin:PhoneNumber");
                string adminPassword = builder.Configuration.GetValue<string>("Admin:Password");

                if (await userManager.FindByEmailAsync(adminEmail) == null) {

                    User newUser = new() {
                        UserName = adminUserName,
                        Email = adminEmail,
                        PhoneNumber = adminPhoneNumber,
                        PasswordHash = adminPassword
                    };

                    await userManager.CreateAsync(newUser);

                    await userManager.AddToRoleAsync(newUser, nameof(RoleType.Admin));
                }
            }

            app.Run();
        }
    }
}