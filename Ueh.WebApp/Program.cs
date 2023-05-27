using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using Ueh.BackendApi.Data.EF;
using Ueh.BackendApi.IRepositorys;
using Ueh.BackendApi.Repositorys;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSession();
builder.Services.AddScoped<IPhanCongRepository, PhancongRepository>();

ExcelPackage.LicenseContext = LicenseContext.NonCommercial; // ho?c LicenseContext.Commercial n?u s? d?ng th??ng m?i

builder.Services.AddDbContext<UehDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("UehDb")));

var app = builder.Build();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();
app.UseSession();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Excel}/{action=ImportExcelFile}/{id?}");

app.Run();
