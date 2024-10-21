using ProductAPI.Data;
using ProductAPI.Repositories;
using ProductAPI.Services;
using System.Diagnostics.CodeAnalysis;

namespace ProductAPI
{
    //[ExcludeFromCodeCoverage] // Excludes this class from code coverage analysis
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            //builder.Services.AddScoped<DapperContext>();
            builder.Services.AddScoped<IDapperContext, DapperContext>();
            builder.Services.AddScoped<IDapperWrapper, DapperWrapper>();

            builder.Services.AddScoped<IProductRepository, ProductRepository>();
            builder.Services.AddScoped<IProductService, ProductService>();


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();

        }
    }
}
