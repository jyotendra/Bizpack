

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Bizpack.Data;
using Bizpack.Data.Configuration;
using Bizpack.Service;
using Bizpack.Service.Account;
using Bizpack.Service.ApiResponse;
using Sock;
using Swashbuckle.AspNetCore.Swagger;
using System.Reflection;
using System.IO;

namespace Bizpack {
    public class Startup {

        public IConfiguration Configuration { get; }

        private AuthConfigurartion AuthConfig;

        public Startup (IConfiguration configuration) {
            Configuration = configuration;
            AuthConfig = new AuthConfigurartion ();
            Configuration.GetSection ("AuthConfiguration").Bind (AuthConfig);
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices (IServiceCollection services) {

            services.Configure<ApiBehaviorOptions> (options => {
                options.SuppressModelStateInvalidFilter = true;
            });

            // TODO: Get connection strings from configuration file
            var connection = @"Server=DESKTOP-JJAFHPF\SQLEXPRESS01;Database=AuthDb;User=sa;Password=qwerty12345;Trusted_Connection=False;ConnectRetryCount=0";
            services.AddDbContext<AppDbContext>
                (options => options.UseSqlServer (connection));

            // TODO: Remove dependency on "AddIdentity" extension - it's enforcing "cookie based authentication"
            services.AddIdentity<AppUser, IdentityRole> ()
                .AddEntityFrameworkStores<AppDbContext> ();

            // TODO: Add encryption key in JWT signing, to be picked from configuration file
            services.AddAuthentication (JwtBearerDefaults.AuthenticationScheme).AddJwtBearer (options => {
                options.TokenValidationParameters = JwtValidatorService.GetValidationParameters (AuthConfig.SecurityKey);
            });

            services.AddCors (options => {
                options.AddPolicy ("CorsPolicy",
                    builder => builder.WithOrigins ("http://localhost:4200")
                    .AllowAnyMethod ()
                    .AllowAnyHeader ()
                    .AllowCredentials ());
            });

            services.InvokeApplicationServices ();

            services.AddMvc ().SetCompatibilityVersion (CompatibilityVersion.Version_2_2);
            services.AddSignalR ();

            // swagger will be generated at : "localhost:5000/swagger"
            services.AddSwaggerGen (c => {
                c.SwaggerDoc ("v1", new Info {
                    Version = "v1",
                        Title = "SignalR App",
                        Description = "SignalR application for crowd count display",
                        Contact = new Contact {
                            Name = "Jyotendra Sharma",
                                Email = "jyotendrasharma92@gmail.com"
                        }
                });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine (AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments (xmlPath);
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure (IApplicationBuilder app, IHostingEnvironment env) {
            if (env.IsDevelopment ()) {
                app.UseDeveloperExceptionPage ();
            } else {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts ();
            }
            // app.UseHttpsRedirection ();

            app.UseStaticFiles ();
            app.UseAuthentication ();

            // Enable SignalR
            app.UseSignalR (routes => {
                routes.MapHub<NotifierHub> ("/dashboard");
            });

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger ();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), 
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI (c => {
                c.SwaggerEndpoint ("/swagger/v1/swagger.json", "My API V1");
            });

            // app.UseMiddleware<ErrorWrappingMiddleware>();
            app.UseMvc ();
        }
    }
}