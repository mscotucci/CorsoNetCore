using Microsoft.EntityFrameworkCore;
using TestWebApi.Infrastructure;

namespace TestWebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddDbContext<BookStoreDbContext>(options => 
                options.UseSqlServer(builder.Configuration.GetConnectionString("BookStoreDatabase")));

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            //Inizializziamo il db se necessario
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                DataBaseInitializer.Initialize(services);
            }

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.Use(async (context, next) =>
            {
                try
                {
                    await next();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    context.Response.StatusCode = 500;
                }
            });

            app.MapControllers();

            app.Run();
        }
    }
}