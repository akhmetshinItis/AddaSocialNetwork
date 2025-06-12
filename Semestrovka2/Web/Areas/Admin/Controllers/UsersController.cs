using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Web.Areas.Admin.Controllers;

public class UsersController : AdminBaseController
{
    private readonly IMediator _mediator;
    
    public UsersController(IMediator mediator) : base(mediator)
    {
        _mediator = mediator;
    }

    public IActionResult Index()
    {
        return View();
    }
} 