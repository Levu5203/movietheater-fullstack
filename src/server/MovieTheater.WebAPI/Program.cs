using System.Reflection;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using MovieTheater.Business.Handlers.Auth;
using MovieTheater.Business.Services;
using MovieTheater.Data;
using MovieTheater.Data.DataSeeding;
using MovieTheater.Data.Repositories;
using MovieTheater.Data.UnitOfWorks;
using MovieTheater.Models.Security;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new() { Title = "Movie Theater Web API", Version = "v1" });

    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter into field the word 'Bearer' following by space and JWT",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT"
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] { }
        }
    });

    // Set the comments path for the Swagger JSON and UI.
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    options.IncludeXmlComments(xmlPath);
});

// Register DbContext
builder.Services.AddDbContext<MovieTheaterDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("MovieTheaterDbConnection"));
});

// Register Identity: UserManager, RoleManager, SignInManager
builder.Services.AddIdentity<User, Role>(options =>
{
    options.SignIn.RequireConfirmedEmail = false;
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequireUppercase = true;
    options.Password.RequiredLength = 8;
    options.User.RequireUniqueEmail = true;
})
    .AddEntityFrameworkStores<MovieTheaterDbContext>()
    .AddDefaultTokenProviders();

// Register IHttpContextAccessor
builder.Services.AddHttpContextAccessor();

// Register UnitOfWork
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// Register IUserIdentity to get current user
builder.Services.AddScoped<IUserIdentity, UserIdentity>();

// Register Token Service
builder.Services.AddScoped<ITokenService, TokenService>();

// Register controllers
builder.Services.AddControllers();

// Register Versiong
builder.Services.AddVersionedApiExplorer(options =>
{
    // Add version 1.0 to the explorer
    options.GroupNameFormat = "'v'VVV";
    options.SubstituteApiVersionInUrl = true;
});

builder.Services.AddApiVersioning(options =>
{
    options.ReportApiVersions = true;
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.DefaultApiVersion = new ApiVersion(1, 0);
});

// Register JWT with Bearer token
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.SaveToken = true;
    options.RequireHttpsMetadata = false;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["JWT:Issuer"],
        ValidAudience = builder.Configuration["JWT:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
            builder.Configuration["JWT:Secret"] ?? throw new InvalidOperationException("JWT:Secret is not configured.")))
    };
});

// Register MediatR
builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssembly(typeof(LoginRequestCommand).Assembly));

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "Movie Theater Web API v1");
        options.EnableDeepLinking();
        options.DisplayRequestDuration();
        options.DocExpansion(Swashbuckle.AspNetCore.SwaggerUI.DocExpansion.None);
    });

    // Seed data
    using var scope = app.Services.CreateScope();
    var context = scope.ServiceProvider.GetRequiredService<MovieTheaterDbContext>();
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<Role>>();

    // Debug information about seed file paths
    var rolesJsonPath = Path.Combine(app.Environment.WebRootPath, "data", "roles.json");
    var usersJsonPath = Path.Combine(app.Environment.WebRootPath, "data", "users.json");
    var roomsJsonPath = Path.Combine(app.Environment.WebRootPath, "data", "rooms.json");
    var genreJsonPath = Path.Combine(app.Environment.WebRootPath, "data", "genres.json");
    var moviesJsonPath = Path.Combine(app.Environment.WebRootPath, "data", "movies.json");
    var showTimeSlotsJsonPath = Path.Combine(app.Environment.WebRootPath, "data", "showTime.json");


    Console.WriteLine($"WebRootPath: {app.Environment.WebRootPath}");
    Console.WriteLine($"Roles JSON path: {rolesJsonPath}");
    Console.WriteLine($"Users JSON path: {usersJsonPath}");
    Console.WriteLine($"Rooms JSON path: {roomsJsonPath}");
    Console.WriteLine($"Genres JSON path: {genreJsonPath}");
    Console.WriteLine($"Movies JSON path: {moviesJsonPath}");
    Console.WriteLine($"ShowTime JSON path: {showTimeSlotsJsonPath}");
    Console.WriteLine($"Roles file exists: {File.Exists(rolesJsonPath)}");
    Console.WriteLine($"Users file exists: {File.Exists(usersJsonPath)}");
    Console.WriteLine($"Rooms file exists: {File.Exists(roomsJsonPath)}");
    Console.WriteLine($"Genres file exists: {File.Exists(genreJsonPath)}");
    Console.WriteLine($"Movies file exists: {File.Exists(moviesJsonPath)}");
    Console.WriteLine($"ShowTime file exists: {File.Exists(showTimeSlotsJsonPath)}");


    // Ensure the directory exists
    Directory.CreateDirectory(Path.Combine(app.Environment.WebRootPath, "data"));

    // Attempt to seed the database
    try
    {
        DbInitializer.Seed(context, userManager, roleManager, rolesJsonPath, usersJsonPath, roomsJsonPath, genreJsonPath, moviesJsonPath, showTimeSlotsJsonPath);
        Console.WriteLine("Database seeded successfully.");
        
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error seeding database: {ex.Message}");
    }
}
// Add authentication and authorization middleware
app.UseAuthentication();
app.UseAuthorization();

app.UseHttpsRedirection();

app.MapControllers();

await app.RunAsync();
