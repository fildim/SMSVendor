using Microsoft.EntityFrameworkCore;
using SMSVendor.Models;
using SMSVendor.Services;

namespace SMSVendor
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<SmsVendorDbContext>(c => c.UseSqlServer(builder.Configuration.GetConnectionString("SMSVendorDB")));

            builder.Services.AddScoped<ISMSVendor, GreekSMSVendor>();
            builder.Services.AddScoped<ISMSVendor, CyprusSMSVendor>();
            builder.Services.AddScoped<ISMSVendor, RestSMSVendor>();
            builder.Services.AddScoped<SMSService>();


            builder.Services.AddAutoMapper(System.Reflection.Assembly.GetExecutingAssembly());



            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.UseRouting();

            app.MapControllers();

            app.Run();
        }
    }
}
