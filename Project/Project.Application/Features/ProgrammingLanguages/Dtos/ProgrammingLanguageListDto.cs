using Application.Features.Frameworks.Dtos;

namespace Application.Features.ProgrammingLanguages.Dtos;

public class ProgrammingLanguageListDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public ICollection<FrameworkListDto> Frameworks { get; set; }
}