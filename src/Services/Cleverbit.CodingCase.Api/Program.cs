using Cleverbit.CodingCase.Api.Extensions;
using Cleverbit.CodingCase.Application;
using Cleverbit.CodingCase.Infrastructure.Extensions;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddLogger(builder.Configuration, builder);
builder.Services.AddHealthCheck(builder.Configuration);

builder.Services.AddInfrastructureRegistration(builder.Configuration);
builder.Services.AddApplicationLayerRegistration();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHealthCheck();
app.MapControllers();
app.Run();
