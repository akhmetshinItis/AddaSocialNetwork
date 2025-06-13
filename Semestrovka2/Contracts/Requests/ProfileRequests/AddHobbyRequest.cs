using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Contracts.Requests.ProfileRequests;

public class AddHobbyRequest
{
    [Required]
    public string Name { get; set; } = string.Empty;
    [Required]
    public List<IFormFile>? Photos { get; set; }
} 