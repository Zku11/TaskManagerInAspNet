using System.ComponentModel.DataAnnotations;

namespace TaskManagerInAspNet.Entities
{
    public class UserTask
    {
        public int Id { get; set; }
        [StringLength(250)]
        [Required]
        public string Title { get; set; }
        public string Description { get; set; }
        public int Order { get; set; }
        public DateTime CreatedDate { get; set; }
        public List<Step> Steps { get; set; }
    }
}
