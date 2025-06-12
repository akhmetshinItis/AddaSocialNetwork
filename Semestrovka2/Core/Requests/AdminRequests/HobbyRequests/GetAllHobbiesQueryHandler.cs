using Contracts.Requests.AdminRequests.HobbyRequests;
using Core.Abstractions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Core.Requests.AdminRequests.HobbyRequests
{
    public class GetAllHobbiesQueryHandler : IRequestHandler<GetAllHobbiesQuery, GetAllHobbiesResponse>
    {
        private readonly IDbContext _context;

        public GetAllHobbiesQueryHandler(IDbContext context)
        {
            _context = context;
        }

        public async Task<GetAllHobbiesResponse> Handle(GetAllHobbiesQuery request, CancellationToken cancellationToken)
            => new GetAllHobbiesResponse
            {
                Hobbies = await _context.Hobbies
                    .Select(x => new GetAllHobbiesResponseHobbyItem
                    {
                        Id = x.Id,
                        Name = x.Name,
                        UserId = x.UserId,
                        UserName = x.User.FirstName + " " + x.User.LastName,
                        CreatedDate = x.CreatedDate,
                        PhotosCount = x.Photos.Count
                    })
                    .ToListAsync(cancellationToken)
            };
    }
} 