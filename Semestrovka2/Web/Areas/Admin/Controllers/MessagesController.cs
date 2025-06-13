using Core.Requests.AdminRequests.MessageRequests;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Contracts.Requests.AdminRequests.MessageRequests;

namespace Web.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Admin")]
public class MessagesController : Controller
{
    private readonly IMediator _mediator;
    
    public MessagesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task<IActionResult> Index()
    {
        return View(await _mediator.Send(new GetAllMessagesQuery()));
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromForm] CreateMessageRequest request)
    {
        var command = new CreateMessageCommand
        {
            ChatId = request.ChatId,
            SenderId = request.SenderId,
            Content = request.Content,
            IsRead = request.IsRead,
            Timestamp = request.Timestamp
        };
        var result = await _mediator.Send(command);
        if (result is { Succeeded: false })
        {
            ModelState.AddModelError("", result.Message ?? "Не удалось создать сообщение.");
            return View(request);
        }
        TempData["SuccessMessage"] = "Сообщение успешно создано!";
        return RedirectToAction("Index");
    }

    [HttpGet]
    public async Task<IActionResult> Edit(Guid id)
    {
        var messages = await _mediator.Send(new GetAllMessagesQuery());
        var message = messages.Messages.FirstOrDefault(m => m.Id == id);
        if (message == null)
            return NotFound();
        var model = new UpdateMessageRequest
        {
            Id = message.Id,
            ChatId = message.ChatId ?? Guid.Empty,
            SenderId = message.SenderId ?? Guid.Empty,
            Content = message.Content,
            IsRead = message.IsRead,
            Timestamp = message.Timestamp
        };
        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(Guid id, [FromForm] UpdateMessageRequest request)
    {
        var command = new UpdateMessageCommand
        {
            Id = id,
            ChatId = request.ChatId,
            SenderId = request.SenderId,
            Content = request.Content,
            IsRead = request.IsRead,
            Timestamp = request.Timestamp
        };
        var result = await _mediator.Send(command);
        if (result is { Succeeded: false })
        {
            ModelState.AddModelError("", result.Message ?? "Не удалось обновить сообщение.");
            return View(request);
        }
        TempData["SuccessMessage"] = "Сообщение успешно обновлено!";
        return RedirectToAction("Index");
    }

    [HttpPost]
    public async Task<IActionResult> Delete(Guid id)
    {
        var command = new DeleteMessageCommand { Id = id };
        var result = await _mediator.Send(command);
        if (result is { Succeeded: false })
        {
            TempData["ErrorMessage"] = result.Message ?? "Не удалось удалить сообщение.";
            return RedirectToAction("Index");
        }
        TempData["SuccessMessage"] = "Сообщение успешно удалено!";
        return RedirectToAction("Index");
    }
} 