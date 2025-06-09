using Contracts.Requests.PhotoRequests;
using MediatR;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Core.Requests.PhotoRequests
{
    public class GetAlbumsQuery : IRequest<GetAlbumsResponse>
    {
        public bool? SortByDate { get; set; }
        public bool? SortByAmount { get; set; }
        public Guid? UserId { get; set; }
    }
}