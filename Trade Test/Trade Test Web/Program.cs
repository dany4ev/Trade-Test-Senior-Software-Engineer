using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

using Trade_Test.Data.EfModels;
using Trade_Test.Utilities.Extensions;
using Trade_Test_Web.Data.EfModels;

namespace Trade_Test_Web
{
    public class Program {

        //Server=(localdb)\\mssqllocaldb;Database=aspnet-Trade_Test_Web-c517d245-dd7c-4027-995c-69e205d63d06;Trusted_Connection=True;MultipleActiveResultSets=true
        // NOTE: keeping these secrets here is not a good practice and these will be moved into some sort of web vault
        public readonly static string TradeTestConnectionString = "Data Source=localhost;Integrated Security=True;Trust Server Certificate=True";
        public static void Main(string[] args) {
            var builder = WebApplication.CreateBuilder(args);


            // Add services to the container.
            builder.Services.AddHttpContextAccessor();
            builder.Services.AddControllersWithViews();

            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));
            
            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<ApplicationDbContext>();

            // Note: Add DI registrations for all dependencies here
            builder.Services.RegisterDependencies();

            builder.Services.AddDbContext<TradeTestDbContext>(options => {
                options.UseSqlServer(TradeTestConnectionString);
            });

            builder.Services.AddMvc();
            builder.Services.AddControllers().AddNewtonsoftJson();

            // Note: Added to allow image/document file uploading
            builder.Services.Configure<FormOptions>(o => {
                o.ValueLengthLimit = int.MaxValue; // Limit on individual form values
                o.MultipartBodyLengthLimit = int.MaxValue; // Limit on form body size
                o.MemoryBufferThreshold = int.MaxValue; // Limit on form header size
            });

            builder.Services.Configure<IISServerOptions>(options => {
                options.MaxRequestBodySize = 73400320; // Limit request size to 70 MB (73400320 bytes)
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment()) {
                app.UseMigrationsEndPoint();
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
            
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            
            app.MapRazorPages();

            app.Run();
        }
    }
}