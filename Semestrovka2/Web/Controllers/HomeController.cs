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

    [Authorize]
    public class HomeController(IMediator mediator) : Controller
    {
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

        [HttpPost("comment")]
        [Authorize]
        public async Task<IActionResult> CreateComment([FromBody] CreateCommentRequest request)
        {
            await mediator.Send(request);
            return Ok();
        }

        [HttpGet("comments/{postId:guid}")]
        [Authorize]
        public async Task<IActionResult> GetComments([FromRoute] Guid postId)
        {
            var response = await mediator.Send(new GetPostCommentsRequest { PostId = postId });
            return Ok(response);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }