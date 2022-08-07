using SampleHotelApi.Domain.Entities;

namespace SampleHotelApi.Infrastructure.DbMock
{
    /// <summary>
    /// Interface describing database.
    /// </summary>
    public interface IDatabase
    {
        /// <summary>
        /// Comments data table.
        /// </summary>
        public DataTable<Comment> Comments { get; }

        /// <summary>
        /// Rooms data table.
        /// </summary>
        public DataTable<Room> Rooms { get; }

        /// <summary>
        /// Reservations data table.
        /// </summary>
        public DataTable<Reservation> Reservations { get; }
    }
}
