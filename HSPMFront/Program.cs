using AuthService.Login;
using AuthService.Services;
using HSPMFront.Services;

namespace HSPMFront
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews(); // Ajoutez les services de contrôleurs avec vues
            builder.Services.AddHttpClient<ApiGatewayService>(); // Ajoutez ApiGatewayService avec HttpClient
            builder.Services.AddRazorPages();
            builder.Services.AddSwaggerGen();

            builder.Services.AddScoped<IAuth, AuthenService>();


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
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.MapRazorPages(); // Si vous utilisez également Razor Pages

            app.Run();
        }
    }
}
