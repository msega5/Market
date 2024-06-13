using Autofac;
using Autofac.Extensions.DependencyInjection;
using Market.Models;
using Market.Repo;
using Microsoft.Extensions.FileProviders;

namespace Market
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var config = new ConfigurationBuilder();
            config.AddJsonFile("appsettings.json");
            var cfg = config.Build();

            builder.Host.ConfigureContainer<ContainerBuilder>(contaierBuilder =>
            {
                contaierBuilder.Register(c => new StoreContext(cfg.GetConnectionString("db"))).InstancePerDependency();
            });

            builder.Services.AddMemoryCache(options =>
            {
                options.TrackStatistics = true;
            });


            builder.Services.AddAutoMapper(typeof(MappingProfile));
            builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

            var app = builder.Build();
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseAuthorization();
            var staticFilesPath = Path.Combine(Directory.GetCurrentDirectory(), "StaticFiles");
            Directory.CreateDirectory(staticFilesPath);
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(
                    staticFilesPath),
                RequestPath = "/static"
            });
            app.MapControllers();
            app.Run();
        }
    }
}
