using Contracts.Requests.AdminRequests.PostRequests;
using MediatR;

namespace Core.Requests.AdminRequests.PostRequests
{
    public class GetPostByIdQuery : IRequest<GetAllPostsResponsePostItem>
    {
        public Guid Id { get; set; }
    }
} 