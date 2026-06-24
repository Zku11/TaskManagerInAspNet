using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskManagerInAspNet.Entities;
using TaskManagerInAspNet.Servicios;

namespace TaskManagerInAspNet.Controllers
{
    [Route("UserTask")]
    public class UserTaskController : ControllerBase
    {
        private readonly ApplicationDbContext applicationDBContext;
        private readonly IUserServices userServices;

        public UserTaskController(ApplicationDbContext applicationDBContext, IUserServices userServices)
        {
            this.applicationDBContext = applicationDBContext;
            this.userServices = userServices;
        }

        [HttpGet]
        public async Task<List<UserTask>> Get()
        {
            return await applicationDBContext.UserTasks.ToListAsync();
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
