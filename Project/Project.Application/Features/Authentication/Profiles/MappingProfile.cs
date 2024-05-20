using Application.Features.Authentication.Command;
using Application.Features.Authentication.Command.LoginCommand;
using Application.Features.Authentication.Command.RegisterCommand;
using Application.Features.Authentication.Dtos;
using AutoMapper;
using Core.Security.Dtos;
using Core.Security.Entities;

namespace Application.Features.Authentication.Profiles;

public class MappingProfile: Profile
{
    public MappingProfile()
    {
        CreateMap<UserDto, User>().ReverseMap();
        CreateMap<UserForRegisterDto, User>().ReverseMap();
        CreateMap<RegisterCommand, User>().ReverseMap();
        CreateMap<UserForLoginDto, User>().ReverseMap();
        CreateMap<LoginCommand, User>().ReverseMap();
        CreateMap<RefreshToken, RefreshTokenDto>().ReverseMap();
    }
}