using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskManagerInAspNet.Entities;
using TaskManagerInAspNet.Models;
using TaskManagerInAspNet.Servicios;

namespace TaskManagerInAspNet.Controllers
{
    [Route("UserTask")]
    public class UserTaskController : ControllerBase
    {
        private readonly ApplicationDbContext applicationDBContext;
        private readonly IUserServices userServices;
        private readonly IMapper mapper;

        public UserTaskController(ApplicationDbContext applicationDBContext, IUserServices userServices, IMapper mapper)
        {
            this.applicationDBContext = applicationDBContext;
            this.userServices = userServices;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<UserTaskDTO>>> Get()
        {
            var userId = userServices.GetUserId();
            var userTask = await applicationDBContext.UserTasks
                .Where(t => t.CreatorUserId == userId)
                .OrderByDescending(t => t.Order)
                .ProjectTo<UserTaskDTO>(mapper.ConfigurationProvider)
                .ToListAsync();
            return userTask;
        }

        [HttpPost]
        public async Task<ActionResult<UserTask>> Post([FromBody] string title)
        {
            var userId = userServices.GetUserId();
            var tasks = await applicationDBContext.UserTasks.AnyAsync(ut => ut.CreatorUserId == userId);
            var maxIndexOrder = 0;
            if (tasks)
            {
                maxIndexOrder = await applicationDBContext.UserTasks.Where(ut => ut.CreatorUserId == userId).Select(ut => ut.Order).MaxAsync();
            }
            var newUserTask = new UserTask()
            {
                Title = title,
                CreatorUserId = userId,
                CreatedDate = DateTime.UtcNow,
                Order = maxIndexOrder + 1
            };
            applicationDBContext.Add(newUserTask);
            await applicationDBContext.SaveChangesAsync();
            return newUserTask;    
        }
    }
}
