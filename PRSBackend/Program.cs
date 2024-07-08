using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PRSBackend.Data;
namespace PRSBackend;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Services.AddDbContext<PRSBackendContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("PRSBackendContext") ?? throw new InvalidOperationException("Connection string 'PRSBackendContext' not found.")));

        // Add services to the container.
        builder.Services.AddCors(); //manually add this
        builder.Services.AddControllers();
        

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        app.UseCors(x=> x.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()); //manually add this

        app.UseAuthorization();
        app.MapControllers();
        app.Run();
    }
}
