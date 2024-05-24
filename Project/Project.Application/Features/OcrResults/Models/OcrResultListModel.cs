using Application.Features.Developers.Dtos.BaseDto;
using Core.Persistence.Paging;
using Project.Application.Features.OcrResults.Dtos.BaseDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Application.Features.OcrResults.Models
{
    public class OcrResultListModel : BasePageableModel
    {
        public IEnumerable<BaseOcrResultDto> Items { get; set; }
    }
}
