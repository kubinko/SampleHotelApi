using Kros.Utils;
using SampleHotelApi.Domain;
using SampleHotelApi.Domain.Entities;
using SampleHotelApi.Infrastructure.DbMock;

namespace SampleHotelApi.Infrastructure
{
    /// <summary>
    /// Repository for hotel rooms.
    /// </summary>
    public class RoomRepository : IRoomRepository
    {
        private readonly IDatabase _db;

        /// <summary>
        /// Ctor.
        /// </summary>
        /// <param name="db">Database.</param>
        public RoomRepository(IDatabase db)
        {
            _db = Check.NotNull(db, nameof(db));
        }

        /// <inheritdoc/>
        public long AddRoom(Room room)
            => _db.Rooms.Add(room);

        /// <inheritdoc/>
        public void UpdateRoom(long id, Room room)
        {
            Room? existingRoom = _db.Rooms.Get(id);
            if (existingRoom != null)
            {
                room.Id = existingRoom.Id;
                room.RoomNo = existingRoom.RoomNo;

                _db.Rooms.Update(id, room);
            }
        }

        /// <inheritdoc/>
        public void RemoveRoom(long id)
            => _db.Rooms.Delete(id);

        /// <inheritdoc/>
        public void RemoveAllRooms()
            => _db.Rooms.DeleteAll();

        /// <inheritdoc/>
        public Room? GetRoom(long id)
            => _db.Rooms.Get(id);

        /// <inheritdoc/>
        public Room? GetRoomByNumber(int roomNo)
            => _db.Rooms.GetAll().FirstOrDefault(room => room.RoomNo == roomNo);

        /// <inheritdoc/>
        public IEnumerable<Room> GetAllRooms()
            => _db.Rooms.GetAll().OrderBy(r => r.RoomNo);
    }
}
