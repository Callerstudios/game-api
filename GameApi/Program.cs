using FluentValidation;
using GameApi.Data;
using GameApi.Handlers;
using GameApi.Repositories.Implementations;
using GameApi.Repositories.Interfaces;
using GameApi.Services.Implementations;
using GameApi.Services.Interfaces;
using GameApi.Validators;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi;
using Scalar.AspNetCore;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi(options =>
{
    options.AddDocumentTransformer((document, context, cancellationToken) =>
    {
        document.Components ??= new OpenApiComponents();

        document.Components.SecuritySchemes ??=
            new Dictionary<string, IOpenApiSecurityScheme>();

        document.Components.SecuritySchemes["Bearer"] =
            new OpenApiSecurityScheme
            {
                Type = SecuritySchemeType.Http,
                Scheme = "bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Name = "Authorization",
                Description = "Enter your JWT token."
            };

        return Task.CompletedTask;
    });
    options.AddOperationTransformer((operation, context, cancellationToken) =>
    {
        var requiresAuthorization =
            context.Description.ActionDescriptor.EndpointMetadata
                .OfType<Microsoft.AspNetCore.Authorization.AuthorizeAttribute>()
                .Any();

        if (!requiresAuthorization)
        {
            return Task.CompletedTask;
        }

        operation.Security ??= new List<OpenApiSecurityRequirement>();

        operation.Security.Add(
    new OpenApiSecurityRequirement
    {
        [
            new OpenApiSecuritySchemeReference(
                "Bearer",
                context.Document,
                "Bearer")
        ] = new List<string>()
    });

        return Task.CompletedTask;
    });
});

builder.Services.AddProblemDetails(options =>
{
    options.CustomizeProblemDetails = context =>
    {
        context.ProblemDetails.Extensions["traceId"] =
            context.HttpContext.TraceIdentifier;

        context.ProblemDetails.Extensions["timestamp"] =
            DateTime.UtcNow;
    };
});

builder.Services.AddExceptionHandler<GlobalExceptionHandler>();

builder.Services.AddScoped<IPlayerRepository, PlayerRepository>();

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddScoped<IPlayerService, PlayerService>();

builder.Services.AddScoped<IPasswordHasher, PasswordHasherService>();

builder.Services.AddScoped<IAuthService, AuthService>();

builder.Services.AddScoped<ITokenService, JwtTokenService>();

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlite(
        builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.Configure<JwtOptions>(
    builder.Configuration.GetSection(JwtOptions.SectionName));

builder.Services
    .AddOptions<JwtOptions>()
    .Bind(builder.Configuration.GetSection(JwtOptions.SectionName))
    .ValidateDataAnnotations()
    .ValidateOnStart();

builder.Services.AddValidatorsFromAssemblyContaining<RegisterDtoValidator>();

var jwtOptions = builder.Configuration
    .GetSection(JwtOptions.SectionName)
    .Get<JwtOptions>()
    ?? throw new InvalidOperationException(
        "JWT configuration is missing.");

builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters =
            new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = jwtOptions.Issuer,

                ValidateAudience = true,
                ValidAudience = jwtOptions.Audience,

                ValidateIssuerSigningKey = true,
                IssuerSigningKey =
                    new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(jwtOptions.SecretKey)),

                ValidateLifetime = true,

                ClockSkew = TimeSpan.Zero
            };
    });

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

    db.Database.Migrate();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();

    app.MapScalarApiReference();
}

app.UseExceptionHandler();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
