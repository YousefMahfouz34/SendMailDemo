
using SendMailDemo.Services;
using SendMailDemo.Settings;

namespace SendMailDemo
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddCors(option =>
            {
                option.AddPolicy("tempPolicy", x =>
                {
                    x.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod();
                });
                option.AddDefaultPolicy(builder =>
                {
                    builder.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin();
                });
            });
            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection("EmailSettings"));
            builder.Services.AddScoped<ISendMailServices, SendMailServices>();
            var app = builder.Build();

            
            app.UseSwagger();
            app.UseSwaggerUI();
            

            app.UseHttpsRedirection();
            app.UseCors("tempPolicy");

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
