using Microsoft.OpenApi.Models;
using ScripturesApi.Domain;
using ScripturesApi.Services.Abstract;
using ScripturesApi.Services;
using ScripturesApi.Middleware;
using Microsoft.EntityFrameworkCore;

namespace ScripturesApi;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
        });

        services.Configure<RouteOptions>(options =>
        {
            options.LowercaseUrls = true;
        });

        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Version = "v1",
                Title = "Scriptures API",
                Description = "An ASP.NET Core Web API for viewing scriptures.",
                TermsOfService = new("https://example.com/terms"),
                Contact = new()
                {
                    Name = "Example Contact",
                    Url = new("https://example.com/contact")
                },
                License = new()
                {
                    Name = "Example License",
                    Url = new("https://example.com/license")
                }
            });

            // todo: xml documentation?

            options.AddSecurityDefinition("ApiKey", new OpenApiSecurityScheme
            {
                Description = "ApiKey must appear in header",
                Type = SecuritySchemeType.ApiKey,
                Name = "x-api-key",
                In = ParameterLocation.Header,
                Scheme = "ApiKeyScheme"
            });

            var key = new OpenApiSecurityScheme()
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "ApiKey"
                },
                In = ParameterLocation.Header
            };

            var requirement = new OpenApiSecurityRequirement
            {
                { key, new List<string>() }
            };

            options.AddSecurityRequirement(requirement);
        });

        services.AddScoped<IClientService, ClientService>();
        services.AddScoped<IIpService, IpService>();

        services.AddAutoMapper(typeof(Program).Assembly);

        services.AddTransient<IScripturesService, ScripturesService>();

        services.AddControllers();
    }

    public void Configure(WebApplication app, IWebHostEnvironment environment)
    {
        using (var scope = app.Services.CreateScope())
        {
            var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            context.Database.Migrate();
        }

        if (app.Environment.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseStaticFiles();
        app.UseSwagger();

        app.UseSwaggerUI(options =>
        {
            options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
            options.RoutePrefix = string.Empty;
        });

        app.UseHttpsRedirection();
        app.UseRouting();

        app.UseMiddleware<ApiKeyMiddleware>();

        // todo: api versioning?

        app.MapControllers();
    }
}
