using Core.Requests.UserRequests.LoginUser;
using MediatR;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using ProFSB.Application.Features.Users.Commands.RegisterUser;
using Web.Models.SignUp;

namespace Web.Controllers
{
    public class SignUpController(IMediator mediator) : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        
        [HttpPost]
        public async Task<RedirectToActionResult> RegisterUser(RegisterUserViewModel request)
        {
            var result = await mediator.Send(new RegisterUserCommand()
            {
                Password = request.Password ?? throw new Exception("Invalid password"),
                Email = request.Email ?? throw new Exception("Invalid email"),
            });
            
            if(result.Result.Succeeded)
                return RedirectToAction("Index", "Home");
            return RedirectToAction("Index", "SignUp");
        }

        [HttpPost]
        public async Task<RedirectToActionResult> LoginUser(LoginUserViewModel request)
        {
            var result = await mediator.Send(new LoginUserQuery()
            {
                Email =  request.Email ?? throw new Exception("Invalid email"),
                Password = request.Password ?? throw new Exception("Invalid password"),
            });

            return RedirectToAction("Index", result.Result.Succeeded ? "Home" : "SignUp");
        }
    }
}