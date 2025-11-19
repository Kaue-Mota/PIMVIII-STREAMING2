using Microsoft.EntityFrameworkCore;
using StreamApi.Data;
using StreamApi.Repositories;
using StreamApi.Repositories.Interfaces;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;


var builder = WebApplication.CreateBuilder(args);

// DB
builder.Services.AddDbContext<StreamApi.Data.StreamingContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// DI - repository
builder.Services.AddScoped<IPlaylistRepository, PlaylistRepository>();

// CORS para testes locais/front
builder.Services.AddCors(options =>
    options.AddPolicy("AllowLocal", p => p.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()));

builder.Services.AddControllers()
    .AddJsonOptions(o =>
    {
        o.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
        o.JsonSerializerOptions.MaxDepth = 32;
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Cria um escopo para obter o StreamingContext e rodar o seed
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<StreamApi.Data.StreamingContext>();
    DbInitializer.Seed(context);
}


if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseCors("AllowLocal");
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
