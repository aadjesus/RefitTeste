using Refit;
using RefitTeste;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services
    .AddRefitClient<ICliente>(new RefitSettings()
    {
        AuthorizationHeaderValueGetter = (rq, ct) => Task.FromResult("token"),

        ContentSerializer = new SystemTextJsonContentSerializer(new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            PropertyNameCaseInsensitive = true
        })
    })
    .ConfigureHttpClient(c => c.BaseAddress = new Uri("https://localhost:7013/api"));

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
