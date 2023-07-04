using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
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
using Ueh.BackendApi.Data.Entities;
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

builder.Services.AddTransient<UserManager<IdentityUser<Guid>>, UserManager<IdentityUser<Guid>>>();
builder.Services.AddTransient<SignInManager<IdentityUser<Guid>>, SignInManager<IdentityUser<Guid>>>();
builder.Services.AddTransient<RoleManager<IdentityRole<Guid>>, RoleManager<IdentityRole<Guid>>>();
builder.Services.AddTransient<IUserRepository, UserRepository>();


builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
});



builder.Services.AddDbContext<UehDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("UehDb")));

builder.Services.AddIdentity<IdentityUser<Guid>, IdentityRole<Guid>>()
              .AddEntityFrameworkStores<UehDbContext>()
              .AddDefaultTokenProviders();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

//builder.Services.AddSwaggerGen(c =>
//{


//    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
//    {
//        Description = @"JWT Authorization header using the Bearer scheme. \r\n\r\n
//                      Enter 'Bearer' [space] and then your token in the text input below.
//                      \r\n\r\nExample: 'Bearer 12345abcdef'",
//        Name = "Authorization",
//        In = ParameterLocation.Header,
//        Type = SecuritySchemeType.ApiKey,
//        Scheme = "Bearer"
//    });

//    c.AddSecurityRequirement(new OpenApiSecurityRequirement()
//       {
//         {
//            new OpenApiSecurityScheme
//               {
//                Reference = new OpenApiReference
//                {
//                  Type = ReferenceType.SecurityScheme,
//                  Id = "Bearer"
//                },
//                  Scheme = "oauth2",
//                  Name = "Bearer",
//                  In = ParameterLocation.Header,
//            },
//                  new List<string>()
//         }
//    });
//});


//string issuer = builder.Configuration.GetValue<string>("Tokens:Issuer");
//string signingKey = builder.Configuration.GetValue<string>("Tokens:Key");
//byte[] signingKeyBytes = System.Text.Encoding.UTF8.GetBytes(signingKey);

//builder.Services.AddAuthentication(opt =>
//{
//    opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
//    opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
//})
//           .AddJwtBearer(options =>
//           {
//               options.RequireHttpsMetadata = false;
//               options.SaveToken = true;
//               options.TokenValidationParameters = new TokenValidationParameters()
//               {
//                   ValidateIssuer = true,
//                   ValidIssuer = issuer,
//                   ValidateAudience = true,
//                   ValidAudience = issuer,
//                   ValidateLifetime = true,
//                   ValidateIssuerSigningKey = true,
//                   ClockSkew = System.TimeSpan.Zero,
//                   IssuerSigningKey = new SymmetricSecurityKey(signingKeyBytes)
//               };
//           });



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
