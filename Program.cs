using HealthApplication.Attributes;
using HealthApplication.Repositories;
using HealthApplication.Services;

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy =>
                      {
                          policy.WithOrigins("https://swolespace.onrender.com/").AllowAnyHeader()
                                                  .AllowAnyMethod();
                      });
});

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHttpClient();

builder.Services.AddScoped<IForecastRepository, ForecastRepository>();
builder.Services.AddScoped<ISupabaseRepository, SupabaseRepository>();
builder.Services.AddScoped<ISupaAuthService, SupaAuthService>();
// dependency injection for auth service in attribute
builder.Services.AddScoped<AuthenticationFilterAttribute>();

var app = builder.Build();



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(MyAllowSpecificOrigins);

app.UseAuthorization();

app.MapControllers();

app.Run();
