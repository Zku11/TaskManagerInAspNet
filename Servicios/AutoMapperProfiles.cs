using AutoMapper;
using TaskManagerInAspNet.Entities;
using TaskManagerInAspNet.Models;

namespace TaskManagerInAspNet.Servicios
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<UserTask, UserTaskDTO>();
        }
    }
}
