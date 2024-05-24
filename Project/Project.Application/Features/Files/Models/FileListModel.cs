using Core.Persistence.Paging;
using Project.Application.Features.Files.Dtos.BaseDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Application.Features.Files.Models
{
    public class FileListModel : BasePageableModel
    {
        public IEnumerable<BaseFileDto> Items { get; set; }
    }
}
