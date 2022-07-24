using MongoDB.Driver;

namespace Shop.Infrastructure.Repositories;

public class RepositoryBase<T> where T: IEntity
{
    protected readonly IMongoCollection<T> _collection;

    public RepositoryBase(IMongoCollection<T> collection)
    {
        _collection = collection;
    }

    public async Task UpdateManyAsync(IEnumerable<T> entities)
    {
        var updates = new List<WriteModel<T>>();
        var filterBuilder = Builders<T>.Filter;

        foreach (var doc in entities)
        {
            var filter = filterBuilder.Where(x => x.Id == doc.Id);
            updates.Add(new ReplaceOneModel<T>(filter, doc));
        }

        await _collection.BulkWriteAsync(updates);
    }
}