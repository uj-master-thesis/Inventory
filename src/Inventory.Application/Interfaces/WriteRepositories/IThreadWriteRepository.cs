namespace Inventory.Application.Interfaces.WriteRepositories;

public interface IThreadWriteRepository : IWriteRepository<Domain.Model.Thread>
{
    Task UpdateTimespan(string id);
}
