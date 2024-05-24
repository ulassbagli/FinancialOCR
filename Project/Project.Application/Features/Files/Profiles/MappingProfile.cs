using AutoMapper;
using Core.Persistence.Paging;
using Domain.Entities;
using Project.Application.Features.Files.Command.CreateCommand;
using Project.Application.Features.Files.Command.DeleteCommand;
using Project.Application.Features.Files.Command.UpdateCommand;
using Project.Application.Features.Files.Dtos.BaseDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using File = Project.Domain.Entities.File;

namespace Project.Application.Features.Files.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            CreateMap<File, BaseFileDto>()
            //.ForMember(dto => dto.FirstName, opt => opt.MapFrom(d => d.User.FirstName))
            //.ForMember(dto => dto.LastName, opt => opt.MapFrom(d => d.User.LastName))
            //.ForMember(dto => dto.Email, opt => opt.MapFrom(d => d.User.Email))
            .ReverseMap();
            CreateMap<File, CreateFileCommand>().ReverseMap();
            CreateMap<File, UpdateFileCommand>().ReverseMap();
            CreateMap<File, FileListDto>().ReverseMap();
            CreateMap<File, DeletedFileDto>().ReverseMap();
            CreateMap<File, DeleteFileCommand>().ReverseMap();
            CreateMap<IPaginate<File>, FileListModel>().ReverseMap();
        }
    }
}
