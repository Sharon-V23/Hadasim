using Entity;
using Dll;
using AutoMapper;
using Bll;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
IConfiguration Configuration = builder.Configuration;

// Add services to the container.

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IPatientDll, PatientDll>();
builder.Services.AddScoped<IPatientBll, PatientBll>();
var ConnectionString = Configuration.GetConnectionString("HMOContext");

builder.Services.AddDbContext<HMOContext>(op=> op.UseSqlServer(ConnectionString));
var mapperConfig = new MapperConfiguration(mc =>
{

    mc.AddProfile(new MapperProfile());
});
IMapper mapper = mapperConfig.CreateMapper();

builder.Services.AddSingleton(mapper);
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
