using Project.Application.Features.OcrResults.Dtos.BaseDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Application.Features.OcrResults.Dtos
{
    public class DeletedOcrResultDto : BaseOcrResultDto
    {
        public DateTime DeletedDate { get; set; }
        public bool isDeleted { get; set; }
    }
}
