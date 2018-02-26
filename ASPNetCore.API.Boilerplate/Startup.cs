using ASPNetCore.API.Boilerplate.DTOs;
using ASPNetCore.API.Boilerplate.Models;
using ASPNetCore.API.Boilerplate.Repositories;
using AspNetCoreRateLimit;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
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
            services.AddScoped<IUrlHelper, UrlHelper>(factory => {
                ActionContext actionContext = factory.GetService<IActionContextAccessor>().ActionContext;
                return new UrlHelper(actionContext);
            });
            services.AddTransient<CompanyRepository>();

            services.AddResponseCaching();

            services.AddHttpCacheHeaders(expirationOption => {
                expirationOption.MaxAge = 600;
            }, validationOption => {
                validationOption.AddMustRevalidate = true;
            });

            services.AddMemoryCache();

            services.Configure<IpRateLimitOptions>(options => {
                options.GeneralRules = new System.Collections.Generic.List<RateLimitRule>() {
                    // All IPs allow to request 1000 times in 5 minutes
                    new RateLimitRule(){
                        Endpoint="*",
                        Limit=1000,
                        Period="5m"
                    }
                    // All IPs only allow send two requests in ten seconds
                    //,new RateLimitRule(){
                    //    Endpoint="*",
                    //    Limit=2,
                    //    Period="10s"
                    //}
                };
            });

            services.AddSingleton<IRateLimitCounterStore, MemoryCacheRateLimitCounterStore>();
            services.AddSingleton<IIpPolicyStore, MemoryCacheIpPolicyStore>();

            services.AddSwaggerGen(options => {
                options.SwaggerDoc("libraryapi", new Swashbuckle.AspNetCore.Swagger.Info
                {
                    Title = "ASP.NET Core API Documentation",
                    Version = "v1"
                });
            });
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

            app.UseIpRateLimiting();

            app.UseResponseCaching();
            app.UseHttpCacheHeaders();

            app.UseMvc();
            app.UseSwagger();
            app.UseSwaggerUI(options => {
                options.SwaggerEndpoint(Configuration["Swagger:LibJsonPath"], "Library API");
            });
        }
    }
}
