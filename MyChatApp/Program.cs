using Microsoft.EntityFrameworkCore;
using MyChatApp.Context;
using MyChatApp.Hubs;
using MyChatApp.Servieses;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSignalR();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ChatContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("ChatDb"));
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("reactApp", builder =>
    {
        builder.WithOrigins("http://localhost:5173")
        .AllowAnyHeader()
        .AllowAnyMethod()
        .AllowCredentials();
    });
});

builder.Services.AddScoped<IAccountServices, AccountServices>();
builder.Services.AddScoped<IMessgeServices, MessgeServices>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.MapHub<ChatHubs>("/Chat");

app.UseCors("reactApp");

app.Run();
