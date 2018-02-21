using ASPNetCore.API.Boilerplate.Data_Structures;
using ASPNetCore.API.Boilerplate.DTOs;
using ASPNetCore.API.Boilerplate.Parameters;
using ASPNetCore.API.Boilerplate.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace ASPNetCore.API.Boilerplate.Controllers
{
    [Route("api/Company")]
    public class CompanyController : Controller
    {
        public CompanyRepository Repo { get; private set; }

        public CompanyController(CompanyRepository repo) {
            Repo = repo;
        }

        [HttpGet(Name = "GetCompanies")]
        [HttpHead()]
        public IActionResult Get(PagingParameter param) {
            if (param == null)
                return BadRequest();

            PagedList<CompanyViewDto> result = Repo.GetAllcompanies(param);
            if (param == null)
                return BadRequest();

            return Ok(result);
        }
    }
}