using VIRTUAL_ASSISTANT.API.Middleware;
using VIRTUAL_ASSISTANT.API.Swagger;
using VIRTUAL_ASSISTANT.Application.Arguments.Auth;
using VIRTUAL_ASSISTANT.IoC;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Configuration
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true)
    .AddEnvironmentVariables();

var allowedOrigins = builder.Configuration.GetSection("AllowedOrigins").Get<string[]>()
                     ?? throw new InvalidOperationException("AllowedOrigins cannot be null or empty");


var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy =>
                      {
                          policy.WithOrigins(allowedOrigins)
                                .AllowAnyMethod()
                                .AllowCredentials()
                                .AllowAnyHeader();
                      });
});

builder.Services.AddControllers();
builder.Services.AddUseCases();
builder.Services.AddServices();
builder.Services.AddHttpClients(builder.Configuration);
builder.Services.AddDbContexts(builder.Configuration);


builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection("JwtOptions"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseMiddleware<JwtClaimsMiddleware>();
app.UseRouting();

app.UseAuthorization();

app.MapControllers();

app.Run();
