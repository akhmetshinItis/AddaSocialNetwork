using System.Diagnostics;
using Contracts.Requests.HomePageRequests;
using Core.Requests.GetHomePageRequests;
using Core.Requests.HomePageRequests;
using Core.Requests.PostRequests;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.Models;

namespace Web.Controllers ;

    public class HomeController(IMediator mediator) : Controller
    {
        [Authorize]
        public async Task<IActionResult> Index()
            => View(await mediator.Send(new GetHomePageQuery()));

        [HttpPost]
        public async Task CreatePost(string? content, IFormFile? file)
        {
            await mediator.Send(new CreatePostCommand
            {
                Content = content,
                File = file,
            });
        }

        [HttpPost("like")]
        [Authorize]
        public async Task<IActionResult> LikePost([FromBody] LikePostRequest request)
        {
            await mediator.Send(request);
            return Ok();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }