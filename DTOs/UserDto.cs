namespace BetterplanAPI.DTOs
{
    public class UserDto
    {
        public int Id { get; set; }
        public string UserFullName { get; set; } = string.Empty;
        public string AdvisorFullName { get; set; } = string.Empty;
        public DateTime Created { get; set; }
    }
}
