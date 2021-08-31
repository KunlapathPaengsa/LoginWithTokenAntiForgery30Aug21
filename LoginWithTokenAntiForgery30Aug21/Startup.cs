using LoginWithTokenAntiForgery30Aug21.CQRS.Auth;
using LoginWithTokenAntiForgery30Aug21.service.Auth;
using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoginWithTokenAntiForgery30Aug21
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

            services.AddControllers();

            #region Configure Swagger  
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "LoginWithTokenAntiForgery30Aug21", Version = "v1" });
                //    c.AddSecurityDefinition("antiforgery", new OpenApiSecurityScheme
                //    {
                //        Name = "Authorization",
                //        Type = SecuritySchemeType.Http,
                //        Scheme = "antiforgery",
                //        In = ParameterLocation.Header,
                //        Description = "Anti Forgery Authorization header using the Bearer scheme."
                //    });
                //    c.AddSecurityRequirement(new OpenApiSecurityRequirement
                //    {
                //        {
                //              new OpenApiSecurityScheme
                //                {
                //                    Reference = new OpenApiReference
                //                    {
                //                        Type = ReferenceType.SecurityScheme,
                //                        Id = "antiforgery"
                //                    }
                //                },
                //                new string[] {}
                //        }
                //    });
            });
            #endregion


            //services.AddHttpContextAccessor();
            //services.AddAuthentication("Anti Forgery")
            //.AddScheme<AuthenticationSchemeOptions, SignInCommandHandler>("Anti Forgery", null);

            //services.AddScoped<IUserService, UserService>();

            services.AddAntiforgery(options =>
                {
                    options.FormFieldName = "AntiforgeryFieldname";
                    options.HeaderName = "X-XSRF-TOKEN";
                    options.SuppressXFrameOptionsHeader = false;
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IAntiforgery antiforgery)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "LoginWithTokenAntiForgery30Aug21 v1"));
            }

            app.UseHttpsRedirection();
            //app.UseAntiforgeryToken();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.Use(next => context =>
            {
                if (
                    string.Equals(context.Request.Path.Value, "/", StringComparison.OrdinalIgnoreCase) ||
                    string.Equals(context.Request.Path.Value, "/api/login", StringComparison.OrdinalIgnoreCase))
                {
                    // We can send the request token as a JavaScript-readable cookie 
                    var tokens = antiforgery.GetAndStoreTokens(context);
                    context.Response.Cookies.Append("XSRF-TOKEN", tokens.RequestToken,
                        new CookieOptions() { HttpOnly = false });
                }
                return next(context);
            });
        }
    }

}
