using Application.Features.ProgrammingLanguages.Dtos.BaseDto;

namespace Application.Features.ProgrammingLanguages.Dtos;

public class UpdatedProgrammingLanguageDto : BaseProgrammingLanguageDto
{
    public bool isDeleted { get; set; }
}