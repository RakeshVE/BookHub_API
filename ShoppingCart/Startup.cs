using CloudinaryDotNet;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using NLog;
using ShoppingCart.DTOs;
using ShoppingCart.Helpers;
using ShoppingCart.Interfaces;
using ShoppingCart.Logger;
using ShoppingCart.Models;
using ShoppingCart.Repositories;
using ShoppingCart.Services;
using Stripe;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCart
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            LogManager.LoadConfiguration(String.Concat(Directory.GetCurrentDirectory(), "/nlog.config"));
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //Cloudinary start
            var cloudName = Configuration.GetValue<string>("AccountSettings:CloudName");
            var apiKey = Configuration.GetValue<string>("AccountSettings:ApiKey");
            var apiSecret = Configuration.GetValue<string>("AccountSettings:ApiSecret");

            if (new[] { cloudName, apiKey, apiSecret }.Any(string.IsNullOrWhiteSpace))
            {
                throw new ArgumentException("Please specify Cloudinary account details!");
            }

            services.AddSingleton(new Cloudinary(new CloudinaryDotNet.Account(cloudName, apiKey, apiSecret)));

            var dbFolder = $"{Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)}\\Cloudinary\\samples";
            System.IO.Directory.CreateDirectory(dbFolder);
            //Cloudinary end

            services.AddScoped<ITokenService, Services.TokenService>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IMenusRepository, MenusRepository>();
            services.AddScoped<IBooksRepository, BooksRepository>();
            services.AddScoped<IOrdersRepository, OrdersRepository>();
            services.AddScoped<ICartRepositories, CartRepositories>();
            services.AddSingleton<ILoggerManager, LoggerService>();
            services.AddScoped<ICorpSalesRepository, CorpSalesRepository>();
            services.AddScoped<IAdminDashboardRepository, AdminDashboardRepository>();

            services.Configure<StripeSettings>(Configuration.GetSection("StripeToken"));
            services.AddDbContext<ShoppingCartContext>(options => options.UseSqlServer(Configuration.GetConnectionString("myConnection")));
            services.AddControllers();
            //services.AddCors();
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", builder => builder.AllowAnyOrigin()
                 .AllowAnyMethod()
                 .AllowAnyHeader());
            });
            services.AddSwaggerGen(c => {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Shopping Cart Api", Version = "v1" });
                c.OperationFilter<SwaggerFileOperationFilter>();
            });
            services.AddSwaggerGen();
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["TokenKey"])),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });
            services.AddAutoMapper(typeof(Startup));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            StripeConfiguration.ApiKey = Configuration.GetSection("Stripe")["SecretKey"];
            //  app.UseCors(x => x.AllowAnyHeader().AllowAnyMethod().WithOrigins("https://localhost:4200"));
            app.UseCors("CorsPolicy");
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseStaticFiles(new StaticFileOptions()
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), @"Resources")),
                RequestPath = new PathString("/Resources")
            });
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ShoppingCart v1"));
        }
    }
}
