using System.ComponentModel.DataAnnotations;

namespace Application.DTO
{
    public abstract class BaseDTO
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

        protected List<ValidationResult> GetValidationErrors<T>(T obj) where T : BaseDTO
        {
            var results = new List<ValidationResult>();
            var context = new ValidationContext(obj, serviceProvider: null, items: null);
            Validator.TryValidateObject(obj, context, results, validateAllProperties: true);
            return results;
        }

        protected T Validate<T>(T obj) where T : BaseDTO
        {
            var errors = GetValidationErrors(obj);

            if (errors.Count > 0)
            {
                obj.StatusErroMessage = true;
                obj.Message += string.Join(";\n", errors.Select(vr => vr.ErrorMessage));
            }

            return obj;
        }
    }
}
