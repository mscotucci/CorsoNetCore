using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using TestWebApi.Application;
using TestWebApi.Application.Impl;
using TestWebApi.Infrastructure;
using TestWebApi.Infrastructure.ADO;
using TestWebApi.Infrastructure.EFCore;

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

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidIssuer = builder.Configuration.GetSection("Jwt:Issuer").Value,
                        ValidateAudience = true,
                        ValidAudience = builder.Configuration.GetSection("Jwt:Audience").Value,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetSection("Jwt:SecretKey").Value))
                    };
                });

            builder.Services.AddTransient<IBooksService, BooksService>();
            builder.Services.AddTransient<IAuthorsService, AuthorsService>();
            builder.Services.AddTransient<ILoginService, LoginService>();
            builder.Services.AddTransient<IJwtService, JwtService>();
            builder.Services.AddTransient<IUsersService, UsersService>();
            builder.Services.AddTransient<IRoleService, RolesService>();

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

            app.UseAuthentication();
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