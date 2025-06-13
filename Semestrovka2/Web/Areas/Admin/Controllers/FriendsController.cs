using MediatR;
using Microsoft.AspNetCore.Mvc;
using Contracts.Requests.AdminRequests.FriendRequests;
using Contracts.Responses.FriendResponses;
using Core.Requests.AdminRequests.FriendRequests;
using Microsoft.AspNetCore.Authorization;

namespace Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class FriendsController : Controller
    {
        private readonly IMediator _mediator;

        public FriendsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _mediator.Send(new GetAllFriendsQuery()));
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] CreateFriendRequest request)
        {
            var command = new CreateFriendCommand
            {
                User1 = request.User1,
                User2 = request.User2
            };

            var result = await _mediator.Send(command);

            if (!result.Succeeded)
            {
                ModelState.AddModelError("", result.Message ?? "Не удалось создать дружбу.");
                return View(request);
            }

            TempData["SuccessMessage"] = "Дружба успешно создана!";
            return RedirectToAction("Index", "Friends", new { area = "Admin" });
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var friends = await _mediator.Send(new GetAllFriendsQuery());
            var friend = friends.Friends.FirstOrDefault(f => f.Id == id);
            if (friend == null)
                return NotFound();

            var model = new UpdateFriendRequest
            {
                Id = friend.Id,
                User1 = friend.UserId,
                User2 = friend.FriendId
            };
            ViewBag.FriendId = id;
            return View("Edit", model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Guid id, [FromForm] UpdateFriendRequest request)
        {
            try
            {
                var command = new UpdateFriendCommand
                {
                    FriendId = id,
                    User1 = request.User1,
                    User2 = request.User2
                };
                await _mediator.Send(command);
                TempData["SuccessMessage"] = "Дружба успешно обновлена!";
                return RedirectToAction("Index", "Friends", new { area = "Admin" });
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                ViewBag.FriendId = id;
                return View("Edit", request);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Guid id)
        {
            var command = new DeleteFriendCommand { FriendId = id };
            var result = await _mediator.Send(command);

            if (!result.Succeeded)
            {
                TempData["ErrorMessage"] = "Не удалось удалить дружбу.";
                return RedirectToAction("Index");
            }

            TempData["SuccessMessage"] = "Дружба успешно удалена!";
            return RedirectToAction("Index", "Friends", new { area = "Admin" });
        }
    }
}