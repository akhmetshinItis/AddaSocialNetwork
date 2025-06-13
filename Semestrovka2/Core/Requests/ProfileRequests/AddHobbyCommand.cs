using MediatR;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace Core.Requests.ProfileRequests;

public class AddHobbyCommand : IRequest<Contracts.Responses.HobbyResponses.AddHobbyResponse>
{
    public string Name { get; set; } = string.Empty;
    public List<IFormFile>? Photos { get; set; }
} 