using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public abstract class BaseDomain
    {
        [Key]
        public int Id { get; set; }
    }
}
