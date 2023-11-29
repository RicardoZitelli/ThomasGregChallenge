using ThomasGregChallenge.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Autofac.Extensions.DependencyInjection;
using Autofac;
using ThomasGregChallenge.Infrastructure.CrossCutting.IOC;
using ThomasGregChallenge.Application.Mapping;
using FluentValidation.AspNetCore;
using FluentValidation;
using ThomasGregChallenge.Application.DTOs.Requests;
using ThomasGregChallenge.Application.Validators;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = builder
    .Configuration
    .GetConnectionString("DefaultConnectionOld")?
    .Replace("{Server}", Environment.GetEnvironmentVariable("DB_HOST"))
    .Replace("{Port}", Environment.GetEnvironmentVariable("DB_PORT"))
    .Replace("{Database}", Environment.GetEnvironmentVariable("DB_NAME"))
    .Replace("{User_Id}", Environment.GetEnvironmentVariable("DB_SA_USER_ID"))
    .Replace("{Password}", Environment.GetEnvironmentVariable("DB_SA_PASSWORD"));

builder.Services.AddDbContext<SqlContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddAutoMapper(typeof(MappingProfile));

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAnyOrigin", builder =>
    {
        builder.AllowAnyOrigin();
        builder.AllowAnyHeader();
        builder.AllowAnyMethod();
    });
});

builder.Host.ConfigureContainer<ContainerBuilder>(builder =>
    builder.RegisterModule(new ModuleIOC()));

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

var app = builder.Build();

var dbcontext = app.Services.GetRequiredService<SqlContext>();

dbcontext.Database.Migrate();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseCors("AllowAnyOrigin");
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
