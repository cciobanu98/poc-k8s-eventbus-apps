using PoC.Shared;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDaprClient();
builder.Services.AddScoped<IEventBus, DaprEventBus>();
builder.Services.AddSingleton<IStore, MemoryStore>();
var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthorization();

app.UseCloudEvents();
app.MapControllers();
app.MapSubscribeHandler();

app.Run();
