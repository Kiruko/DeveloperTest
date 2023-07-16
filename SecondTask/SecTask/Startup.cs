using Microsoft.EntityFrameworkCore;

namespace SecTask
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            string connectionString = Configuration.GetConnectionString("DefaultConnection");

            services.AddDbContext<PatientDbContext>(options =>
                options.UseNpgsql(connectionString)
            );

            //services.AddScoped<PatientDbContext>();
            services.AddScoped<SecTask.Controllers.BMIController>();
            services.AddControllersWithViews();
            services.AddMvc().AddViewOptions(
                options => options.HtmlHelperOptions.ClientValidationEnabled = true);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapControllerRoute(
                    name: "bmi",
                    pattern: "BMI/CalculateBMI",
                    defaults: new { controller = "BMI", action = "CalculateBMI" });
                endpoints.MapControllerRoute(
                    name: "patient",
                    pattern: "BMI/AddPatient",
                    defaults: new { controller = "BMI", action = "AddPatient" });
                endpoints.MapControllerRoute(
                    name: "statistics",
                    pattern: "BMI/GetStatistics",
                    defaults: new { controller = "BMI", action = "GetStatistics" });
                endpoints.MapControllerRoute(
                    name: "statistics",
                    pattern: "BMI/GetStatisticsAge",
                    defaults: new { controller = "BMI", action = "GetStatisticsAge" });
            });
        }
    }
}
