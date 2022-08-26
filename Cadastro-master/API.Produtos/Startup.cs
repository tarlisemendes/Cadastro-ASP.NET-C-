using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Produtos.DAL;
using API.Produtos.Models;
using API.Produtos.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Swagger;

namespace API.Produtos
{
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            string connectionString = Configuration.GetConnectionString("Default");

            services.AddDbContext<Context>(options =>
                options.UseSqlServer(connectionString)
            );

            services.AddTransient<IData, Data>();
            services.AddTransient<IRepository<Produto>, Repository<Produto>>();
            services.AddTransient<IRepository<Fornecedor>, Repository<Fornecedor>>();
            services.AddTransient<IRepository<Usuario>, Repository<Usuario>>();

            //services.AddTransient<IService<Produto>, Service<Produto>>();
            services.AddSwaggerGen(options => {
                
                options.SwaggerDoc("v1", new Info { Title = "API - CRUD ", Description = "Documentação da API", Version = "1.0" });
                
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, 
            IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();


            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Versão 1.0");
               
            });

            //serviceProvider.GetService<IData>().InicializaDB();

        }
    }
}
