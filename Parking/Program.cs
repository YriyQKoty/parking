using System.Text.Json.Serialization;
using Parking.Db;
using Parking.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddScoped<IOwnersService,OwnersService>();
builder.Services.AddScoped<ICitiesService,CitiesService>();
builder.Services.AddScoped<IParkingHistoryService,ParkingHistoryService>();
builder.Services.AddScoped<IStationsService,StationsService>();
builder.Services.AddScoped<ICarsService,CarsService>();
builder.Services.AddScoped<IParkingDetailsService,ParkingDetailsService>();

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<AppDbContext>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(policyBuilder =>
{
    policyBuilder.AllowAnyHeader();
    policyBuilder.AllowAnyMethod();
    policyBuilder.AllowAnyOrigin();
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
