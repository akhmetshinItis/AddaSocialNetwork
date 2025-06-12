using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Web.Areas.Admin.Controllers;

public class MessagesController : AdminBaseController
{
    public MessagesController(IMediator mediator) : base(mediator)
    {
    }

    public IActionResult Index()
    {
        return AdminView();
    }
} 