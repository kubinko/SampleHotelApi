using SampleHotelApi.Domain.Entities;

namespace SampleHotelApi.Infrastructure.DbMock
{
    /// <summary>
    /// In-memory data table.
    /// </summary>
    /// <typeparam name="TData"></typeparam>
    public class DataTable<TData> where TData : class, IEntityWithId, new()
    {
        private readonly Dictionary<long, TData> _data = new();
        private long _currentId = 0;

        /// <summary>
        /// Adds new record.
        /// </summary>
        /// <param name="record">New record.</param>
        /// <returns>ID of new record.</returns>
        public long Add(TData record)
        {
            record.Id = ++_currentId;
            _data.Add(record.Id, record);

            return record.Id;
        }

        /// <summary>
        /// Attempts to retrieve record with <paramref name="id"/>.
        /// </summary>
        /// <param name="id">ID of record to be retrieved.</param>
        /// <returns>Record with specified ID; <c>null</c>, if record was not found.</returns>
        public TData? Get(long id)
            => _data.TryGetValue(id, out TData? value) ? value : null;

        /// <summary>
        /// Retrieves all records.
        /// </summary>
        /// <returns>Collection of all records.</returns>
        public IEnumerable<TData> GetAll()
            => _data.Values;

        /// <summary>
        /// Updates record with <paramref name="id"/>.
        /// </summary>
        /// <param name="id">ID of record to be updated.</param>
        /// <param name="record">Record.</param>
        public void Update(long id, TData record)
        {
            _data[id] = record;
        }

        /// <summary>
        /// Removes record with <paramref name="id"/>.
        /// </summary>
        /// <param name="id">ID of record to be removed.</param>
        public void Delete(long id)
            => _data.Remove(id);

        /// <summary>
        /// Removes all records.
        /// </summary>
        public void DeleteAll()
            => _data.Clear();
    }
}
