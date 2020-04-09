﻿using System;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using WordFinder_Business;
using WordFinder_Repository;

namespace WordFinder
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        { Configuration = configuration; }

        public void ConfigureServices(IServiceCollection services)
        {
            var key = Configuration["auth:key"];
            var skey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            
            services.AddDbContext<ApiContext>(builder => 
                builder.UseNpgsql(Configuration["database:cs"],
                    b => b.MigrationsAssembly("WordFinder")));
            services.AddScoped<WordService>();
            services.AddScoped<UserService>();
            services.AddMvc()
                .AddJsonOptions(x => 
                    x.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = false;
                    options.SaveToken = true;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidIssuer = Configuration["auth:issuer"],
                        ValidateIssuer = true,
                        ValidAudience = Configuration["auth:audience"],
                        ValidateAudience = true,
                        IssuerSigningKey = skey,
                        ValidateIssuerSigningKey = true,
                        RequireExpirationTime = true,
                        ValidateLifetime = true,
                    };
                });
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment()) 
            { app.UseDeveloperExceptionPage(); }
            else {  app.UseHsts(); }

            app.UseCors(o => o.AllowAnyOrigin());
            app.UseAuthentication();
            app.UseMvc();
        }
    }
}