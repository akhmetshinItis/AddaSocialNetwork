using Contracts.Requests.AdminRequests.UserRequests;
using Core.Abstractions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Core.Requests.AdminRequests.UserRequests
{
    public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, GetAllUsersResponse>
    {
        private readonly IDbContext _context;

        public GetAllUsersQueryHandler(IDbContext context)
        {
            _context = context;
        }

        public async Task<GetAllUsersResponse> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
            => new GetAllUsersResponse
            {
                Users = await _context.Users.Select(x => new GetAllUsersResponseUserItem
                {
                    Id = x.Id,
                    Age = x.Age,
                    Country = x.Country,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    Gender = x.Gender ? "male" : "female",
                    IdentityUserId = x.IdentityUserId,
                    ImageUrl = x.ImageUrl,
                    CreatedDate = x.CreatedDate,
                }).ToListAsync(cancellationToken: cancellationToken)
            };
    }
}