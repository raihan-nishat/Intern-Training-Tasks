using AutoMapper;
using MiniTaskManagerApi.DTOs.Tasks;
using MiniTaskManagerApi.Models;

namespace MiniTaskManagerApi.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<TaskItem, TaskResponseDto>();
            CreateMap<TaskCreateDto, TaskItem>();
            CreateMap<TaskUpdateDto, TaskItem>();

        }
    }
}
