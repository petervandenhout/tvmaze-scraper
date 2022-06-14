using TvMazeScraper;
using TvMazeScraper.EntityFrameworkCore;
using Opw.HttpExceptions.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services
    .AddControllers()
    .AddHttpExceptions();

builder.Services.AddTvMazeScraperCore(builder.Configuration);
builder.Services.AddTvMazeScraperEntityFrameworkCore(builder.Configuration);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// UseHttpExceptions is the first middleware component added to the pipeline. Therefore,
// the UseHttpExceptions Middleware catches any exceptions that occur in later calls.
// When using HttpExceptions you don't need to use UseExceptionHandler or UseDeveloperExceptionPage.
app.UseHttpExceptions();

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
