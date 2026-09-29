using Mini_Task_Manager_API.Models;
using AutoMapper;
using Mini_Task_Manager_API.DTOs.Tasks;

namespace Mini_Task_Manager_API.Mapping
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles() 
        {
            CreateMap<TaskItem, TaskResponseDto>();

            CreateMap<TaskCreateDto, TaskItem>();

            CreateMap<TaskUpdateDto, TaskItem>();
        }
    }
}
