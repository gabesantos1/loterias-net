﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Loterias.Application.Interfaces;
using Loterias.Application.Service;
using Loterias.Data.Context;
using Loterias.Data.Repositories;
using Loterias.Domain.Interfaces.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Loterias.API
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
            services.AddDbContext<LoteriasContext>(options =>
                options.UseSqlite(Configuration.GetConnectionString("Sqlite")));

            services.AddScoped<IRepositoryConcursoSena, RepositoryConcursoSena>();
            services.AddScoped<IRepositoryConcursoLotofacil, RepositoryConcursoLotofacil>();
            services.AddScoped<IRepositoryConcursoQuina, RepositoryConcursoQuina>();
            services.AddScoped<IRepositoryGanhadoresFacil, RepositoryGanhadoresFacil>();
            services.AddScoped<IRepositoryGanhadoresQuina, RepositoryGanhadoresQuina>();
            services.AddScoped<IRepositoryGanhadoresSena, RepositoryGanhadoresSena>();

            services.AddSingleton<ISenaService, SenaService>();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
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
        }
    }
}
