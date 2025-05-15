public interface IHoaRepository
{
    Task<List<Hoa>> GetAllAsync();
    Task<Hoa?> GetByIdAsync(string id);
    Task CreateAsync(Hoa hoa);
    Task UpdateAsync(string id, Hoa hoa);
    Task DeleteAsync(string id);
}

