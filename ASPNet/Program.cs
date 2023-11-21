using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.OpenApi.Models;

namespace ASPNet
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddCors();
            builder.Services.AddControllers();
            builder.Services.AddSwaggerGen(option =>
            {
                option.SwaggerDoc("v1", new OpenApiInfo { Title = "", Version = "1v" });
                option.TagActionsBy(api =>
                {
                    if (api.GroupName != null)
                    {
                        return new[] { api.GroupName };
                    }

                    var controllerActionDescriptor = api.ActionDescriptor as ControllerActionDescriptor;
                    if (controllerActionDescriptor != null)
                    {
                        return new[] { controllerActionDescriptor.ControllerName };
                    }

                    throw new InvalidOperationException("Unable to determine tag for endpoint.");
                });
                option.DocInclusionPredicate((name, api) => true);
            });


            var app = builder.Build();

            app.MapControllers();

            //app.MapControllerRoute(
            //            name: "default",
            //            pattern: "{controller =Home}/{action=Index}/{id?}");

            app.UseCors(builder => builder.AllowAnyOrigin());

            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("./v1/swagger.json", "v1");
            });

            app.Run();
        }
    }
}