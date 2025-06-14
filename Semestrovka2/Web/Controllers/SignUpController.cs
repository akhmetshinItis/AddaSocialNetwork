using System.ComponentModel.DataAnnotations;
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
        public async Task<IActionResult> RegisterUser(RegisterUserViewModel request)
        {
            if (!ModelState.IsValid)
            {
                // Если модель невалидна — вернуть представление с ошибками
                return View("Index", new SignUpViewModel
                {
                    RegisterUserViewModel = request,
                });
            }

            try
            {
                var command = new RegisterUserCommand()
                {
                    Email = request.Email!,
                    Password = request.Password!,
                    FirstName = request.FirstName!,
                    LastName = request.LastName!,
                    Age = request.Age,
                    Country = request.Country
                };

                var result = await mediator.Send(command);

                if (result.Result.Succeeded)
                    return RedirectToAction("Index", "Home");

                // Если регистрация провалилась, но ModelState валиден, можно добавить ошибки из результата
                foreach (var error in result.Result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }

                return View("Index", new SignUpViewModel
                {
                    RegisterUserViewModel = request,
                });
            }
            catch (ValidationException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View("Index", new SignUpViewModel
                {
                    RegisterUserViewModel = request,
                });
            }
            catch (Exception)
            {
                ModelState.AddModelError(string.Empty, "Произошла ошибка при регистрации");
                return View("Index", new SignUpViewModel
                {
                    RegisterUserViewModel = request,
                });
            }
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