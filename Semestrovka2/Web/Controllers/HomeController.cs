using System.Diagnostics;
using Core.Requests.GetHomePageRequests;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.Models;

namespace Web.Controllers ;

    public class HomeController(IMediator mediator) : Controller
    {
        [Authorize]
        public async Task<IActionResult> Index()
            => View(await mediator.Send(new GetHomePageQuery()));
        
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }