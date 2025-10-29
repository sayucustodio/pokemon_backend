using pokemon_backend.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

// para el cache
builder.Services.AddMemoryCache();

// agregue esto para el pokeapi
builder.Services.AddHttpClient<PokemonApiService>(client =>
{
    client.BaseAddress = new Uri("https://pokeapi.co/api/v2/");
});
builder.Services.AddScoped<IPokemonApiService, PokemonApiService>();

//cors para conectar con el front 
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp",
        policy =>
        {
            var frontendUrl = Environment.GetEnvironmentVariable("FRONTEND_URL") ?? "http://localhost:3000";
            policy.WithOrigins(frontendUrl)
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});




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
//agregue para usar el corn que se creo para conectar con el servidor de react
app.UseCors("AllowReactApp");
app.UseAuthorization();

app.MapControllers();

app.Run();
