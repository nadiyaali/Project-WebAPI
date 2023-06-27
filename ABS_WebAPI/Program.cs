global using ABS_WebAPI.Services.AgeService;
global using ABS_WebAPI.Controllers;
global using ABS_WebAPI.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//Using dependency injection of service to controller leads to runtime error of interface missing 
builder.Services.AddScoped<IAgeService, AgeService>();

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
