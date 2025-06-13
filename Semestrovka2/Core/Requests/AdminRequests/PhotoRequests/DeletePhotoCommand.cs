using MediatR;

namespace Core.Requests.AdminRequests.PhotoRequests
{
    public class DeletePhotoCommand : IRequest
    {
        public Guid PhotoId { get; set; }
    }
} 