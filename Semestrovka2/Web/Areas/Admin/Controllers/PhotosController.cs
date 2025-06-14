using Core.Requests.AdminRequests.PhotoRequests;
using Core.Requests.PhotoRequests;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Web.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Admin, Owner")]
public class PhotosController : Controller
{
    private readonly IMediator _mediator;
    
    public PhotosController(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task<IActionResult> Index()
    {
        return View(await _mediator.Send(new GetAllPhotosQuery()));
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreatePhotoRequest request)
    {
        if (!ModelState.IsValid)
            return View(request);

        await _mediator.Send(new CreatePhotoCommand
        {
            File = request.Photo,
            UserId = request.UserId,
            AlbumId = request.AlbumId
        });

        TempData["SuccessMessage"] = "Фото успешно создано";
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(Guid id)
    {
        var photo = await _mediator.Send(new GetPhotoByIdQuery { Id = id });
        if (photo == null)
            return NotFound();

        ViewBag.PhotoId = id;
        return View("Create", new CreatePhotoRequest
        {
            UserId = photo.UserId,
            AlbumId = photo.AlbumId
        });
    }

    [HttpPost]
    public async Task<IActionResult> Edit(Guid id, CreatePhotoRequest request)
    {
        if (!ModelState.IsValid)
            return View("Create", request);

        await _mediator.Send(new UpdatePhotoCommand
        {
            PhotoId = id,
            File = request.Photo,
            UserId = request.UserId,
            AlbumId = request.AlbumId
        });

        TempData["SuccessMessage"] = "Фото успешно обновлено";
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [Authorize(Roles = "Owner")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _mediator.Send(new DeletePhotoCommand { PhotoId = id });
        TempData["SuccessMessage"] = "Фото успешно удалено";
        return RedirectToAction(nameof(Index));
    }
} 