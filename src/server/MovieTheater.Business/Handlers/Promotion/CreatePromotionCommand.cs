using System;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace MovieTheater.Business.Handlers.Promotion;

public class CreatePromotionCommand : IRequest<Guid>
{
    public string PromotionTitle { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Discount { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public IFormFile? Image { get; set; }
}
