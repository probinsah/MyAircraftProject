using Microsoft.OpenApi;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddControllers();

builder.Services.AddSingleton<DapperContext>();
builder.Services.AddScoped<AircraftMaintenanceRepository>();
Dapper.DefaultTypeMap.MatchNamesWithUnderscores = true;

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Aircraft Maintenance API",
        Description = "API for managing aircraft maintenance records",
        Contact = new OpenApiContact
        {
            Name = "Praveen Kumar Sah",
            Email = "praveen.kumar.sah@company.com"
        }
    }); 
});

var app = builder.Build();
// ----------- Middleware---------
app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "Aircraft Maintenance API V1");
    options.RoutePrefix = string.Empty; // Set Swagger UI at the app's root
});
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.MapControllers();

app.Run();

