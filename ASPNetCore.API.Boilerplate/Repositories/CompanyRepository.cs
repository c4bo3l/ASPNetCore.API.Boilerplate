using ASPNetCore.API.Boilerplate.Data_Structures;
using ASPNetCore.API.Boilerplate.DTOs;
using ASPNetCore.API.Boilerplate.Models;
using ASPNetCore.API.Boilerplate.Parameters;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;

namespace ASPNetCore.API.Boilerplate.Repositories
{
    public class CompanyRepository:BaseRepository
    {
        #region Properties
        public IConfiguration Configuration { get; private set; }
        #endregion

        public CompanyRepository(MyDBContext context, 
            IConfiguration config) : base(context)
        {
            Configuration = config;
        }

        public PagedList<CompanyViewDto> GetAllcompanies(PagingParameter param) {
            if (Context == null || param == null)
                return null;

            if (param.PageNumber <= 0)
                param.PageNumber = 1;

            if (param.PageSize <= 0 || param.PageSize > int.Parse(Configuration["Paging:MaxPageSize"]))
                param.PageSize = int.Parse(Configuration["Paging:PageSize"]);

            return PagedList<CompanyViewDto>.Create(param, 
                Mapper.Map<List<CompanyViewDto>>(Context.Companies.OrderBy(c => c.Name).ToList()).AsQueryable());
        }
    }
}
