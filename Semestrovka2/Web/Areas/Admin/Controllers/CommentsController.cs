using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Web.Areas.Admin.Controllers;

public class CommentsController : AdminBaseController
{
    public CommentsController(IMediator mediator) : base(mediator)
    {
    }

    public IActionResult Index()
    {
        return AdminView();
    }
} 