using System.Security.Claims;
using System.Text;
using Ecommerce.Business.src.AutoMapper;
using Ecommerce.Business.src.Dtos;
using Ecommerce.Business.src.ServiceInterfaces;
using Ecommerce.Business.src.Services;
using Ecommerce.Domain.src.Entities;
using Ecommerce.Domain.src.RepoInterfaces;
using Ecommerce.WebApi.src.Database;
using Ecommerce.WebApi.src.RepoImplementations;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;

var builder = WebApplication.CreateBuilder(args);

//add database into the application
builder.Services.AddDbContext<DatabaseContext>();

// Add services to the container.
builder.Services.AddControllers();

// Add services for auto dependency injection
// Repo
builder.Services.AddScoped<IProductRepo, ProductRepo>();
builder.Services.AddScoped<IUserRepo, UserRepo>();
builder.Services.AddScoped<IOrderRepo, OrderRepo>();
builder.Services.AddScoped<IOrderProductRepo, OrderProductRepo>();

// Service
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddTransient<IOrderService, OrderService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddTransient<IOrderProductService, OrderProductService>();

// Add AutoMapper
builder.Services.AddAutoMapper(config => config.AddProfile(typeof(AutoMapperProfile)));
builder.Services.AddAutoMapper(typeof(Program).Assembly);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options => 
{
    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        Description = "Bearer token authentication",
        Name = "Authentication",
        In = ParameterLocation.Header
    });
    options.OperationFilter<SecurityRequirementsOperationFilter>();
});

//config route
builder.Services.Configure<RouteOptions>(options =>
{
    options.LowercaseUrls = true;
});

//config authentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidIssuer = "ecommerce-backend",
        ValidateAudience = false,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("ecommerce-backend-authservice-security-key")),
        ValidateIssuerSigningKey = true
    };
});

//add authorization policy
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminOnly", policy => policy.RequireClaim(ClaimTypes.Role, "Admin"));
});

//add Cors
builder.Services.AddCors(options =>
{
    // options.AddPolicy("AllowOrigin", policy => policy.WithOrigins("https://localhost:3000").AllowAnyHeader().AllowAnyMethod());
    // options.AddDefaultPolicy(policy => policy.WithOrigins("https://localhost:3000").AllowAnyHeader().AllowAnyMethod());
    options.AddDefaultPolicy(
        builder => {
            builder.WithOrigins("https://localhost:3000").AllowAnyHeader().AllowAnyOrigin();
        }
    );
});

var app = builder.Build();

// Configure the HTTP request pipeline.
// if (app.Environment.IsDevelopment())
// {
    app.UseSwagger();
    app.UseSwaggerUI();
    // app.UseSwaggerUI(c =>
    // {
    //     c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
    //     c.RoutePrefix = string.Empty;
    // });
// }

app.UseCors();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
