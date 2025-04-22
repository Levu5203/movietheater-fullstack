using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MovieTheater.Business.ViewModels.Showtime;
using MovieTheater.Data.UnitOfWorks;

namespace MovieTheater.Business.Handlers.Showtime;

public class ShowtimeGetByIdHandler(IUnitOfWork unitOfWork, IMapper mapper) : BaseHandler(unitOfWork, mapper),
                                                            IRequestHandler<ShowtimeGetByIdQuery, ShowtimeViewModel>
{
    public async Task<ShowtimeViewModel> Handle(ShowtimeGetByIdQuery request, CancellationToken cancellationToken)
    {
        var query = await _unitOfWork.ShowtimeRepository.GetQuery()
            .Include(st => st.Movie)
            .Include(st => st.CinemaRoom)
            .Include(st => st.ShowTimeSlot)
            .FirstOrDefaultAsync(st => st.Id == request.Id, cancellationToken);
        if (query.ShowTimeSlot == null)
        {
            Console.WriteLine("ShowTimeSlot is NULL!");
        }
        return _mapper.Map<ShowtimeViewModel>(query);
    }
}
