using Contracts.Requests.AdminRequests.HobbyRequests;
using Core.Requests.AdminRequests.HobbyRequests;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Web.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Admin")]
public class HobbiesController : Controller
{
    private readonly IMediator _mediator;
    
    public HobbiesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task<IActionResult> Index()
    {
        return View(await _mediator.Send(new GetAllHobbiesQuery()));
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromForm] CreateHobbyRequest request)
    {
        var command = new CreateHobbyCommand
        {
            UserId = request.UserId,
            Name = request.Name
        };
        var result = await _mediator.Send(command);
        if (!result.Succeeded)
        {
            ModelState.AddModelError("", result.Message ?? "Не удалось создать хобби.");
            return View(request);
        }
        TempData["SuccessMessage"] = "Хобби успешно создано!";
        return RedirectToAction("Index", "Hobbies", new { area = "Admin" });
    }

    [HttpGet]
    public async Task<IActionResult> Edit(Guid id)
    {
        var hobbies = await _mediator.Send(new GetAllHobbiesQuery());
        var hobby = hobbies.Hobbies.FirstOrDefault(h => h.Id == id);
        if (hobby == null)
            return NotFound();
        var model = new UpdateHobbyRequest
        {
            Id = hobby.Id,
            UserId = hobby.UserId,
            Name = hobby.Name
        };
        ViewBag.HobbyId = id;
        return View("Edit", model);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(Guid id, [FromForm] UpdateHobbyRequest request)
    {
        try
        {
            var command = new UpdateHobbyCommand
            {
                HobbyId = id,
                UserId = request.UserId,
                Name = request.Name
            };
            await _mediator.Send(command);
            TempData["SuccessMessage"] = "Хобби успешно обновлено!";
            return RedirectToAction("Index", "Hobbies", new { area = "Admin" });
        }
        catch (Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            ViewBag.HobbyId = id;
            return View("Edit", request);
        }
    }

    [HttpPost]
    public async Task<IActionResult> Delete(Guid id)
    {
        var command = new DeleteHobbyCommand { HobbyId = id };
        var result = await _mediator.Send(command);
        if (!result.Succeeded)
        {
            TempData["ErrorMessage"] = "Не удалось удалить хобби.";
            return RedirectToAction("Index");
        }
        TempData["SuccessMessage"] = "Хобби успешно удалено!";
        return RedirectToAction("Index", "Hobbies", new { area = "Admin" });
    }
} 