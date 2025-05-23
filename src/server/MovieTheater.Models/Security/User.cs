using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace MovieTheater.Models.Security;

[Table("Users", Schema = "Security")]
public class User : IdentityUser<Guid>, IMasterDataBaseEntity
{
    [Required]
    [StringLength(50, MinimumLength = 1)]
    public required string FirstName { get; set; }

    [Required]
    [StringLength(50, MinimumLength = 1)]
    public required string LastName { get; set; }

    [NotMapped]
    public string DisplayName => $"{FirstName} {LastName}";

    [StringLength(255)]
    public string? Address { get; set; }

    public DateTime? DateOfBirth { get; set; }

    [Required]
    public required string Gender { get; set; }

    [Required]
    [StringLength(18, MinimumLength = 10)]
    public required string IdentityCard { get; set; }

    public string? Avatar { get; set; }

    public int TotalScore { get; set; } = 0;
    public DateTime CreatedAt { get; set; }

    [ForeignKey(nameof(CreatedBy))]
    public Guid? CreatedById { get; set; }
    public User? CreatedBy { get; set; }
    public DateTime? UpdatedAt { get; set; }

    [ForeignKey(nameof(UpdatedBy))]
    public Guid? UpdatedById { get; set; }
    public User? UpdatedBy { get; set; }
    public DateTime? DeletedAt { get; set; }

    [ForeignKey(nameof(DeletedBy))]
    public Guid? DeletedById { get; set; }
    public User? DeletedBy { get; set; }
    public bool IsDeleted { get; set; }
    public bool IsActive { get; set; }
}
