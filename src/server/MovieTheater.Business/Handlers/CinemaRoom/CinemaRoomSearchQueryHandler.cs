using System;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MovieTheater.Business.ViewModels.Room;
using MovieTheater.Core;
using MovieTheater.Core.Extensions;
using MovieTheater.Data.UnitOfWorks;

namespace MovieTheater.Business.Handlers.CinemaRoom;

public class CinemaRoomSearchQueryHandler : BaseHandler, IRequestHandler<CinemaRoomSearchQuery, PaginatedResult<CinemaRoomViewModel>>
{
    public CinemaRoomSearchQueryHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
    {
    }

    public async Task<PaginatedResult<CinemaRoomViewModel>> Handle(CinemaRoomSearchQuery request, CancellationToken cancellationToken)
    {
        var query = _unitOfWork.CinemaRoomRepository.GetQuery(true);

        // Filter by keyword
        if (!string.IsNullOrEmpty(request.Keyword))
        {
            var keywordLower = request.Keyword.ToLower();

            query = query.Where(x =>
                x.Name.ToLower().Contains(keywordLower)
            );
        }

        // Filter by capacity range
        if (request.MinCapacity.HasValue)
        {
            query = query.Where(u => (u.SeatColumns*u.SeatRows) >= request.MinCapacity.Value);
        }

        if (request.MaxCapacity.HasValue)
        {
            query = query.Where(u => (u.SeatColumns*u.SeatRows) <= request.MaxCapacity.Value);
        }

        // Count total items
        int total = await query.CountAsync(cancellationToken);

        // Sort
        if (!string.IsNullOrEmpty(request.OrderBy))
        {
            query = query.OrderByExtension(request.OrderBy, request.OrderDirection.ToString());
        }
        else
        {
            query = query.OrderBy(x => x.Name);
        }

        // Get data with pagination
        var items = await query.Skip(request.PageSize * (request.PageNumber - 1))
            .Take(request.PageSize)
            .Include(x => x.CreatedBy)
            .Include(x => x.UpdatedBy)
            .Include(x => x.DeletedBy)
            .ToListAsync(cancellationToken);

        // Map to view models
        var viewModels = _mapper.Map<IEnumerable<CinemaRoomViewModel>>(items);

        // Return paginated result
        return new PaginatedResult<CinemaRoomViewModel>(request.PageNumber, request.PageSize, total, viewModels);
    }
}
