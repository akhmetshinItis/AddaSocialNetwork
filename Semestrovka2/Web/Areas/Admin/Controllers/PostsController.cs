using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Web.Areas.Admin.Controllers;

public class PostsController : AdminBaseController
{
    public PostsController(IMediator mediator) : base(mediator)
    {
    }

    public IActionResult Index()
    {
        return AdminView();
    }
} 