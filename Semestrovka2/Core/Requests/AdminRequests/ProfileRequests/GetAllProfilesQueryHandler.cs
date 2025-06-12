using Contracts.Requests.AdminRequests.ProfileRequests;
using Core.Abstractions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Core.Requests.AdminRequests.ProfileRequests
{
    public class GetAllProfilesQueryHandler : IRequestHandler<GetAllProfilesQuery, GetAllProfilesResponse>
    {
        private readonly IDbContext _context;

        public GetAllProfilesQueryHandler(IDbContext context)
        {
            _context = context;
        }

        public async Task<GetAllProfilesResponse> Handle(GetAllProfilesQuery request, CancellationToken cancellationToken)
            => new GetAllProfilesResponse
            {
                Profiles = await _context.ProfileDatas
                    .Select(x => new GetAllProfilesResponseProfileItem
                    {
                        Id = x.Id,
                        UserId = x.UserId,
                        BackgroundImage = x.BackgroundImage,
                        AboutMe = x.AboutMe,
                        WorkingZone = x.WorkingZone,
                        Education = x.Education,
                        Contacts = x.Contacts,
                        FacebookLink = x.FacebookLink,
                        TwitterLink = x.TwitterLink,
                        GoogleLink = x.GoogleLink,
                        PinterestLink = x.PinterestLink,
                        CreatedDate = x.CreatedDate,
                    })
                    .ToListAsync(cancellationToken)
            };
    }
} 