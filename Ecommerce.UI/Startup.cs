using Ecommerce.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Ecommerce;
using Ecommerce.BLL.Abstract;
using Ecommerce.BLL.Concrate;

namespace Ecommerce.UI
{
    //Proje �lk Run edildi�inde �al��an kod sat�rlar�  "Startup" i�erisinde bulunur 
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            #region Register EcommerceDbContext with DependencyInjection 
      
            services.AddDbContext<EcommerceDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("EcommerceDevConnection")));

            //Dependency Injection Lifetimes - Scoped, Singleton, Transient(container'dan istenen objenin ne zaman instance create edilece�ini yada ne zaman yeniden create edilmesi gerekti�ini sa�lar)
            //Container Implemantasyon
            services.AddScoped<IProductRepository, ProductRepository>(); //Kullan�c�ya �zg� instance , kullan�c� instance'� bir kere olu�tuurup onun �zerinden i�lem
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
         
            #endregion

            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
