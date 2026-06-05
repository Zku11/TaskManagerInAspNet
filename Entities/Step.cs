namespace TaskManagerInAspNet.Entities
{
    public class Step
    {
        public Guid Id { get; set; }
        public int UserTaskId { get; set; }
        public UserTask UserTask { get; set; }
        public string Description { get; set; }
        public bool Done { get; set; }
        public int Order { get; set; }
    }
}
