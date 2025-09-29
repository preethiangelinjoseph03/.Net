using Microsoft.EntityFrameworkCore;
using Questionnaire.Data;
using Questionnaire.Model.DbSet;
using Questionnaire.Model.Profiler;
using Questionnaire.Repository;
using Questionnaire.Repository.Interface;
using Questionnaire.Middleware;
using Serilog;
using Questionnaire.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .WriteTo.Console()
    .WriteTo.File("log.txt", rollingInterval: RollingInterval.Day)
    .CreateBootstrapLogger();

try
{
    const string applicationName = "Questionnaire Generating System";
    Log.Information("{AppName} service starting.", applicationName);
    var builder = WebApplication.CreateBuilder(args);

    // Register IHttpContextAccessor for injection into DbContext
    builder.Services.AddHttpContextAccessor();

    builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
    builder.Services.AddProblemDetails();

    // Add DbContext with connection string
    builder.Services.AddDbContext<QuestionnaireDBContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("QuestionnaireContext")));

    builder.Services.AddAuthentication().AddBearerToken(IdentityConstants.BearerScheme);
    builder.Services.AddAuthorization();

    builder.Services.AddControllers();

    builder.Services.AddEndpointsApiExplorer();

    builder.Services.AddSwaggerGen(options =>
    {
        options.SwaggerDoc("v1", new() { Title = "Questionnaire API", Version = "v1" });
        options.TagActionsBy(api =>
        {
            if (api.GroupName != null) return new[] { api.GroupName };
            if (api.ActionDescriptor.RouteValues.TryGetValue("controller", out var controllerName))
                return new[] { controllerName };
            return new[] { "Identity" };
        });
        options.DocInclusionPredicate((name, api) => true);

        options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme()
        {
            Name = "Authorization",
            Type = SecuritySchemeType.ApiKey,
            Scheme = "Bearer",
            BearerFormat = "JWT",
            In = ParameterLocation.Header,
            Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 1safsfsdfdfd\"",
        });
        options.OperationFilter<SecurityRequirementsOperationFilter>();
    });

    builder.Services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
    builder.Services.AddTransient(typeof(IQuestionnaireRepository), typeof(QuestionnaireRepository));

    builder.Services.AddAutoMapper(typeof(MappingProfiler));

    builder.Services.AddSingleton<TimeProvider>(TimeProvider.System);

    bool IsDesignTime()
    {
        // This detects EF Core tooling (like migrations)
        return AppDomain.CurrentDomain.FriendlyName.StartsWith("ef", StringComparison.OrdinalIgnoreCase)
            || AppDomain.CurrentDomain.FriendlyName.Contains("DesignTime", StringComparison.OrdinalIgnoreCase);
    }

    if (!IsDesignTime())
    {
        // Use Serilog as logging provider
        builder.Host.UseSerilog((context, services, configuration) =>
        {
            configuration.ReadFrom.Configuration(context.Configuration);
        });
    }

    if (!IsDesignTime())
    {
        builder.Services.AddIdentityCore<ApplicationUser>(options =>
        {
            options.SignIn.RequireConfirmedEmail = false;
        })
        .AddRoles<IdentityRole>()
        .AddEntityFrameworkStores<QuestionnaireDBContext>()
        .AddApiEndpoints();
    }

    builder.Services.AddDataProtection();

    builder.Services.AddCors(options =>
    {
        options.AddPolicy("QuestionnaireAngularApp",
            builder =>
            {
                builder.WithOrigins("http://localhost:4200")
                       .AllowAnyHeader()
                       .AllowAnyMethod()
                       .AllowCredentials();
            });
    });

    var app = builder.Build();

    if (app.Environment.IsDevelopment() || true)
    {
        app.UseDeveloperExceptionPage();
        app.UseSwagger();
        app.UseSwaggerUI(o =>
        {
            o.DocExpansion(Swashbuckle.AspNetCore.SwaggerUI.DocExpansion.None);
        });
    }

    app.MapIdentityApi<ApplicationUser>()
        .WithGroupName("Identity");

    app.UseExceptionHandler();
    app.UseHttpsRedirection();
    app.UseCors("QuestionnaireAngularApp");
    app.UseAuthentication();
    app.UseAuthorization();
    app.MapControllers();   

    await app.RunAsync();
}
catch (Exception e)
{
    Log.Fatal(e, "Application terminated unexpectedly");
}
finally
{
    Log.Information("Service stopped working.");
    await Log.CloseAndFlushAsync();
}
