using Application.Features.Developers.Dtos.BaseDto;
using Core.Persistence.Paging;

namespace Application.Features.Developers.Models;

public class DeveloperListModel : BasePageableModel 
{
    public IEnumerable<BaseDeveloperDto> Items { get; set; }
}