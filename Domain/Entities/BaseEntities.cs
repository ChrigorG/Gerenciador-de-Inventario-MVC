using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public abstract class BaseEntities
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int Id { get; set; }

        [Column("status_deleted")]
        public bool StatusDeleted { get; set; } = false;

        [Required]
        [Column("id_user_created")]
        public int IdUserCreated { get; set; }

        [Required]
        [Column("datetime_create")]
        public DateTime DatetimeCreate { get; set; }

        [Column("id_user_updated")]
        public int? IdUserUpdated { get; set; }

        [Column("datetime_updated")]
        public DateTime? DatetimeUpdated { get; set; }
    }
}
