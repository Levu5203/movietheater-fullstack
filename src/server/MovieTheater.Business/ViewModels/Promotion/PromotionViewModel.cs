using System;

namespace MovieTheater.Business.ViewModels.Promotion;

public class PromotionViewModel
{
    public Guid Id { get; set; }
    public required string PromotionTitle { get; set; }

    public required string Description { get; set; }

    public required decimal Discount { get; set; }

    public required DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }
    public required string Image { get; set; }
}
