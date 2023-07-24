using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using OfficeOpenXml;
using Swashbuckle.AspNetCore.Filters;
using System.Configuration;
using System.Text;
using System.Text.Json.Serialization;
using Ueh.BackendApi.Data.EF;
using Ueh.BackendApi.IRepositorys;
using Ueh.BackendApi.Repositorys;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
ExcelPackage.LicenseContext = LicenseContext.NonCommercial;




builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

//Declare DI
builder.Services.AddScoped<ILoaiRepository, LoaiRepository>();
builder.Services.AddScoped<IDotRepository, DotRepository>();
builder.Services.AddScoped<ISinhvienRepository, SinhvienRepository>();
builder.Services.AddScoped<ILichsuRepository, LichsuRepository>();
builder.Services.AddScoped<IGiangvienRepository, GiangvienRepository>();
builder.Services.AddScoped<IDangkyRepository, DangkyRepository>();
builder.Services.AddScoped<IPhancongRepository, PhancongRepository>();
builder.Services.AddScoped<IChuyennganhRepository, ChuyennganhRepository>();
builder.Services.AddScoped<IChitietRepository, ChitietRepository>();
builder.Services.AddScoped<IKetquaRepository, KetquaRepository>();
builder.Services.AddScoped<IChamcheoRepository, ChamcheoRepository>();
builder.Services.AddScoped<IKhoaRepository, KhoaRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();



builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
});



builder.Services.AddDbContext<UehDbContext>(options =>

options.UseSqlServer(builder.Configuration.GetConnectionString("UehDb")));


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

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();
//app.Run("http://0.0.0.0");
app.Run();

//app.Run("http://0.0.0.0");
