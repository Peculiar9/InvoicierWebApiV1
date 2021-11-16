using InvoicierWebApiV1.Data.AuthModels;
using InvoicierWebApiV1.Service.OrganizationServices;
using InvoicierWebApiV1.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Text;

namespace InvoicierWebApiV1
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

            var configString = Configuration.GetConnectionString("InvoicierConnection");
            services.AddDbContext<InvoicierDbContext>(options => 
            options.UseSqlServer(configString));
             services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.Password.RequiredLength = 8;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireDigit = false;
                options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                options.User.RequireUniqueEmail = false;
            })
                .AddEntityFrameworkStores<InvoicierDbContext>()
                .AddDefaultTokenProviders();

            //AuthenticationService
            services.AddAuthentication(options => {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(options =>
                {
                    options.SaveToken = true;
                    options.RequireHttpsMetadata = true;
                    options.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidAudience = Configuration["JWT:ValidAudience"],
                        ValidIssuer = Configuration["JWT:ValidIssuer"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JWT:SecretKey"]))

                    };
                });
            
             services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
             services.AddScoped<IOrganizationServices, OrganizationService>();
             services.AddScoped<InvoiceService, InvoiceServiceRepo>();
             services.AddScoped<IClientService, ClientServiceRepo>();
             services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
             services.AddSwaggerGen(c =>
             {
                 c.SwaggerDoc("v1", new OpenApiInfo { Title = "InvoicierWebApiV1", Version = "v1" });
                 var securityScheme = new OpenApiSecurityScheme
                 {
                     Name = "JWT Authentication",
                     Description = "Enter JWT Bearer token **_only_**",
                     In = ParameterLocation.Header,
                     Type = SecuritySchemeType.Http,
                     Scheme = "bearer", // must be lower case
                     BearerFormat = "JWT",
                     Reference = new OpenApiReference
                     {
                         Id = JwtBearerDefaults.AuthenticationScheme,
                         Type = ReferenceType.SecurityScheme
                     }
                 };
                 c.AddSecurityDefinition(securityScheme.Reference.Id, securityScheme);
                 c.AddSecurityRequirement(new OpenApiSecurityRequirement
                 {
                     {securityScheme, new string[] { }}
                 });

             }); 
        }   

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "InvoicierWebApiV1 v1"));


            }

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
