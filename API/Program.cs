using Autofac.Extensions.DependencyInjection;
using Autofac;
using Business.Mapping;
using DataAccess.Contexts;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using API.Modules;
using Core.UnitOfWorks;
using Core.DataAccess.Repositories;
using Core.Services;
using Business.Services.Abstract;
using Business.Services.Concrete;
using DataAccess.Repositories.Abstract;
using DataAccess.Repositories.Concrete.EntityFramework;
using NLog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

LogManager.LoadConfiguration(String.Concat(Directory.GetCurrentDirectory(), "/nlog.config"));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(
   x => x.UseSqlServer(builder.Configuration.GetConnectionString("SqlConnection"), option =>
   {
       option.MigrationsAssembly(Assembly.GetAssembly(typeof(AppDbContext)).GetName().Name);
   }
    ));

builder.Services.AddControllers(config =>
{
    config.RespectBrowserAcceptHeader = true;
    config.ReturnHttpNotAcceptable = true;
});
builder.Services.AddControllers().AddXmlDataContractSerializerFormatters();


//builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(EfGenericRepository<>));
//builder.Services.AddScoped(typeof(IService<>), typeof(Service<>));
//builder.Services.AddScoped<IMissionService, MissionService>();
//builder.Services.AddScoped<IMissionDal, EfMissionDal>();
//builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Host.UseServiceProviderFactory
    (new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder => containerBuilder.RegisterModule(new AutofacModule()));

builder.Services.AddAutoMapper(typeof(MapProfile));

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
