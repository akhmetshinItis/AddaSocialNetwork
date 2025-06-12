using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Web.Areas.Admin.Controllers;

public class HobbiesController : AdminBaseController
{
    public HobbiesController(IMediator mediator) : base(mediator)
    {
    }

    public IActionResult Index()
    {
        return AdminView();
    }
} 