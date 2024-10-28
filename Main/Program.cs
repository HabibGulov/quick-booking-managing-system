using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<QuickBookingDbContext>(x=>x.UseNpgsql(builder.Configuration["ConnectionString"]));
builder.Services.AddControllers();
builder.Services.AddBookingRepositories();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();
app.MapControllers();

app.Run();