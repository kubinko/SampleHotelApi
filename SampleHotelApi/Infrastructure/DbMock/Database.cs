using SampleHotelApi.Domain.Entities;

namespace SampleHotelApi.Infrastructure.DbMock
{
    /// <summary>
    /// In-memory database.
    /// </summary>
    public class Database : IDatabase
    {
        /// <inheritdoc/>
        public DataTable<Comment> Comments { get; } = new();

        /// <inheritdoc/>
        public DataTable<Room> Rooms { get; } = new();

        /// <inheritdoc/>
        public DataTable<Reservation> Reservations { get; } = new();

        /// <inheritdoc/>
        public DataTable<Transaction> Transactions { get; } = new();
    }
}
