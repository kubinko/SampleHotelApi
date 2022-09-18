using Kros.Utils;
using SampleHotelApi.Application.Services;
using SampleHotelApi.Domain;
using SampleHotelApi.Domain.Entities;
using SampleHotelApi.Infrastructure.DbMock;
using System.Text;

namespace SampleHotelApi.Infrastructure
{
    /// <summary>
    /// Repository for room reservations.
    /// </summary>
    public class ReservationRepository : IReservationRepository
    {
        private const string CodeAlphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        private const int CodeLength = 6;

        private readonly IDatabase _db;
        private static readonly Dictionary<string, long> _codeStore = new(StringComparer.InvariantCultureIgnoreCase);
        private readonly IActiveUserInfoService _userInfo;

        /// <summary>
        /// Ctor.
        /// </summary>
        /// <param name="db">Database.</param>
        /// <param name="userInfo"><see cref="IActiveUserInfoService"/>.</param>
        public ReservationRepository(IDatabase db, IActiveUserInfoService userInfo)
        {
            _db = Check.NotNull(db, nameof(db));
            _userInfo = Check.NotNull(userInfo, nameof(userInfo));
        }

        /// <inheritdoc/>
        public long CreateReservation(Reservation reservation)
        {
            reservation.ReservationCode = GenerateUniqueReservationCode();
            reservation.State = ReservationState.Created;
            reservation.CreatedBy = _userInfo.UserId;
            reservation.CreatedTimestamp = DateTimeProvider.DateTimeOffsetUtcNow;
            reservation.LastModifiedBy = reservation.CreatedBy;
            reservation.LastModifiedTimestamp = reservation.CreatedTimestamp;

            _db.Reservations.Add(reservation);
            _codeStore.Add(reservation.ReservationCode, reservation.Id);

            return reservation.Id;
        }

        /// <inheritdoc/>
        public void UpdateReservation(long id, Reservation reservation)
        {
            Reservation? existingReservation = _db.Reservations.Get(id);
            if (existingReservation != null)
            {
                existingReservation.DateFrom = reservation.DateFrom;
                existingReservation.DateTo = reservation.DateTo;
                existingReservation.NumberOfGuests = reservation.NumberOfGuests;
                existingReservation.CustomerName = reservation.CustomerName;
                existingReservation.ContactEmail = reservation.ContactEmail;
                existingReservation.ContactPhoneNumber = reservation.ContactPhoneNumber;
                existingReservation.AccomodationPrice = reservation.AccomodationPrice;
                existingReservation.State = reservation.State;
                existingReservation.LastModifiedBy = _userInfo.UserId;
                existingReservation.LastModifiedTimestamp = DateTimeProvider.DateTimeOffsetUtcNow;
            }
        }

        /// <inheritdoc/>
        public void UpdateReservationState(long id, ReservationState state)
        {
            Reservation? existingReservation = _db.Reservations.Get(id);
            if (existingReservation != null)
            {
                existingReservation.State = state;
                existingReservation.LastModifiedBy = _userInfo.UserId;
                existingReservation.LastModifiedTimestamp = DateTimeProvider.DateTimeOffsetUtcNow;
            }
        }

        /// <inheritdoc/>
        public void RemoveReservation(long id)
        {
            Reservation? existingReservation = _db.Reservations.Get(id);
            if (existingReservation != null)
            {
                _db.Reservations.Delete(id);
                _codeStore.Remove(existingReservation.ReservationCode);
            }
        }

        /// <inheritdoc/>
        public Reservation? GetReservation(long id)
            => _db.Reservations.Get(id);

        /// <inheritdoc/>
        public Reservation? GetReservation(string reservationCode)
            => _codeStore.TryGetValue(reservationCode, out long id)
                ? _db.Reservations.Get(id)
                : null;

        /// <inheritdoc/>
        public IEnumerable<Reservation> GetAllReservations()
            => _db.Reservations.GetAll();

        /// <inheritdoc/>
        public IEnumerable<Reservation> GetAllReservations(DateTime dateFrom, DateTime dateTo)
            => _db.Reservations.GetAll().Where(r => r.DateFrom <= dateTo && r.DateTo >= dateFrom);

        /// <inheritdoc/>
        public IEnumerable<Reservation> GetAllReservations(long roomId)
            => _db.Reservations.GetAll().Where(r => r.RoomId == roomId);

        /// <inheritdoc/>
        public IEnumerable<Reservation> GetAllReservations(long roomId, DateTime dateFrom, DateTime dateTo)
            => _db.Reservations.GetAll().Where(r => r.RoomId == roomId && r.DateFrom <= dateTo && r.DateTo >= dateFrom);

        private string GenerateUniqueReservationCode()
        {
            var rand = new Random(Environment.TickCount);

            string codeSuggestion;
            do
            {
                var sb = new StringBuilder();
                for (int i = 0; i < CodeLength; i++)
                {
                    sb.Append(CodeAlphabet[rand.Next(CodeAlphabet.Length)]);
                }
                codeSuggestion = sb.ToString();

            } while (_codeStore.ContainsKey(codeSuggestion));

            return codeSuggestion;
        }
    }
}
