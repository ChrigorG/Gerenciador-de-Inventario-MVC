
namespace Application.DTO
{
    public class BaseDTO
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public Boolean StatusMessage { get; set; }
        public String Message { get; set; } = string.Empty;
        public bool StatusDeleted { get; set; } = false;
        public int IdUserCreated { get; set; }
        public DateTime DatetimeCreate { get; set; }
        public int? IdUserUpdated { get; set; }
        public DateTime? DatetimeUpdated { get; set; }
    }
}
