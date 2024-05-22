using Core.Persistence.Paging;
using Project.Application.Features.Companies.Dtos.BaseDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Application.Features.Companies.Models
{
    public class CompanyListModel : BasePageableModel
    {
        public IEnumerable<BaseCompanyDto> Items { get; set; }
    }
}
