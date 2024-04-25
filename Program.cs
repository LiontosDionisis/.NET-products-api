using AutoMapper;
using FluentValidation;
using ProductsApp.DAO;
using ProductsApp.DTO;
using ProductsApp.Services;
using ProductsApp.Validators;
using Serilog;

namespace ProductsApp
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);
			builder.Host.UseSerilog((context, config) =>
			{
				config.ReadFrom.Configuration(context.Configuration);
				/*config
					.MinimumLevel.Debug()
					.MinimumLevel.Override("Microsoft", LogEventLevel.Information)
					.Enrich.FromLogContext()
					//.WriteTo.Console()
					.WriteTo.File(
						"Logs/logs.txt",
						rollingInterval: RollingInterval.Day,   // Create a new log file daily
						outputTemplate: "[{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} {SourceContext} [{Level}] " +
										"{Message}{NewLine}{Exception}",
						retainedFileCountLimit: null, // Set to null to keep all log files
						fileSizeLimitBytes: null // Set to null to disable file size limit
					); */
			});

			// Add services to the container.
			builder.Services.AddRazorPages();
			builder.Services.AddScoped<IProductDAO, ProductDAOImpl>();
			builder.Services.AddScoped<IProductService, ProductServiceImpl>();
			builder.Services.AddScoped<IValidator<ProductInsertDTO>, ProductInsertValidator>();
			builder.Services.AddScoped<IValidator<ProductUpdateDTO>, ProductUpdateValidator>();
			builder.Services.AddAutoMapper(typeof(Mapper));

			var app = builder.Build();

			// Configure the HTTP request pipeline.
			if (!app.Environment.IsDevelopment())
			{
				app.UseExceptionHandler("/Error");
				// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
				app.UseHsts();
			}

			app.UseHttpsRedirection();
			app.UseStaticFiles();

			app.UseRouting();

			app.UseAuthorization();

			app.MapRazorPages();

			app.Run();
		}
	}
}
