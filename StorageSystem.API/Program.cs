using AutoMapper;
using Microsoft.EntityFrameworkCore;
using StorageSystem.Api;
using StorageSystem.API.Context;
using StorageSystem.API.Context.Repository;
using StorageSystem.API.Extensions;
using StorageSystem.API.Services;
using StorageSystem.Shared.Context;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<StorageDbContext>(option =>
{
    var connectionString = builder.Configuration.GetConnectionString("StorageDbConnection");
    option.UseSqlite(connectionString);

}).AddUnitOfWork<StorageDbContext>()
.AddCustomRepository<User, UserRepository>()
.AddCustomRepository<StorageDetail, StorageDetailRepository>()
.AddCustomRepository<StorageOutDetail, StorageOutDetailRepository>()
.AddCustomRepository<StorageStatus, StorageStatusRepository>();

builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<IStorageStatusService, StorageStatusService>();
builder.Services.AddTransient<IStorageDetailService, StorageDetailService>();
builder.Services.AddTransient<IStorageOutDetailService, StorageOutDetailService>();

var automapperConfog = new MapperConfiguration(config =>
  {
      config.AddProfile(new AutoMapperProFile());
  });

builder.Services.AddSingleton(automapperConfog.CreateMapper());

builder.Services.AddControllers();
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

app.UseAuthorization();

app.MapControllers();

app.Run();
