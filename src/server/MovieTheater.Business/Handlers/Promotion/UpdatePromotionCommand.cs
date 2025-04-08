using System;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace MovieTheater.Business.Handlers.Promotion;

public class UpdatePromotionCommand : IRequest<bool>
{
    public Guid Id { get; set; }
    public string? PromotionTitle { get; set; }
    public string? Description { get; set; }
    public decimal Discount { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public IFormFile? Image { get; set; }
}

