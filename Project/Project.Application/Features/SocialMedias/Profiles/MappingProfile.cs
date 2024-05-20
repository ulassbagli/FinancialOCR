using Application.Features.SocialMedias.Commands.CreateCommand;
using Application.Features.SocialMedias.Commands.DeleteCommand;
using Application.Features.SocialMedias.Commands.UpdateCommand;
using Application.Features.SocialMedias.Dtos;
using Application.Features.SocialMedias.Dtos.BaseDto;
using Application.Features.SocialMedias.Models;
using AutoMapper;
using Core.Persistence.Paging;
using Domain.Entities;

namespace Application.Features.SocialMedias.Profiles;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<SocialMedia, BaseSocialMediaDto>().ReverseMap();
        CreateMap<SocialMedia, CreateSocialMediaCommand>().ReverseMap();
        CreateMap<SocialMedia, UpdateSocialMediaCommand>().ReverseMap();
        CreateMap<SocialMedia, SocialMediaListDto>().ReverseMap();
        CreateMap<SocialMedia, DeletedSocialMediaDto>().ReverseMap();
        CreateMap<SocialMedia, DeleteSocialMediaCommand>().ReverseMap();
        CreateMap<IPaginate<SocialMedia>, SocialMediaListModel>().ReverseMap();
    }
}