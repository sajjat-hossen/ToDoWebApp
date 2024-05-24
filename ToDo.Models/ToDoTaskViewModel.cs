namespace ToDo.Models
{
    public class ToDoTaskViewModel
    {
        public IEnumerable<ToDoTask> ToDoTasks { get; set; }
        public int PageSize { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int TotalTasks { get; set; }
        public string QueryTerm { get; set; }
        public int StartTaskNumber { get; set; }
        public int EndTaskNumber { get; set; }
    }
}
