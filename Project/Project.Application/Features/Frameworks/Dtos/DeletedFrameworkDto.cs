using Application.Features.Frameworks.Dtos.BaseDto;

namespace Application.Features.Frameworks.Dtos;

public class DeletedFrameworkDto : BaseFrameworkDto
{
    public DateTime DeletedDate { get; set; }
    public bool isDeleted { get; set; }
}