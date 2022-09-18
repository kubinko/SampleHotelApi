using SampleHotelApi.Domain.Entities;

namespace SampleHotelApi.Domain
{
    /// <summary>
    /// Interface describing repository for room reservations.
    /// </summary>
    public interface IReservationRepository
    {
        /// <summary>
        /// Creates new reservation.
        /// </summary>
        /// <param name="reservation">Reservation.</param>
        /// <returns>New reservation ID.</returns>
        long CreateReservation(Reservation reservation);

        /// <summary>
        /// Updates reservation information.
        /// </summary>
        /// <param name="id">Reservation ID.</param>
        /// <param name="reservation">New reservation information.</param>
        void UpdateReservation(long id, Reservation reservation);

        /// <summary>
        /// Updates reservation state.
        /// </summary>
        /// <param name="id">Reservation ID.</param>
        /// <param name="state">New reservation state.</param>
        void UpdateReservationState(long id, ReservationState state);

        /// <summary>
        /// Removes reservation with <paramref name="id"/>.
        /// </summary>
        /// <param name="id">Reservation ID.</param>
        void RemoveReservation(long id);

        /// <summary>
        /// Attempts to retrieve reservation with <paramref name="id"/>.
        /// </summary>
        /// <param name="id">Reservation ID.</param>
        /// <returns>Reservation; <c>null</c>, if reservation with specified ID does not exist.</returns>
        Reservation? GetReservation(long id);

        /// <summary>
        /// Attempts to retrieve reservation with <paramref name="reservationCode"/>.
        /// </summary>
        /// <param name="reservationCode">Reservation code.</param>
        /// <returns>Reservation; <c>null</c>, if reservation with specified code does not exist.</returns>
        Reservation? GetReservation(string reservationCode);

        /// <summary>
        /// Retrieves all reservations.
        /// </summary>
        /// <returns>Reservations.</returns>
        IEnumerable<Reservation> GetAllReservations();

        /// <summary>
        /// Retrieves all reservations between <paramref name="dateFrom"/> and <paramref name="dateTo"/> inclusive.
        /// </summary>
        /// <param name="dateFrom">Start of search interval.</param>
        /// <param name="dateTo">End of search interval.</param>
        /// <returns>Reservations.</returns>
        IEnumerable<Reservation> GetAllReservations(DateTime dateFrom, DateTime dateTo);

        /// <summary>
        /// Retrieves all reservations for room with <paramref name="roomId"/>.
        /// </summary>
        /// <param name="roomId">Room ID.</param>
        /// <returns>Reservations.</returns>
        IEnumerable<Reservation> GetAllReservations(long roomId);

        /// <summary>
        /// Retrieves all reservations for room with <paramref name="roomId"/>
        /// between <paramref name="dateFrom"/> and <paramref name="dateTo"/> inclusive.
        /// </summary>
        /// <param name="roomId">Room ID.</param>
        /// <param name="dateFrom">Start of search interval.</param>
        /// <param name="dateTo">End of search interval.</param>
        /// <returns>Reservations.</returns>
        IEnumerable<Reservation> GetAllReservations(long roomId, DateTime dateFrom, DateTime dateTo);
    }
}
