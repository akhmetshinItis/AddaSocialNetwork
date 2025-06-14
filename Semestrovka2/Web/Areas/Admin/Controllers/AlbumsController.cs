using Contracts.Requests.AdminRequests.AlbumRequests;
using Core.Requests.AdminRequests.AlbumRequests;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Web.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Admin,Owner")]
public class AlbumsController : Controller
{
    private readonly IMediator _mediator;
    
    public AlbumsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task<IActionResult> Index()
    {
        return View(await _mediator.Send(new GetAllAlbumsQuery()));
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }
    
    [HttpPost]
    public async Task<IActionResult> Create([FromForm] CreateAlbumRequest request)
    {
        var command = new CreateAlbumCommand
        {
            Name = request.Name,
            UserId = request.UserId
        };

        var result = await _mediator.Send(command);

        if (!result.Succeeded)
        {
            ModelState.AddModelError("", "Failed to create album.");
            return View(request);
        }

        TempData["SuccessMessage"] = "Альбом успешно создан!";
        return RedirectToAction("Index", "Albums", new { area = "Admin" });
    }

    [HttpGet]
    public async Task<IActionResult> Edit(Guid id)
    {
        var albums = await _mediator.Send(new GetAllAlbumsQuery());
        var album = albums.Albums.FirstOrDefault(a => a.Id == id);
        
        if (album == null)
        {
            return NotFound();
        }

        var model = new CreateAlbumRequest
        {
            Name = album.Name,
            UserId = album.UserId
        };

        ViewBag.AlbumId = id;
        return View("Create", model);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(Guid id, [FromForm] CreateAlbumRequest request)
    {
        try
        {
            var command = new UpdateAlbumCommand
            {
                AlbumId = id,
                Name = request.Name,
                UserId = request.UserId
            };

            await _mediator.Send(command);
            TempData["SuccessMessage"] = "Альбом успешно обновлен!";
            return RedirectToAction("Index", "Albums", new { area = "Admin" });
        }
        catch (Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            ViewBag.AlbumId = id;
            return View("Create", request);
        }
    }
    
    [HttpPost]
    [Authorize(Roles = "Owner")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var command = new DeleteAlbumCommand { AlbumId = id };
        var result = await _mediator.Send(command);

        if (!result.Succeeded)
        {
            TempData["ErrorMessage"] = "Failed to delete album.";
            return RedirectToAction("Index");
        }

        TempData["SuccessMessage"] = "Album successfully deleted!";
        return RedirectToAction("Index", "Albums", new { area = "Admin" });
    }
} 