namespace Front
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Ajout des services n�cessaires
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configuration de l'application
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            // Servir les fichiers statiques de React
            app.UseDefaultFiles(); // Cela permet de servir index.html par d�faut
            app.UseStaticFiles(); // Cela permet de servir des fichiers statiques du r�pertoire wwwroot

            // Rediriger les requ�tes non g�r�es par l'API vers l'application React
            app.MapFallbackToFile("index.html");

            app.Run();
        }
    }
}
