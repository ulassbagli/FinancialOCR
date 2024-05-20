using Application.Features.Frameworks.Dtos;
using Core.Persistence.Paging;

namespace Application.Features.Frameworks.Models;

public class FrameworkListModel : BasePageableModel
{
    public ICollection<FrameworkListDto> Items { get; set; }
}