using Microsoft.EntityFrameworkCore;

namespace TaskManagerInAspNet.Entities
{
    public class AttachedFile
    {
        public Guid Id { get; set; }
        public int UserTaskId { get; set; }
        public UserTask UserTask { get; set; }
        [Unicode(false)]
        public string Url { get; set; }
        public string Title { get; set; }
        public int Order { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
