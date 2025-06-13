using Core.Requests.AdminRequests.ChatRequests;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Contracts.Requests.AdminRequests.ChatRequests;

namespace Web.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Admin")]
public class ChatsController : Controller
{
    private readonly IMediator _mediator;
    
    public ChatsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task<IActionResult> Index()
    {
        return View(await _mediator.Send(new GetAllChatsQuery()));
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromForm] CreateChatRequest request)
    {
        var command = new CreateChatCommand
        {
            User1Id = request.User1Id,
            User2Id = request.User2Id
        };
        var result = await _mediator.Send(command);
        if (result is { Succeeded: false })
        {
            ModelState.AddModelError("", result.Message ?? "Не удалось создать чат.");
            return View(request);
        }
        TempData["SuccessMessage"] = "Чат успешно создан!";
        return RedirectToAction("Index");
    }

    [HttpGet]
    public async Task<IActionResult> Edit(Guid id)
    {
        var chats = await _mediator.Send(new GetAllChatsQuery());
        var chat = chats.Chats.FirstOrDefault(c => c.Id == id);
        if (chat == null)
            return NotFound();
        var model = new UpdateChatRequest
        {
            Id = chat.Id,
            User1Id = chat.User1Id,
            User2Id = chat.User2Id
        };
        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(Guid id, [FromForm] UpdateChatRequest request)
    {
        var command = new UpdateChatCommand
        {
            Id = id,
            User1Id = request.User1Id,
            User2Id = request.User2Id
        };
        var result = await _mediator.Send(command);
        if (result is { Succeeded: false })
        {
            ModelState.AddModelError("", result.Message ?? "Не удалось обновить чат.");
            return View(request);
        }
        TempData["SuccessMessage"] = "Чат успешно обновлен!";
        return RedirectToAction("Index");
    }

    [HttpPost]
    public async Task<IActionResult> Delete(Guid id)
    {
        var command = new DeleteChatCommand { Id = id };
        var result = await _mediator.Send(command);
        if (result is { Succeeded: false })
        {
            TempData["ErrorMessage"] = result.Message ?? "Не удалось удалить чат.";
            return RedirectToAction("Index");
        }
        TempData["SuccessMessage"] = "Чат успешно удален!";
        return RedirectToAction("Index");
    }
} 