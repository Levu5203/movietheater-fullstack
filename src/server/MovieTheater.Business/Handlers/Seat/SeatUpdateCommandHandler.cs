using AutoMapper;
using MediatR;
using MovieTheater.Business.ViewModels.Seat;
using MovieTheater.Data.UnitOfWorks;

namespace MovieTheater.Business.Handlers.Seat
{
    public class SeatUpdateCommandHandler : BaseHandler, IRequestHandler<SeatUpdateCommand, SeatViewModel>
    {
        public SeatUpdateCommandHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }

        public async Task<SeatViewModel> Handle(SeatUpdateCommand request, CancellationToken cancellationToken)
        {
            var seat = await _unitOfWork.SeatRepository.GetByIdAsync(request.Id);
            Console.WriteLine($"Seat {request.Id}");

            if (seat == null)
            {
                throw new KeyNotFoundException("Seat not found.");
            }

            seat.SeatType = request.NewType;

            return _mapper.Map<SeatViewModel>(seat);
        }
    }
}