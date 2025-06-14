using Core.Requests.AdminRequests.ProfileRequests;
using Core.Requests.ProfileRequests.UpdateProfile;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Web.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Admin,Owner")]
public class ProfilesController : Controller
{
    private readonly IMediator _mediator;
    
    public ProfilesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task<IActionResult> Index()
    {
        return View(await _mediator.Send(new GetAllProfilesQuery()));
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromForm] UpdateProfileRequest request)
    {
        if (!ModelState.IsValid)
        {
            return View(request);
        }

        await _mediator.Send(request);
        TempData["SuccessMessage"] = "Профиль успешно создан!";
        return RedirectToAction("Index");
    }

    [HttpGet]
    public async Task<IActionResult> Edit(Guid id)
    {
        var profiles = await _mediator.Send(new GetAllProfilesQuery());
        var profile = profiles.Profiles.FirstOrDefault(p => p.Id == id);
        
        if (profile == null)
        {
            return NotFound();
        }

        var model = new UpdateProfileRequest
        {
            AboutMe = profile.AboutMe,
            WorkingZone = profile.WorkingZone,
            Education = profile.Education,
            Contacts = profile.Contacts,
            FacebookLink = profile.FacebookLink,
            TwitterLink = profile.TwitterLink,
            GoogleLink = profile.GoogleLink,
            PinterestLink = profile.PinterestLink
        };

        ViewBag.ProfileId = id;
        return View("Create", model);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(Guid id, [FromForm] UpdateProfileRequest request)
    {
        try
        {
            var command = new UpdateProfileCommand
            {
                ProfileId = id,
                AboutMe = request.AboutMe,
                WorkingZone = request.WorkingZone,
                Education = request.Education,
                Contacts = request.Contacts,
                FacebookLink = request.FacebookLink,
                TwitterLink = request.TwitterLink,
                GoogleLink = request.GoogleLink,
                PinterestLink = request.PinterestLink
            };

            await _mediator.Send(command);
            TempData["SuccessMessage"] = "Профиль успешно обновлен!";
            return RedirectToAction("Index");
        }
        catch (Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            ViewBag.ProfileId = id;
            return View("Create", request);
        }
    }

    [HttpPost]
    [Authorize(Roles = "Owner")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var command = new DeleteProfileCommand { ProfileId = id };
        var result = await _mediator.Send(command);

        if (!result.Succeeded)
        {
            TempData["ErrorMessage"] = "Не удалось удалить профиль.";
            return RedirectToAction("Index");
        }

        TempData["SuccessMessage"] = "Профиль успешно удален!";
        return RedirectToAction("Index");
    }
} 