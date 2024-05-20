namespace Application.Features.Developers.Dtos.BaseDto;

public class BaseDeveloperDto
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
}