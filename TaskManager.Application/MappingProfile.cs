using AutoMapper;
using TaskManager.Application.DTOs;
using TaskManager.Domain.Entities;

namespace TaskManager.Application
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Project, ProjectReadDto>()
                .ForMember(dest => dest.OwnerName,
                           opt => opt.MapFrom(src => src.CreatedBy.FirstName + " " + src.CreatedBy.LastName));

            CreateMap<ProjectCreateDto, Project>();
        }
    }
}
