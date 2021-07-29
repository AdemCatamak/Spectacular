using System;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using UserManagement.Api.Db;
using UserManagement.Api.Db.Repo;
using UserManagement.Api.Models;

namespace UserManagement.Api
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
            services.AddControllers()
                    .AddJsonOptions(options => { options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()); });
            services.AddSwaggerGen(c => { c.SwaggerDoc("v1", new OpenApiInfo {Title = "UserManagement.Api", Version = "v1"}); });

            services.AddDbContext<UserDbContext>(builder => builder.UseInMemoryDatabase("user-db"));
            services.AddScoped<IUserRepository, UserRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, UserDbContext userDbContext)
        {
            SeedData(userDbContext);
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "UserManagement.Api v1"));
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }

        private void SeedData(UserDbContext userDbContext)
        {
            userDbContext.Database.EnsureCreated();

            User user1 = User.Create("ali", Genders.Male, DateTime.UtcNow.AddYears(-30));
            User user2 = User.Create("veli", Genders.Male, DateTime.UtcNow.AddYears(-20));
            User user3 = User.Create("ayse", Genders.Female, DateTime.UtcNow.AddYears(-25));
            User user4 = User.Create("ipek", Genders.Female, DateTime.UtcNow.AddYears(-15));

            userDbContext.AddRange(user1, user2, user3, user4);
            userDbContext.SaveChanges();
        }
    }
}