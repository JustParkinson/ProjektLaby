using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SchoolRegister.DAL.EF;
using SchoolRegister.Model.DataModels;
using SchoolRegister.Services.Configuration.AutoMapperProfiles;

namespace SchoolRegister.Web
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAutoMapper(typeof(MainProfile));

            services.AddDbContext<ApplicationDbContext>(options =>
            //here you can define a database type.
            options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"))
            );
            services.AddDatabaseDeveloperPageExceptionFilter();
            services.AddDefaultIdentity<User>(options => options.SignIn.RequireConfirmedAccount = false)
            .AddRoles<Role>()
            .AddRoleManager<RoleManager<Role>>()
            .AddUserManager<UserManager<User>>()
            .AddEntityFrameworkStores<ApplicationDbContext>();
            services.AddTransient(typeof(ILogger), typeof(Logger<Startup>));
            services.AddControllersWithViews();
        }
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}