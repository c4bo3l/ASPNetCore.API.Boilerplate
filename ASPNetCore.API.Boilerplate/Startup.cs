using ASPNetCore.API.Boilerplate.DTOs;
using ASPNetCore.API.Boilerplate.Models;
using ASPNetCore.API.Boilerplate.Repositories;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ASPNetCore.API.Boilerplate
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
            services.AddMvc(options => {
                /*
                 * Return status code 406 Not Accepted if client request unsupported media type.
                 * It indicates from "Accept" attribute in request header.
                 * As default, this API only support JSON media type.
                 * "Accept: application/json".
                 */
                options.ReturnHttpNotAcceptable = true;

                /*
                 * Uncomment this line to support XML format.
                 * setupConfig.OutputFormatters.Add(new XmlDataContractSerializerOutputFormatter());
                 */
            });

            services.AddDbContext<MyDBContext>(options => options.UseSqlServer(Configuration.GetConnectionString("Default")));

            services.AddTransient<CompanyRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            Mapper.Initialize(config => {
                config.CreateMap<CompanyModel, CompanyViewDto>();
                config.CreateMap<EmployeeModel, EmployeeViewDto>().
                ForMember(dest => dest.Name,
                    src =>
                    {
                        src.ResolveUsing<string>(e => string.Format("{0} {1}", e.FirstName, e.LastName));
                    });
            });

            app.UseMvc();
        }
    }
}
