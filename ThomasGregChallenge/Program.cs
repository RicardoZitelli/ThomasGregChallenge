using ThomasGregChallenge.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Autofac.Extensions.DependencyInjection;
using Autofac;
using ThomasGregChallenge.Infrastructure.CrossCutting.IOC;
using ThomasGregChallenge.Application.Mapping;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1",
        new OpenApiInfo
        {
            Title = "Thomas Greg Challenge - V1",
            Version = "v1"
        }
     );

    var filePath = Path.Combine(AppContext.BaseDirectory, "ThomasGregChallenge.xml");
    c.IncludeXmlComments(filePath);

});

var connectionString = builder
    .Configuration
    .GetConnectionString("DefaultConnection")?
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
