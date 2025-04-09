using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieTheater.Business.Handlers.Ticket;
using MovieTheater.Business.ViewModels.Ticket;

namespace MovieTheater.API.Controllers
{
    [Route("api/history-score")]
    [ApiController]
    [Authorize] // Bắt buộc phải đăng nhập
    public class HistoryScoreController : ControllerBase
    {
        private readonly IMediator _mediator;

        public HistoryScoreController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// GetHistoryScore
        /// </summary>
        /// <param name="cancellationToken">Token để hủy yêu cầu</param>
        /// <returns>Danh sách lịch sử điểm</returns>
        [HttpGet]
        public async Task<IActionResult> GetHistoryScore(CancellationToken cancellationToken)
        {
            // Lấy UserId từ token đăng nhập
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null || !Guid.TryParse(userIdClaim.Value, out var userId))
            {
                return Unauthorized("User chưa đăng nhập hoặc không hợp lệ.");
            }

            // Gửi Query đến MediatR
            var query = new GetHistoryScoreByMemberIdQuery(userId);
            var historyScores = await _mediator.Send(query, cancellationToken);

            if (historyScores == null || historyScores.Count == 0)
            {
                return NoContent(); // Trả về 204 nếu không có dữ liệu
            }

            return Ok(historyScores);
        }
    }
}
