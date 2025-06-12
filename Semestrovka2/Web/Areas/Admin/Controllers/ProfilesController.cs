using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Web.Areas.Admin.Controllers;

public class ProfilesController : AdminBaseController
{
    public ProfilesController(IMediator mediator) : base(mediator)
    {
    }

    public IActionResult Index()
    {
        return AdminView();
    }
} 