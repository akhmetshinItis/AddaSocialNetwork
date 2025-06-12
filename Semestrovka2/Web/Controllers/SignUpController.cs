using Core.Requests.UserRequests.LoginUser;
using Core.Requests.UserRequests.RegisterUser;
using MediatR;
using Microsoft.AspNetCore.Mvc;
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
                FirstName = request.FirstName ?? throw new Exception("Invalid first name"),
                LastName = request.LastName ?? throw new Exception("Invalid last name"),
                Age = request.Age,
                Country = request.Country,
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