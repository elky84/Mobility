using Mobility.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using Mobility.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Diagnostics;
using Newtonsoft.Json;
using System.Net;
using Mobility.Protocols.Exception;
using Mobility.Protocols.Common;
using Mobility.Protocols.Code;

namespace Mobility
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public static void ConfigureExceptionHandler(IApplicationBuilder app)
        {
            app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                    context.Response.ContentType = "application/json";

                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                    if (contextFeature != null)
                    {
                        if (contextFeature.Error.GetType() == typeof(LogicException))
                        {
                            var logicException = (LogicException)contextFeature.Error;
                            context.Response.StatusCode = (int)logicException.HttpStatusCode;

                            await context.Response.WriteAsync(JsonConvert.SerializeObject(new ResponseHeader
                            {
                                StatusCode = (int)logicException.ResultCode,
                                Code = logicException.ResultCode
                            }));
                        }
                        else
                        {
                            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                            await context.Response.WriteAsync(JsonConvert.SerializeObject(new ResponseHeader
                            {
                                StatusCode = (int)ResultCode.UnknownException,
                                Code = ResultCode.UnknownException
                            }));
                        }
                    }
                });
            });
        }

        public class CustomJsonResolver : CamelCasePropertyNamesContractResolver
        {
            public override JsonContract ResolveContract(Type type)
            {
                if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Dictionary<,>))
                {
                    return new DefaultContractResolver().ResolveContract(type);
                }
                else
                {
                    return base.ResolveContract(type);
                }
            }
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<DatabaseContext>(opts => opts.UseSqlite("Data Source=Mobility.db"));

            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddMvc(option => option.EnableEndpointRouting = false);

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "API", Version = "v1" });
            });

            services.ConfigureSwaggerGen(options =>
            {
                options.CustomSchemaIds(x => x.FullName);
            });

            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });

            services.AddControllers()
                .AddNewtonsoftJson(options =>
                {
                    // Return JSON responses in LowerCase?
                    options.SerializerSettings.ContractResolver = new CustomJsonResolver();

                    // Resolve Looping navigation properties
                    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                });

            services.AddSwaggerGenNewtonsoftSupport();

            services.AddTransient<MemberService>();
            services.AddTransient<CallService>();
            services.AddTransient<TaxiService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                serviceScope.ServiceProvider.GetService<DatabaseContext>().Database.Migrate();
            }

            ConfigureExceptionHandler(app);

            app.UseCookiePolicy();

            app.UseMvc();

            app.UseSwagger(c =>
            {
                c.RouteTemplate = "api-docs/{documentName}/swagger.json";
            });

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/api-docs/v1/swagger.json", "Mobility API V1");
                c.RoutePrefix = string.Empty;
            });


            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
