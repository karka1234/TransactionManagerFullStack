using backend.DB;
using backend.Services.Adapters;
using backend.Services.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Serialization;


namespace backend
{
    public class Startup
    {
        private string connString { get; }
        public Startup(IConfiguration configuration)
        {
            connString = configuration.GetConnectionString("Default");
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add CORS policy
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAllOrigins",
                    builder => builder.WithOrigins("http://localhost:4200") // Allow all origins
                                    .AllowAnyHeader()
                                    .AllowAnyMethod());
            });

            
            services.AddScoped<ITransactionRepository, TransactionRepository>();            
            services.AddScoped<IAccountRepository, AccountRepository>();
            services.AddScoped<ITransactionAdapter, TransactionAdapter>();
            
            services.AddDbContext<AppDbContext>(options => 
            {   
                
                options.UseSqlite(connString);
            });
            services.AddControllers()
                .AddNewtonsoftJson(options =>
                {
                    options.SerializerSettings.ContractResolver = new DefaultContractResolver();
                });

            //services.AddSwaggerGen();
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {            
            app.UseRouting();
            // Use the CORS policy
            app.UseCors("AllowAllOrigins");
            //app.UseCors("AllowAllOrigins");
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
            
            //app.UseSwagger();
            //app.UseSwaggerUI();
        }
    }
}