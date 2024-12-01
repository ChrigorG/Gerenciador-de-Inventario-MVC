using System.ComponentModel.DataAnnotations;

namespace Shared.Helper
{
    public static class ValidationEntities
    {
        public static List<ValidationResult> Validate<T>(T obj)
        {
            var results = new List<ValidationResult>();
            var context = new ValidationContext(obj, serviceProvider: null, items: null);
            Validator.TryValidateObject(obj, context, results, validateAllProperties: true);
            return results;
        }
    }
}
