using Microsoft.AspNetCore.Http.Features;
using Microsoft.EntityFrameworkCore;

using System.Reflection;

using Trade_Test.Data.EfModels;
using Trade_Test.Utilities.Extensions;

namespace Trade_Test
{
    public class Program
    {
        // NOTE: keeping these secrets here is not a good practice and these will be moved into some sort of web vault
        public readonly static string TradeTestConnectionString = "";
        public readonly static string JwtSecret = "";

        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddHttpContextAccessor();

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // Set the comments path for the Swagger JSON and UI.
            var xmlPath = Path.Combine(AppContext.BaseDirectory, $"{Assembly.GetExecutingAssembly().GetName().Name}.xml");
            builder.Services.AddSwaggerDocumentation("Trade Test API", "v1", xmlPath);


            // Note: Add DI registrations for all dependencies here
            builder.Services.RegisterDependencies();

            var serviceProvider = builder.Services.BuildServiceProvider();

            builder.Services.AddDbContext<TradeTestDbContext>(options =>
            {
                options.UseSqlServer(TradeTestConnectionString);
            });

            // Note: configure jwt authentication
            //builder.Services.ConfigureJwtAuthentication(JwtSecret);

            builder.Services.AddMvc();
            builder.Services.AddControllers().AddNewtonsoftJson();

            // Note: Added to allow image/document file uploading
            builder.Services.Configure<FormOptions>(o =>
            {
                o.ValueLengthLimit = int.MaxValue; // Limit on individual form values
                o.MultipartBodyLengthLimit = int.MaxValue; // Limit on form body size
                o.MemoryBufferThreshold = int.MaxValue; // Limit on form header size
            });

            builder.Services.Configure<IISServerOptions>(options =>
            {
                options.MaxRequestBodySize = 73400320; // Limit request size to 70 MB (73400320 bytes)
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
                app.UseSwaggerDocumentation("/swagger/v1/swagger.json", "Trade Test Api v1");
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            //app.ConfigureCustomExceptionMiddleware();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            //app.MapControllers();
            app.Run();
        }
    }
}
