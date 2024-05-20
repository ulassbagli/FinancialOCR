using Application.Features.Developers.Dtos.BaseDto;

namespace Application.Features.Developers.Dtos;

public class DeletedDeveloperDto : BaseDeveloperDto
{
    public DateTime DeletedDate { get; set; }
    public bool isDeleted { get; set; }
}