using EmailMngmntApi.Configuration.DependencyInjection;
using EmailMngmntApi.EntityModels;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddCors(options => options.AddPolicy(name: "NgOrigins",
    policy =>
    {
        policy.WithOrigins("http://localhost:4200").AllowAnyMethod().AllowAnyHeader();
    }));


builder.Services.AddSwaggerGen(x =>
{
    x.SwaggerDoc(
        "v1",
        new OpenApiInfo
        {
            Title = "EmailManagementApi",
            Version = "v1",
        });

    x.AddSecurityDefinition
        (
            "Bearer",
            new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey
            }
        );

    x.AddSecurityRequirement
    (
        new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Id="Bearer",
                        Type = ReferenceType.SecurityScheme
                    }
                },
                new List<string>()
            }
        });

    x.CustomSchemaIds(y => y.FullName);

    x.DocInclusionPredicate((docName, apiDesc) =>
    {
        string assemblyName = ((ControllerActionDescriptor)apiDesc.ActionDescriptor).ControllerTypeInfo.Assembly.GetName().Name;

        string currentAssemblyInfo = typeof(Program).Assembly.GetName().Name;
        return currentAssemblyInfo == assemblyName;
    });
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8
                .GetBytes(builder.Configuration.GetSection("AppSettings:Token").Value)),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });

DI_Configuration.AddDependencies(builder.Services, builder.Configuration);

builder.Services.AddDbContext<EmailManagementApiContext>();


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
