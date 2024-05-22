using Application.Features.Developers.Dtos.BaseDto;
using Project.Application.Features.Accountants.Dtos.BaseDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Application.Features.Accountants.Dtos
{
    public class DeletedAccountantDto : BaseAccountantDto
    {
        public DateTime DeletedDate { get; set; }
        public bool isDeleted { get; set; }
    }
}
