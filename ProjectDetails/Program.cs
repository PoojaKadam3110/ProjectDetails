using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using ProjectDetailsAPI.Data;
using ProjectDetailsAPI.Implementation;
using ProjectDetailsAPI.Implementation.ProjectsImp;
using ProjectDetailsAPI.Mappings;
using ProjectDetailsAPI.Repositories;
using ProjectDetailsAPI.Services;
using ProjectDetailsAPI.Services.IProjects;
using System.Reflection;
using System.Text;

// for cors
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
var builder = WebApplication.CreateBuilder(args);


builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy =>
                      {
                          //policy.WithOrigins("https://projectdetails.azurewebsites.net");
                          policy.WithOrigins("http://localhost:3000").AllowAnyHeader().WithMethods("POST", "PUT", "DELETE", "GET");
                      });
});

//For MediatR
builder.Services.AddMediatR(Assembly.GetExecutingAssembly());


// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


//For Autorization
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "Project Details API", Version = "v1" });
    options.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new OpenApiSecurityScheme
    {
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = JwtBearerDefaults.AuthenticationScheme
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = JwtBearerDefaults.AuthenticationScheme
                },
                Scheme = "Oauth2",
                Name = JwtBearerDefaults.AuthenticationScheme,
                In = ParameterLocation.Header
            },
            new List<string>()
        }
    });
});

// For DbContext()
builder.Services.AddDbContext<ProjectDetailsDbContext>(options =>
 options.UseSqlServer(builder.Configuration.GetConnectionString("ProjectDetailsConnectionStrings")));
// For AuthDbContext()
builder.Services.AddDbContext<ProjectDetailsAuthDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("ProjectsAuthConnectionStrings"))); 

//add repositories 
builder.Services.AddScoped<ITokenRepository, TokenRepository>(); //for token creation

//For Generic Repo Unit DI
builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();

builder.Services.AddTransient<IClientRepository,ClientRepository>(); //becoz i have added this before thats why this line needed
builder.Services.AddTransient<IProjectsRepository, ProjectsRepository>(); //becoz i have added this before thats why this line needed

//inject AutoMapper
builder.Services.AddAutoMapper(typeof(AutoMapperProfiles)); 


//for Token
builder.Services.AddIdentityCore<IdentityUser>()
    .AddRoles<IdentityRole>()
    .AddTokenProvider<DataProtectorTokenProvider<IdentityUser>>("ProjectDetails")
    .AddEntityFrameworkStores<ProjectDetailsAuthDbContext>()
    .AddDefaultTokenProviders();


builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireUppercase = true;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequiredLength = 7;
    options.Password.RequiredUniqueChars = 1;
});


//End of Token
//For Authentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    });



//End of Authentication

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication(); //for Authentication


app.UseCors(MyAllowSpecificOrigins);

app.UseAuthorization();

app.MapControllers();

app.Run();
