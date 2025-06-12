using Contracts.Requests.AdminRequests.ProfileRequests;
using MediatR;

namespace Core.Requests.AdminRequests.ProfileRequests
{
    public class DeleteProfileCommand : IRequest<DeleteProfileResponse>
    {
        public Guid ProfileId { get; set; }
    }
} 