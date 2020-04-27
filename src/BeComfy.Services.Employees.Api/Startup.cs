using System;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using BeComfy.Common.CqrsFlow.Handlers;
using BeComfy.Common.Mongo;
using BeComfy.Common.RabbitMq;
using BeComfy.Services.Employees.Application.Commands;
using BeComfy.Services.Employees.Application.Commands.CommandHandlers;
using BeComfy.Services.Employees.Core.Domain;
using BeComfy.Services.Employees.Core.Repositories;
using BeComfy.Services.Employees.Infrastructure.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace BeComfy.Services.Employees.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        public IContainer Container { get; private set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddMongo();
            services.AddMongoRepository<Employee>("Employees");

            services.AddTransient<IEmployeesRepository, EmployeesRepository>();
            services.AddTransient<ICommandHandler<CreateEmployee>, CreateEmployeeHandler>();

            var builder = new ContainerBuilder();
            builder.Populate(services);
            builder.AddRabbitMq();
            Container = builder.Build();

            return new AutofacServiceProvider(Container);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            app.UseRabbitMq()
                .SubscribeCommand<CreateEmployee>();

            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
