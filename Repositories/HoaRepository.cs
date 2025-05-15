using Microsoft.Extensions.Options;
using MongoDB.Driver;
using aspTest.Models;


public class HoaRepository : IHoaRepository
{
    private readonly IMongoCollection<Hoa> _hoaCollection;

    public HoaRepository(IOptions<MongoDbSettings> settings, IMongoClient client)
    {
        var database = client.GetDatabase(settings.Value.DatabaseName);
        _hoaCollection = database.GetCollection<Hoa>("hoas");
    }

    public async Task<List<Hoa>> GetAllAsync() =>
        await _hoaCollection.Find(_ => true).ToListAsync();

    public async Task<Hoa?> GetByIdAsync(string id) =>
        await _hoaCollection.Find(h => h.Id == id).FirstOrDefaultAsync();

    public async Task CreateAsync(Hoa hoa) =>
        await _hoaCollection.InsertOneAsync(hoa);

    public async Task UpdateAsync(string id, Hoa hoa) =>
        await _hoaCollection.ReplaceOneAsync(h => h.Id == id, hoa);

    public async Task DeleteAsync(string id) =>
        await _hoaCollection.DeleteOneAsync(h => h.Id == id);
}
