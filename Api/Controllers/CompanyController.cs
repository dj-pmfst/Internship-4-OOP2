using Api.Common;
using Application.Common.Handlers.Companies;
using Application.DTOs.Companies;
using Domain.Persistence.Companies;
using Domain.Persistence.Users;
using Microsoft.AspNetCore.Mvc;
using static Application.DTOs.Companies.AddCompanyRequest;

namespace API.Controllers
{
    [ApiController]
    [Route("api/companies")]
    public class CompaniesController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAll(
            [FromServices] ICompanyUnitOfWork unitOfWork,
            [FromServices] IUserRepository userRepository,
            [FromQuery] string username,
            [FromQuery] string password)
        {
            var request = new GetAllCompaniesRequest(username, password);
            var handler = new GetAllCompaniesRequestHandler(unitOfWork, userRepository);
            var result = await handler.ProcessAuthorizedRequestAsync(request);

            return result.ToActionResult(this);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(
            [FromServices] ICompanyUnitOfWork unitOfWork,
            [FromServices] IUserRepository userRepository,
            [FromRoute] int id,
            [FromQuery] string username,
            [FromQuery] string password)
        {
            var request = new GetCompanyRequest(id, username, password);
            var handler = new GetCompanyRequestHandler(unitOfWork, userRepository);
            var result = await handler.ProcessAuthorizedRequestAsync(request);

            return result.ToActionResult(this);
        }

        [HttpPost]
        public async Task<IActionResult> Create(
            [FromServices] ICompanyUnitOfWork unitOfWork,
            [FromBody] CreateCompanyRequest request)
        {
            var handler = new CreateCompanyRequestHandler(unitOfWork);
            var result = await handler.ProcessAuthorizedRequestAsync(request);

            return result.ToActionResult(this);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(
            [FromServices] ICompanyUnitOfWork unitOfWork,
            [FromRoute] int id,
            [FromBody] UpdateCompanyRequest request)
        {
            request.Id = id;

            var handler = new UpdateCompanyRequestHandler(unitOfWork);
            var result = await handler.ProcessAuthorizedRequestAsync(request);

            return result.ToActionResult(this);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(
            [FromServices] ICompanyUnitOfWork unitOfWork,
            [FromRoute] int id,
            [FromQuery] string username,
            [FromQuery] string password)
        {
            var request = new DeleteCompanyRequest(id, username, password);
            var handler = new DeleteCompanyRequestHandler(unitOfWork);
            var result = await handler.ProcessAuthorizedRequestAsync(request);

            return result.ToActionResult(this);
        }
    }
}