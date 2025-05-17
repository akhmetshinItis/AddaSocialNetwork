using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers ;

    [ApiController]
    [Route("[controller]")]
    public class UserController(IMediator mediator) : ControllerBase
    {
        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser([FromBody] RegisterUserRequest request)
        {
            return Ok(await mediator.Send(new RegisterUserCommand()
            {
                Password = request.Password,
                Email = request.Email,
            }));
        }
        
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginUserRequest request)
        {
            return Ok(await mediator.Send(new LoginUserCommand()
            {
                Email = request.Email,
                Password = request.Password
            }));
        }
        
        // хотел сначала сделать с ролями, но потом понял что там надо клеймсы как то заполнять, так что будет просто метод
        
        // /// <summary>
        // /// Сменить пароль менеджера
        // /// </summary>
        // /// <param name="request">Запрос на смену пароля</param>
        // /// <returns>-</returns>
        // [HttpPost("ResetManagerPassword")]
        // public async Task<IActionResult> ResetPassword([FromBody] ResetManagerPasswordCommand request)
        //     => Ok(await mediator.Send(request));
    }