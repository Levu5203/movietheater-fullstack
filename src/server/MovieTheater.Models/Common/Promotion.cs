using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieTheater.Models.Common;

[Table("Promotions", Schema = "Common")]
public class Promotion : MasterDataBaseEntity, IMasterDataBaseEntity
{   

        [Required(ErrorMessage = "Promotion title is required")]
        [StringLength(100, ErrorMessage = "Title cannot exceed 100 characters")]
        public required string PromotionTitle { get; set; }

        [StringLength(500, ErrorMessage = "Description cannot exceed 500 characters")]
        public required string Description { get; set; }

        [Required(ErrorMessage = "Discount is required")]
        [Range(0, 1, ErrorMessage = "Discount must be between 0 and 100")]
        [Column(TypeName = "decimal(3,2)")]
        public required decimal Discount { get; set; }

        [Required(ErrorMessage = "Start date is required")]
        public required DateTime StartDate { get; set; }

        [Required(ErrorMessage = "End date is required")]
        public DateTime EndDate { get; set; }

        [Required(ErrorMessage = "Image URL is required")]
        [Url(ErrorMessage = "Invalid image URL format")]
        public required string Image { get; set; }

        public virtual ICollection<Ticket> Tickets { get; set; } = [];
}
