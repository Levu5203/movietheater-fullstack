using MediatR;
using MovieTheater.Business.ViewModels.Promotion;

namespace MovieTheater.Business.Handlers.Promotion;

public class GetPromotionListQuery : IRequest<List<PromotionViewModel>>
{

}
