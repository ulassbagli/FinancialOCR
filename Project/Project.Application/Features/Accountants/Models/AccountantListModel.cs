using Core.Persistence.Paging;
using Project.Application.Features.Accountants.Dtos.BaseDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Application.Features.Accountants.Models
{
    public class AccountantListModel : BasePageableModel
    {
        public IEnumerable<BaseAccountantDto> Items { get; set; }
    }
}
