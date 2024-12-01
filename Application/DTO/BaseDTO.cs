
namespace Application.DTO
{
    public class BaseDTO
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public bool StatusErroMessage { get; set; } = false;
        public string Message { get; set; } = string.Empty;
        public bool StatusDeleted { get; set; } = false;
        public int IdUserCreated { get; set; }
        public string NameUserCreated { get; set; } = string.Empty;
        public DateTime DatetimeCreate { get; set; }
        public int? IdUserUpdated { get; set; }
        public string NameUserUpdated { get; set; } = string.Empty;
        public DateTime? DatetimeUpdated { get; set; }
    }
}
