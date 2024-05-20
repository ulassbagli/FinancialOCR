using Application.Features.ProgrammingLanguages.Dtos.BaseDto;

namespace Application.Features.ProgrammingLanguages.Dtos;

public class DeletedProgrammingLanguageDto : BaseProgrammingLanguageDto
{
    public DateTime DeletedDate { get; set; }
    public bool isDeleted { get; set; }
}