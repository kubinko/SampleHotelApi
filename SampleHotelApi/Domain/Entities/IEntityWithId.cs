namespace SampleHotelApi.Domain.Entities
{
    /// <summary>
    /// Interface describing entity with ID.
    /// </summary>
    public interface IEntityWithId
    {
        /// <summary>
        /// Entity ID.
        /// </summary>
        public long Id { get; set; }
    }
}
