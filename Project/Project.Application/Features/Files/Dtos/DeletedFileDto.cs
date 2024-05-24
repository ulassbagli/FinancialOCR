using Project.Application.Features.Files.Dtos.BaseDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Application.Features.Files.Dtos
{
    public class DeletedFileDto : BaseFileDto
    {
        public DateTime DeletedDate { get; set; }
        public bool isDeleted { get; set; }
    }
}
