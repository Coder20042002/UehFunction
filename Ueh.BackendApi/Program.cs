using Microsoft.EntityFrameworkCore;
using Ueh.BackendApi.Data.EF;
using Ueh.BackendApi.IRepositorys;
using Ueh.BackendApi.Repositorys;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddScoped<ILoaiRepository, LoaiRepository>();
builder.Services.AddScoped<IDotRepository, DotRepository>();
builder.Services.AddScoped<ISinhvienRepository, SinhvienRepository>();
builder.Services.AddScoped<IReviewRepository, ReviewRepository>();
builder.Services.AddScoped<IReviewerRepository, ReviewerRepository>();
builder.Services.AddScoped<IGiangvienRepository, GiangvienRepository>();






// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(option => option.AddDefaultPolicy(policy => policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()));

builder.Services.AddDbContext<UehDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("UehDb")));


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
