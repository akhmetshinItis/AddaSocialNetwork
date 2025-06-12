using Contracts.Requests.AdminRequests.PostRequests;
using MediatR;

namespace Core.Requests.AdminRequests.PostRequests
{
    public class DeletePostCommand : IRequest<DeletePostResponse>
    {
        public Guid Id { get; set; }
        public Guid PostId { get; set; }
    }
} 