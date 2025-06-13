using Core.Requests.AdminRequests.CommentRequests;
using Core.Requests.CommentRequests;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Web.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Admin")]
public class CommentsController : Controller
{
    private readonly IMediator _mediator;
    
    public CommentsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task<IActionResult> Index()
    {
        return View(await _mediator.Send(new GetAllCommentsQuery()));
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateCommentRequest request)
    {
        if (!ModelState.IsValid)
            return View(request);

        await _mediator.Send(new CreateCommentCommand
        {
            Content = request.Content,
            PostId = request.PostId,
            UserId = request.UserId
        });

        TempData["SuccessMessage"] = "Комментарий успешно создан";
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(Guid id)
    {
        var comment = await _mediator.Send(new GetCommentByIdQuery { Id = id });
        if (comment == null)
            return NotFound();

        ViewBag.CommentId = id;
        return View("Create", new CreateCommentRequest
        {
            Content = comment.Content,
            PostId = comment.PostId,
            UserId = comment.UserId
        });
    }

    [HttpPost]
    public async Task<IActionResult> Edit(Guid id, CreateCommentRequest request)
    {
        if (!ModelState.IsValid)
            return View("Create", request);

        await _mediator.Send(new UpdateCommentCommand
        {
            CommentId = id,
            Content = request.Content,
            PostId = request.PostId,
            UserId = request.UserId
        });

        TempData["SuccessMessage"] = "Комментарий успешно обновлен";
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _mediator.Send(new DeleteCommentCommand { CommentId = id });
        TempData["SuccessMessage"] = "Комментарий успешно удален";
        return RedirectToAction(nameof(Index));
    }
} 