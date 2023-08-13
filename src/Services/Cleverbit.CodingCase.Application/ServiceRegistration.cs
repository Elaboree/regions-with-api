using MediatR;
using MediatR.Pipeline;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Cleverbit.CodingCase.Application.Behaviors;
using Cleverbit.CodingCase.Application.Services.Abstract;
using Cleverbit.CodingCase.Application.Services;
using FluentValidation;
using Cleverbit.CodingCase.Application.Validators;

namespace Cleverbit.CodingCase.Application;
public static class ServiceRegistration
{
    public static IServiceCollection AddApplicationLayerRegistration(this IServiceCollection services)
    {

        //MediatR and service pipelines
        var assembly = Assembly.GetExecutingAssembly();
        services.AddMediatR(assembly);
        services.AddAutoMapper(assembly);

        services.AddValidatorsFromAssembly(typeof(AddEmployeeCommandValidator).Assembly);
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        services.AddTransient(typeof(IRequestExceptionHandler<,,>), typeof(ExceptionHandlingBehavior<,,>));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));

        services.AddScoped<IRegionEmployeeService, RegionEmployeeService>();

        return services;
    }
}