using BusinessLayer.Services;
using DataAccessLayer.Data;
using DataAccessLayer.Repositories;
using EntityLayer.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors();
builder.Services.AddControllers()
                .AddNewtonsoftJson();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        builder => builder.AllowAnyOrigin()
                          .AllowAnyMethod()
                          .AllowAnyHeader());
});

// Configure Database Context
builder.Services.AddDbContext<TaskDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
                         b => b.MigrationsAssembly("DataAccessLayer")));

// Configure JWT Authentication
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"], // Ensure this is set correctly
        ValidAudience = builder.Configuration["Jwt:Audience"], // Ensure this is set correctly
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),
        ClockSkew = TimeSpan.Zero
    };
});




// Dependency Injection for Services and Repositories
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<IUserRepo, UserRepo>();
builder.Services.AddScoped<TaskService>();
builder.Services.AddScoped<ITaskRepo, TaskRepo>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    // Configure CORS to allow any origin, method, and header for development purposes
    app.UseCors(x => x.AllowAnyMethod()
                      .AllowAnyHeader()
                      .SetIsOriginAllowed(origin => true)
                      .AllowCredentials());
}

app.UseHttpsRedirection();
app.UseAuthentication(); // Add Authentication Middleware
app.UseAuthorization();  // Add Authorization Middleware

app.MapControllers();

app.Run();
