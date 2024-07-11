
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using TournamentAPI.Data;
using TournamentAPI.Filters;
using TournamentAPI.Services;
using TournamentAPI.Services.LoggedUser;
using TournamentAPI.Services.Password;
using TournamentAPI.Services.Token;
using TournamentAPI.Services.User;

namespace TournamentAPI;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddDbContext<ApplicationDbContext>(options =>
        options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));
        builder.Services.AddScoped<ITournamentService, TournamentService>();
        builder.Services.AddScoped<IUserService, UserService>();
        builder.Services.AddScoped<ILoggedUser, LoggedUser>();
        builder.Services.AddCors(options =>
           {
               options.AddDefaultPolicy(builder =>
               {
                   builder.AllowAnyOrigin()
                          .AllowAnyMethod()
                          .AllowAnyHeader();
               });
           });

        builder.Services.AddScoped<AuthenticatedUserAttribute>();

        var section = builder.Configuration.GetRequiredSection("Configurations:Key");

        builder.Services.AddScoped(option => new PasswordEncryption(section.Value));

        var sectionKey = builder.Configuration.GetRequiredSection("Configurations:TokenKey");
        var sectionTimeExpired = builder.Configuration.GetRequiredSection("Configurations:TokenTimeExpired");

        builder.Services.AddScoped(options => new TokenController(int.Parse(sectionTimeExpired.Value), sectionKey.Value));

        builder.Services.AddControllers();
        builder.Services.AddHttpContextAccessor();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "Tournament", Version = "v1" });

            // Configuração para autenticação usando Bearer token (JWT)
            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Description = "Token de autorização Bearer no formato JWT",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey
            });

            // Adicione a política de autorização
            c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" }
                        },
                        new string[] {}
                    }
                });
        });


        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseCors();

        app.UseHttpsRedirection();

        app.UseAuthorization();


        app.MapControllers();

        app.Run();
    }
}
