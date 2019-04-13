using System;
using AutoMapper;
using ElQueue.BLL.Services;
using ElQueue.DAL.Infrastructure;
using ElQueue.DAL.Repositories;
using ElQueue.DAL.UnitOfWork;
using ElQueue.Orchestrator.QueueOrchestrator;
using ElQueue.Web.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ElQueue.Web
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
            services.AddDbContext<QueueContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("QueueConnection")));

            ConfigureApplicationServices(services);

            services.AddAutoMapper();
            services.AddSwaggerGen(SwaggerConfig.Register());
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        private void ConfigureApplicationServices(IServiceCollection services)
        {
            services.AddScoped<IQueueRepository, QueueRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IAccountRepository, AccountRepository>();
            services.AddScoped<ITimeSlotRepository, TimeSlotRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<IQueueService, QueueService>();

            services.AddScoped<IQueueOrchestrator, QueueOrchestrator>();

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
                app.UseHsts();
            }

            ConfigureSwagger(app);
            app.UseHttpsRedirection();
            app.UseMvc();
        }

        private void ConfigureSwagger(IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c => {
                c.SwaggerEndpoint("../swagger/v1/swagger.json", "ElQueue V1");
            });
        }
    }
}
