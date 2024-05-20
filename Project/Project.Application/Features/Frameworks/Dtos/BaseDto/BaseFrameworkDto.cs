namespace Application.Features.Frameworks.Dtos.BaseDto;

public class BaseFrameworkDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string? Version { get; set; }
    public string ProgrammingLanguageName { get; set; }
}