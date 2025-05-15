using MongoDB.Driver;
using aspTest.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.Configure<MongoDbSettings>(builder.Configuration.GetSection("MongoDbSettings"));

builder.Services.AddSingleton<IMongoClient>(s =>
{
    var settings = builder.Configuration.GetSection("MongoDbSettings").Get<MongoDbSettings>();
    return new MongoClient(settings!.ConnectionString);
});

builder.Services.AddScoped<IHoaRepository, HoaRepository>();
var app = builder.Build();

app.MapGet("/", () =>
{
    return Results.Ok("Hello my friends");
});

app.MapGet("/hoa", async (IHoaRepository repo) =>
{
    var list = await repo.GetAllAsync();
    return Results.Ok(list);
});

app.MapGet("/hoa/{id}", async (string id, IHoaRepository repo) =>
{
    var hoa = await repo.GetByIdAsync(id);
    return hoa is not null ? Results.Ok(hoa) : Results.NotFound();
});

app.MapPost("/hoa", async (Hoa hoa, IHoaRepository repo) =>
{
    await repo.CreateAsync(hoa);
    return Results.Created($"/hoa/{hoa.Id}", hoa);
});

app.MapPut("/hoa/{id}", async (string id, Hoa hoa, IHoaRepository repo) =>
{
    var oldHoa = await repo.GetByIdAsync(id);
    if (oldHoa is null) return Results.NotFound();

    hoa.Id = id;
    await repo.UpdateAsync(id, hoa);
    return Results.Ok(hoa);
});

app.MapDelete("/hoa/{id}", async (string id, IHoaRepository repo) =>
{
    var hoa = await repo.GetByIdAsync(id);
    if (hoa is null) return Results.NotFound();

    await repo.DeleteAsync(id);
    return Results.NoContent();
});
app.Run();
