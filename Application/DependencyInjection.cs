using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Register MediatR
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
            // Register AutoMapper
            //services.AddAutoMapper(typeof(AutoMapperProfiles));
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            //// Register Validations
            //services.AddValidatorsFromAssemblies(new Assembly[] { Assembly.GetExecutingAssembly() });
            //// Global Exception Handling
            //services.AddExceptionHandler<GlobalExceptionHandler>();
            //services.AddProblemDetails();
            //// End Global Exception Handling
            //services.AddTransient<TemplatesType>();
            //services.AddTransient<TemplatesQuery>();
            //services.AddTransient<ISchema, TemplateSchema>();
            //services.AddGraphQL(b => b
            //    .AddAutoSchema<TemplatesQuery>()  // schema
            //    .AddSystemTextJson());   // serializer
            ////services.AddGraphQLServer()
            ////    .AddQueryType<TemplatesQuery>()
            ////    .AddType<TemplatesType>()
            ////    .AddProjections()
            ////    .AddFiltering()
            ////    .AddSorting();
            return services;
        }
    }
}
