using AutoMapper;
using MediatR;
using MovieTheater.Business.ViewModels.Room;
using MovieTheater.Data.UnitOfWorks;

namespace MovieTheater.Business.Handlers.Seat
{
    public class MultipleSeatsUpdateCommandHandler : BaseHandler, IRequestHandler<MultipleSeatsUpdateCommand, IEnumerable<SeatViewModel>>
    {
        public MultipleSeatsUpdateCommandHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }

        public async Task<IEnumerable<SeatViewModel>> Handle(MultipleSeatsUpdateCommand request, CancellationToken cancellationToken)
        {
            if (request.Commands == null || !request.Commands.Any())
                throw new ArgumentException("No seat update commands provided.");

            var updatedSeats = new List<SeatViewModel>();

            using var transaction = await _unitOfWork.BeginTransactionAsync();

            try
            {
                foreach (var command in request.Commands)
                {
                    var seat = await _unitOfWork.SeatRepository.GetByIdAsync(command.Id);

                    if (seat == null)
                        throw new KeyNotFoundException($"Seat with ID {command.Id} not found.");

                    seat.SeatType = command.NewType;

                    updatedSeats.Add(_mapper.Map<SeatViewModel>(seat));
                }

                await _unitOfWork.SaveChangesAsync();
                await transaction.CommitAsync();

                return updatedSeats;
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
    }
}
