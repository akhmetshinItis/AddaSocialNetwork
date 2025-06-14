using Core.Requests.AdminRequests.PostRequests;
using Core.Requests.PostRequests;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Web.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Admin, Owner")]
public class PostsController : Controller
{
    private readonly IMediator _mediator;
    
    public PostsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task<IActionResult> Index()
    {
        return View(await _mediator.Send(new GetAllPostsQuery()));
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreatePostRequest request)
    {
        if (!ModelState.IsValid)
            return View(request);

        await _mediator.Send(new CreatePostCommand
        {
            Content = request.Content,
            File = request.Photo,
            UserId = request.UserId
        });

        TempData["SuccessMessage"] = "Пост успешно создан";
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(Guid id)
    {
        var post = await _mediator.Send(new GetPostByIdQuery { Id = id });
        if (post == null)
            return NotFound();

        ViewBag.PostId = id;
        return View("Create", new CreatePostRequest
        {
            Content = post.Content,
            UserId = post.UserId
        });
    }

    [HttpPost]
    public async Task<IActionResult> Edit(Guid id, CreatePostRequest request)
    {
        if (!ModelState.IsValid)
            return View("Create", request);

        await _mediator.Send(new UpdatePostCommand
        {
            PostId = id,
            Content = request.Content,
            File = request.Photo,
            UserId = request.UserId
        });

        TempData["SuccessMessage"] = "Пост успешно обновлен";
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [Authorize(Roles = "Owner")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _mediator.Send(new DeletePostCommand { PostId = id });
        TempData["SuccessMessage"] = "Пост успешно удален";
        return RedirectToAction(nameof(Index));
    }
} 