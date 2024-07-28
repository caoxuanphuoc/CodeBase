using CodeBase.Core.Entities;
using CodeBase.EntityFrameworkCore;
using CodeBase.EntityFrameworkCore.Repositories;
using CodeBase.EntityFrameworkCore.Repositories.UnitOfWork;
using CodeBase.Service.Handlers.V1.Example;
using CodeBase.Service.Mapper;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<CodebBaseDbContext>(options =>
    options.UseSqlServer(connectionString));
ConfigureServices( builder.Services, builder.Configuration);


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();


void ConfigureServices(IServiceCollection services, IConfiguration configuration)
{

    // Register AutoMapper
    services.AddAutoMapper(typeof(MappingProfile));

    services.AddScoped<IUnitOfWork, EFUnitOfWork>();
    services.AddScoped<IBaseRepository<ExampleEntity, int>, BaseRepository<ExampleEntity, int>>();
    services.AddScoped<IExampleAppService, ExampleAppService>();

}