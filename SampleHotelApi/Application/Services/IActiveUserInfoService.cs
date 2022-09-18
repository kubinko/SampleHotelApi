namespace SampleHotelApi.Application.Services
{
    /// <summary>
    /// Interface describing service with active user information.
    /// </summary>
    public interface IActiveUserInfoService
    {
        /// <summary>
        /// Current user ID.
        /// </summary>
        long UserId { get; set; }

        /// <summary>
        /// Current user role.
        /// </summary>
        string Role { get; set; }

        /// <summary>
        /// Indicator whether current user is admin.
        /// </summary>
        bool IsAdmin { get; set; }
    }
}
