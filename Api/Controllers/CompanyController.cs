using Microsoft.AspNetCore.Mvc;
using Domain.Persistence.Companies;

namespace API.Controllers
{
    [ApiController]
    [Route("api/companies")]
    public class CompaniesController : ControllerBase
    {
        // GET /api/companies?username=x&password=y
        [HttpGet]
        public async Task<IActionResult> GetAll(
            [FromServices] ICompanyUnitOfWork unitOfWork,
            [FromQuery] string username,
            [FromQuery] string password)
        {
            var request = new GetAllCompaniesRequest(username, password);
            var handler = new GetAllCompaniesRequestHandler(unitOfWork);
            var result = await handler.ProcessAuthorizedRequestAsync(request);

            return result.ToActionResult(this);
        }

        // GET /api/companies/{id}?username=x&password=y
        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(
            [FromServices] ICompanyUnitOfWork unitOfWork,
            [FromRoute] int id,
            [FromQuery] string username,
            [FromQuery] string password)
        {
            var request = new GetCompanyRequest(id, username, password);
            var handler = new GetCompanyRequestHandler(unitOfWork);
            var result = await handler.ProcessAuthorizedRequestAsync(request);

            return result.ToActionResult(this);
        }

        // POST /api/companies
        [HttpPost]
        public async Task<IActionResult> Create(
            [FromServices] ICompanyUnitOfWork unitOfWork,
            [FromBody] CreateCompanyRequest request)
        {
            var handler = new CreateCompanyRequestHandler(unitOfWork);
            var result = await handler.ProcessAuthorizedRequestAsync(request);

            return result.ToActionResult(this);
        }

        // PUT /api/companies/{id}
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

        // DELETE /api/companies/{id}?username=x&password=y
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