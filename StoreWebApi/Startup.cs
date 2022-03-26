using Domain.Interfaces;
using Domain.Models;
using Infrastructure.Models;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StoreWebApi.v1.Controllers.Articles.Services;
using StoreWebApi.v1.Controllers.Articles.Services.Intefaces;
using StoreWebApi.v1.Controllers.Customers.Services;
using StoreWebApi.v1.Controllers.Customers.Services.Interfaces;
using StoreWebApi.v1.Controllers.Purchases.Services;
using StoreWebApi.v1.Controllers.Purchases.Services.Interfaces;

namespace StoreWebApi
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            
            services.AddDbContext<StoreDbContext>(options => options.UseSqlServer("Data Source=.;Initial Catalog=StoreDB;Integrated Security=True"));
            services.AddTransient<IRepository<Article>, ArticleRepository>();
            services.AddTransient<IRepository<Customer>, CustomerRepository>();
            services.AddTransient<IRepository<Purchase>, PurchaseRepository>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddTransient<IArticleService, ArticleService>();
            services.AddTransient<ICustomerService, CustomerService>();
            services.AddTransient<IPurchaseService, PurchaseService>();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
