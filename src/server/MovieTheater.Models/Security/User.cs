using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace MovieTheater.Models.Security;

[Table("Users", Schema = "Security")]
public class User : IdentityUser<Guid>, IMasterDataBaseEntity
{
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string Password { get; set; }
    public required string Address { get; set; }
    public DateTime DateOfBirth { get; set; }
    public required string Gender { get; set; }
    public required string IdentityCard { get; set; }
    public required int TotalScore { get; set; }
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
