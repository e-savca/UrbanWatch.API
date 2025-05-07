using UrbanWatchAPI.Infrastructure.Mongo;
using UrbanWatchAPI.Application.PublicTransport.Routes.Mapping;
using UrbanWatchAPI.Application.PublicTransport.Routes.Queries.GetAllRoutes;
using UrbanWatchAPI.Infrastructure.Mongo.Repositories;
using UrbanWatchAPI.Infrastructure.Redis;
using UrbanWatchAPI.Presentation.Configuration;

var builder = WebApplication.CreateBuilder(args);

#region Pull secrets from Infisical

builder.Configuration.AddInfisical(
    token:       builder.Configuration["INFISICAL_TOKEN"] ?? throw new InvalidOperationException("INFISICAL_TOKEN missing"),
    workspaceId: builder.Configuration["INFISICAL_WORKSPACE_ID"] ?? throw new InvalidOperationException("INFISICAL_WORKSPACE_ID missing"),
    environment: builder.Configuration["INFISICAL_ENVIRONMENT"] ?? "prod",
    folder:      "/",
    baseUrl:     builder.Configuration["INFISICAL_URL"] ?? "http://vault.home"
);

#endregion

builder.WebHost.UseUrls("http://0.0.0.0:5020");

#region Infrastructure

// bind env vars for MongoSetting
builder.Services.Configure<MongoSettings>(options =>
{
    options.Host     = builder.Configuration["MONGO_HOST"]     ?? throw new Exception("MONGO_HOST missing");
    options.Port     = int.Parse(builder.Configuration["MONGO_PORT"] ?? "0");
    options.Database = builder.Configuration["MONGO_DATABASE"] ?? throw new Exception("MONGO_DATABASE missing");
    options.Username = builder.Configuration["MONGO_USERNAME"] ?? throw new Exception("MONGO_USERNAME missing");
    options.Password = builder.Configuration["MONGO_PASSWORD"] ?? throw new Exception("MONGO_PASSWORD missing");
});

// Register MongoContext
builder.Services.AddSingleton<MongoContext>();

// Register RedisContext
builder.Services.AddSingleton<RedisContext>();

// Add repositories
builder.Services.AddSingleton<RouteRepository>();
builder.Services.AddSingleton<ShapeRepository>();
builder.Services.AddSingleton<StopRepository>();
builder.Services.AddSingleton<StopTimeRepository>();
builder.Services.AddSingleton<TripRepository>();
// builder.Services.AddSingleton<VehicleSnapshotRepository>();

#endregion

#region Application 

// Add MediatR
builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(typeof(GetAllRoutesQuery).Assembly);
});

// Add AutoMapper
builder.Services.AddAutoMapper(typeof(RoutesMappingProfile).Assembly);

#endregion

#region Presentation

builder.Services.AddControllers();

builder.Services.AddAuthorization();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

#endregion

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.MapGet("/", () => Results.Redirect("/swagger")).ExcludeFromDescription();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();