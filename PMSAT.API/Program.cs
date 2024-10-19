using Microsoft.AspNetCore.Cors.Infrastructure;
using PMSAT.API.Configs;
using PMSAT.BusinessTier.Constants;
using PMSAT.BusinessTier.Converter;
using SEP_PMSAT.BusinessTier.Middlewares;
using System.Text.Json.Serialization;

namespace PMSAT.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddCors(options =>
            {
                options.AddPolicy(name: CorsConstant.PolicyName,
                    policy =>
                    {
                        policy.WithOrigins("*")
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                    });
            });
            builder.Services.AddControllers().AddJsonOptions(x =>
            {
                x.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                x.JsonSerializerOptions.Converters.Add(new TimeOnlyJsonConverter());
            });

            // Add services to the container.
            builder.Services.AddDatabase(builder.Configuration);
            builder.Services.AddUnitOfWork();
            builder.Services.AddServices();
            builder.Services.AddHttpClient();
            builder.Services.AddJwtValidation();
            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            builder.Services.AddHttpContextAccessor();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddConfigSwagger();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            app.UseSwagger();
            app.UseSwaggerUI();
            app.UseMiddleware<ExceptionHandlingMiddleware>();

            app.UseCors(CorsConstant.PolicyName);
            app.UseAuthentication();

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
