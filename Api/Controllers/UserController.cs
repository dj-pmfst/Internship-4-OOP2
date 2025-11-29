using Application.Common.Handlers.Users;
using Application.Common.Users.Handlers;
using Application.DTOs.Users;
using Domain.Persistence.Users;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UsersController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAll([FromServices] IUserUnitOfWork unitOfWork)
        {
            var requestHandler = new GetAllUsersRequestHandler(unitOfWork);
            var result = await requestHandler.ProcessAuthorizedRequestAsync(new GetAllRequest());
            return result.ToActionResult(this);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get([FromServices] IUserUnitOfWork unitOfWork, [FromRoute] int id)
        {
            var requestHandler = new GetAllUsersRequestHandler(unitOfWork);
            var result = await requestHandler.ProcessAuthorizedRequestAsync(new GetAllRequest(id));
            return result.ToActionResult(this);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromServices] IUserUnitOfWork unitOfWork, [FromBody] CreateUserRequest request)
        {
            var requestHandler = new CreateUserRequestHandler(unitOfWork);
            var result = await requestHandler.ProcessAuthorizedRequestAsync(request);
            return result.ToActionResult(this);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(
            [FromServices] IUserUnitOfWork unitOfWork,
            [FromRoute] int id,
            [FromBody] UpdateUserRequest request)
        {
            request.Id = id;

            var handler = new UpdateUserRequestHandler(unitOfWork);
            var result = await handler.ProcessAuthorizedRequestAsync(request);

            return result.ToActionResult(this);
        }

        [HttpPut("deactivate/{id:int}")]
        public async Task<IActionResult> Deactivate(
            [FromServices] IUserUnitOfWork unitOfWork,
            [FromRoute] int id)
        {
            var handler = new DeactivateUserRequestHandler(unitOfWork);
            var result = await handler.ProcessAuthorizedRequestAsync(new DeactivateUserRequest(id));

            return result.ToActionResult(this);
        }

        [HttpPut("activate/{id:int}")]
        public async Task<IActionResult> Activate(
            [FromServices] IUserUnitOfWork unitOfWork,
            [FromRoute] int id)
        {
            var handler = new ActivateUserRequestHandler(unitOfWork);
            var result = await handler.ProcessAuthorizedRequestAsync(new ActivateUserRequest(id));

            return result.ToActionResult(this);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(
            [FromServices] IUserUnitOfWork unitOfWork,
            [FromRoute] int id)
        {
            var handler = new DeleteUserRequestHandler(unitOfWork);
            var result = await handler.ProcessAuthorizedRequestAsync(new DeleteUserRequest(id));

            return result.ToActionResult(this);
        }

        [HttpPost("import-external")]
        public async Task<IActionResult> ImportExternal([FromServices] IUserUnitOfWork unitOfWork)
        {
            var handler = new ImportExternalUsersRequestHandler(unitOfWork);
            var result = await handler.ProcessAuthorizedRequestAsync(new ImportExternalUsersRequest());

            return result.ToActionResult(this);
        }
    }
}