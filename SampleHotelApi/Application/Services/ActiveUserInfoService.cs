namespace SampleHotelApi.Application.Services
{
    /// <summary>
    /// Service with active user information.
    /// </summary>
    public class ActiveUserInfoService : IActiveUserInfoService
    {
        /// <ineritdoc/>
        public long UserId { get; set; }

        /// <ineritdoc/>
        public string Role { get; set; } = "";

        /// <ineritdoc/>
        public bool IsAdmin { get; set; }
    }
}
