using System;
using Microsoft.AspNetCore.Http;

namespace MovieTheater.Business.ViewModels.Promotion;

public class PromotionViewModel
{
    public Guid Id { get; set; }
    public string? PromotionTitle { get; set; }

    public string? Description { get; set; }

    public decimal Discount { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }
    
    public string? Image { get; set; }
}
