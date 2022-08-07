using SampleHotelApi.Domain.Entities;

namespace SampleHotelApi.Domain
{
    /// <summary>
    /// Interface describing repository for hotel rooms.
    /// </summary>
    public interface IRoomRepository
    {
        /// <summary>
        /// Creates new room.
        /// </summary>
        /// <param name="room">Room.</param>
        /// <returns>New room ID.</returns>
        long AddRoom(Room room);

        /// <summary>
        /// Updates room information.
        /// </summary>
        /// <param name="id">Room ID.</param>
        /// <param name="room">New room information.</param>
        void UpdateRoom(long id, Room room);

        /// <summary>
        /// Removes room with <paramref name="id"/>.
        /// </summary>
        /// <param name="id">Room ID.</param>
        void RemoveRoom(long id);

        /// <summary>
        /// Attempts to retrieve room with <paramref name="id"/>.
        /// </summary>
        /// <param name="id">Room ID.</param>
        /// <returns>Room; <c>null</c>, if room with specified ID does not exist.</returns>
        Room? GetRoom(long id);

        /// <summary>
        /// Attempts to retrieve room by its number.
        /// </summary>
        /// <param name="roomNo">Room number.</param>
        /// <returns>Room; <c>null</c>, if room with specified number does not exist.</returns>
        Room? GetRoomByNumber(long roomNo);

        /// <summary>
        /// Retrieves all rooms.
        /// </summary>
        /// <returns>Rooms.</returns>
        IEnumerable<Room> GetAllRooms();
    }
}
