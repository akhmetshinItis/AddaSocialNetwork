using Contracts.Requests.UserRequests.RegisterUser;
using Core.Requests.AdminRequests.UserRequests;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Web.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Admin")]
public class UsersController : Controller
{
    private readonly IMediator _mediator;
    
    public UsersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task<IActionResult> Index()
    {
        return View(await _mediator.Send(new GetAllUsersQuery()));
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }
    
    [HttpPost]
    public async Task<IActionResult> Create([FromForm] RegisterUserRequest request)
    {
        var command = new RegisterUserCommandAdmin
        {
            Email = request.Email,
            Password = request.Password,
            FirstName = request.FirstName,
            LastName = request.LastName,
            Gender = request.Gender,
            Age = request.Age,
            Country = request.Country
        };

        var result = await _mediator.Send(command);

        if (!result.Result.Succeeded)
        {
            foreach (var error in result.Result.Errors)
                ModelState.AddModelError("", error.ToString() ?? string.Empty);
            return View(request);
        }

        TempData["SuccessMessage"] = "Пользователь успешно создан!";
        return RedirectToAction("Index", "Users", new { area = "Admin" });
    }

    [HttpGet]
    public async Task<IActionResult> Edit(Guid id)
    {
        var users = await _mediator.Send(new GetAllUsersQuery());
        var user = users.Users.FirstOrDefault(u => u.Id == id);
        
        if (user == null)
        {
            return NotFound();
        }

        var model = new RegisterUserRequest
        {
            Email = user.Email,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Gender = user.Gender == "male",
            Age = user.Age,
            Country = user.Country
        };

        ViewBag.UserId = id;
        return View("Create", model);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(Guid id, [FromForm] RegisterUserRequest request)
    {
        try
        {
            var command = new UpdateUserCommand
            {
                UserId = id,
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
                Gender = request.Gender,
                Age = request.Age,
                Country = request.Country
            };

            await _mediator.Send(command);
            TempData["SuccessMessage"] = "Пользователь успешно обновлен!";
            return RedirectToAction("Index", "Users", new { area = "Admin" });
        }
        catch (Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            ViewBag.UserId = id;
            return View("Create", request);
        }
    }
    
    [HttpPost]
    public async Task<IActionResult> Delete(Guid id)
    {
        var command = new DeleteUserCommand { UserId = id };
        var result = await _mediator.Send(command);

        if (!result.Succeeded)
        {
            TempData["ErrorMessage"] = "Failed to delete user.";
            return RedirectToAction("Index");
        }

        TempData["SuccessMessage"] = "User successfully deleted!";
        return RedirectToAction("Index", "Users", new { area = "Admin" });
    }
} 